using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitConverter.Models
{
    public class Unit
    {
        public bool BaseUnit { get; set; }
        public string Name { get; set; }
        public string Sign { get; set; }
        public decimal Ratio { get; set; }
        public decimal Add { get; set; }

        public string ShowName
        {
            get
            {
                return $"{Name} ({Sign})";
            }
        }

    }
}
