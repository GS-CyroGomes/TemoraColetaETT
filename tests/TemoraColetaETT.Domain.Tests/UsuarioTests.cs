using System.Text.Json;

namespace TemoraColetaETT.Domain.Tests;

public class UsuarioTests
{
    [Fact]
    public void TesteCriarUsuario()
    {
        /*
        var empresa = new Empresa
        {
            Id = Guid.NewGuid(),
            Localizacao = "Bahia - BA"
        };
        empresa.ValidateWithApiAsync("01108339000250").Wait();

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid(),
            Login = "Cyro",
            HashedPassword = "123456",
            Role = "Admin",
            Nome = "Cyro Gomes",
            DataNascimento = DateTime.Parse("1990-01-01"),
            Rg = "1234567",
            Cpf = "00011122233",
            OrgaoEmissor = "SSP",
            UfEmissor = "SP",
            EmpresaId = empresa.Id
        };
        usuario.Validate();

        var jsonEmpresa = JsonSerializer.Serialize(
            empresa,
            new JsonSerializerOptions { WriteIndented = true }
        );

        var pessoaJson = System.Text.Json.JsonSerializer.Serialize(
            (Pessoa)usuario,
            new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
        );
        
        var jsonUser = JsonSerializer.Serialize(
            usuario,
            new JsonSerializerOptions { WriteIndented = true }
        );

        Console.WriteLine($"\n\n\n\nEmpresa : {jsonEmpresa}");
        Console.WriteLine($"\n\nPessoa : {pessoaJson}");
        Console.WriteLine($"\n\nUsuario : {jsonUser}");
        */
    }
}