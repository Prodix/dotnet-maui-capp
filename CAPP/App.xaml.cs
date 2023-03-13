using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace CAPP;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
		{
			h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
		});


		MainPage = new NavigationPage(new LoginPage());

    }


	
}
