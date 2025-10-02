using CommunityToolkit.Mvvm.ComponentModel;using CommunityToolkit.Mvvm.Input;using TemoraColetaETT.Application.DTOs;using TemoraColetaETT.Application.Interfaces;using TemoraColetaETT.Infrastructure.Services;
namespace TemoraColetaETT.UI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _cpf;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _senha;

        [ObservableProperty]
        private bool? _isBusy;

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync()
        {
            IsBusy = true;
            try
            {
                var loginRequest = new LoginRequestDto { Cpf = Cpf, Senha = Senha };
                var response = await _authService.LoginAsync(loginRequest);

                LogService.LogInfo($"Login bem-sucedido para o CPF: {Cpf}");

                // TODO: Salvar o token e navegar para a próxima página
                await Shell.Current.DisplayAlert("Sucesso", $"Token recebido: {response.Token}", "OK");
            }
            catch (Exception ex)
            {
                LogService.LogError("Falha no login", ex);
                await Shell.Current.DisplayAlert("Erro", "Falha ao fazer login. Tente novamente.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(Cpf)
                && !string.IsNullOrWhiteSpace(Senha)
                && !(IsBusy ?? false);
        }
    }
}