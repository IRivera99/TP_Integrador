//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class UAV : Operator
    { 
        public UAV(int id, double maxSpeed, Locations location) : base(id, maxSpeed, location, OperatorTypes.UAV)
        {
            battery = new Battery(4000);
            maxLoad = 5;
        }

        public override string ToString()
        {
            return "Dron UAV " + base.ToString();
        }

        public override string ToStringStateOnly()
        {
            return "Dron UAV " + base.ToStringStateOnly();
        }
    }
}
