namespace TemoraColetaETT.Application.Interfaces;

// Contrato para registrar logs
public interface ILogService
{
    void LogInfo(string message, Exception? ex = null);
    void LogError(string message, Exception? ex = null);
}