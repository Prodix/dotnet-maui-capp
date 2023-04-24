namespace CAPP.Pages.MainBlock;

public partial class MainStatsPage : ContentPage
{
	public MainStatsPage()
	{
		InitializeComponent();
	}

    private void GoToActivityPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Activity");
    }

    private void GoToCalculatorPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Calculator");
    }
}