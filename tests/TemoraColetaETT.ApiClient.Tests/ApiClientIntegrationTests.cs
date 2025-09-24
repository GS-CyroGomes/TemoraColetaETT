// tests/TemoraColetaETT.ApiClient.Tests/ApiClientIntegrationTests.cs

using Moq;
using TemoraColetaETT.ApiClient;
using TemoraColetaETT.Application.Interfaces;
using TemoraColetaETT.Domain;
using Xunit.Abstractions; // <-- PASSO 1: Adicione este using

namespace TemoraColetaETT.ApiClient.Tests;

// ===================================================================
// PASSO 2: Crie esta pequena classe de log DENTRO do seu arquivo de teste.
// Ela é uma implementação REAL de ILogService que escreve no console do teste.
// ===================================================================
public class TestOutputLogger : ILogService
{
    private readonly ITestOutputHelper _output;

    public TestOutputLogger(ITestOutputHelper output)
    {
        _output = output;
    }

    // Este é o método que o compilador disse que estava faltando.
    // Agora ele está implementado.
    public void LogInfo(string message, Exception? ex = null)
    {
        _output.WriteLine($"[INFO] {message} | Exception: {ex?.Message}");
    }

    // Adicionei este também para garantir, caso você tenha um LogInfo sem exceção.
    // Se não tiver, não há problema em mantê-lo.
    public void LogInfo(string message)
    {
        _output.WriteLine($"[INFO] {message}");
    }

    public void LogError(string message, Exception? ex = null)
    {
        _output.WriteLine($"[ERROR] {message} | Exception: {ex?.Message}");
    }
}


public class ApiClientIntegrationTests
{
    // ===================================================================
    // PASSO 3: Prepare o teste para receber o helper de saída do xUnit.
    // ===================================================================
    private readonly ITestOutputHelper _output;

    public ApiClientIntegrationTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task EnviarCadastroUsuarioAsync_QuandoOnline_DeveChamarApiReal()
    {
        // Arrange
        // Mockamos apenas o que não é o foco do teste
        var mockOfflineService = new Mock<IOfflineSyncService>();
        
        // Criamos o dublê de conectividade para simular que estamos ONLINE
        var realConnectivity = new Mock<IConnectivityService>();
        realConnectivity.Setup(c => c.GetCurrentNetworkStatus()).Returns(NetworkStatus.Connected);
        
        // ===================================================================
        // PASSO 4: Use a nossa implementação REAL do logger.
        // ===================================================================
        var testLogger = new TestOutputLogger(_output);

        // Instanciamos o serviço injetando nosso logger de teste REAL
        var service = new ApiClientService(
            mockOfflineService.Object,
            testLogger, // <-- Não estamos mais usando um Mock aqui!
            realConnectivity.Object
        );

        var novoUsuario = new Usuario 
        {
            Id = Guid.NewGuid(),
            Nome = "Usuario Integracao Real",
            DataNascimento = DateTime.Parse("1990-01-01"),
            Rg = "1111111",
            Cpf = "111.111.111-11",
            OrgaoEmissor = "SSP",
            UfEmissor = "BA",
            EmpresaId = Guid.NewGuid(),
            UsuarioId = Guid.NewGuid(),
            Login = "integracao.real",
            HashedPassword = "real_hash",
            Role = "Tester"
        };
        
        _output.WriteLine(">>> IMPORTANTE: Sua API local DEVE estar rodando para este teste passar. <<<");
        
        // Act
        var resultado = await service.EnviarCadastroUsuarioAsync(novoUsuario);

        // Assert
        Assert.True(resultado);
    }
}