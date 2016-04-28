﻿using SensorLogInserterRe.Calculators.CalculatorComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLogInserterRe.Calculators
{
    static class ClimbingResistanceCalculator
    {
        public static double CalcClimbingResistanceForce(double vehicleMass, double theta)//登坂抵抗力
        {
            return vehicleMass * Math.Sin(theta) * Constants.GravityResistanceCoefficient;
        }
        public static double CalcClimbingResistancePower(double vehicleMass, double theta, double vehicleSpeed)//登坂抵抗による損失エネルギー,kWh/s
        {
            return CalcClimbingResistanceForce(vehicleMass, theta) * vehicleSpeed / 3600 / 1000;
        }
    }
}