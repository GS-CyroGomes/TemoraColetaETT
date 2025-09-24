using TemoraColetaETT.Domain;

namespace TemoraColetaETT.Application.Interfaces;

// Contrato para comunicação com a API externa
public interface IApiClientService
{
    Task<bool> LoginAsync(string login, string password);
    Task<bool> EnviarCadastroUsuarioAsync(Usuario usuario);
}