using System.Data.SqlClient;

namespace Lunacod.Classes.DataBase
{
        public static class ClientsTable
        {
            readonly public static string DataAdapterString_Clients = "SELECT Series.Number as 'Номер заказа', Renovs.DateGot as 'Дата готовности', Contractors.Name as 'ФИО', Devices.[Group] + ' ' + Devices.Maker + ' ' + Devices.Model as 'Устройство',Renovs.SumToPay as 'Стоимость', Contractors.Phone as 'Номер телефона'\r\nFROM Renovs JOIN Series ON Renovs.SerieID = Series.ID \r\n\t\t\tJOIN Contractors ON Series.ContractorID = Contractors.ID\r\n\t\t\tJOIN Devices ON Renovs.DeviceID = Devices.ID\r\nWHERE Renovs.Status = '1'\r\nORDER BY Renovs.DateGot;";

            readonly public static SqlConnection connTable = new(Settings.GetConnString());
            public static void OpenConnection()
            {
                if (connTable.State == System.Data.ConnectionState.Closed)
                {
                    connTable.Open();
                }
            }
        /// <summary>
        /// Закрывает соединение с БД
        /// </summary>
            public static void CloseConnection()
            {
                if (connTable.State == System.Data.ConnectionState.Open)
                {
                    connTable.Close();
                }
            }
        }

        public static class StatisticsTable
        {
            readonly public static SqlConnection connTable = new(Settings.GetLogTableConnString());
            public static void OpenConnection()
            {
                if (connTable.State == System.Data.ConnectionState.Closed)
                {
                    connTable.Open();
                }

            }
            public static void CloseConnection()
            {
                if (connTable.State == System.Data.ConnectionState.Open)
                {
                    connTable.Close();
                }
            }
        }
        public static class AutomaticTable
        {
            readonly public static string DataAdapterString_AutomaticTable = "SELECT Series.Number as 'Номер заказа', \r\n\t   CAST(Renovs.DateOut as date) as 'Дата выдачи', \r\n\t   Contractors.Name as 'ФИО', \r\n\t   Devices.[Group] + ' ' + Devices.Maker + ' ' + Devices.Model as 'Устройство', \r\n\t   Contractors.Phone as 'Номер телефона'\r\nFROM Renovs JOIN Series ON Renovs.SerieID = Series.ID \r\n\t\t\tJOIN Contractors ON Series.ContractorID = Contractors.ID\r\n\t\t\tJOIN Devices ON Renovs.DeviceID = Devices.ID\r\nWHERE Contractors.IsBad = 0 AND Renovs.Status = '2' AND CONVERT(date,Renovs.DateOut) = DateAdd(year,-1,CONVERT (date, GETDATE()))\r\n\r\nORDER BY Renovs.DateOut;";

            readonly public static SqlConnection connTable = new(Settings.GetConnString());
            public static void OpenConnection()
            {
                if (connTable.State == System.Data.ConnectionState.Closed)
                {
                    connTable.Open();
                }
            }
            public static void CloseConnection()
            {
                if (connTable.State == System.Data.ConnectionState.Open)
                {
                    connTable.Close();
                }
            }
        }
    }
