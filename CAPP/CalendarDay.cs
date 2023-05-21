using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public class CalendarDay : INotifyPropertyChanged
    {
        private DateTime _date;
        private int _month;
        private int _day;
        private string _dayOfWeek;
        private SolidColorBrush _color = new SolidColorBrush(Brush.Transparent.Color);
        private double _opacity = 0;
        private Color _textBrush = Microsoft.Maui.Graphics.Color.FromArgb("C4C4C4");

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public int Month
        {
            get { return _month; }
            set
            {
                _month = value;
                OnPropertyChanged("Month");
            }
        }

        public int Day
        {
            get { return _day; }
            set
            {
                _day = value;
                OnPropertyChanged("Day");
            }
        }

        public string DayOfWeek
        {
            get { return _dayOfWeek; }
            set
            {
                _dayOfWeek = value;
                OnPropertyChanged("DayOfWeek");
            }
        }

        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public Color TextBrush
        {
            get { return _textBrush; }
            set
            {
                _textBrush = value;
                OnPropertyChanged("TextBrush");
            }
        }

        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                OnPropertyChanged("Opacity");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
