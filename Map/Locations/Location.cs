using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador.Map.Locations
{
    abstract class Location
    {
        protected string name;
        protected LocationTypes type;
        protected int coordX;
        protected int coordY;

        protected Location(string name, int coordX, int coordY)
        {
            this.name = name;
            this.coordX = coordX;
            this.coordY = coordY;
        }

        public string GetName()
        {
            return name;
        }

        public (int, int) GetCoords()
        {
            return (coordX, coordY);
        }

        public LocationTypes GetLocationType()
        {
            return type;
        }
    }
}
