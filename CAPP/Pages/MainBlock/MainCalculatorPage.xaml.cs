namespace CAPP.Pages.MainBlock;

public partial class MainCalculatorPage : ContentPage
{
	public MainCalculatorPage()
	{
		InitializeComponent();
	}

    private void GoToActivityPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Activity");
    }

    private void GoToStatsPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Stats");
    }
}