//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    abstract class Operador
    {
        protected int id;
        protected int capacidadBateria;
        protected int mAhActuales;
        protected bool standby; //true= STANDBY, false=OPERATIVO
        protected int cargaPesoMaximo; //Carga de peso, no de batería
        protected int cargaPesoActual; //Carga de peso, no de batería
        protected double velocidadOptima;
        protected double velocidadActual;
        protected string localizacion;

        protected Operador(int id, int capacidadBateria, int mAhActuales, int cargaPesoMaxima, double velocidadOptima, string localizacion)
        {
            this.id = id;
            this.capacidadBateria = capacidadBateria;            
            this.mAhActuales = mAhActuales;
            standby = false;
            this.cargaPesoMaximo = cargaPesoMaxima;
            cargaPesoActual = 0;
            this.velocidadOptima = velocidadOptima;
            velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(this.mAhActuales));
            this.localizacion = localizacion.ToUpper();            
        }

        protected Operador(int id)
        {
            this.id = id;
            capacidadBateria = 0;
            mAhActuales = 0;
            cargaPesoMaximo = 0;
            cargaPesoActual = 0;
            velocidadOptima = 0;
            velocidadActual = CalcularVelocidadActual(mAhActuales);
            localizacion = string.Empty;
        }

        protected double CalcularVelocidadActual(int porcentajeBateria)
        {
            double velocidad = velocidadOptima;

            for (int i = (porcentajeBateria - 100); i <= -10; i += 10)
            {
                velocidad -= velocidadOptima * 0.05;
            }

            return velocidad;
        }    
        
        protected int CalcularNivelBateria(int mAh)
        {
            return Convert.ToInt32((mAh * 100) / capacidadBateria); 
        }

        protected void DescargarBateria(int mAhConsumidos)
        {
            if (mAhConsumidos > mAhActuales)
                mAhActuales = 0;
            else
                mAhActuales -= mAhConsumidos;

            velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(mAhActuales));
        }

        protected void CargarBatería(int mAhAñadidos)
        {
            if ((mAhAñadidos + mAhActuales) > capacidadBateria)
                mAhActuales = capacidadBateria;
            else
                mAhActuales += mAhAñadidos;

            velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(mAhActuales));
        }

        protected bool AñadirCargaFisica(int kilos)
        {
            bool añadido = false;

            if (kilos < cargaPesoMaximo && (kilos + cargaPesoActual) < cargaPesoMaximo)
            {
                cargaPesoActual += kilos;
                añadido = true;
            }

            return añadido;
        }

        protected void DescargarCargaFisica(int kilos)
        {
            if(kilos > cargaPesoActual)
                cargaPesoActual = 0;
            else
                cargaPesoActual -= kilos;
        }

        public bool Moverse(string localizacion, int kilometrosARecorrer)
        {
            bool seMovio = false;
            int mAhConsumidos = 0;
            int nivelBateria = CalcularNivelBateria(mAhActuales);
            double velocidad = velocidadActual;
            double tiempoUso = Math.Round(((capacidadBateria / 100) * 0.001), 2); //Calcula cuanto tiempo de uso tengo con 1% de batería         

            if (localizacion.ToUpper().Equals("CUARTEL"))
            {
                seMovio = true;
                kilometrosARecorrer = 0;
            }

            while(!standby && kilometrosARecorrer > 0 && nivelBateria > 0)
            {
                kilometrosARecorrer -= Convert.ToInt32(tiempoUso * velocidad); //Le resta el resultado de calcular cuantos kilometros recorri con 1% de batería
                nivelBateria--; //Resta 1% de batería
                mAhConsumidos += capacidadBateria / 100; //Agrega la cantidad de mAh correspondientes a 1% a la cantidad de mAh consumidos
                velocidad = CalcularVelocidadActual(nivelBateria); //Calcula la velocidad correspondiente al nivel de batería

                if (nivelBateria > 0 && kilometrosARecorrer <= 0)
                    seMovio = true;
            }

            if (seMovio)
            {
                this.localizacion = localizacion.ToUpper();
                DescargarBateria(mAhConsumidos);
            }                

            return seMovio;
        }

        public bool TransferirBateria(Operador operadorDonatario)
        {
            bool transferenciaRealizada = false;
            int bateriaFaltanteDonatorio = operadorDonatario.capacidadBateria - operadorDonatario.mAhActuales;

            if (!standby && localizacion.Equals(operadorDonatario.localizacion))
            {
                operadorDonatario.CargarBatería(mAhActuales);
                DescargarBateria(bateriaFaltanteDonatorio);
                transferenciaRealizada = true;                               
            }            

            return transferenciaRealizada;
        }

        public bool TransferirCarga(Operador operadorDonatario)
        {
            bool transferenciaRealizada = false;

            if (!standby && localizacion.Equals(operadorDonatario.localizacion) && operadorDonatario.AñadirCargaFisica(cargaPesoActual))
            {
                DescargarCargaFisica(cargaPesoActual);
                transferenciaRealizada = true;
            }

            return transferenciaRealizada;
        }

        public bool VolverCuartelDescargaFisica()
        {
            bool vueltaYDescarga = false;

            if (!standby)
            {
                Moverse("cuartel", 0);
                DescargarCargaFisica(cargaPesoActual);
                vueltaYDescarga = true;
            }

            return vueltaYDescarga;
        }

        public bool VolverCuartelCargarBateria()
        {
            bool vueltaYCarga = false;

            if (!standby)
            {
                Moverse("cuartel", 0);
                CargarBatería((capacidadBateria - mAhActuales));
                vueltaYCarga = true;
            }

            return vueltaYCarga;
        }
    }
}
