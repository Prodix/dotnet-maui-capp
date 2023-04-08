using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPP
{
    internal class AccountData
    {
        public string? username { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int? height { get; set; }
        public int? goal { get; set; }
        public double? currentweight { get; set; }
        public double? wishweight { get; set; }
        public string? birthdate { get; set; }
        public string? gender { get; set; }
        public bool isCheckOnly { get; set; }
    }
}
