using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class UAV : Operator
    {
        int propellers;

        readonly Random random = new Random();

        public UAV(int id, int propQuant, string localization, double maxSpeed) : base(id, localization, maxSpeed)
        {
            propellers = propQuant;
            battery = new Battery(4000);
            maxLoad = 5;
            maxSpeed = random.Next(30, 60);
        }

        public UAV(int id, string localization, double maxSpeed) : base(id, localization, maxSpeed)
        {
            propellers = random.Next(3,6);
            battery = new Battery(4000);
            maxLoad = 5;
            maxSpeed = random.Next(30, 60);
        }

        public override string ToString()
        {
            return "Dron UAV " + base.ToString();
        }
    }
}
