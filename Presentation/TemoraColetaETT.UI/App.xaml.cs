using MauiApp = Microsoft.Maui.Controls.Application;
using TemoraColetaETT.UI.Views;

namespace TemoraColetaETT.UI
{
    public partial class App : MauiApp
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}