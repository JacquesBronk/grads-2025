using Microsoft.Extensions.Logging;
using Moq;
using Refit;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;
using Retro.Ad.Domain;
using Retro.Ad.Infrastructure;
using Retro.Greeter.Infrastructure.Services;
using AdService = Retro.Ad.Infrastructure.AdService;

namespace E2ERetroShop.AdsTests;

public class AdsTests
{
    private readonly Mock<IAdRepository> _mockAdRepository;
    private readonly Mock<IAdMetricsRepository> _mockMetricsRepository;
    private readonly Mock<IAdsAdminApi> _mockAdsAdminApi;
    private readonly Mock<ILogger<AdService>> _mockLogger;
    private readonly AdService _adService;

    public AdsTests()
    {
        _mockAdRepository = new Mock<IAdRepository>();
        _mockMetricsRepository = new Mock<IAdMetricsRepository>();
        _mockAdsAdminApi = new Mock<IAdsAdminApi>();
        _mockLogger = new Mock<ILogger<AdService>>();
        _adService = new AdService(
            _mockAdRepository.Object,
            _mockMetricsRepository.Object,
            _mockAdsAdminApi.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task GetAdDetailById_ReturnsAdDetail_Success()
    {
        // Arrange
        var adDetail = new AdDetail
        {
            Id = Guid.NewGuid(),
            Title = "Test Ad",
            IsActive = true
        };
        
        _mockAdRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(adDetail);

        var request = new GetAdDetailByIdRequest(adDetail.Id);

        // Act
        var result = await _adService.GetAdDetailById(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(adDetail.Id, result.Id);
    }

    [Fact]
    public async Task GetAdDetailById_ThrowsException_WhenNotFound()
    {
        // Arrange
        _mockAdRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AdDetail)null);

        var request = new GetAdDetailByIdRequest(Guid.NewGuid());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () =>
            await _adService.GetAdDetailById(request));

        Assert.Equal("Ad not found", exception.Message);
    }

    [Fact]
    public async Task GetAdsFromTimestampAsync_ReturnsAds_Success()
    {
        // Arrange
        var ads = new PagedAdDetailResponse(
            Items: new List<AdDetailResponse> { new(), new() },
            PageNumber: 1,
            PageSize: 10,
            TotalCount: 2
        );

        var response = new ApiResponse<PagedAdDetailResponse>(new HttpResponseMessage(System.Net.HttpStatusCode.OK), ads, new RefitSettings());

        _mockAdsAdminApi.Setup(api => api.GetAdsFromDateAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _adService.GetAdsFromTimestampAsync(DateTimeOffset.Now, 1, 10, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetAdsFromTimestampAsync_LogsError_WhenApiFails()
    {
        // Arrange
        var response = new ApiResponse<PagedAdDetailResponse>(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest), null, new RefitSettings());

        _mockAdsAdminApi.Setup(api => api.GetAdsFromDateAsync(It.IsAny<DateTimeOffset>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _adService.GetAdsFromTimestampAsync(DateTimeOffset.Now, 1, 10, CancellationToken.None);

        // Assert
        Assert.Empty(result);
        _mockLogger.Verify(logger =>
            logger.LogError(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
    }

    [Fact]
    public async Task TrackAdViewAsync_ReturnsMappedAdResponse_Success()
    {
        // Arrange
        var ad = new AdDetail { Id = Guid.NewGuid(), Title = "Tracked Ad" };
        var request = new AdViewMetric { AdId = ad.Id };

        _mockAdRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ad);
        
        _mockMetricsRepository.Setup(repo => repo.TrackAdViewAsync(It.IsAny<AdViewMetric>(), It.IsAny<CancellationToken>()));

        // Act
        var result = await _adService.TrackAdViewAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ad.Id, result.Id);
    }

    [Fact]
    public async Task TrackAdViewAsync_ThrowsException_WhenAdNotFound()
    {
        // Arrange
        _mockAdRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AdDetail)null);

        var request = new AdViewMetric { AdId = Guid.NewGuid() };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () =>
            await _adService.TrackAdViewAsync(request));

        Assert.Equal("Ad with Id: {request.AdId} not found", exception.Message);
    }
}
