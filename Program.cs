//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Integrador.Territory;
using TP_Integrador.Territory.Locations;
using TP_Integrador.Operators.Types;

namespace TP_Integrador
{
    class Program
    {
        static void Main(string[] args)
        {
            SaveSystem saveSystem = SaveSystem.GetInstance();
            Map map = saveSystem.LoadMap();
            List<Quarter> quarters = map.GetQuarterLocations();
            foreach (Quarter quarter in quarters)
            {
                saveSystem.LoadOperators(quarter);
            }        
            map.PrintMap();
            Console.WriteLine("Terminado");
            Console.ReadLine();
        }        
    }
}
