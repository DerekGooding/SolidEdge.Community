using SolidEdgeCommunity;

namespace QA;

internal static class Program
{
    private static void Main()
    {
        var application = SolidEdgeUtils.Connect(true);
        application.Visible = true;
    }
}