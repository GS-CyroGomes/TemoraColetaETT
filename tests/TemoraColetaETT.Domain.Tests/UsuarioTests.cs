using System;
using System.Text.Json;
using TemoraColetaETT.Domain.Entities;
using Xunit;

namespace TemoraColetaETT.Domain.Tests
{
    public class PessoaTests
    {
        [Fact]
        public void CriarUsuario()
        {
            var empresa = new Empresa("OpenAI Brasil", "00000000000000");

            var usuario = new Usuario(
               "Cyro Matheus Barroso Gomes",
               DateTime.Parse("02/08/1996"),
               "06262658513",
               "1606856995",
               "Secretaria do Estado",
               "BA",
               "senha123", 
               empresa);

            Assert.NotNull(usuario);
            Assert.Equal("Cyro Matheus Barroso Gomes", usuario.NomeCompleto);
            Assert.Equal("06262658513", usuario.Cpf);
            Assert.Equal("senha123", usuario.Senha);
            Assert.Equal("OpenAI Brasil", usuario.Empresa.Nome);

            Console.WriteLine(JsonSerializer.Serialize(
                empresa,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                }
            ));

            Console.WriteLine(JsonSerializer.Serialize(
                usuario,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                }
            ));
        }
    }
}
