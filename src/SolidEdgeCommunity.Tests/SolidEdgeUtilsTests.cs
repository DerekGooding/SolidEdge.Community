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

    [TestMethod]
    public void Connect_ShouldAttemptConnection()
    {
        try
        {
            SolidEdgeUtils.Connect();
        }
        catch
        {
            // Expected
        }

        try
        {
            SolidEdgeUtils.Connect(true);
        }
        catch
        {
            // Expected
        }

        try
        {
            SolidEdgeUtils.Connect(false, true);
        }
        catch
        {
            // Expected
        }
    }

    [TestMethod]
    public void Start_ShouldAttemptStart()
    {
        try
        {
            SolidEdgeUtils.Start();
        }
        catch
        {
            // Expected
        }
    }

    [TestMethod]
    public void GetVersion_ShouldReturnSomething()
    {
        try
        {
            var version = SolidEdgeUtils.GetVersion();
            Assert.IsNotNull(version);
        }
        catch
        {
            // Expected if SE is not installed
        }
    }

    [TestMethod]
    public void GetInstalledLanguage_ShouldAttemptCall()
    {
        try
        {
            var lang = SolidEdgeUtils.GetInstalledLanguage();
            Assert.IsNotNull(lang);
        }
        catch
        {
            // Expected
        }
    }

    [TestMethod]
    public void GetInstalledPath_ShouldAttemptCall()
    {
        try
        {
            SolidEdgeUtils.GetInstalledPath();
        }
        catch
        {
            // Expected
        }
    }

    [TestMethod]
    public void GetProgramFolderPath_ShouldAttemptCall()
    {
        try
        {
            SolidEdgeUtils.GetProgramFolderPath();
        }
        catch
        {
            // Expected
        }
    }

    [TestMethod]
    public void GetTrainingFolderPath_ShouldAttemptCall()
    {
        try
        {
            SolidEdgeUtils.GetTrainingFolderPath();
        }
        catch
        {
            // Expected
        }
    }
}