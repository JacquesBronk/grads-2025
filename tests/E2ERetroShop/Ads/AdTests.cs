using E2ERetroShop.Util;
using Retro.Ad.Contracts.Request;

namespace E2ERetroShop.Ads;

public class AdTests
{
    private readonly AdsGateway _adGateway = new();

    [Fact]
    public async Task GivenAdRequest_WhenAdRequestIsValid_ThenAdCreateSuccessful()
    {
        // Arrange
        var adRequest = new UpsertAdRequest(
            null,
            "TEST_AD",
            "Test Ad Item",
            "This is a simpleTest item",
            "http://test.co",
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddDays(1),
            true,
            false,
            "<test>test</test>",
            "test",
            DateTimeOffset.Now, 
            null,
            null
           );

        // Act
         await _adGateway.CreateAdAsync(adRequest, CancellationToken.None);
       

        // Assert
        //Handled in gateway with EnsureStatusCode()
    }
   
    [Fact]
    public async Task GivenUpdateAdRequest_WhenAdExists_ThenAdIsUpdatedSuccessful()
    {
        // Arrange
        var ads = await _adGateway.GetAllAds(1, 1, CancellationToken.None);
        
        var adRequest = new UpsertAdRequest(
            ads.Items.First().Id,
            ads.Items.First().Title + " Updated",
            ads.Items.First().FullDescription + " Updated",
            ads.Items.First().ShortDescription,
            ads.Items.First().ImageUrl,
            ads.Items.First().StartDateTime,
            ads.Items.First().EndDateTime,
            true,
            false,
            ads.Items.First().RenderedHtml,
            ads.Items.First().CreatedBy,
            ads.Items.First().CreatedDateTime,
            "Test-Suite",
            DateTimeOffset.Now);

        // Act
        await _adGateway.UpdateAdAsync(adRequest, CancellationToken.None);
        var adResponse = await _adGateway.GetAdByIdAsync(ads.Items.First().Id, CancellationToken.None);

        // Assert
        Assert.NotNull(adResponse);
        Assert.Equal(adRequest.Title, adResponse.Title);
        Assert.Equal(adRequest.FullDescription, adResponse.FullDescription);
        Assert.Equal(adRequest.ImageUrl, adResponse.ImageUrl);
        
    }
    
    [Fact]
    public async Task GivenDeleteAdRequest_WhenAdExists_ThenAdIsDeletedSuccessful()
    {
        // Arrange
        var ads = await _adGateway.GetAllAds(1, 1, CancellationToken.None);
        
        // Act
        await _adGateway.DeleteAdAsync(ads.Items.First().Id, CancellationToken.None);
        var adResponse = await _adGateway.GetAdByIdAsync(ads.Items.First().Id, CancellationToken.None);

        // Assert
        Assert.Null(adResponse);
    }
    
    [Fact]
    public async Task GivenGetAllAdsRequest_WhenAdsExists_ThenAdsAreReturned()
    {
        // Arrange
        var ads = await _adGateway.GetAllAds(1, 1, CancellationToken.None);
        
        // Act
        var adResponse = await _adGateway.GetAllAds(1, 1, CancellationToken.None);

        // Assert
        Assert.NotNull(adResponse);
        Assert.Equal(ads.Items.First().Id, adResponse.Items.First().Id);
        Assert.Equal(ads.Items.First().Title, adResponse.Items.First().Title);
        Assert.Equal(ads.Items.First().FullDescription, adResponse.Items.First().FullDescription);
        Assert.Equal(ads.Items.First().ImageUrl, adResponse.Items.First().ImageUrl);
    }
    
}