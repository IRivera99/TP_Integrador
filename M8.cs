using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class M8: Operator
    {
        int legs;

        readonly Random random = new Random();

        public M8(int id, string localization, double maxSpeed) : base(id, localization, maxSpeed)
        {
            legs = 2;
            battery = new Battery(12250);
            maxLoad = 250;
            maxSpeed = random.Next(10, 45);
        }

        public override string ToString()
        {
            return "Semi-Humanoide M8 " + base.ToString();
        }
    }
}
