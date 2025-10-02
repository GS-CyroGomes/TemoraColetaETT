namespace TemoraColetaETT.UI;

using TemoraColetaETT.UI.Views;
using Application = Microsoft.Maui.Controls.Application;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new LoginView();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}