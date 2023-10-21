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
        int id;        
        int capacidadBateria;
        int mAhActuales;
        bool standby; //true= STANDBY, false=OPERATIVO
        int cargaMaxima;
        int cargaActual;
        double velocidadOptima;
        double velocidadActual;
        string localizacion;

        protected Operador(int id, int capacidadBateria, int mAhActuales, int cargaMaxima, double velocidadOptima, string localizacion)
        {
            this.id = id;
            this.capacidadBateria = capacidadBateria;            
            this.mAhActuales = mAhActuales;
            standby = false;
            this.cargaMaxima = cargaMaxima;
            cargaActual = 0;
            this.velocidadOptima = velocidadOptima;
            velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(this.mAhActuales));
            this.localizacion = localizacion.ToUpper();            
        }

        protected Operador(int id)
        {
            this.id = id;
            capacidadBateria = 0;
            mAhActuales = 0;
            cargaMaxima = 0;
            cargaActual = 0;
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

            velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(this.mAhActuales));
        }

        protected void CargarBatería(int mAhAñadidos)
        {
            if ((mAhAñadidos + mAhActuales) > capacidadBateria)
                mAhActuales = capacidadBateria;
            else
                mAhActuales += mAhAñadidos;

            velocidadActual = CalcularVelocidadActual(CalcularNivelBateria(this.mAhActuales));
        }

        protected bool AñadirCargaFisica(int kilos)
        {
            bool añadido = false;

            if (kilos < cargaMaxima && (kilos + cargaActual) < cargaMaxima)
            {
                cargaActual += kilos;
                añadido = true;
            }

            return añadido;
        }

        protected void DescargarCargaFisica(int kilos)
        {
            if(kilos > cargaActual)
                cargaActual = 0;
            else
                cargaActual -= kilos;
        }

        public bool Moverse(string localizacion, int kilometrosARecorrer)
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

            while(kilometrosARecorrer > 0 && nivelBateria > 0)
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
                this.localizacion = localizacion.ToUpper();
                DescargarBateria(mAhConsumidos);
            }                

            return seMovio;
        }

        public bool TransferirBateria(Operador operadorDonatario)
        {
            bool transferenciaRealizada = false;
            int bateriaFaltanteDonatorio = operadorDonatario.capacidadBateria - operadorDonatario.mAhActuales;

            if (localizacion.Equals(operadorDonatario.localizacion) && !standby)
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

            if (localizacion.Equals(operadorDonatario.localizacion) && operadorDonatario.AñadirCargaFisica(cargaActual) && !standby)
            {
                DescargarCargaFisica(cargaActual);
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
                DescargarCargaFisica(cargaActual);
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
                CargarBatería((capacidadBateria - cargaActual));
                vueltaYCarga = true;
            }

            return vueltaYCarga;
        }
    }
}
