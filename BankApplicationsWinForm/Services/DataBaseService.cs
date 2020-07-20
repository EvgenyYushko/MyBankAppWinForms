using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplicationsWinForm.Services
{
    public static class DataBaseService
    {
        // получаем строку подключения
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        /// <summary>
        /// Обновить данные в базе
        /// </summary>
        /// <param name="sqlConditions">условие выборки</param>
        /// <param name="sqlParam">"Login"</param>
        /// <param name="nameParam">"Евгений"</param>
        /// <param name="nameTable">Имя таблицы "tbUsers"</param>
        /// <param name="data">Данные для сохранения</param>
        /// <param name="userId"></param>
        /// <param name="nameColumn">Имя столбца</param>
        /// <returns></returns>
        public static bool ExecUpdate(string sqlConditions, string sqlParam, string nameParam, string nameTable, string data, int userId, string nameColumn)
        {
            var dt = ExecSelect($"SELECT * FROM {nameTable}", sqlConditions, sqlParam, nameParam, nameTable);

            if (dt.Rows.Count != 0)
            {
                string sqlExpression = $"UPDATE {nameTable} SET [{nameColumn}] = '{data}' WHERE User_ID = {userId}";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlExpression, connection);

                    int rowAffected = command.ExecuteNonQuery();

                    connection.Close();

                    if (rowAffected > 0)
                        return true;
                }

                return true;
            }
            else
            {
                //string sqlExpression = $"INSERT tbUsers VALUES ('{FIO}', 'true', NULL, '{password}', '{login}')";
                Service.LogWrite($"Отсутствует данный пользователь");
                MessageBox.Show($"Отсутствует данный пользователь!", "Ошибка входа");
                return false;
            }

        }

        public static bool ExecInsert(string sqlExpression)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int rowAffected = command.ExecuteNonQuery();
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
        public static DataTable ExecSelect(string sqlSelect, string sqlConditions, string sqlParam, string nameParam, string nameTable)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter da;

            DataSet tempDataset = new DataSet("temp");
            try
            {
                connection.Open();
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
    }
}
