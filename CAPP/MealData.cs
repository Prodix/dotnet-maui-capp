using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public class MealData : INotifyPropertyChanged
    {
        private int _id;
        private string _date;
        private string _type;
        private double _kcal = 0;
        private double _carb = 0;
        private double _protein = 0;
        private double _fat = 0;
        private ObservableCollection<MealItem> _items = new ObservableCollection<MealItem>();

        [PrimaryKey]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        public double Kcal
        {
            get { return _kcal; }
            set
            {
                _kcal = value;
                OnPropertyChanged("Kcal");
            }
        }
        public double Carb
        {
            get { return _carb; }
            set
            {
                _carb = value;
                OnPropertyChanged("Carb");
            }
        }
        public double Protein
        {
            get { return _protein; }
            set
            {
                _protein = value;
                OnPropertyChanged("Protein");
            }
        }
        public double Fat
        {
            get { return _fat; }
            set
            {
                _fat = value;
                OnPropertyChanged("Fat");
            }
        }
        [Ignore]
        public ObservableCollection<MealItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
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
