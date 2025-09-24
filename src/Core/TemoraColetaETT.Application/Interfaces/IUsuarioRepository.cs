using TemoraColetaETT.Domain;

namespace TemoraColetaETT.Application.Interfaces;

// Contrato para operações de usuário no banco de dados local
public interface IUsuarioRepository
{
    Task<Usuario> GetByLoginAsync(string login);
    Task AddAsync(Usuario usuario);
    Task<IEnumerable<Usuario>> GetAllAsync();
}