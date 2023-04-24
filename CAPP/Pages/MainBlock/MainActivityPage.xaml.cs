namespace CAPP.Pages.MainBlock;

public partial class MainActivityPage : ContentPage
{
    public MainActivityPage()
	{
		BindingContext = this;
		InitializeComponent();
    }

    private void GoToCalculatorPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Calculator");
    }

    private void GoToStatsPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Stats");
    }

}