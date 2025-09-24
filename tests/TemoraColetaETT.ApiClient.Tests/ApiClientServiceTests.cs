using Moq;
using TemoraColetaETT.ApiClient;
using TemoraColetaETT.Application.Interfaces;
using TemoraColetaETT.Domain;

namespace TemoraColetaETT.ApiClient.Tests;

public class ApiClientServiceTests
{
    [Fact]
    public async Task EnviarCadastroUsuarioAsync()
    {
        // Arrange
        var empresa = new Empresa
        {
            Id = Guid.NewGuid(),
            Localizacao = "Bahia - BA"
        };
        empresa.ValidateWithApiAsync("01108339000250").Wait();

        var novoUsuario = new Usuario
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
        novoUsuario.Validate();

        // Criamos dublês (Mocks) para as dependências
        var mockOfflineService = new Mock<IOfflineSyncService>();
        var mockLogService = new Mock<ILogService>();
        var mockConnectivity = new Mock<IConnectivityService>();

        // Configuramos o comportamento do dublê de conectividade
        mockConnectivity.Setup(c => c.GetCurrentNetworkStatus()).Returns(NetworkStatus.Disconnected);

        // Criamos a instância real do nosso serviço, mas injetando os dublês
        var service = new ApiClientService(
            mockOfflineService.Object,
            mockLogService.Object,
            mockConnectivity.Object
        );

        // Act
        await service.EnviarCadastroUsuarioAsync(novoUsuario);

        // Assert
        // Verificamos se o método do dublê de fila foi chamado exatamente 1 vez.
        // Isso prova que a lógica de offline funcionou.
        mockOfflineService.Verify(s => s.AdicionarAcaoNaFilaAsync(It.IsAny<string>(), It.IsAny<Usuario>()), Times.Once);

        // Verificamos se o método do dublê de log foi chamado para informar que está sem conexão.
        // Isso prova que a escrita de logs está funcionando.
        mockLogService.Verify(l => l.LogInfo(It.Is<string>(msg => msg.Contains("Sem conexão")), null), Times.Once);
    }
}