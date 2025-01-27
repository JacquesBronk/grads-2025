using E2ERetroShop.Util;
using Retro.Orders.Contracts.Request;
using Retro.Profile;

namespace E2ERetroShop.OrderTests;

public class OrderTests
{
    private readonly OrderGateway _orderGateway = new();
    private readonly StockGateway _stockGateway = new();
    
    private readonly Guid _userId;

    public OrderTests()
    {
        var userDetails = GetUser().GetAwaiter().GetResult();
        _userId = Guid.Parse(userDetails.UserId);
    }
    
    [Fact]
    public async Task GivenOrderRequest_WhenOrderRequestIsValid_ThenOrderCreateSuccessful()
    {
        // Arrange
        var stockItem =  await _stockGateway.GetStockItemsAsync(1, 1, CancellationToken.None);
        var orderRequest = new CreateOrderRequest(_userId, [new OrderItemRequest(stockItem.Items.Select(x => x.Id).FirstOrDefault(), 1)]);
        
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
        var stockItem =  await _stockGateway.GetStockItemsAsync(1, 1, CancellationToken.None);
        var orderRequest = new CreateOrderRequest(_userId, [new OrderItemRequest(stockItem.Items.Select(x => x.Id).FirstOrDefault(), 1)]);
        
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
        var stockItem =  await _stockGateway.GetStockItemsAsync(1, 1, CancellationToken.None);
        var orderRequest1 = new CreateOrderRequest(_userId, [new OrderItemRequest(stockItem.Items.Select(x => x.Id).FirstOrDefault(), 1)]);
        var orderRequest2 = new CreateOrderRequest(_userId, [new OrderItemRequest(stockItem.Items.Select(x => x.Id).FirstOrDefault(), 1)]);
        
        // Act
        var orderResponse1 = await _orderGateway.CreateOrderAsync(orderRequest1, CancellationToken.None);
        var orderResponse2 = await _orderGateway.CreateOrderAsync(orderRequest2, CancellationToken.None);
        var getOrdersResponse = await _orderGateway.GetUserOrdersAsync(_userId, CancellationToken.None);

        // Assert
        Assert.NotNull(getOrdersResponse);
        Assert.Equal(_userId, getOrdersResponse[0].UserId);
        Assert.Null(getOrdersResponse[0].PaymentId);
        Assert.Equal(orderResponse1.OrderItems[0].StockItemId, getOrdersResponse[0].OrderItems[0].StockItemId);
        Assert.Equal(orderResponse1.OrderItems[0].Quantity, getOrdersResponse[0].OrderItems[0].Quantity);
        
        Assert.Equal(_userId, getOrdersResponse[1].UserId);
        Assert.Null(getOrdersResponse[1].PaymentId);
    }
    
    [Fact]
    public async Task GivenGetOrdersByUserRequest_WhenOrdersExistsForUserWithMultipleItems_ThenOrdersIsRetrievedSuccessful()
    {
        // Arrange
        var stockItems =  await _stockGateway.GetStockItemsAsync(1, 2, CancellationToken.None);
        var stockItem1 = stockItems.Items.First();
        var stockItem2 = stockItems.Items.Skip(1).First();
        var orderItem1 = new OrderItemRequest(stockItem1.Id, 1);
        var orderItem2 = new OrderItemRequest(stockItem2.Id, 2);
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
        Assert.Equal(_userId, getOrdersResponse[1].UserId);
        Assert.Null(getOrdersResponse[1].PaymentId);
    }
    
    private async Task<User> GetUser()
    {
        AuthHelper authHelper = new();
        var token = await authHelper.GetTokenAsync();
        var userDetails = authHelper.GetUserFromToken(token);
        
        return userDetails;
    }
}