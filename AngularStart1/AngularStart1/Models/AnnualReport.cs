using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularStart1.Models
{
    public class AnnualReport
    {
        public AnnualReport()
        {
            YearA = DateTime.Now.AddYears(0).Year.ToString();
            YearB = DateTime.Now.AddYears(-1).Year.ToString();
            YearC = DateTime.Now.AddYears(-2).Year.ToString();
        }
        public string YearA { get; set; }
        public string YearB { get; set; }
        public string YearC { get; set; }

        public int CountA { get; set; }
        public int CountB { get; set; }
        public int CountC { get; set; }

        public int UsegA { get; set; }
        public int UsegB { get; set; }
        public int UsegC { get; set; }
    }
}