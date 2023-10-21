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
            //TEST!
            int mAhActuales = 1500;
            int capacidadBateria = 6500;            
            double velocidadOptima = 50;
            double velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(mAhActuales));

            double CalcularVelocidadActual(int porcentajeBateria)
            {
                double velocidad = velocidadOptima;

                for (int i = (porcentajeBateria - 100); i <= -10; i += 10)
                {
                    velocidad -= velocidadOptima * 0.05;
                }

                return velocidad;
            }

            int CalcularNivelBateria(int mAh)
            {
                return Convert.ToInt32((mAh * 100) / capacidadBateria);
            }

            void DescargarBateria(int mAhConsumidos)
            {
                if (mAhConsumidos > mAhActuales)
                    mAhActuales = 0;
                else
                    mAhActuales -= mAhConsumidos;

                velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(mAhActuales));
            }

            bool Moverse(string localizacion, int kilometrosARecorrer)
            {
                bool seMovio = false;
                int mAhConsumidos = 0;
                int nivelBateria = CalcularNivelBateria(mAhActuales);
                double velocidad = velocidadActual;                
                int mAhBajaVelocidad = Convert.ToInt32(capacidadBateria / 100); //Cada cuantos mAh se me baja la velocidad
                double tiempoBajaVelocidad = Math.Round((mAhBajaVelocidad * 0.001), 2); //Cada cuanto tiempo me baja la velocidad

                if (localizacion.ToUpper().Equals("CUARTEL"))
                {
                    seMovio = true;
                    kilometrosARecorrer = 0;
                }

                while (kilometrosARecorrer > 0 && nivelBateria > 0)
                {
                    kilometrosARecorrer -= Convert.ToInt32(tiempoBajaVelocidad * velocidad);
                    nivelBateria--;
                    mAhConsumidos += capacidadBateria / 100;
                    velocidad = CalcularVelocidadActual(nivelBateria);

                    if (nivelBateria > 0 && kilometrosARecorrer <= 0)
                        seMovio = true;
                }

                if (seMovio)
                {
                    //this.localizacion = localizacion.ToUpper();
                    DescargarBateria(mAhConsumidos);
                }

                return seMovio;
            }
            Console.WriteLine(Moverse("CACA", 45));
            Console.WriteLine($"{mAhActuales}");
            Console.WriteLine($"{velocidadActual}");
            Console.WriteLine(Moverse("CACA", 44));
            Console.WriteLine($"{mAhActuales}");
            Console.WriteLine($"{velocidadActual}");
            //int mAhBajaVelocidad = Convert.ToInt32((10 * 6500) / 100);
            //double tiempoBajaVelocidad = Math.Round((mAhBajaVelocidad*0.001), 2);
            //int distRecorridaAntesBaja = Convert.ToInt32(tiempoBajaVelocidad * 35);
            //Console.WriteLine($"cada cuanto mha me baja velocidad: {mAhBajaVelocidad}mAh");
            //Console.WriteLine($"cada cuanto tiempo me baja velocidad: {tiempoBajaVelocidad}hrs");
            //Console.WriteLine($"cuantos km hago antes que me baje velocidad: {distRecorridaAntesBaja}kms");
            Console.ReadLine();
        }
    }
}
