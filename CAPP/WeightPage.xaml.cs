using Android.Hardware.Lights;
using Microsoft.Maui.Controls.Shapes;

namespace CAPP;

public partial class WeightPage : ContentPage
{

    bool IsFirstEntryValid = false;
    bool IsSecondEntryValid = false;
    bool IsTwo = false;
    int heightValue = 0;
    int mode = 0;
    Border border;

	public WeightPage(bool IsTwo, int mode, int heightValue)
	{
        this.mode = mode;
        this.IsTwo = IsTwo;
        this.heightValue = heightValue;
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
        if (IsFirstEntryValid && IsSecondEntryValid && IsTwo)
        {
            GoNextButton.IsEnabled = true;
            GoNextButton.TextColor = new SolidColorBrush(Color.FromArgb("#F83D7F")).Color;
            GoNextButton.Background = SolidColorBrush.White.Color;
        }
        else if (IsFirstEntryValid && !IsTwo)
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
        if (!String.IsNullOrEmpty(((Entry)sender).Text) && ((Entry)sender).Text != ".")
        {
            double value = Convert.ToDouble(((Entry)sender).Text.Replace(".", ","));
            double bmi = value / (heightValue / 100 * (heightValue / 100));
            if (bmi < 18.5 && mode == 1)
            {
                IsFirstEntryValid = false;
                FirstLabel.Text = "У вас дефицит веса, мы не рекомендуем вам худеть";
                DefaultBorder.StrokeThickness = 1;
            }
            else if (bmi > 25 && mode == 2)
            {
                IsFirstEntryValid = false;
                FirstLabel.Text = "Ваш вес слишком высок мы не рекомендуем вам его повышать";
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
            if (((Entry)sender).Text == ".")
            {
                ((Entry)sender).Text = "";
            }
            IsFirstEntryValid = false;
            FirstLabel.Text = "Поле не может быть пустым";
            DefaultBorder.StrokeThickness = 1;
        }
    
        buttonValid();
    }

    private void entryChangedTwo(object sender, TextChangedEventArgs e)
    {
        if (!String.IsNullOrEmpty(((Entry)sender).Text) && ((Entry)sender).Text != ".")
        {
            double value = Convert.ToDouble(((Entry)sender).Text.Replace(".", ","));
            double bmi = value / (heightValue / 100 * (heightValue / 100));
            if (bmi >= 18.5 && bmi <= 25)
            {
                if (mode == 2 && (value == Convert.ToDouble(FirstEntry.Text.Replace(".", ",")) || value < Convert.ToDouble(FirstEntry.Text.Replace(".", ","))))
                {
                    IsSecondEntryValid = false;
                    SecondLabel.Text = "Желаемый вес должен быть больше";
                    border.StrokeThickness = 1;
                }
                else if (mode == 1 && (value == Convert.ToDouble(FirstEntry.Text.Replace(".", ",")) || value > Convert.ToDouble(FirstEntry.Text.Replace(".", ","))))
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
            else if (bmi >= 25)
            {
                IsSecondEntryValid = false;
                SecondLabel.Text = "Вес слишком большой";
                border.StrokeThickness = 1;
            }
            else
            {
                IsSecondEntryValid = false;
                SecondLabel.Text = "Вес слишком маленький";
                border.StrokeThickness = 1;
            }
        }
        else
        {
            if (((Entry)sender).Text == ".")
            {
                ((Entry)sender).Text = "";
            }
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

