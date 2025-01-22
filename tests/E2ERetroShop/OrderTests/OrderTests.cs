using E2ERetroShop.Util;
using Retro.Orders.Contracts.Request;

namespace E2ERetroShop.OrderTests;

public class OrderTests
{
    private readonly OrderGateway _orderGateway = new();
    
    private readonly Guid _userId = Guid.NewGuid();
    
    [Fact]
    public async Task GivenOrderRequest_WhenOrderRequestIsValid_ThenOrderCreateSuccessful()
    {
        // Arrange
        var orderRequest = new CreateOrderRequest(_userId, [new OrderItemRequest(Guid.NewGuid(), 1)]);
        
        // Act
        var orderResponse = await _orderGateway.CreateOrderAsync(orderRequest, CancellationToken.None);
        
        // Assert
        Assert.NotNull(orderResponse);
        Assert.Equal(orderRequest.UserId, orderResponse.UserId);
        Assert.Null(orderResponse.PaymentId);
        Assert.Equal(orderRequest.OrderItems.Length, orderResponse.OrderItems.Length);
        Assert.Equal(orderRequest.OrderItems[0].StockItemId, orderResponse.OrderItems[0].StockItemId);
        Assert.Equal(orderRequest.OrderItems[0].Quantity, orderResponse.OrderItems[0].Quantity);
    }
    
    [Fact]
    public async Task GivenGetOrderRequest_WhenOrderExists_ThenOrderIsRetrievedSuccessful()
    {
        // Arrange
        var orderRequest = new CreateOrderRequest(_userId, [new OrderItemRequest(Guid.NewGuid(), 1)]);
        
        // Act
        var orderResponse = await _orderGateway.CreateOrderAsync(orderRequest, CancellationToken.None);
        var getOrderResponse = await _orderGateway.GetOrderByIdAsync(orderResponse.Id, CancellationToken.None);
        
        // Assert
        Assert.NotNull(getOrderResponse);
        Assert.Equal(orderRequest.UserId, getOrderResponse.UserId);
        Assert.Null(orderResponse.PaymentId);
        Assert.Equal(getOrderResponse.OrderItems.Length, getOrderResponse.OrderItems.Length);
        Assert.Equal(getOrderResponse.OrderItems[0].StockItemId, getOrderResponse.OrderItems[0].StockItemId);
        Assert.Equal(getOrderResponse.OrderItems[0].Quantity, getOrderResponse.OrderItems[0].Quantity);
    }
    
    [Fact]
    public async Task GivenGetOrdersByUserRequest_WhenOrdersExistsForUser_ThenOrdersIsRetrievedSuccessful()
    {
        // Arrange
        var orderRequest1 = new CreateOrderRequest(_userId, [new OrderItemRequest(Guid.NewGuid(), 1)]);
        var orderRequest2 = new CreateOrderRequest(_userId, [new OrderItemRequest(Guid.NewGuid(), 1)]);
        
        // Act
        var orderResponse1 = await _orderGateway.CreateOrderAsync(orderRequest1, CancellationToken.None);
        var orderResponse2 = await _orderGateway.CreateOrderAsync(orderRequest2, CancellationToken.None);
        var getOrdersResponse = await _orderGateway.GetUserOrdersAsync(_userId, CancellationToken.None);

        // Assert
        Assert.NotNull(getOrdersResponse);
        Assert.Equal(_userId, getOrdersResponse[0].UserId);
        Assert.Null(getOrdersResponse[0].PaymentId);
        Assert.Equal(orderResponse1.OrderItems.Length, getOrdersResponse[0].OrderItems.Length);
        Assert.Equal(orderResponse1.OrderItems[0].StockItemId, getOrdersResponse[0].OrderItems[0].StockItemId);
        Assert.Equal(orderResponse1.OrderItems[0].Quantity, getOrdersResponse[0].OrderItems[0].Quantity);
        
        Assert.Equal(_userId, getOrdersResponse[1].UserId);
        Assert.Null(getOrdersResponse[1].PaymentId);
        Assert.Equal(orderResponse2.OrderItems.Length, getOrdersResponse[1].OrderItems.Length);
        Assert.Equal(orderResponse2.OrderItems[1].StockItemId, getOrdersResponse[1].OrderItems[0].StockItemId);
        Assert.Equal(orderResponse2.OrderItems[1].Quantity, getOrdersResponse[1].OrderItems[0].Quantity);
    }
    
    [Fact]
    public async Task GivenGetOrdersByUserRequest_WhenOrdersExistsForUserWithMultipleItems_ThenOrdersIsRetrievedSuccessful()
    {
        // Arrange
        var orderItem1 = new OrderItemRequest(Guid.NewGuid(), 1);
        var orderItem2 = new OrderItemRequest(Guid.NewGuid(), 2);
        var orderRequest1 = new CreateOrderRequest(_userId, [orderItem2, orderItem1]);
        var orderRequest2 = new CreateOrderRequest(_userId, [orderItem1, orderItem2]);
        
        // Act
        var orderResponse1 = await _orderGateway.CreateOrderAsync(orderRequest1, CancellationToken.None);
        var orderResponse2 = await _orderGateway.CreateOrderAsync(orderRequest2, CancellationToken.None);
        var getOrdersResponse = await _orderGateway.GetUserOrdersAsync(_userId, CancellationToken.None);

        // Assert
        Assert.NotNull(getOrdersResponse);
        Assert.Equal(_userId, getOrdersResponse[0].UserId);
        Assert.Null(getOrdersResponse[0].PaymentId);
        Assert.Equal(orderResponse1.OrderItems.Length, getOrdersResponse[0].OrderItems.Length);
        Assert.Equal(_userId, getOrdersResponse[1].UserId);
        Assert.Null(getOrdersResponse[1].PaymentId);
        Assert.Equal(orderResponse2.OrderItems.Length, getOrdersResponse[1].OrderItems.Length);
    }
}