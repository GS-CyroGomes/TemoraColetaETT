using TemoraColetaETT.Domain;

namespace TemoraColetaETT.Application.Interfaces;

// Contrato para a fila de envio offline
public interface IOfflineSyncService
{
    Task AdicionarAcaoNaFilaAsync(string tipoAcao, object dados);
    Task ProcessarFilaAsync();
}