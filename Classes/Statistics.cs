using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Lunacod.Classes
{
    internal static class Statistics
    {
        public static int AllMessagesCount;
        public static int SMSRUMessagesCount;
        public static int MessagioMessagesCount;
        public static int AutomaticMessagesCount;
        public static int ManuallyMessagesCount;
        private static class Query
        {
            public static string AllAboutOnePhone(string ORDER_ID)
            {
                return $"SELECT * FROM MessageSendLogs WHERE ORDER_ID = '{ORDER_ID}'";
            }
            /// <summary>
            /// Все записи за определенное время
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string All(string date1,string date2)
            {
                return @$"SELECT 
                      LOG_ID,
                      ORDER_ID,
                      FIO,
                      DEVICE,
                      PHONE,
                      SEND_TYPE,
                      SEND_DATE,
                      SEND_TIME,
                      MESSAGE_TYPE,
                      MESSAGE_STATUS,
                      MESSAGE_TEXT
                      FROM MessageSendLogs
                      WHERE SEND_DATE BETWEEN CAST('{date1}' AS DATETIME) AND CAST('{date2}' AS DATETIME)
                      ORDER BY LOG_ID DESC";
            }
            /// <summary>
            /// Все записи по SMS.RU за определенное время
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string SMSRU(string date1,string date2)
            {
                return $"SELECT * FROM MessageSendLogs " +
                    $"WHERE SEND_DATE BETWEEN CAST('{date1}' AS DATETIME) AND CAST('{date2}' AS DATETIME)\r\n" +
                    $"AND SEND_TYPE = 'SMS.RU'";
            }
            /// <summary>
            /// Все записи по Messagio за определенное время
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string Messagio(string date1,string date2)
            {
                return $"SELECT * FROM MessageSendLogs WHERE SEND_DATE BETWEEN CAST('{date1}' AS DATETIME) AND CAST('{date2}' AS DATETIME) AND SEND_TYPE = 'Messagio'";
            }
            /// <summary>
            /// Все записи автоматики за определенное время
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string Automatic(string date1,string date2)
            {
                return $"SELECT * FROM MessageSendLogs WHERE SEND_DATE BETWEEN CAST('{date1}' AS DATETIME) AND CAST('{date2}' AS DATETIME) AND SEND_TYPE = 'Automatic'";
            }
            /// <summary>
            /// Все записи сделанные вручную за определенное время
            /// </summary>
            /// <param name="date"></param>
            /// <returns></returns>
            public static string Manually(string date1, string date2)
            {
                return $"SELECT * FROM MessageSendLogs WHERE SEND_DATE BETWEEN CAST('{date1}' AS DATETIME) AND CAST('{date2}' AS DATETIME) AND SEND_TYPE = 'Manual'";
            }
        }
        /// <summary>
        /// Получить статистику отправок выбранному клиенту
        /// </summary>
        public static void GetStatistics(string ORDER_ID, ref string MessagesCount)
        {
            DataBase.StatisticsTable.OpenConnection();

            using SqlDataAdapter AllAboutOnePhoneDataAdapter = new(Query.AllAboutOnePhone(ORDER_ID), DataBase.StatisticsTable.connTable);

            using DataTable dataTable = new();
            AllAboutOnePhoneDataAdapter.Fill(dataTable);
            MessagesCount = dataTable.Rows.Count.ToString();

            DataBase.StatisticsTable.CloseConnection();
        }
        /// <summary>
        /// Получить таблицу со статистикой
        /// </summary>
        public static void GetStatistics(string date1, string date2, ref DataGridView dgvStatistics, string QueryFilter, string QuerySearch)
        {
            DataBase.StatisticsTable.OpenConnection();

            using SqlDataAdapter AllDateDataAdapter = new(Query.All(date1, date2), DataBase.StatisticsTable.connTable);

            using DataTable dataTable = new();
            AllMessagesCount = 0;
            SMSRUMessagesCount = 0;
            MessagioMessagesCount = 0;
            AutomaticMessagesCount = 0;
            ManuallyMessagesCount = 0;

            AllDateDataAdapter.Fill(dataTable);

            dgvStatistics.Columns.Add("LOG_ID", "Номер лога");
            dgvStatistics.Columns.Add("ORDER_ID", "Номер заказа");
            dgvStatistics.Columns.Add("FIO", "ФИО");
            dgvStatistics.Columns.Add("DEVICE", "Устройство");
            dgvStatistics.Columns.Add("PHONE", "Номер телефона");
            dgvStatistics.Columns.Add("SEND_TYPE", "Тип отправки");
            dgvStatistics.Columns.Add("SEND_DATE", "Дата отправки");
            dgvStatistics.Columns.Add("SEND_TIME", "Время отправки");
            dgvStatistics.Columns.Add("MESSAGE_TYPE", "Тип сообщения");
            dgvStatistics.Columns.Add("MESSAGE_STATUS", "Статус");
            dgvStatistics.Columns.Add("MESSAGE_TEXT", "Текст");


            foreach (DataRow row in dataTable.Rows)
            {
                dgvStatistics.Rows.Add(row.ItemArray);
                if   ((dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[1].Value.ToString().ToLower().Contains(QuerySearch.ToLower())
                     ||dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[2].Value.ToString().ToLower().Contains(QuerySearch.ToLower())
                     ||dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[3].Value.ToString().ToLower().Contains(QuerySearch.ToLower())
                     ||dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[4].Value.ToString().ToLower().Contains(QuerySearch.ToLower())
                     ||dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[10].Value.ToString().ToLower().Contains(QuerySearch.ToLower()))
                    
                    && dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Value.ToString().Contains(QueryFilter))
                {
                    switch (dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Value)
                    {
                        case "Messagio": dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightSteelBlue; 
                                         dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionBackColor = Color.LightSteelBlue;
                                         dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionForeColor = Color.Black; break;

                        case "SMS.RU": dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightYellow; 
                                       dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionBackColor = Color.LightYellow;
                                       dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionForeColor = Color.Black; break;

                        case "Automatic": dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.BackColor = Color.LightPink; 
                                          dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionBackColor = Color.LightPink;
                                          dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionForeColor = Color.Black; break;

                        case "Manual": dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.BackColor = Color.SandyBrown; 
                                       dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionBackColor = Color.SandyBrown;
                                       dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Style.SelectionForeColor = Color.Black; break;

                        default: break;
                    }
                    if (dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[9].Value.ToString().Contains("Запрос отправлен") || dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[9].Value.ToString().Contains("Запрос выполнен"))
                    {
                        dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGreen;
                        dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Green;
                        dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Black;
                    }   
                    else
                    {   
                        dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                        dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Gray;
                        dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.White;
                    }

                    if (dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Value.ToString().Contains("Messagio")) { MessagioMessagesCount++; }
                    if (dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Value.ToString().Contains("Automatic")) { AutomaticMessagesCount++; }
                    if (dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Value.ToString().Contains("SMS.RU")) { SMSRUMessagesCount++; }
                    if (dgvStatistics.Rows[dgvStatistics.Rows.Count - 1].Cells[5].Value.ToString().Contains("Manual")) { ManuallyMessagesCount++; }
                }
                else
                {  
                    dgvStatistics.Rows.Remove(dgvStatistics.Rows[dgvStatistics.Rows.Count-1]);
                }
                AllMessagesCount = dgvStatistics.Rows.Count;
            }
            DataBase.StatisticsTable.CloseConnection();
        }
        public static void GetStatistics(string date1,string date2)
        {
            try
            {
                DataBase.StatisticsTable.OpenConnection();

                using SqlDataAdapter AllDateDataAdapter = new (Query.All(date1,date2), DataBase.StatisticsTable.connTable);
                using SqlDataAdapter SMSRUDateDataAdapter = new (Query.SMSRU(date1, date2), DataBase.StatisticsTable.connTable);
                using SqlDataAdapter MessagioDateDataAdapter = new (Query.Messagio(date1, date2), DataBase.StatisticsTable.connTable);
                using SqlDataAdapter AutomaticDateDataAdapter = new (Query.Automatic(date1, date2), DataBase.StatisticsTable.connTable);
                using SqlDataAdapter ManuallyDateDataAdapter = new (Query.Manually(date1, date2), DataBase.StatisticsTable.connTable);

                using DataTable dataTable = new();
                AllDateDataAdapter.Fill(dataTable);
                AllMessagesCount = dataTable.Rows.Count;

                dataTable.Clear();
                SMSRUDateDataAdapter.Fill(dataTable);
                SMSRUMessagesCount = dataTable.Rows.Count;

                dataTable.Clear();
                MessagioDateDataAdapter.Fill(dataTable);
                MessagioMessagesCount = dataTable.Rows.Count;

                dataTable.Clear();
                AutomaticDateDataAdapter.Fill(dataTable);
                AutomaticMessagesCount = dataTable.Rows.Count;

                dataTable.Clear();
                ManuallyDateDataAdapter.Fill(dataTable);
                ManuallyMessagesCount = dataTable.Rows.Count;


                DataBase.StatisticsTable.CloseConnection();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "Ошибка при получении статистики",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Добавляет запись в статистику об отправках
        /// </summary>
        /// <param name="ORDER_ID"></param>
        /// <param name="FIO"></param>
        /// <param name="DEVICE"></param>
        /// <param name="PHONE"></param>
        /// <param name="SEND_TYPE"></param>
        /// <param name="SEND_DATE"></param>
        /// <param name="SEND_TIME"></param>
        /// <param name="MESSAGE_TYPE"></param>
        /// <param name="MESSAGE_STATUS"></param>
        /// <param name="MESSAGE_TEXT"></param>
        static public void AddStatisticsRow(
                                string? ORDER_ID,
                                string? FIO,
                                string? DEVICE,
                                string? PHONE,
                                string? SEND_TYPE,
                                string? SEND_DATE,
                                string? SEND_TIME,
                                string? MESSAGE_TYPE,
                                string? MESSAGE_STATUS,
                                string? MESSAGE_TEXT)
        {
            string AddStatRowQuery = $@"INSERT INTO MessageSendLogs
                                        VALUES('{ORDER_ID}',
                                               '{FIO}',
                                               '{DEVICE}',
                                               '{PHONE}',
                                               '{SEND_TYPE}',
                                               '{SEND_DATE}',
                                               '{SEND_TIME}',
                                               '{MESSAGE_TYPE}',
                                               '{MESSAGE_STATUS}',
                                               '{MESSAGE_TEXT}')";

            DataBase.StatisticsTable.OpenConnection();
            using SqlCommand cmd = new(AddStatRowQuery,DataBase.StatisticsTable.connTable);
            cmd.ExecuteNonQuery();
            DataBase.StatisticsTable.CloseConnection();

        }
    }
}
