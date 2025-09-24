namespace TemoraColetaETT.Application.Interfaces;

public enum NetworkStatus
{
    Unknown,
    Connected,
    Disconnected
}

public interface IConnectivityService
{
    NetworkStatus GetCurrentNetworkStatus();
}