﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SensorLogInserterRe.Models;

namespace SensorLogInserterRe.Daos
{
    class TripsDao
    {
        private static readonly string TableName = "trips";
        public static readonly string ColumnTripId = "trip_id";
        public static readonly string ColumnDriverId = "driver_id";
        public static readonly string ColumnCarId = "car_id";
        public static readonly string ColumnSensorId = "sensor_id";
        public static readonly string ColumnStartTime = "start_time";
        public static readonly string ColumnEndTime = "end_time";
        public static readonly string ColumnStartLatitude = "start_latitude";
        public static readonly string ColumnStartLongitude = "start_longitude";
        public static readonly string ColumnEndLatitude = "end_latitude";
        public static readonly string ColumnEndLongitude = "end_longitude";
        public static readonly string ColumnConsumedEnergy = "consumed_energy";
        public static readonly string ColumnTripDirection = "trip_direction";
        public static readonly string ColumnValidation = "validation";

        public static void Insert(DataTable dataTable)
        {
            DatabaseAccesser.Insert(TableName, dataTable);
        }

        public static DataTable Get()
        {
            string query = "SELECT * FROM " + TableName;

            return DatabaseAccesser.GetResult(query);
        }

        public static DataTable Get(InsertDatum datum)
        {
            var query = new StringBuilder();

            query.AppendLine("SELECT *");
            query.AppendLine($"FROM {TripsDao.TableName}");
            query.AppendLine($"WHERE {TripsDao.ColumnDriverId} = {datum.DriverId}");
            query.AppendLine($"AND {TripsDao.ColumnCarId} = {datum.CarId}");
            query.AppendLine($"AND {TripsDao.ColumnSensorId} = {datum.SensorId}");
            query.AppendLine($"AND {TripsDao.ColumnStartTime} >= '{datum.StartTime}'");
            query.AppendLine($"AND {TripsDao.ColumnEndTime} <= '{datum.EndTime}'");
            query.AppendLine($"ORDER BY {ColumnStartTime}");

            return DatabaseAccesser.GetResult(query.ToString());
        }

        public static int GetMaxTripId()
        {
            string query = $"SELECT MAX({ColumnTripId}) AS max_id FROM {TableName}";

            return DatabaseAccesser.GetResult(query).Rows[0].Field<int?>("max_id") ?? 0;
        }

        public static bool IsExsistsTrip(DataRow row)
        {
            var query = new StringBuilder();
            query.AppendLine($"SELECT *");
            query.AppendLine($"FROM {TableName}");
            query.AppendLine($"WHERE {ColumnDriverId} = {row.Field<int>(ColumnDriverId)}");
            query.AppendLine($"  AND {ColumnCarId} = {row.Field<int>(ColumnCarId)}");
            query.AppendLine($"  AND {ColumnSensorId} = {row.Field<int>(ColumnSensorId)}");
            // SQL ServerではDateTime(1)型のミリ秒を切り上げするので±１秒の間をもうける
            query.AppendLine($"  AND {ColumnStartTime} > '{row.Field<DateTime>(ColumnStartTime).AddSeconds(-1)}'");
            query.AppendLine($"  AND {ColumnEndTime} < '{row.Field<DateTime>(ColumnEndTime).AddSeconds(1)}'");

            return DatabaseAccesser.GetResult(query.ToString()).AsEnumerable().Count() != 0;
        }

    }
}
