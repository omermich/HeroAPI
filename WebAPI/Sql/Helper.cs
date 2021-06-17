using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;
using System.Data;
using System.Data.SqlClient;
using System;

namespace WebAPI.Sql
{
    public static class Helper
    {
        public const string DataSource = "hero-sql-server-2021.database.windows.net" 
                              , UserID = "omermich"            
                            , Password = "omich6!!"     
                      , InitialCatalog = "hero-db";

        public static string ConnectionString() {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = DataSource; 
            builder.UserID = UserID;            
            builder.Password = Password;     
            builder.InitialCatalog = InitialCatalog;

            return builder.ConnectionString;
        }

        public const string TrainersDB = "[dbo].[Trainers]"
                            , HeroesDB = "[dbo].[Heroes]";


        public static SortedDictionary<string, SqlDbType> GetParams()
        => new SortedDictionary<string, SqlDbType>()
        {
          {"id", SqlDbType.Int},
          {"name", SqlDbType.VarChar},
          {"guideId", SqlDbType.Int},
          {"startingDate", SqlDbType.VarChar},
          {"dailyTrainCount", SqlDbType.Int},
          {"primaryColor", SqlDbType.VarChar},
          {"secondaryColor", SqlDbType.VarChar},
          {"ability", SqlDbType.Int},
          {"startingPower", SqlDbType.Int},
          {"currentPower", SqlDbType.Int}
        };

        public static String Read(SqlDataReader reader) {
            // i, s, i, s, i, s, s, i, i, i
            return (
              reader.GetInt32(0) + "$"
            + reader.GetString(1) + "$"
            + reader.GetInt32(2) + "$"
            + reader.GetString(3) + "$"
            + reader.GetInt32(4) + "$"
            + reader.GetString(5) + "$"
            + reader.GetString(6) + "$"
            + reader.GetInt32(7) + "$"
            + reader.GetInt32(8) + "$"
            + reader.GetInt32(9) + "$");
        }
    }
}