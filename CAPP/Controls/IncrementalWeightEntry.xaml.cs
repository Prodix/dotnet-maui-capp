using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;

namespace CAPP.Controls;

public partial class IncrementalWeightEntry : ContentView
{
    private double entryValue;
    private bool isLoadEvent = true;
    private bool isToastEnabled = true;

    public readonly BindableProperty MinimalValueProperty = BindableProperty.Create(nameof(MinimalValue), typeof(double), typeof(IncrementalEntry), 30.0);
    public readonly BindableProperty MaximalValueProperty = BindableProperty.Create(nameof(MaximalValue), typeof(double), typeof(IncrementalEntry), 600.0);
    public readonly BindableProperty MeasureProperty = BindableProperty.Create(nameof(Measure), typeof(string), typeof(IncrementalEntry), "");
    public readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(nameof(ColumnSpacing), typeof(int), typeof(IncrementalEntry), 25);
    public readonly BindableProperty GridWidthProperty = BindableProperty.Create(nameof(GridWidth), typeof(int), typeof(IncrementalEntry), 350);
    public readonly BindableProperty EntryWidthProperty = BindableProperty.Create(nameof(EntryWidth), typeof(int), typeof(IncrementalEntry), 220);
    public readonly BindableProperty MaximumTextProperty = BindableProperty.Create(nameof(EntryWidth), typeof(string), typeof(IncrementalEntry), "Вы перейдёте верхнюю границу веса!");
    public readonly BindableProperty MinimumTextProperty = BindableProperty.Create(nameof(EntryWidth), typeof(string), typeof(IncrementalEntry), "Вы перейдёте нижнюю границу веса!");

    public string MinimumText
    {
        get { return (string)GetValue(MinimumTextProperty); }
        set { SetValue(MinimumTextProperty, value); }
    }

    public string MaximumText
    {
        get { return (string)GetValue(MaximumTextProperty); }
        set { SetValue(MaximumTextProperty, value); }
    }

    public double MinimalValue
    {
        get { return (double)GetValue(MinimalValueProperty); }
        set { SetValue(MinimalValueProperty, value); }
    }

    public double MaximalValue
    {
        get { return (double)GetValue(MaximalValueProperty); }
        set { SetValue(MaximalValueProperty, value); }
    }

    public new string Measure
    {
        get { return (string)GetValue(MeasureProperty); }
        set { SetValue(MeasureProperty, value); }
    }

    public int ColumnSpacing
    {
        get { return (int)GetValue(ColumnSpacingProperty); }
        set { SetValue(ColumnSpacingProperty, value); }
    }

    public int GridWidth
    {
        get { return (int)GetValue(GridWidthProperty); }
        set { SetValue(GridWidthProperty, value); }
    }

    public int EntryWidth
    {
        get { return (int)GetValue(EntryWidthProperty); }
        set { SetValue(EntryWidthProperty, value); }
    }

    public bool IsLoadEvent
    {
        get { return isLoadEvent; }
        set { isLoadEvent = value; }
    }

    public bool IsToastEnabled
    {
        get { return isToastEnabled; }
        set { isToastEnabled = value; }
    }

    public double Value
    {
        get { return entryValue; }
        set
        {
            entryValue = value;
            OnPropertyChanged(nameof(Value));
        }
    }

    public bool IsMaximumToastEnabled { get; set; } = true;
    public bool IsMinimumToastEnabled { get; set; } = true;

    public IncrementalWeightEntry()
    {
        InitializeComponent();
        BindingContext = this;

        PlusButton.Behaviors.Add(new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior()
        {
            TintColor = (Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]
        });

        MinusButton.Behaviors.Add(new CommunityToolkit.Maui.Behaviors.IconTintColorBehavior()
        {
            TintColor = (Color)Application.Current.Resources.MergedDictionaries.First()["Primary"]
        });
    }

    public async void PlusTapped(object sender, EventArgs e)
    {
        if (entryValue + 0.5 <= MaximalValue)
        {
            Value += 0.5;
        }
        else
        {
            Value = MaximalValue;
            if (isToastEnabled && IsMaximumToastEnabled)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = MaximumText;
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
        }
    }

    public async void MinusTapped(object sender, EventArgs e)
    {
        if (entryValue - 0.5 >= MinimalValue)
        {
            Value -= 0.5;
        }
        else
        {
            Value = MinimalValue;
            if (isToastEnabled && IsMinimumToastEnabled)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                string text = MinimumText;
                ToastDuration duration = ToastDuration.Short;
                double fontSize = 14;

                var toast = Toast.Make(text, duration, fontSize);

                await toast.Show(cancellationTokenSource.Token);
            }
        }
    }

    private void PageLoaded(object sender, EventArgs e)
    {
        if (IsLoadEvent)
        {
            Value = MinimalValue;
        }
    }


}