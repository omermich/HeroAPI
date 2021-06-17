using System;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebAPI.Sql
{
    public class InsertDB : ActionOnDB
    {
        private readonly ILogger _logger;
        private string _tableName;
        private SortedDictionary<string, SqlDbType> _params;


        public InsertDB(ILogger logger, string table)
        {
            this._logger = logger;
            this._tableName = table;
            this._params = Helper.GetParams();
        }

        protected override void Log(string msg) {
            _logger.LogInformation(msg);
        }

        public void Run(object obj) {
            
            using(SqlConnection connection = new SqlConnection(Helper.ConnectionString()))
            {
                connection.Open();
                System.Reflection.PropertyInfo pi;

                string insertInto = $"INSERT INTO {_tableName} (";
                string valuesStr = "VALUES (";
                int i = 0;
                foreach (string paramKey in _params.Keys)
                {
                    insertInto += (paramKey + ",");
                    pi = obj.GetType().GetProperty(paramKey);
                    if (pi.ToString().Contains("String")) {
                        valuesStr += ("'" + pi.GetValue(obj, null).ToString() + "',");
                    }
                    else
                        valuesStr += (pi.GetValue(obj, null).ToString() + ",");
                    i++;
                }
                insertInto += "\b) ";
                valuesStr += "\b);";

                string sql = insertInto + valuesStr;
                Log(sql);
                using(SqlCommand cmd = new SqlCommand(sql,connection)) 
                {
                    foreach (KeyValuePair<string, SqlDbType> entry in _params) {
                        cmd.Parameters.Add(entry.Key, entry.Value);
                    }
                    cmd.CommandType = CommandType.Text;
                    Log("text: " + cmd.CommandText.ToString());
                    cmd.ExecuteNonQuery(); 
                }
            }
        }

        public void Parse() {

        }
    }
}