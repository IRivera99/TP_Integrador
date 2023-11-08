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
        protected Locations location;
        protected OperatorTypes type;

        protected Operator(int id, int batteryCapacity, int maxLoad, double maxSpeed, Locations location, OperatorTypes type)
        {
            this.id = id;
            battery = new Battery(batteryCapacity);            
            standBy = false;
            this.maxLoad = maxLoad;
            actualLoad = 0;
            this.maxSpeed = maxSpeed;
            this.location = location;  
            this.type = type;
        }

        protected Operator(int id, double maxSpeed, Locations location, OperatorTypes type)
        {
            this.id = id;
            standBy = false;
            actualLoad = 0;
            this.maxSpeed = maxSpeed;
            this.location = location;
            this.type = type;
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

        public Locations GetLocation()
        {
            return location;
        }

        public int GetId()
        {
            return id;
        }

        public bool IsStandBy()
        {
            return standBy;
        }

        public void ChangeState(bool standBy)
        {
            this.standBy = standBy;
        }

        public bool Travel(Locations location)
        {
            bool done = false;
            double speed = CalculateActualSpeed();
            int kmToTravel = (int)location;
            int milliAmpsToConsume = Convert.ToInt32((kmToTravel * 1000) / speed);

            if (location.Equals(this.location))
            {
                done = true;
            }

            if(!standBy && !done && battery.ConsumeBattery(milliAmpsToConsume))
            {
                done = true;
                this.location = location;
            }

            return done;
        }

        public bool TransferBattery(Operator opReciever)
        {
            bool done = false;
            int batteryNeeded = opReciever.battery.GetBatteryNeeded();

            if (!standBy && location.Equals(opReciever.location))
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

            if (!standBy && location.Equals(opReciever.location) && opReciever.AddLoad(actualLoad))
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
                Travel(Locations.Cuartel);
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
            string state = "Operativo";
            if (standBy)
            {
                state = "Stand By";
            }
            return "ID: " + id + 
                   "\nCarga restante: " + battery.GetBatteryPercentage() + "% (" + battery.GetCharge() + " mAh)" +
                   "\nEstado: " + state +
                   "\nPeso: " + actualLoad + " / " + maxLoad + " kg" +
                   "\nVelocidad: " + CalculateActualSpeed() + " km/h" +
                   "\nLocalización: " + location.ToString().Replace('_', ' ');
        }

        public virtual string ToStringStateOnly()
        {
            string state = "Operativo";
            if (standBy)
            {
                state = "Stand By";
            }
            return "ID: " + id +
                   "\nEstado: " + state;
        }

        public string ToStringIdOnly()
        {
            return "ID: " + id;
        }
    }
}
