using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Integrador.Map.Locations;

namespace TP_Integrador.Map
{
    internal class Map
    {
        List<Location> map;
        const int maxQarters = 3;
        const int minQarters = 1;

        public Map(int maxX, int maxY) 
        {
            map = new List<Location>();
            MapBuilder(maxX, maxY, map);            
        }

        private void MapBuilder(int maxX, int maxY, List<Location> map)
        {
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    //map.Add(new Location(i, j));
                }
            }
        }

        private bool MapChecker(List<Location> map)
        {
            bool ok = true;
            return ok;
        }

        //private LocationTypes SelectRandomLocationType()
    }
}
