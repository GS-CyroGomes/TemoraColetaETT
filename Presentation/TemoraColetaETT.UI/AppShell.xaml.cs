using TemoraColetaETT.UI.Views;
using Microsoft.Maui.Controls;


namespace TemoraColetaETT.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Opcional, mas boa prática para navegação
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        }
    }
}