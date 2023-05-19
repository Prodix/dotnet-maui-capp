#if ANDROID
				using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif


namespace CAPP;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		#if ANDROID
				Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
				{
					h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
				});
		#endif


		MainPage = new AppShell();

    }


	
}
