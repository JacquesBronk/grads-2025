using E2ERetroShop.Util;
using Retro.Stock.Contracts.Request;
using Retro.Stock.Domain;

namespace E2ERetroShop.Stock;

public class StockTests
{
    private readonly StockGateway _stockGateway = new();

    [Fact]
    public async Task GivenStockRequest_WhenStockRequestIsValid_ThenStockCreateSuccessful()
    {
        // Arrange
        var stockRequest = new UpsertStockRequest(
            null,
            "TEST_SKU",
            "Test Stock Item",
            "This is a simpleTest item",
            "http://test.co",
            StockCondition.New,
            10,
            50,
            new[] { "AAA", "BBB", "CCC" },
            false,
            null,
            DateTimeOffset.Now,
            null,
            "TEST-Suite",
            null);

        // Act
         await _stockGateway.CreateStockItemAsync(stockRequest, CancellationToken.None);
         var stockResponse = await _stockGateway.GetStockItemBySkuAsync(stockRequest.Sku, CancellationToken.None);

        // Assert
        Assert.NotNull(stockResponse);
        Assert.Equal(stockRequest.Title, stockResponse.Name);
        Assert.Equal(stockRequest.Quantity, stockResponse.Quantity);
    }
   
    [Fact]
    public async Task GivenUpdateStockRequest_WhenStockExists_ThenStockIsUpdatedSuccessful()
    {
        // Arrange
        var stockRequest = new UpsertStockRequest(
            null,
            "TEST_SKU",
            "Test Stock Item",
            "This is a simpleTest item",
            "http://test.co",
            StockCondition.New,
            10,
            50,
            new[] { "AAA", "BBB", "CCC" },
            false,
            null,
            DateTimeOffset.Now,
            null,
            "TEST-Suite",
            null);

        // Act
        var stockResponse = await _stockGateway.CreateStockItemAsync(stockRequest, CancellationToken.None);
        var createdStock = await _stockGateway.GetStockItemBySkuAsync(stockRequest.Sku, CancellationToken.None);
        
        var updatedStockRequest = new UpsertStockRequest(
            createdStock.Id,
            "TEST_SKU_UPDATE",
            "Test Stock Item",
            "This is a simpleTest item",
            "http://test.co",
            StockCondition.New,
            10,
            90,
            new[] { "AAA", "DDD", "CCC" },
            false,
            null,
            DateTimeOffset.Now,
            DateTimeOffset.Now,
            "TEST-Suite-UPDATE",
            "Test-Suite");
        var updateRequest = await _stockGateway.UpdateStockItemAsync(updatedStockRequest, CancellationToken.None);

        var updatedRequest = await _stockGateway.GetStockItemByIdAsync(createdStock.Id, CancellationToken.None);
        
        // Assert
        Assert.NotNull(updatedRequest);
        Assert.Equal(updatedRequest.Id, createdStock.Id);
        Assert.Equal(updatedRequest.Quantity, 90);
        Assert.Equal(updatedRequest.Sku, "TEST_SKU_UPDATE");
        Assert.Equal(updatedRequest.Tags, new[] { "AAA", "DDD", "CCC" });
    }
    
    
    [Fact]
    public async Task GivenDeleteStockId_WhenStockExists_ThenStockIsDeletedSuccessful()
    {
        // Arrange
        var stockRequest = new UpsertStockRequest(
            null,
            "TEST_SKU",
            "Test Stock Item",
            "This is a simpleTest item",
            "http://test.co",
            StockCondition.New,
            10,
            50,
            new[] { "AAA", "BBB", "CCC" },
            false,
            null,
            DateTimeOffset.Now,
            null,
            "TEST-Suite",
            null);
        await _stockGateway.CreateStockItemAsync(stockRequest, CancellationToken.None);
        var createdStock = await _stockGateway.GetStockItemBySkuAsync(stockRequest.Sku, CancellationToken.None);
        // Act
        await _stockGateway.DeleteStockItemAsync(createdStock.Id, CancellationToken.None);

        //Asserts happen on the Gateway with ensure status code
    }
    
    //TODO: There seems to be no condition in the stock response to validate if we're actually only getting new stock
    [Fact]
    public async Task GivenNewStockCondition_WhenStockExists_ThenStockIsFound()
    {
        // Arrange
        var newStock = await _stockGateway.GetStockByCondition(1,10,StockCondition.New, CancellationToken.None);

        // Act
        var count = newStock.Items.Count();
        
        // Assert
        Assert.NotNull(newStock);
        Assert.True(count > 0);
        Assert.Equal(count, 10);
        
    }
    
}

