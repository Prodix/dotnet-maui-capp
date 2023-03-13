using Microsoft.Maui.Controls.Shapes;

namespace CAPP;

public partial class WeightPage : ContentPage
{

    bool IsFirstEntryValid = false;
    bool IsSecondEntryValid = false;
    int mode;
    Border border;

	public WeightPage(bool IsTwo, int mode)
	{
        this.mode = mode;
        InitializeComponent();
        if (IsTwo)
        {

            Entry entry = new Entry
            {
                Placeholder = "Введите ваш желаемый вес",
                WidthRequest = 320,
                HeightRequest = 66,
                FontFamily = "OpenSans",
                FontSize = 20,
                Keyboard = Keyboard.Numeric
            };

            entry.TextChanged += entryChangedTwo;

            border = new Border
            {
                Content = entry,
                Shadow = new Shadow
                {
                    Brush = SolidColorBrush.Black,
                    Offset = new Point(0, 4),
                    Opacity = 0.15f
                },
                StrokeShape = new RoundRectangle
                {
                    CornerRadius = new CornerRadius(50, 50, 50, 50)
                },
                Stroke = new SolidColorBrush(Color.FromArgb("#9CF93D")),
                WidthRequest = 349,
                HeightRequest = 66,
                StrokeThickness = 0,
                Margin = new Thickness(0, 0, 0, 25)
            };

            vertLayout.Insert(vertLayout.Children.IndexOf(SecondLabel)+1, border);

        }
    }

    private void buttonValid()
    {
        if (IsFirstEntryValid && IsSecondEntryValid)
        {
            GoNextButton.IsEnabled = true;
            GoNextButton.TextColor = new SolidColorBrush(Color.FromArgb("#F83D7F")).Color;
            GoNextButton.Background = SolidColorBrush.White.Color;
        }
        else
        {
            GoNextButton.IsEnabled = false;
            GoNextButton.TextColor = SolidColorBrush.White.Color;
            GoNextButton.Background = new SolidColorBrush(Color.FromArgb("#747474")).Color;
        }
    }

    private void entryChangedOne(object sender, TextChangedEventArgs e)
    {
        if (!String.IsNullOrEmpty(((Entry)sender).Text))
        {
            if (Convert.ToDouble(((Entry)sender).Text) < 40)
            {
                IsFirstEntryValid = false;
                FirstLabel.Text = "Слишком маленькиий вес";
                DefaultBorder.StrokeThickness = 1;
            }
            else
            {
                IsFirstEntryValid = true;
                FirstLabel.Text = "";
                DefaultBorder.StrokeThickness = 0;
            }
        }
        else 
        { 
            IsFirstEntryValid = false;
            FirstLabel.Text = "Поле не может быть пустым";
            DefaultBorder.StrokeThickness = 1;
        }
    
        buttonValid();
    }

    private void entryChangedTwo(object sender, TextChangedEventArgs e)
    {
        if (!String.IsNullOrEmpty(((Entry)sender).Text))
        {
            double value = Convert.ToDouble(((Entry)sender).Text);
            if (value >= 50 && value <= 150)
            {
                if (mode == 2 && (value == Convert.ToDouble(FirstEntry.Text) || value < Convert.ToDouble(FirstEntry.Text)))
                {
                    IsSecondEntryValid = false;
                    SecondLabel.Text = "Желаемый вес должен быть больше";
                    border.StrokeThickness = 1;
                }
                else if (mode == 1 && (value == Convert.ToDouble(FirstEntry.Text) || value > Convert.ToDouble(FirstEntry.Text)))
                {
                    IsSecondEntryValid = false;
                    SecondLabel.Text = "Желаемый вес должен быть меньше";
                    border.StrokeThickness = 1;
                }
                else
                {
                    IsSecondEntryValid = true;
                    SecondLabel.Text = "";
                    border.StrokeThickness = 0;
                }
            }
            else
            {
                IsSecondEntryValid = false;
                SecondLabel.Text = value.ToString();
                border.StrokeThickness = 1;
            }
        }
        else
        {
            SecondLabel.Text = "Поле не может быть пустым";
            border.StrokeThickness = 1;
        }
        
        buttonValid();
    }

    private async void OnAccountCreating(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GenderPage(), false);
    }

}

