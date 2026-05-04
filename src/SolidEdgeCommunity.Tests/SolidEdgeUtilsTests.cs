namespace SolidEdgeCommunity.Tests;

[TestClass]
public class SolidEdgeUtilsTests
{
    [TestMethod]
    public void GetTrainingFolderPath_ShouldReturnCorrectPath()
    {
        // This test is tricky because GetTrainingFolderPath calls GetInstalledPath,
        // which calls GetProgramFolderPath, which uses SEInstallDataLib.
        // Without an interface for installation data, we can't easily unit test this.
        // However, we can test that it uses Path.Combine correctly if we could mock the underlying calls.

        // Since we can't easily mock the static calls in the same class without refactoring,
        // we'll focus on testing the testable logic if any exists.

        // SolidEdgeUtils is mostly a wrapper around COM and Registry.
        // We'll leave this as a placeholder or move to integration tests if needed.
    }
}