//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class Program
    {
        static void Main(string[] args)
        {
            Quarter cuartel = new Quarter("Cuartel");

            AddOperatorsToQuarter(cuartel, 10);

            

            Console.ReadLine();
        }

        static void ListOperators(Quarter quarter)
        {
            Console.WriteLine("====================================");
            foreach (Operator op in quarter.GetOperators())
            {
                Console.WriteLine(op.ToString() +
                    "\n====================================");

            }
        }
        
        static void AddOperatorsToQuarter(Quarter quarter, int amount)
        {
            Random random = new Random();
            string[] localizations = { "CORDOBA", "CUARTEL", "BUENOS AIRES", "SAN JUAN", "MENDOZA", "CHACO", "TUCUMAN" };

            for (int i = 0; i < amount; i++)
            {
                int id = random.Next(0, amount + 100);
                string localization = localizations[random.Next(0, localizations.Length - 1)];

                switch (random.Next(0,3))
                {
                    case 0:
                        {
                            UAV uav = new UAV(id, localization, random.Next(30, 60));
                            quarter.AddOperator(uav);
                        }                        
                        break;
                    case 1:
                        {
                            K9 k9 = new K9(id, localization, random.Next(20, 50));
                            quarter.AddOperator(k9);
                        }                        
                        break;
                    case 2:
                        {
                            M8 m8 = new M8(id, localization, random.Next(15, 40));
                            quarter.AddOperator(m8);
                        }
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
