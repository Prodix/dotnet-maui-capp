namespace CAPP.Controls;

public partial class SpinnerCalendar : ContentView
{

    public static new readonly BindableProperty WidthProperty = BindableProperty.Create(nameof(Width), typeof(int), typeof(SpinnerCalendar), 180);
    public static readonly BindableProperty CalendarBackgroundProperty = BindableProperty.Create(nameof(CalendarBackground), typeof(Color), typeof(SpinnerCalendar), Color.FromArgb("#F83D7F"));
    public static readonly BindableProperty MinimalYearProperty = BindableProperty.Create(nameof(MinYear), typeof(int), typeof(SpinnerCalendar), 1950);
    public static readonly BindableProperty MaximalYearProperty = BindableProperty.Create(nameof(MaxYear), typeof(int), typeof(SpinnerCalendar), DateTime.Now.Year + 10);
    public static readonly BindableProperty AgeLimitProperty = BindableProperty.Create(nameof(AgeLimit), typeof(int), typeof(SpinnerCalendar), 0);

    public int MinYear
    {
        get => (int)GetValue(SpinnerCalendar.MinimalYearProperty);
        set => SetValue(SpinnerCalendar.MinimalYearProperty, value);
    }

    public int MaxYear
    {
        get => (int)GetValue(SpinnerCalendar.MaximalYearProperty);
        set => SetValue(SpinnerCalendar.MaximalYearProperty, value);
    }

    public int AgeLimit
    {
        get => (int)GetValue(SpinnerCalendar.AgeLimitProperty);
        set => SetValue(SpinnerCalendar.AgeLimitProperty, value);
    }


    public new int Width
    {
        get => (int)GetValue(SpinnerCalendar.WidthProperty);
        set => SetValue(SpinnerCalendar.WidthProperty, value);
    }

    public Color CalendarBackground
    {
        get => (Color)GetValue(SpinnerCalendar.CalendarBackgroundProperty);
        set => SetValue(SpinnerCalendar.CalendarBackgroundProperty, value);
    }

    int currentMonth = DateTime.Now.Month;
    int currentDay = DateTime.Now.Day;
    int currentYear = DateTime.Now.Year;

    bool IsLeapYear = DateTime.IsLeapYear(DateTime.Now.Year);

    public List<string> monthsList { get; set; }

    public List<string> daysList { get; set; }

    public List<string> yearsList { get; set; }

    public int CurrentYear
    {
        get
        {
            return currentYear;
        }

        set
        {
            if (IsLeapYear != DateTime.IsLeapYear(value))
            {
                daysList = GenerateDays(value, currentMonth);
                scrollDays.ItemsSource = daysList;
                scrollDays.ScrollTo(currentDay - 1, animate: false);
                IsLeapYear = !IsLeapYear;
            }
            currentYear = value;
        }
    }

    public int CurrentMonth
    {
        get
        {
            return currentMonth;
        }

        set
        {
            if (currentMonth != value)
            {
                daysList = GenerateDays(CurrentYear, value);
                scrollDays.ItemsSource = daysList;
                scrollDays.ScrollTo(currentDay - 1, animate: false);
                currentMonth = value;
            }
        }
    }

    public string CurrentDay
    {
        get 
        {
            if (currentDay < 10)
                return "0" + currentDay.ToString();
            else
            {
                return currentDay.ToString();
            }
        }
        set => currentDay = Convert.ToInt32(value);
    }

    public SpinnerCalendar()
    {
        InitializeComponent();
        monthsList = new List<string> { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "" };
        BindingContext = this;
    }

    private List<string> GenerateDays(int year, int month)
    {

        int daysCount = DateTime.DaysInMonth(year, month);
        List<string> list = new List<string> { "" };
        list.AddRange(Enumerable.Range(1, daysCount).Select(n => n.ToString()));
        list.Add("");
        return list;
    }

    private List<string> GenerateYears()
    {
        List<string> list = new List<string> { "" };
        list.AddRange(Enumerable.Range(DateTime.Now.Year - 50, DateTime.Now.Year + 50 - (DateTime.Now.Year - 50) + 1).Reverse().Select(n => n.ToString()));
        list.Add("");
        return list;
    }

    private List<string> GenerateYears(int minYear, int maxYear = 0, int ageLimit = 0)
    {
        List<string> list = new List<string> { "" };
        if (ageLimit > 0 && currentYear - ageLimit >= minYear)
        {
            CurrentYear = DateTime.Now.Year - ageLimit;
            list.AddRange(Enumerable.Range(minYear, CurrentYear - minYear + 1).Reverse().Select(n => n.ToString()));
            list.Add("");
            return list;
        }
        else if (maxYear > 0 && maxYear > minYear)
        {
            list.AddRange(Enumerable.Range(minYear, maxYear - minYear + 1).Reverse().Select(n => n.ToString()));
            list.Add("");
            return list;
        }
        else
        {
            return GenerateYears();
        }
    }

    private void YearsScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        CurrentYear = Convert.ToInt32(yearsList[e.CenterItemIndex]);
        scrollYears.SelectedItem = yearsList[e.CenterItemIndex];
    }

    private void MonthsScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        CurrentMonth = e.CenterItemIndex;
        scrollMonths.SelectedItem = monthsList[e.CenterItemIndex];

    }

    private void DaysScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        currentDay = e.CenterItemIndex;
        scrollDays.SelectedItem = daysList[e.CenterItemIndex];
    }

    private void MonthsLoaded(object sender, EventArgs e)
    {
        scrollMonths.ScrollTo(currentMonth, position: ScrollToPosition.Center, animate: false);
    }

    private void YearsLoaded(object sender, EventArgs e)
    {
        yearsList = GenerateYears(MinYear, maxYear: MaxYear, ageLimit: AgeLimit);
        scrollYears.ItemsSource = yearsList;
        if (DateTime.Now.Year <= Convert.ToInt32(yearsList[1]))
        {
            scrollYears.ScrollTo(yearsList.IndexOf(DateTime.Now.Year.ToString()), position: ScrollToPosition.Center, animate: false);
        }
    }
}