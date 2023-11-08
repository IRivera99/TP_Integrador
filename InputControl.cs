//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    abstract class InputControl
    {
        public static int ReadIntOnly()
        {
            int intReturn = 0;            
            bool converted = false;

            while (!converted)
            {
                string input = Console.ReadLine();
                try
                {
                    intReturn = Convert.ToInt32(input);
                    converted = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error! Por favor ingrese solo números.");
                    Console.ReadKey();
                }                
            }
            return intReturn;
        }
    }
}
