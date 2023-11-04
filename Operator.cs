//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    abstract class Operator
    {
        protected int id;
        protected Battery battery;
        protected bool standBy; //true= STANDBY, false = OPERATIVE
        protected int maxLoad; 
        protected int actualLoad;
        protected double maxSpeed;
        protected string localization;

        protected Operator(int id, int batteryCapacity, int maxLoad, double maxSpeed, string localization)
        {
            this.id = id;
            battery = new Battery(batteryCapacity);            
            standBy = false;
            this.maxLoad = maxLoad;
            actualLoad = 0;
            this.maxSpeed = maxSpeed;
            this.localization = localization.ToUpper();            
        }

        protected Operator(int id, string localization, double maxSpeed)
        {
            this.id = id;
            standBy = false;
            actualLoad = 0;
            this.maxSpeed = maxSpeed;
            this.localization = localization.ToUpper();
        }

        protected double CalculateActualSpeed()
        {
            double speed = maxSpeed;
            int loop = (actualLoad * 10) / maxLoad;

            while(loop > 0)
            {
                speed = -maxSpeed * 0.05;
                loop--;
            }

            return speed;
        }

        protected bool AddLoad(int kg)
        {
            bool added = false;

            if (kg < maxLoad && (kg + actualLoad) < maxLoad)
            {
                actualLoad += kg;
                added = true;
            }

            return added;
        }

        protected void SubstractLoad(int kg)
        {
            if (kg > actualLoad)
                actualLoad = 0;
            else
                actualLoad -= kg;
        }

        public string GetLocalization()
        {
            return localization;
        }

        public int GetId()
        {
            return id;
        }

        public bool Travel(string localization, int kmToTravel)
        {
            bool done = false;
            double speed = CalculateActualSpeed();
            int milliAmpsToConsume = Convert.ToInt32((kmToTravel * 1000) / speed);
            

            if (localization.ToUpper().Equals("CUARTEL"))
            {
                done = true;
            }

            if(!standBy && !done && battery.EnoughBattery(milliAmpsToConsume))
            {
                done = true;
                this.localization = localization.ToUpper();
                battery.ConsumeBattery(milliAmpsToConsume);
            }

            return done;
        }

        public bool TransferBattery(Operator opReciever)
        {
            bool done = false;
            int batteryNeeded = opReciever.battery.GetBatteryNeeded();

            if (!standBy && localization.Equals(opReciever.localization))
            {
                opReciever.battery.ChargeBattery(battery.GetCharge());
                battery.ConsumeBattery(batteryNeeded);
                done = true;
            }

            return done;
        }

        public bool TransferLoad(Operator opReciever)
        {
            bool done = false;

            if (!standBy && localization.Equals(opReciever.localization) && opReciever.AddLoad(actualLoad))
            {
                SubstractLoad(actualLoad);
                done = true;
            }

            return done;
        }

        public void ReturnToQuarter()
        {
            if (!standBy)
            {
                Travel("cuartel", 0);
            }
        }

        public bool ReturnQuarterAndUnload()
        {
            bool done = false;

            if (!standBy)
            {
                ReturnToQuarter();
                SubstractLoad(actualLoad);
                done = true;
            }

            return done;
        }

        public bool ReturnQuarterAndCharge()
        {
            bool done = false;

            if (!standBy)
            {
                ReturnToQuarter();
                battery.ChargeFullBattery();
                done = true;
            }

            return done;
        }

        public override string ToString()
        {
            string state = "Activo";
            if (standBy)
            {
                state = "Stand By";
            }
            return "ID: " + id + 
                   "\nCarga restante: " + battery.GetBatteryPercentage() + "% (" + battery.GetCharge() + " mAh)" +
                   "\nEstado: " + state +
                   "\nPeso: " + actualLoad + " / " + maxLoad + " kg" +
                   "\nVelocidad: " + CalculateActualSpeed() + " km/h" +
                   "\nLocalización: " + localization;
        }
    }
}
