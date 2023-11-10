//Creado por Ignacio Rivera
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class Battery
    {
        int maxMilliAmps;
        int actualMilliAmps;

        public Battery(int maxMilliAmps)
        {
            this.maxMilliAmps = maxMilliAmps;
            actualMilliAmps = maxMilliAmps;
        }

        public bool EnoughBattery(int requestedMilliAmps)
        {
            bool enough = false;

            if (requestedMilliAmps <= actualMilliAmps)
                enough = true;

            return enough;
        }

        public int GetCharge()
        {
            return actualMilliAmps;
        }

        public int GetBatteryPercentage()
        {
            return Convert.ToInt32((actualMilliAmps * 100) / maxMilliAmps);
        }

        public int GetBatteryNeeded()
        {
            return maxMilliAmps - actualMilliAmps;
        }

        public bool ConsumeBattery(int mAh)
        {
            bool canConsume = EnoughBattery(mAh);

            if (canConsume)
            {
                actualMilliAmps -= mAh;
            }                

            return canConsume;
        }

        public int ChargeBattery(int mAh)
        {
            if((mAh + actualMilliAmps) > maxMilliAmps)
            {
                actualMilliAmps = maxMilliAmps;
            }
            else
            {
                actualMilliAmps += mAh;
            }

            return actualMilliAmps;
        }
        
        public void ChargeFullBattery()
        {
            actualMilliAmps = maxMilliAmps;
        }
    }
}
