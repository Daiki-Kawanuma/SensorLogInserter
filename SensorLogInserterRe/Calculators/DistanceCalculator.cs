﻿using System;
using System.Device.Location;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorLogInserterRe.Calculators
{
    class DistanceCalculator
    {
        static double a=6378137, b=6356752.314245;//a:長半径，b:短半径
        public static float CalcDistance(GeoCoordinate geoFirst, GeoCoordinate geoSecond)
        {
            double result=calcHubenyFormula(geoFirst,geoSecond);//ヒュベニの公式で距離を計算
            return (float)result;
        }
        //ヒュベニの公式
        private static double calcHubenyFormula(GeoCoordinate geoFirst,GeoCoordinate geoSecond)
        {
            double differenceLongitude, differenceLattitude,M,N,cosMyuY,yFirst,ySecond,distance;
            differenceLattitude = conversionDegreeToRadian(geoFirst.Latitude - geoSecond.Latitude);//緯度の差
            differenceLongitude = conversionDegreeToRadian(geoFirst.Longitude - geoSecond.Longitude);//経度の差
            M = calcMeridianCurvature(geoFirst.Latitude, geoSecond.Latitude);//子午線曲率半径
            N = calcPrimeVerticalCircleCurvature(geoFirst.Latitude, geoSecond.Latitude);//卯酉線曲率半径
            yFirst = conversionDegreeToRadian(geoFirst.Latitude);//緯度をラジアンに変換
            ySecond = conversionDegreeToRadian(geoSecond.Latitude);//緯度をラジアンに変換
            cosMyuY = Math.Cos((yFirst + ySecond) / 2);//緯度の平均値のコサイン
            distance = Math.Sqrt(Math.Pow(differenceLattitude * M, 2) + Math.Pow(differenceLongitude * N * cosMyuY, 2));//距離の計算
                return distance;
        }
        //子午線曲率半径
        private static double calcMeridianCurvature(double lattitudeFirst,double lattitudeSecond)
        {
            double M,W,e2;
            W = calcW(lattitudeFirst, lattitudeSecond);//Wを計算
            e2 = calcE2();//第一離心率の２乗を計算
            M = a * (1 - e2) / Math.Pow(W, 3);//子午線曲率半径を計算
            return M;
        }
        //卯酉線曲率半径
        private static double calcPrimeVerticalCircleCurvature(double lattitudeFirst,double lattitudeSecond)
        {
            double N, W;
            W = calcW(lattitudeFirst, lattitudeSecond);//Wを計算
            N = a / W;//卯酉線曲率半径を計算
            return N;
        }
        //ヒュベニの公式のWを導出
        private static double calcW(double lattitudeFirst,double lattitudeSecond)
        {
            double W,yFirst,ySecond,sinMyuY2,e2;
            yFirst = conversionDegreeToRadian(lattitudeFirst);//緯度をラジアンに変換
            ySecond = conversionDegreeToRadian(lattitudeSecond);//緯度をラジアンに変換
            sinMyuY2 =Math.Pow(Math.Sin( (yFirst + ySecond) / 2),2);//緯度の平均値のサインの２乗
            e2 = calcE2(); //第一離心率の２乗
            W = Math.Sqrt(1-e2*sinMyuY2);//Wを計算
            return W;
        }
        //第一離心率の２乗
        private static double calcE2()
        {
            return (Math.Pow(a, 2)-Math.Pow(b,2))/Math.Pow(a,2);
        }
        //degreeをradianに変換
        private static double conversionDegreeToRadian(double degree)
        {
            double radian;
            radian = degree * Math.PI / 180;
            return radian;
        }
        


    }
}
