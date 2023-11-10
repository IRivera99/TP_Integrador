using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador.Map.Locations
{
    class Common : Location
    {
        public Common(string name, int coordX, int coordY) : base(name, coordX, coordY)
        {
            type = LocationTypes.Common;
        }        
    }
}
