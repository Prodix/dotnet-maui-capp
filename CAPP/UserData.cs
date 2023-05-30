using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public class UserData
    {
        public int Mode { get; set; }
        public int HeightValue { get; set; }
        public double WeightValue { get; set; }
        public double IntakeCoefficient { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public double Bmi { get; set; }
        public int CalorieIntake { get; set; }
        public int Carb { get; set; }
        public int Fat { get; set; }
        public int Protein { get; set; }
        public string Theme { get; set; } = "Синяя";
    }
}
