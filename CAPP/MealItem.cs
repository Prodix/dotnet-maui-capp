using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public class MealItem : INotifyPropertyChanged
    {
        private int _id;
        private int _mealId;
        private int? _recipeId;
        private int? _productId;
        private int _weight;
        private string _name = "Empty";
        private double _calorie = 0;

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public int Meal_id
        {
            get { return _mealId; }
            set
            {
                _mealId = value;
                OnPropertyChanged("Meal_id");
            }
        }
        public int? Recipe_id
        {
            get { return _recipeId; }
            set
            {
                _recipeId = value;
                OnPropertyChanged("Recipe_id");
            }
        }
        public int? Product_id
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChanged("Product_id");
            }
        }
        public int Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }
        [Ignore]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        } 
        [Ignore]
        public double Calorie
        {
            get { return _calorie; }
            set
            {
                _calorie = value;
                OnPropertyChanged("Calorie");
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
