using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemoraColetaETT.Application.Interfaces;

namespace TemoraColetaETT.UI.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _welcomeMessage = "Bem-vindo!";

        [ObservableProperty]
        private bool _isBusy;

        public DashboardViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        public async Task LoadDashboardAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                // Sua lógica aqui
                WelcomeMessage = "Dashboard carregado!";
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task InitializeAsync()
        {
            if (LoadDashboardCommand.CanExecute(null))
            {
                LoadDashboardCommand.Execute(null);
            }
        }
    }
}