using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class K9 : Operator
    {
        int legs;

        public K9(int id, string localization, double maxSpeed) : base(id, localization, maxSpeed)
        {
            legs = 4;
            battery = new Battery(6500);
            maxLoad = 40;
        }

        public override string ToString()
        {
            return "Cuadrupedo K9 " + base.ToString();
        }
    }
}
