using CommunityToolkit.Maui.Views;

namespace CAPP.Controls;

public partial class SettingsPopup : Popup
{
	int height = 0;
	double weight = 0;
	UserData userData = new UserData();

	public SettingsPopup(double weight, int height)
	{
		this.weight = weight;
		this.height = height;

		InitializeComponent();

		Weight.Text = weight.ToString();
		Height.Text = height.ToString();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		userData.WeightValue = Math.Round(double.Parse(Weight.Text), 2);
		userData.HeightValue = Convert.ToInt32(double.Parse(Height.Text));

		Close(userData);
    }
}