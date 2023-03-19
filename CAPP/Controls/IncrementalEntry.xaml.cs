namespace CAPP.Controls;

public partial class IncrementalEntry : ContentView
{
    private int entryValue;

    public readonly BindableProperty MinimalValueProperty = BindableProperty.Create(nameof(MinimalValue), typeof(int), typeof(IncrementalEntry), 30);
    public readonly BindableProperty MaximalValueProperty = BindableProperty.Create(nameof(MaximalValue), typeof(int), typeof(IncrementalEntry), 600);
    public readonly BindableProperty MeasureProperty = BindableProperty.Create(nameof(Measure), typeof(string), typeof(IncrementalEntry), "");
    public readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(nameof(ColumnSpacing), typeof(int), typeof(IncrementalEntry), 25);
    public new readonly BindableProperty WidthProperty = BindableProperty.Create(nameof(Width), typeof(int), typeof(IncrementalEntry), 300);
    public readonly BindableProperty EntryWidthProperty = BindableProperty.Create(nameof(EntryWidth), typeof(int), typeof(IncrementalEntry), 220);

    public int MinimalValue
    {
        get { return (int)GetValue(MinimalValueProperty); }
        set { SetValue(MinimalValueProperty, value); }
    }

    public int MaximalValue
    {
        get { return (int)GetValue(MaximalValueProperty); }
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

    public new int Width
    {
        get { return (int)GetValue(WidthProperty); }
        set { SetValue(WidthProperty, value); }
    }

    public int EntryWidth
    {
        get { return (int)GetValue(EntryWidthProperty); }
        set { SetValue(EntryWidthProperty, value); }
    }

    public int Value
    {
        get { return entryValue; }
        set
        {
            entryValue = value;
            OnPropertyChanged(nameof(Value));
        }
    }

    public IncrementalEntry()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private void PlusTapped(object sender, EventArgs e)
    {
        if (entryValue != MaximalValue)
        {
            Value++;
        }
    }

    private void MinusTapped(object sender, EventArgs e)
    {
        if (entryValue != MinimalValue)
        {
            Value--;
        }
    }

    private void PageLoaded(object sender, EventArgs e)
    {
        Value = MinimalValue;
    }


}