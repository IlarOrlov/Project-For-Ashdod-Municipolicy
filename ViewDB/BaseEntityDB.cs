using Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ViewDB
{
    public abstract class BaseEntityDB
    {
        private int id;                                  // the ID number of the object in the Database table
        private static string connectionString = null;   // the connection string for the database
        protected SqlConnection connection;               // the connection between the database and the program
        protected SqlCommand command;                     // the SQL command
        protected SqlDataReader reader;                   // the reader who reads from the database

        protected abstract BaseEntity NewEntity();

        public BaseEntityDB()
        {
            connection = GetConnection();
            command = new SqlCommand();
            command.Connection = connection;
        }

        public static SqlConnection GetConnection()
        {
            if (connectionString == null)
            {
                connectionString = "Server=tcp:ashdodarchievesqlserver.database.windows.net,1433;Initial Catalog=ProjectDB;Persist Security Info=False;User ID=ArchievesProjectServer;Password=ArCh13vEsS3Rver;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            }

            return new SqlConnection(connectionString);
        }

        protected virtual void CreateModel(BaseEntity entity)
        {
            if (entity != null)
            {
                try
                {
                    entity.ID = int.Parse(reader["ID"].ToString());
                }
                catch
                {
                    Console.WriteLine("No ID in DB.");
                }
            }
        }

        protected int SelectCountResult(string sqlRequest, Dictionary<string, object> parameters)
        {
            int result = 0;   // the result of the SQL request from the database

            try
            {
                command.CommandText = sqlRequest;
                AddParametersToCommand(parameters);
                connection.Open();
                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return result;
        }

        protected int SendSqlCommand(string sqlRequest, Dictionary<string, object> parameters)
        {
            int records = 0;

            try
            {
                command.CommandText = sqlRequest;
                AddParametersToCommand(parameters);
                connection.Open();
                records = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return records;
        }

        protected virtual List<BaseEntity> Select(string sqlCommandTxt, Dictionary<string, object> parameters)
        {
            List<BaseEntity> list = new List<BaseEntity>();

            try
            {
                connection.Open();
                command.CommandText = sqlCommandTxt;
                AddParametersToCommand(parameters);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    CreateModel(entity);
                    list.Add(entity);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return list;
        }

        /// <summary>
        /// Safely replaces parameter placeholders in the SQL command text with sanitized values,
        /// adding N for Unicode strings (e.g., Hebrew).
        /// </summary>
        /// <param name="parameters">A dictionary of parameter names and values.</param>
        private void AddParametersToCommand(Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return;

            foreach (var param in parameters)
            {
                string key = param.Key;
                object value = param.Value;
                string safeValue;

                if (value == null)
                {
                    safeValue = "NULL";
                }
                else if (value is string strVal)
                {
                    // Escape single quotes by doubling them
                    string escaped = strVal.Replace("'", "''");

                    // Detect Hebrew or any Unicode and add N prefix if needed
                    bool containsHebrew = System.Text.RegularExpressions.Regex.IsMatch(escaped, @"[\u0590-\u05FF]");
                    safeValue = containsHebrew ? $"N'{escaped}'" : $"'{escaped}'";
                }
                else if (value is DateTime dtVal)
                {
                    // Format datetime safely
                    safeValue = $"'{dtVal:yyyy-MM-dd HH:mm:ss}'";
                }
                else if (value is bool boolVal)
                {
                    safeValue = boolVal ? "1" : "0";
                }
                else
                {
                    // Assume number
                    safeValue = value.ToString();
                }

                // Replace the @key in the command text
                command.CommandText = command.CommandText.Replace(key, safeValue);
            }
        }
    }
}