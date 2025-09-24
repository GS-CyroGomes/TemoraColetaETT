using TemoraColetaETT.Application.Interfaces;
using TemoraColetaETT.Domain;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TemoraColetaETT.ApiClient;

public class ApiClientService : IApiClientService
{
    // ... (propriedades e construtor continuam iguais) ...
    private readonly HttpClient _httpClient;
    private readonly IOfflineSyncService _offlineSyncService;
    private readonly ILogService _logService;
    private readonly IConnectivityService _connectivityService;

    public ApiClientService(
        IOfflineSyncService offlineSync, 
        ILogService logService, 
        IConnectivityService connectivityService)
    {
        _httpClient = new HttpClient { BaseAddress = new System.Uri("http://localhost:56109/apiTemoraETT/") };
        _offlineSyncService = offlineSync;
        _logService = logService;
        _connectivityService = connectivityService;
    }

    public async Task<bool> EnviarCadastroUsuarioAsync(Usuario usuario)
    {
        // ... (lógica de offline continua igual) ...
        _logService.LogInfo($"Tentando enviar cadastro para usuário: {usuario.Login}");

        if (_connectivityService.GetCurrentNetworkStatus() != NetworkStatus.Connected)
        {
            _logService.LogInfo("Sem conexão. Adicionando à fila offline.");
            await _offlineSyncService.AdicionarAcaoNaFilaAsync("CADASTRO_USUARIO", usuario);
            return false; 
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("usuarios/cadastro", usuario);

            string responseBody = await response.Content.ReadAsStringAsync();
            _logService.LogInfo($"Resposta da API (Cadastro): StatusCode={response.StatusCode}, Corpo={responseBody}");

            response.EnsureSuccessStatusCode(); 
            
            _logService.LogInfo($"Cadastro do usuário {usuario.Login} enviado com sucesso.");
            return true;
        }
        catch (System.Exception ex)
        {
            _logService.LogError($"Erro ao enviar cadastro para a API: {ex.Message}", ex);
            await _offlineSyncService.AdicionarAcaoNaFilaAsync("CADASTRO_USUARIO", usuario);
            return false;
        }
    }
    
    public async Task<bool> LoginAsync(string login, string password) 
    {
        _logService.LogInfo($"Tentando login para usuário: {login}");
        var loginRequest = new { Login = login, Password = password };

        try
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", loginRequest);

            string responseBody = await response.Content.ReadAsStringAsync();
            _logService.LogInfo($"Resposta da API (Login): StatusCode={response.StatusCode}, Corpo={responseBody}");

            if (response.IsSuccessStatusCode)
            {
                _logService.LogInfo($"Login para {login} bem-sucedido.");
                return true;
            }

            _logService.LogError($"Falha no login para {login}. Status: {response.StatusCode}");
            return false;
        }
        catch (System.Exception ex)
        {
            _logService.LogError($"Erro de conexão durante o login: {ex.Message}", ex);
            return false;
        }
    }
}