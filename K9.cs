//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class K9 : Operator
    {
        public K9(int id, double maxSpeed, Locations location) : base(id, maxSpeed, location)
        {
            battery = new Battery(6500);
            maxLoad = 40;
        }

        public override string ToString()
        {
            return "Cuadrupedo K9 " + base.ToString();
        }

        public override string ToStringStateOnly()
        {
            return "Cuadrupedo K9 " + base.ToStringStateOnly();
        }
    }
}
