using Moq;
using Refit;
using Retro.Ad.Contracts.Request;
using Retro.Ad.Contracts.Response;
using Retro.Domain;
using Retro.Greeter.Contracts.Request;
using Retro.Greeter.Infrastructure;
using Retro.Greeter.Infrastructure.Services;
using Retro.ResultWrappers;
using Xunit;

namespace E2ERetroShop.GreeterTests;

public class GreeterTests
{
    #region AdService Tests

    [Fact]
    public async Task GetPersonalizedAdsAsync_ReturnsAds_Success()
    {
        // Arrange
        var mockApi = new Mock<IAdsApi>();
        var expectedAds = new List<AdResponse> { new AdResponse(), new AdResponse() };
        var apiResponse = new ApiResponse<IEnumerable<AdResponse>>(new HttpResponseMessage(System.Net.HttpStatusCode.OK), expectedAds, new RefitSettings(), null);

        mockApi.Setup(api => api.GetPersonalizedAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        var service = new AdService(mockApi.Object);

        // Act
        var result = await service.GetPersonalizedAdsAsync("user123", 1234567890, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task GetPersonalizedAdsAsync_ThrowsError_WhenApiFails()
    {
        // Arrange
        var mockApi = new Mock<IAdsApi>();
        var apiResponse = new ApiResponse<IEnumerable<AdResponse>>(new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest), null, new RefitSettings(), null);

        mockApi.Setup(api => api.GetPersonalizedAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(apiResponse);

        var service = new AdService(mockApi.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ApiException>(async () => await service.GetPersonalizedAdsAsync("user123", 1234567890, CancellationToken.None));
    }

    #endregion

    #region SessionService Tests

    [Fact]
    public async Task GetActiveSessionsAsync_ReturnsPagedSessions_Success()
    {
        // Arrange
        var mockRepository = new Mock<ISessionRepository>();
        var expectedSessions = new PaginatedResult<Session>(
            items: new List<Session> { new Session(), new Session() },
            totalCount: 2,
            pageNumber: 1,
            pageSize: 10
        );

        mockRepository.Setup(repo => repo.GetActiveSessionsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSessions);

        var service = new SessionService(mockRepository.Object, null);

        // Act
        var result = await service.GetActiveSessionsAsync(new GetAllByPageRequest(1, 10), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSession_Success()
    {
        // Arrange
        var mockRepository = new Mock<ISessionRepository>();
        var sessionId = Guid.NewGuid();
        var session = new Session { Id = sessionId };

        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(session);

        var service = new SessionService(mockRepository.Object, null);

        // Act
        var result = await service.GetByIdAsync(new GetByIdRequest(sessionId), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sessionId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsException_WhenNotFound()
    {
        // Arrange
        var mockRepository = new Mock<ISessionRepository>();

        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Session)null);

        var service = new SessionService(mockRepository.Object, null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () =>
            await service.GetByIdAsync(new GetByIdRequest(Guid.NewGuid()), CancellationToken.None));

        Assert.Equal("Session not found", exception.Message);
    }

    [Fact]
    public async Task UpdateStateAsync_TogglesState_Success()
    {
        // Arrange
        var mockRepository = new Mock<ISessionRepository>();
        var session = new Session { Id = Guid.NewGuid(), IsActive = true };

        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(session);
        mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Session>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(session);

        var service = new SessionService(mockRepository.Object, null);

        // Act
        var result = await service.UpdateStateAsync(new UpdateSessionRequest(session), CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsActive);
    }

    [Fact]
    public async Task UpdateStateAsync_ThrowsException_WhenSessionNotFound()
    {
        // Arrange
        var mockRepository = new Mock<ISessionRepository>();

        mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Session)null);

        var service = new SessionService(mockRepository.Object, null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () =>
            await service.UpdateStateAsync(new UpdateSessionRequest(null), CancellationToken.None));

        Assert.Equal("Session not found", exception.Message);
    }

    #endregion
}