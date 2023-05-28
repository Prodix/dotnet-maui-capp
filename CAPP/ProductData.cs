using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    public class ProductData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Fat { get; set; }
        public double Carb { get; set; }
        public double Protein { get; set; }
        public double Kcal { get; set; }
        public double Weight { get; set; } = 100;
        [Ignore]
        public string Type { get; set; } = "Product";
    }
}
