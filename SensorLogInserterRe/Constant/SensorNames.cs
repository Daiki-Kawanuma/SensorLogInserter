﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLogInserterRe.Constant
{
    class SensorNames
    {
        public const string At570_2 = "AT570-2";
        public const string At570_3 = "AT570-3";
        public const string At570_4 = "AT570-4";
        public const string Nexus7_2013_3 = "Nexus7(2013)-3";
        public const string Nexus7_2013_4 = "Nexus7(2013)-4";
        public const string Nexus7_2013_5 = "nexus7(2013)-5LTE";

        public static int GetSensorId(string sensorName)
        {
            switch (sensorName)
            {
                case SensorNames.At570_2:
                    return 8;
                case SensorNames.At570_3:
                    return 12;
                // TODO 他のも全部書く
                default:
                    return -1;
            }
        }
    }
}