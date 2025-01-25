using Retro.Orders.Contracts;
using Retro.Orders.Contracts.Request;
using Retro.Orders.Contracts.Response;
using Retro.Orders.Domain;
using Retro.Stock.Contracts.Response;

namespace Retro.Orders.Infrastructure;

public class OrderService(IOrderRepository orderRepository, Gateway gateway) : IOrderService
{
    public async Task<OrderResponse> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderByIdAsync(orderId, cancellationToken);
        var stockItemIds = order.OrderItems.Select(s => s.StockItemId).ToArray();
        List<StockResponse> stockItems = [];

        foreach (var stockItemId in stockItemIds)
        {
            stockItems.Add(await gateway.GetStockItemAsync(stockItemId, cancellationToken));
        }

        var orderItems = order.OrderItems
            .Join(stockItems, item => item.StockItemId, stockItem => stockItem.Id, (orderItem, stockItem) => (orderItem, stockItem));
        
        return new OrderResponse(
            order.Id,
            order.UserId,
            order.PaymentId,
            order.Status,
            order.OrderTotal, 
            orderItems.Select(s => 
                new OrderItemResponse(
                    s.orderItem.StockItemId, 
                    s.orderItem.Quantity, 
                    s.orderItem.StockItemPrice, 
                    s.orderItem.OrderItemPrice, 
                    s.stockItem.Name, 
                    s.stockItem.Description, 
                    s.orderItem.IsDiscounted, 
                    s.orderItem.DiscountPercentage, 
                    s.stockItem.ImageUrl)).ToArray());
    }

    public async Task<OrderResponse[]> GetUserOrdersAsync(Guid userId, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetUserOrdersAsync(userId, cancellationToken);
        
        var stockItemIds = orders.SelectMany(s => s.OrderItems).Select(s => s.StockItemId).ToArray();
        List<StockResponse> stockItems = [];

        foreach (var stockItemId in stockItemIds)
        {
            stockItems.Add(await gateway.GetStockItemAsync(stockItemId, cancellationToken));
        }
        
        return orders.Select(o => new OrderResponse(
            o.Id,
            o.UserId,
            o.PaymentId,
            o.Status,
            o.OrderTotal, 
            o.OrderItems
                .Join(stockItems, item => item.StockItemId, stockItem => stockItem.Id, (orderItem, stockItem) => (orderItem, stockItem))
                .Select(s => 
                    new OrderItemResponse(
                        s.orderItem.StockItemId, 
                        s.orderItem.Quantity, 
                        s.orderItem.StockItemPrice, 
                        s.orderItem.OrderItemPrice, 
                        s.stockItem.Name, 
                        s.stockItem.Description, 
                        s.orderItem.IsDiscounted, 
                        s.orderItem.DiscountPercentage, 
                        s.stockItem.ImageUrl))
                .ToArray()
            )).ToArray();
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest order, CancellationToken cancellationToken)
    {
        var stockItemIds = order.OrderItems.Select(s => s.StockItemId).ToArray();
        List<StockResponse> stockItems = [];

        foreach (var stockItemId in stockItemIds)
        {
            stockItems.Add(await gateway.GetStockItemAsync(stockItemId, cancellationToken));
        }
        
        var orderItems = order.OrderItems
            .Join(stockItems, item => item.StockItemId, stockItem => stockItem.Id, (orderItem, stockItem) => (orderItem, stockItem))
            .Select(s => new OrderItem(s.orderItem.StockItemId, s.orderItem.Quantity, s.stockItem.Price))
            .ToArray();
        
        var domainOrder = await orderRepository.CreateOrderAsync(new Order(order.UserId, null, orderItems), cancellationToken);
        
        var orderItemsResponse = orderItems
            .Join(stockItems, item => item.StockItemId, stockItem => stockItem.Id, (orderItem, stockItem) => (orderItem, stockItem))
            .Select(s => new OrderItemResponse(
                s.orderItem.StockItemId, 
                s.orderItem.Quantity, 
                s.orderItem.StockItemPrice, 
                s.orderItem.OrderItemPrice, 
                s.stockItem.Name, 
                s.stockItem.Description, 
                s.orderItem.IsDiscounted, 
                s.orderItem.DiscountPercentage, 
                s.stockItem.ImageUrl))
            .ToArray();
        
        return new OrderResponse(
            domainOrder.Id,
            domainOrder.UserId,
            domainOrder.PaymentId,
            domainOrder.Status,
            domainOrder.OrderTotal, 
            orderItemsResponse);
    }

    public async Task<OrderResponse> CompleteOrderAsync(CompleteOrderRequest request, CancellationToken cancellationToken)
    {
        if (request.PaymentId == Guid.Empty)
        {
            throw new InvalidOperationException("PaymentId is required");
        }
        
        var order = await orderRepository.GetOrderByIdAsync(request.OrderId, cancellationToken);
        
        if (order.Status == OrderStatus.Paid)
        {
            throw new InvalidOperationException("Order is already paid");
        }
        
        order.Status = OrderStatus.Paid;
        order.PaymentId = request.PaymentId;
        await orderRepository.UpdateOrderAsync(order, cancellationToken);
        
        var stockItemIds = order.OrderItems.Select(s => s.StockItemId).ToArray();
        List<StockResponse> stockItems = [];

        foreach (var stockItemId in stockItemIds)
        {
            stockItems.Add(await gateway.GetStockItemAsync(stockItemId, cancellationToken));
        }

        var orderItems = order.OrderItems
            .Join(stockItems, item => item.StockItemId, stockItem => stockItem.Id, (orderItem, stockItem) => (orderItem, stockItem));
        
        return new OrderResponse(
            order.Id,
            order.UserId,
            order.PaymentId,
            order.Status,
            order.OrderTotal, 
            orderItems.Select(s => 
                new OrderItemResponse(
                    s.orderItem.StockItemId, 
                    s.orderItem.Quantity, 
                    s.orderItem.StockItemPrice, 
                    s.orderItem.OrderItemPrice, 
                    s.stockItem.Name, 
                    s.stockItem.Description, 
                    s.orderItem.IsDiscounted, 
                    s.orderItem.DiscountPercentage, 
                    s.stockItem.ImageUrl)).ToArray());
    }

    public async Task<OrderResponse> CancelOrderAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetOrderByIdAsync(id, cancellationToken);

        switch (order.Status)
        {
            case OrderStatus.Paid:
                throw new InvalidOperationException("Order is already paid");
            case OrderStatus.Cancelled:
                throw new InvalidOperationException("Order is already cancelled");
        }
        
        order.Status = OrderStatus.Cancelled;
        await orderRepository.UpdateOrderAsync(order, cancellationToken);
        
        var stockItemIds = order.OrderItems.Select(s => s.StockItemId).ToArray();
        List<StockResponse> stockItems = [];

        foreach (var stockItemId in stockItemIds)
        {
            stockItems.Add(await gateway.GetStockItemAsync(stockItemId, cancellationToken));
        }

        var orderItems = order.OrderItems
            .Join(stockItems, item => item.StockItemId, stockItem => stockItem.Id, (orderItem, stockItem) => (orderItem, stockItem));
        
        return new OrderResponse(
            order.Id,
            order.UserId,
            order.PaymentId,
            order.Status,
            order.OrderTotal, 
            orderItems.Select(s => 
                new OrderItemResponse(
                    s.orderItem.StockItemId, 
                    s.orderItem.Quantity, 
                    s.orderItem.StockItemPrice, 
                    s.orderItem.OrderItemPrice, 
                    s.stockItem.Name, 
                    s.stockItem.Description, 
                    s.orderItem.IsDiscounted, 
                    s.orderItem.DiscountPercentage, 
                    s.stockItem.ImageUrl)).ToArray());
    }
}