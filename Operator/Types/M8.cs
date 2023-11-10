//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class M8: Operator
    {
        public M8(int id, double maxSpeed, LocationTypes location) : base(id, maxSpeed, location, OperatorTypes.M8)
        {
            battery = new Battery(12250);
            maxLoad = 250;
        }

        public override string ToString()
        {
            return "Semi-Humanoide M8 " + base.ToString();
        }

        public override string ToStringStateOnly()
        {
            return "Semi-Humanoide M8 " + base.ToStringStateOnly();
        }
    }
}
