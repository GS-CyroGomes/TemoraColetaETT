namespace TemoraColetaETT.Domain;

public class Usuario : Pessoa
{
    public required Guid UsuarioId { get; init; } = Guid.NewGuid();
    public required string Login { get; init; }
    public required string HashedPassword { get; init; }
    public required string Role { get; init; } // "Admin", "User"

    public override void Validate()
    {
        base.Validate();

        if (string.IsNullOrWhiteSpace(Login))
            throw new ArgumentException("Login é obrigatório.");

        if (string.IsNullOrWhiteSpace(HashedPassword))
            throw new ArgumentException("Senha é obrigatória.");

        if (Role != "Admin" && Role != "User")
            throw new ArgumentException("Role inválido. Use 'Admin' ou 'User'.");
    }
}