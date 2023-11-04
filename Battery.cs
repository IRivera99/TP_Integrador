using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Integrador
{
    class Battery
    {
        int maxMiliAmps;
        int actualMiliAmps;

        public Battery(int maxMiliAmps)
        {
            this.maxMiliAmps = maxMiliAmps;
            actualMiliAmps = maxMiliAmps;
        }

        public bool EnoughBattery(int requestedMiliAmps)
        {
            bool enough = false;

            if (requestedMiliAmps <= actualMiliAmps)
                enough = true;

            return enough;
        }

        public int GetCharge()
        {
            return actualMiliAmps;
        }

        public int GetBatteryPercentage()
        {
            return Convert.ToInt32((actualMiliAmps * 100) / maxMiliAmps);
        }

        public int GetBatteryNeeded()
        {
            return maxMiliAmps - actualMiliAmps;
        }

        public int ConsumeBattery(int mAh)
        {
            if (EnoughBattery(mAh))
                actualMiliAmps -= mAh;

            return actualMiliAmps;
        }

        public int ChargeBattery(int mAh)
        {
            if((mAh + actualMiliAmps) > maxMiliAmps)
            {
                actualMiliAmps = maxMiliAmps;
            }
            else
            {
                actualMiliAmps += mAh;
            }

            return actualMiliAmps;
        }
        
        public void ChargeFullBattery()
        {
            actualMiliAmps = maxMiliAmps;
        }
    }
}
