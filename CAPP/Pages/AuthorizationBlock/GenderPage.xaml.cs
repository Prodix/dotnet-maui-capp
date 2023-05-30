namespace CAPP;

public partial class GenderPage : ContentPage
{
    int mode;
    int heightValue;
    double weightValue;
    double intakeCoefficient;

    public GenderPage(int mode, int heightValue, double weightValue, double intakeCoefficient)
	{
		InitializeComponent();
        this.mode = mode;
        this.heightValue = heightValue;
        this.weightValue = weightValue;
        this.intakeCoefficient = intakeCoefficient;
	}

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        if (CheckOne.IsChecked)
            await Navigation.PushAsync(new BirthPage(mode, heightValue, weightValue, "m", intakeCoefficient));
        else 
            await Navigation.PushAsync(new BirthPage(mode, heightValue, weightValue, "f", intakeCoefficient));
    }

    private void ButtonOn()
    {
        GoNextButton.IsEnabled = true;
    }

    private void ButtonOff()
    {
        GoNextButton.IsEnabled = false;
    }

    private void OnTapOne(object sender, TappedEventArgs e)
    {
        if (CheckOne.IsChecked)
        {
            CheckOne.IsChecked = false;
            ButtonOff();
        }
        else
        {
            CheckOne.IsChecked = true;
            CheckTwo.IsChecked = false;
            ButtonOn();
        }
    }

    private void OnTapTwo(object sender, TappedEventArgs e)
    {
        if (CheckTwo.IsChecked)
        {
            CheckTwo.IsChecked = false;
            ButtonOff();
        }
        else
        {
            CheckTwo.IsChecked = true;
            CheckOne.IsChecked = false;
            ButtonOn();
        }
    }
}

