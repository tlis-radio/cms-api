using System.Diagnostics;

namespace Tlis.Cms.Shared;

public static class Telemetry
{
    public static readonly string ServiceName = "cms-api";

    public static readonly ActivitySource ActivitySource = new(ServiceName);
}