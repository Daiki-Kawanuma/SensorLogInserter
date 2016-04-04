﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLogInserterRe.Calculators
{
    static class RollingResistanceCalculator
    {
        public static double CalcRollingResistanceForce(double myu, double vehicleMass, double theta)//転がり抵抗力
        {
            return myu * vehicleMass * Math.Cos(theta)* 9.80665;
        }
        public static double CalcRollingResistancePower(double myu, double vehicleMass, double theta, double vehicleSpeed)//転がり抵抗による損失エネルギー,kWh/s
        {
            return CalcRollingResistanceForce(myu, vehicleMass, theta) * vehicleSpeed / 1000 / 3600;
        }
    }
}