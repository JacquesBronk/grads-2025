using E2ERetroShop.Util;
using Retro.Profile;

namespace E2ERetroShop.ProfileTests;

public class ProfileTests
{
    private readonly ProfileGateway _profileGateway = new();
    
    private readonly Guid _userId;
    private readonly string _userName;
    
    private readonly Guid _profileId = Guid.Parse("4e0bf88e-efbb-4f16-bd5f-b14dccfe5b9e");

    public ProfileTests()
    {
        var user = GetUser();
        _userId = Guid.Parse(user.UserId);
        _userName = user.UserName;
    }

    #region ProfileTests
        [Fact]
        public async Task GivenProfileRequest_WhenProfileRequestIsValid_ThenProfileWorksUniversally()
        {
            // Arrange
            var profileRequest = new Profile()
            {
                Id = _profileId,
                UserId = _userId.ToString(),
                UserName = _userName,
                Email = "test@email.com"
            };

            // Act
            var profileResponse = await _profileGateway.CreateProfileAsync(profileRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(profileResponse);
            Assert.Equal(profileRequest.Id, profileResponse.Id);
            Assert.Equal(profileRequest.UserId, profileResponse.UserId, ignoreCase: true);
            Assert.Equal(profileRequest.UserName, profileResponse.UserName, ignoreCase: true);
            Assert.Equal(profileRequest.Email, profileResponse.Email);
            
            // Arrange
            var updateProfileRequest = new Profile()
            {
                Id = _profileId,
                UserId = _userId.ToString(),
                UserName = _userName,
                Email = "tester@email.com"
            };

            // Act
            var updatedProfileResponse = await _profileGateway.UpdateProfileAsync(updateProfileRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedProfileResponse);
            Assert.Equal(updateProfileRequest.Id, updatedProfileResponse.Id);
            Assert.Equal(updateProfileRequest.UserId, updatedProfileResponse.UserId, ignoreCase: true);
            Assert.Equal(updateProfileRequest.UserName, updatedProfileResponse.UserName, ignoreCase: true);
            Assert.Equal(updateProfileRequest.Email, updatedProfileResponse.Email);
            
            // Act
            var getProfileResponse = await _profileGateway.GetProfileByIdAsync(_profileId, CancellationToken.None);
            
            // Assert
            Assert.NotNull(getProfileResponse);
            Assert.Equal(updateProfileRequest.Id, getProfileResponse.Id);
            Assert.Equal(updateProfileRequest.UserId, getProfileResponse.UserId, ignoreCase: true);
            Assert.Equal(updateProfileRequest.UserName, getProfileResponse.UserName, ignoreCase: true);
            Assert.Equal(updateProfileRequest.Email, getProfileResponse.Email);
            
            // Act
            var deleteProfileResponse = await _profileGateway.DeleteProfileAsync(_profileId, CancellationToken.None);
            
            // Assert
            Assert.NotNull(deleteProfileResponse);
            Assert.Equal(updateProfileRequest.Id, deleteProfileResponse.Id);
            Assert.Equal(updateProfileRequest.UserId, deleteProfileResponse.UserId, ignoreCase: true);
            Assert.Equal(updateProfileRequest.UserName, deleteProfileResponse.UserName, ignoreCase: true);
            Assert.Equal(updateProfileRequest.Email, deleteProfileResponse.Email);
        }
    #endregion

    #region Contracts
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForCreateProfileRequestClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Contracts", "Request", "CreateProfileRequest.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'CreateProfileRequest.cs' does not exist at the specified location: {filePath}");
        }
        
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForUpdateProfileRequestClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Contracts", "Request", "UpdateProfileRequest.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'UpdateProfileRequest.cs' does not exist at the specified location: {filePath}");
        }
        
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForGetProfileRequestClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Contracts", "Request", "GetProfileRequest.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'GetProfileRequest.cs' does not exist at the specified location: {filePath}");
        }
        
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForDeleteProfileRequestClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Contracts", "Request", "DeleteProfileRequest.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'DeleteProfileRequest.cs' does not exist at the specified location: {filePath}");
        }
        
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForProfileResponseClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Contracts", "Response", "ProfileResponse.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'ProfileResponse.cs' does not exist at the specified location: {filePath}");
        }
    #endregion

    #region Domain
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForProfileClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Domain", "Profile.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'Profile.cs' does not exist at the specified location: {filePath}");
        }
    #endregion

    #region Infrastructure
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForProfileRepositoryInterface_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Infrastructure", "IProfileRepository.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'IProfileRepository.cs' does not exist at the specified location: {filePath}");
        }
        
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForProfileRepositoryClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Infrastructure", "ProfileRepository.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'ProfileRepository.cs' does not exist at the specified location: {filePath}");
        }
        
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForProfileServiceInterface_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Infrastructure", "IProfileService.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'IProfileService.cs' does not exist at the specified location: {filePath}");
        }
        
        [Fact]
        public void GivenSolutionRoot_WhenCheckingForProfileServiceClass_ThenFileShouldExistInSpecifiedLocation()
        {
            // Arrange
            var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName;
            var filePath = Path.Combine(solutionRoot, "services", "libs", "Retro.Profile.Infrastructure", "ProfileService.cs");

            // Act
            var fileExists = File.Exists(filePath);

            // Assert
            Assert.True(fileExists, $"The file 'ProfileService.cs' does not exist at the specified location: {filePath}");
        }
    #endregion

    #region Helper methods
        private User GetUser()
        {
            return _profileGateway.GetKeyCloakUser(CancellationToken.None)
                       .GetAwaiter()
                       .GetResult() ??
                   new User()
                   {
                       UserName = "Test User",
                       UserId = Guid.NewGuid()
                           .ToString()
                   };
        }
    #endregion
}