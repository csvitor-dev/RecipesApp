using RecipesApp.Application.Services;

namespace CommonTestUtilities.Services;

public static class EncryptMockFactory
{
    public static PasswordEncryptionService CreateMock() => new("dot");
}