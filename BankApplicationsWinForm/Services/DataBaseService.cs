using BankApplicationsWinForm.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplicationsWinForm.Services
{
    public static class DataBaseService
    {
        // получаем строку подключения
        static string _connectionDefaultStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static string _connectionMasterStr = ConfigurationManager.ConnectionStrings["ConnectionToMaster"].ConnectionString;

        /// <summary>
        /// Создать БД если она отсутствует
        /// </summary>
        public async static Task<bool> CheckCreateDB()
        {
            bool isExistsDB = false, isExistsDBTables = false;

            try
            {
                var db = await ExecSelect($"SELECT * FROM sys.databases", "name = @name", "name", "BankApp", "sys.databases", _connectionMasterStr);

                if (db)
                {
                    Service.LogWrite("БД BankApp найдена!");
                    isExistsDB = true;
                }
                else Service.LogWrite("БД BankApp не сущесвует!");

                if (!isExistsDB)
                {
                    isExistsDB = await CreateDB(_connectionMasterStr);
                }

                if (isExistsDB)
                {
                    var tbUsers = await ExecSelect($"SELECT * FROM tbUsers", "tbUsers") == null ? false : true;
                    var tbFiles = await ExecSelect($"SELECT * FROM tbFiles", "tbFiles") == null ? false : true;
                    isExistsDBTables = tbUsers && tbFiles;

                    if (!isExistsDBTables)
                        isExistsDBTables = await CreateTables();
                }
                else
                {
                    Service.LogWrite("Ошибка создания БД!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Service.LogWrite("Ошибка создания БД или таблиц!" + $"{e.Message.ToString()}");
            }

            return isExistsDB && isExistsDBTables;
        }

        private async static Task<bool> CreateTables()
        {
            SqlConnection connection = new SqlConnection(_connectionDefaultStr);
            string query = Resources.CreateTables;
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                await connection.OpenAsync();
                command.Transaction = connection.BeginTransaction();

                await command.ExecuteNonQueryAsync();
                
                command.Transaction.Commit();
            }
            catch (Exception e)
            {
                Service.LogWrite("Ошибка создания таблиц в БД BankApp - " + e.Message);
                command.Transaction.Rollback();
                return false;
            }
            finally
            {
                connection.Close();
            }

            Service.LogWrite("БД BankApp заполнена таблицами, всё ОК!");
            return true;
        }

        private async static Task<bool> CreateDB(string conStr)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\SQL INSTAL\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\");
            if (!dirInfo.Exists)
                dirInfo.Create();

            using (SqlConnection connection = new SqlConnection(conStr))
            {
                var str = Resources.CheckCreateDB;
                SqlCommand command = new SqlCommand(str, connection);

                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();
            }

            var db = await ExecSelect($"SELECT * FROM sys.databases", "name = @name", "name", "BankApp", "sys.databases", conStr);

            if (db)
            {
                Service.LogWrite("БД BankApp успешно создана");
                return true;
            }

            return false;
        }

        public async static Task<bool> ExecSelect(string sqlSelect, string sqlConditions, string sqlParam, string nameParam, string nameTable, string conStr)
        {
            bool result;
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sqlExpression = sqlSelect + $" WHERE {sqlConditions}";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = null;

                await connection.OpenAsync();

                command.Parameters.AddWithValue(sqlParam, nameParam);   // добавление параметра в коллекцию параметров команды
                reader = await command.ExecuteReaderAsync();
                result = reader.HasRows;
                reader.Close();

                return result;
            }
        }

        /// <summary>
        /// Обновить данные в базе
        /// </summary>
        /// <param name="sqlConditions">условие выборки</param>
        /// <param name="sqlParam">"Login"</param>
        /// <param name="nameParam">"Евгений"</param>
        /// <param name="nameTable">Имя таблицы "tbUsers"</param>
        /// <returns></returns>
        public async static Task<bool> ExecUpdate(string sqlConditions, string sqlParam, string nameParam, string nameTable, string setConditions)
        {
            var dt = await ExecSelect($"SELECT * FROM {nameTable}", sqlConditions, sqlParam, nameParam, nameTable);

            if (dt.Rows.Count != 0)
            {
                string sqlExpression = $"UPDATE {nameTable} {setConditions} WHERE User_ID = {nameParam}";

                using (SqlConnection connection = new SqlConnection(_connectionDefaultStr))
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    int rowAffected = await command.ExecuteNonQueryAsync();

                    if (rowAffected > 0)
                        return true;
                }

                return true;
            }
            else
            {
                Service.LogWrite($"Отсутствует данный пользователь");
                MessageBox.Show($"Отсутствует данный пользователь!", "Ошибка входа");
                return false;
            }

        }

        public async static Task<bool> ExecInsert(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(_connectionDefaultStr))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int rowAffected = await command.ExecuteNonQueryAsync();
                if (rowAffected > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Получить данные из базы
        /// </summary>
        /// <param name="sqlSelect">Выражения выборки</param>
        /// <param name="sqlConditions">Условия выборки</param>
        /// <param name="sqlParam"></param>
        /// <param name="nameParam"></param>
        /// <param name="nameTable"></param>
        /// <returns>Таблицу результаа запроса</returns>
        public async static Task<DataTable> ExecSelect(string sqlSelect, string sqlConditions, string sqlParam, string nameParam, string nameTable)
        {
            SqlConnection connection = new SqlConnection(_connectionDefaultStr);
            SqlDataAdapter da;

            DataSet tempDataset = new DataSet("temp");
            try
            {
                await connection.OpenAsync();
                da = new SqlDataAdapter();

                string sqlExpression = sqlSelect + $" WHERE {sqlConditions}";

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.Parameters.AddWithValue(sqlParam, nameParam);   // добавление параметра в коллекцию параметров команды
                da.SelectCommand = command;

                da.Fill(tempDataset, nameTable);
            }
            catch (Exception e)
            {
                Service.LogWrite($"Нет доступа к данным! Проверьте настройки! : {e.ToString()}");
                return null;
            }
            finally
            {
                connection.Close();
            }
            return tempDataset.Tables[nameTable];

            #region OldRealization
            //SqlDataReader reader = command.ExecuteReader();

            //if (reader.HasRows) // если есть данные
            //{
            //    // выводим названия столбцов
            //    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetName(0), reader.GetName(1), reader.GetName(2), reader.GetName(3), reader.GetName(4));

            //    while (reader.Read()) // построчно считываем данные
            //    {
            //        object id = reader.GetValue(0);
            //        object FIO = reader.GetValue(1);
            //        object genderge = reader.GetValue(2);
            //        object dateOfBirthd = reader.GetValue(3);
            //        login = reader.GetValue(4);
            //    }
            //}
            //reader.Close(); 
            #endregion
        }

        public async static Task<DataTable> ExecSelect(string sqlSelect, string nameTable)
        {
            SqlConnection connection = new SqlConnection(_connectionDefaultStr);
            SqlDataAdapter da;

            DataSet tempDataset = new DataSet("temp");
            try
            {
                await connection.OpenAsync();
                da = new SqlDataAdapter();

                string sqlExpression = sqlSelect;

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                da.SelectCommand = command;
                da.Fill(tempDataset, nameTable);
            }
            catch (Exception e)
            {
                Service.LogWrite($"Нет доступа к данным! Проверьте настройки! : {e.ToString()}");
                return null;
            }
            finally
            {
                connection.Close();
            }
            return tempDataset.Tables[nameTable];
        }
    }
}
