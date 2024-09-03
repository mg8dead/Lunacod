using Lunacod.Classes.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lunacod.Classes
{
    public static class Automatic
    {
        public static bool SendingNotify { get; private set; }
        private static string NameClient(string FIO)
        {
            //массив с Ф.И.О.
            string[] _f_i_o_ = FIO.Split(' ');

            if (_f_i_o_.Length == 3)
            {
                return _f_i_o_[1];
            }
            else if (_f_i_o_.Length == 2)
            {
                return _f_i_o_[1];
            }
            else
            {
                return _f_i_o_[0];
            }
        }
        public static async void SendNotify(DataGridView dataGridView_1YDevices,ComboBox comboBoxMessageTemplates)
        {
            try
            {
                for (int i = 0; i < dataGridView_1YDevices.Rows.Count; i++, SendingNotify = true)
                {
                    string message = $"{NameClient(dataGridView_1YDevices.Rows[i].Cells[2].Value.ToString())}, уведомляем, что прошел год с момента чистки вашего компьютера. Для записи на повторную чистку запишитесь по телефону +73517349949";;
                    Messagio.SendMessage("Automatic",
                                         dataGridView_1YDevices.Rows[i].Cells[0].Value.ToString(),
                                         dataGridView_1YDevices.Rows[i].Cells[2].Value.ToString(),
                                         dataGridView_1YDevices.Rows[i].Cells[3].Value.ToString(),
                                         dataGridView_1YDevices.Rows[i].Cells[4].Value.ToString(),
                                         message,
                                         false,
                                         comboBoxMessageTemplates.Items[2].ToString()
                                         );
                }
                await Task.Delay(60000);
                SendingNotify = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public static void Table_Automatic_Update(ref DataGridView dgvAutomatics,ref Label tableAutomaticUpdateTime)
        {
            tableAutomaticUpdateTime.Text = $"Обновлено в {Helpers.TimeNow()}";
            try
            {
                Console.WriteLine("Обновлена таблица автоматики");
                //открытие соединения с бд
                AutomaticTable.OpenConnection();

                //проверка на наличие соединения
                if (AutomaticTable.connTable.State == ConnectionState.Open)
                {
                    using SqlDataAdapter adapter = new(AutomaticTable.DataAdapterString_AutomaticTable, AutomaticTable.connTable);//запрос в бд
                    using DataTable dt = new();//создание таблицы
                    adapter.Fill(dt);//Заполнение таблицы данными
                    AutomaticTable.CloseConnection();
                    dgvAutomatics.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.ToString(), "Автоматика.Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
