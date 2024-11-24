using RecipesApp.Application.Services;

namespace CommonTestUtilities.Services;

public class EncryptMockFactory
{
    public static PasswordEncryptionService CreateMock()
        => new PasswordEncryptionService("dot");
}