using Lunacod.Classes;
using Lunacod.Classes.DataBase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static Lunacod.Properties.Resources;


namespace Lunacod
{
    public partial class Form_main : Form
    {
        public Form_main()
        {
            InitializeComponent();
        }

        private void Form_main_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ChangeSelectedPanel(panel_ready_technics, button_ready_technics);

            Settings.GetSettings();
            Settings.FillSMSRUcodesXml();

            SMSRU.CheckBalance(); textBox_smsrubalance.Text = SMSRU.Balance;

            UpdateStatistics();

            comboBox_MessageTemplates.Text = comboBox_MessageTemplates.Items[0].ToString();
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

            Table_ReadyTechnics_Update();
            Automatic.Table_Automatic_Update(ref dataGridView_Automatics, ref label_tableautomaticupdatetime);
            this.Cursor = Cursors.Default;
        }

        private void Button_ready_technics_Click(object sender, EventArgs e)
        {
            ChangeSelectedPanel(panel_ready_technics, button_ready_technics);
            Table_ReadyTechnics_Update();
        }

        private void Button_sms_Click(object sender, EventArgs e)
        {
            ChangeSelectedPanel(panel_sms, button_sms);
        }

        private void Button_messagio_Click(object sender, EventArgs e)
        {
            ChangeSelectedPanel(panel_messagio, button_messagio);
        }

        private void Button_settings_Click(object sender, EventArgs e)
        {
            Settings.GetSettings();

            textBox_DataSource.Text = Settings.SettingDataSource;
            textBox_InitialCatalog.Text = Settings.SettingInitialCatalog;
            textBox_Password.Text = Settings.SettingPassword;
            textBox_UserID.Text = Settings.SettingUserID;
            textBox_SMSRUAPI.Text = Settings.SettingSMSRUAPI;
            textBox_Messagio_SenderCode.Text = Settings.SettingMessagio_SenderCode;
            textBox_Messagio_Projectlogin.Text = Settings.SettingMessagio_Projectlogin;

            Button_SaveChanges.Enabled = false;
            Button_Cancel.Enabled = false;
            Settings.SettingsChange = false;

            ChangeSelectedPanel(panel_settings, button_settings);
        }

        private void Button_about_Click(object sender, EventArgs e)
        {
            ChangeSelectedPanel(panel_about, button_about);
        }
        private void Button_automatic_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ChangeSelectedPanel(panel_automatic, Button_automatic);
            Automatic.Table_Automatic_Update(ref dataGridView_Automatics, ref label_tableautomaticupdatetime);
            Cursor = Cursors.Default;
        }
        /// <summary>
        /// Принимает в себя два аргумента, Panel и Button.
        /// Изменяет стили кнопок в приложении
        /// </summary>
        /// <param name="selected_panel"></param>
        /// <param name="clicked_button"></param>
        private void ChangeSelectedPanel(Panel selected_panel, Button clicked_button)
        {
            if (Settings.SettingsChange == false)
            {
                if (selected_panel != panel_sms)
                {
                    ClearTextboxSMS();
                }
                if (selected_panel != panel_messagio)
                {
                    ClearTextboxMessagio();
                }
                if (selected_panel != panel_settings)
                {
                    Passchar_change_hideall();
                }
                if (selected_panel == panel_settings || selected_panel == panel_about)
                {
                    panel_statistics.Hide();
                }
                else
                {
                    panel_statistics.Show();
                }


                panel_ready_technics.Visible = false;
                panel_sms.Visible = false;
                panel_messagio.Visible = false;
                panel_settings.Visible = false;
                panel_automatic.Visible = false;
                panel_about.Visible = false;

                button_ready_technics.BackColor = Color.FromArgb(19, 56, 140);
                button_ready_technics.ForeColor = Color.White;
                button_sms.BackColor = Color.FromArgb(19, 56, 140);
                button_sms.ForeColor = Color.White;
                button_messagio.BackColor = Color.FromArgb(19, 56, 140);
                button_messagio.ForeColor = Color.White;
                button_settings.BackColor = Color.FromArgb(19, 56, 140);
                button_settings.ForeColor = Color.White;
                Button_automatic.BackColor = Color.FromArgb(19, 56, 140);
                Button_automatic.ForeColor = Color.White;
                button_about.BackColor = Color.FromArgb(19, 56, 140);
                button_about.ForeColor = Color.White;

                selected_panel.Visible = true;

                clicked_button.BackColor = Color.White;
                clicked_button.ForeColor = Color.Black;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            else
            {
                SaveSettings();
            }
        }

        private void Button_ready_technics_update_table_Click(object sender, EventArgs e)
        {
            Table_ReadyTechnics_Update();
        }

        /// <summary>
        /// Обновляет таблицу с готовой техникой
        /// </summary>
        void Table_ReadyTechnics_Update()
        {
            this.Cursor = Cursors.WaitCursor;
            label_readytechnics_tableUpdateTime.Text = $"Обновлено в {Helpers.TimeNow()}";
            try
            {
                dataGridView_UsersTable.Columns.Clear();
                //открытие соединения с бд
                ClientsTable.OpenConnection();

                //проверка на наличие соединения
                if (ClientsTable.connTable.State == ConnectionState.Open)
                {
                    using SqlDataAdapter adapter = new(ClientsTable.DataAdapterString_Clients, ClientsTable.connTable);//запрос в бд
                    using DataTable dt = new();//создание таблицы
                    adapter.Fill(dt);//Заполнение таблицы данными

                    ClientsTable.CloseConnection();

                    dataGridView_UsersTable.Columns.Add("Number", "Номер заказа");
                    dataGridView_UsersTable.Columns.Add("DateGot", "Дата готовности");
                    dataGridView_UsersTable.Columns.Add("FIO", "ФИО");
                    dataGridView_UsersTable.Columns.Add("Devices", "Устройство");
                    dataGridView_UsersTable.Columns.Add("SumToPay", "Стоимость");
                    dataGridView_UsersTable.Columns.Add("Phone", "Номер телефона");
                    dataGridView_UsersTable.Columns.Add("Messages_Count", "Кол-во отправленных сообщений");
                    
                    string PriceToSend = "";
                    string MessagesCount = "";
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        dataGridView_UsersTable.Rows.Add(dtRow.ItemArray);
                        SMSRU.CheckMessagePrice(dtRow[5].ToString(), "", ref PriceToSend);
                        if (PriceToSend.Length > 1)
                        {

                        }
                        else
                        {
                            dataGridView_UsersTable.Rows[^1].DefaultCellStyle.BackColor = Color.FromArgb(240,240,240);
                        }
                        Statistics.GetStatistics(dataGridView_UsersTable.Rows[^1].Cells[0].Value.ToString(), ref MessagesCount);
                        dataGridView_UsersTable.Rows[^1].Cells[6].Value = MessagesCount;
                    }
                }
                else
                {
                    MessageBox.Show("Не удается установить соединение с БД.\nПроверьте введенные данные в настройках.", "Таблица клиентов",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Готовая техника.Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;

        }

        private void Button_SaveChanges_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
        /// <summary>
        /// Сохраняет настройки введенные textBox'ы
        /// </summary>
        private void SaveSettings()
        {
            DialogResult result = MessageBox.Show(
                "Сохранить изменения?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.Yes)
            {
                MessageBox.Show
                    (
                    "Для применения настроек программа перезапустится.",
                    "Сохранение настроек...",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    );
                Settings.SaveSettings(textBox_DataSource.Text,
                                      textBox_InitialCatalog.Text,
                                      textBox_UserID.Text,
                                      textBox_Password.Text,
                                      textBox_SMSRUAPI.Text,
                                      textBox_Messagio_SenderCode.Text,
                                      textBox_Messagio_Projectlogin.Text);
                Application.Restart();
            }
            else
            {
                Settings.SettingsChange = false;
                Button_SaveChanges.Enabled = false;
                Button_Cancel.Enabled = false;
                ChangeSelectedPanel(panel_ready_technics, button_ready_technics);
            }
        }
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Settings.SettingsChange = false;
            ChangeSelectedPanel(panel_ready_technics, button_ready_technics);
        }
        private void Settings_Changed(object sender, EventArgs e)
        {
            Button_SaveChanges.Enabled = true;
            Button_Cancel.Enabled = true;
            Settings.SettingsChange = true;
        }

        //переменная содержит в себе номер выделенной строки
        int SelectedRowsIndex;

        private void DataGridView_UsersTable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dataGridView_UsersTable.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dataGridView_UsersTable.ClearSelection();
                    dataGridView_UsersTable.Rows[hit.RowIndex].Selected = true;
                    contextMenuStrip.Show(dataGridView_UsersTable, e.Location);
                    SelectedRowsIndex = hit.RowIndex;
                }
            }
        }

        private void SMSRUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //вывод ячеек выделенной строки по переменным
            string FIO = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[2].Value.ToString();
            string OrderID = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[0].Value.ToString();
            string PriceToPay = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[4].Value.ToString();
            string Phone = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[5].Value.ToString();

            //массив с Ф.И.О.
            string[] _f_i_o_ = FIO.Split(' ');

            //создание формы отправки сообщения

            textBox_SMSRUDevice.Text = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[3].Value.ToString();
            textBox_SMSRUFIO.Text = FIO;
            textBox_SMSRUPhoneNumber.Text = Phone;
            textBox_SMSRUOrderID.Text = OrderID;

            if (_f_i_o_.Length == 3)
            {
                string stringMessage = $"{_f_i_o_[1]} {_f_i_o_[2]}, заказ {OrderID}, готов к выдаче, к оплате {PriceToPay}₽";
                textBox_SMSRUMessage.Text = stringMessage;
            }
            if (_f_i_o_.Length == 2)
            {
                string stringMessage = $"{_f_i_o_[1]}, заказ {OrderID}, готов к выдаче, к оплате {PriceToPay}₽";
                textBox_SMSRUMessage.Text = stringMessage;
            }
            if (_f_i_o_.Length == 1)
            {
                string stringMessage = $"{_f_i_o_[0]}, заказ {OrderID}, готов к выдаче, к оплате {PriceToPay}₽";
                textBox_SMSRUMessage.Text = stringMessage;
            }
            string PriceToSend = "";
            SMSRU.CheckMessagePrice(Phone, textBox_SMSRUMessage.Text, ref PriceToSend);
            textBox_SMSRUPriceToSend.Text = PriceToSend;
            ChangeSelectedPanel(panel_sms, button_sms);
        }
        private void Button_sms_clearAllTextBox_Click(object sender, EventArgs e)
        {
            ClearTextboxSMS();
        }
        /// <summary>
        /// Очищает TextBox'ы на панели SMS.RU
        /// </summary>
        private void ClearTextboxSMS()
        {
            textBox_SMSRUDevice.Clear();
            textBox_SMSRUOrderID.Clear();
            textBox_SMSRUFIO.Clear();
            textBox_SMSRUPhoneNumber.Clear();
            textBox_SMSRUPriceToSend.Clear();
            textBox_SMSRUMessage.Clear();
        }
        private void TextBox_SMSRUMessage_TextChanged(object sender, EventArgs e)
        {
            string PriceToSend = "";
            SMSRU.CheckMessagePrice(textBox_SMSRUPhoneNumber.Text, textBox_SMSRUMessage.Text, ref PriceToSend);
            textBox_SMSRUPriceToSend.Text = PriceToSend;
        }

        private void Button_SMSRU_SendMessage_Click(object sender, EventArgs e)
        {
            SMSRU.SendMessage(
                textBox_SMSRUOrderID.Text,
                textBox_SMSRUFIO.Text,
                textBox_SMSRUDevice.Text,
                textBox_SMSRUPhoneNumber.Text,
                textBox_SMSRUMessage.Text);
            ChangeSelectedPanel(panel_ready_technics, button_ready_technics);
            UpdateStatistics();
            SMSRU.CheckBalance();
            textBox_smsrubalance.Text = SMSRU.Balance;
        }
        /// <summary>
        /// Очищает TextBox'ы на панели Messagio
        /// </summary>
        private void ClearTextboxMessagio()
        {
            textBox_MessagioOrderID.Clear();
            textBox_FIO_messagio.Clear();
            textBox_message_messagio.Clear();
            textbox_phoneNumber_messagio.Clear();
            textBox_MessagioDevice.Clear();
        }


        private void MessagioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //вывод ячеек выделенной строки по переменным
            string FIO = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[2].Value.ToString();
            string OrderID = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[0].Value.ToString();
            string Phone = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[5].Value.ToString();
            string Device = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[3].Value.ToString();
            textBox_MessagioOrderID.Text = OrderID;

            textBox_MessagioDevice.Text = dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[3].Value.ToString();

            //создание формы отправки сообщения
            textBox_FIO_messagio.Text = FIO;
            textbox_phoneNumber_messagio.Text = Phone;
            Messagio.FormirateMessage(ref comboBox_MessageTemplates,ref textBox_message_messagio,FIO, OrderID, Device);

            ChangeSelectedPanel(panel_messagio, button_messagio);
        }
        private void Button_Messagio_SendMessage_Click(object sender, EventArgs e)
        {
            
            Messagio.SendMessage(
                "Messagio",
                textBox_MessagioOrderID.Text,
                textBox_FIO_messagio.Text,
                textBox_MessagioDevice.Text,
                textbox_phoneNumber_messagio.Text,
                textBox_message_messagio.Text,
                true,
                comboBox_MessageTemplates.Text);
            UpdateStatistics();
            ChangeSelectedPanel(panel_ready_technics, button_ready_technics);
        }
        private void ComboBox_MessageTemplates_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Messagio.FormirateMessage(ref comboBox_MessageTemplates, ref textBox_message_messagio, textBox_FIO_messagio.Text, textBox_MessagioOrderID.Text, textBox_MessagioDevice.Text);
        }

        private void Button_messagio_clearAllTextBox_Click(object sender, EventArgs e)
        {
            ClearTextboxMessagio();
        }

        private void Button_AutomaticTable_update_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Automatic.Table_Automatic_Update(ref dataGridView_Automatics,ref label_tableautomaticupdatetime);
            this.Cursor = Cursors.Default;
        }
        private async void Panel_statistics_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateStatistics();
                Form_statistics form_Statistics = new(dateTimePicker1, dateTimePicker2);
                form_Statistics.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка панели статистики",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        void UpdateStatistics()
        {
            this.Cursor = Cursors.WaitCursor;
            label_StatsUpdatedTime.Text = $"Обновлено в {Helpers.TimeNow()}";
            try
            {
                Statistics.GetStatistics(dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString());

                textBox_StatAtSelectedTime.Text = Statistics.AllMessagesCount.ToString();
                textBox_StatSMSRU.Text = Statistics.SMSRUMessagesCount.ToString();
                textBox_StatMessagio.Text = Statistics.MessagioMessagesCount.ToString();
                textBox_StatAutomatic.Text = Statistics.AutomaticMessagesCount.ToString();
                textBox_StatManually.Text = Statistics.ManuallyMessagesCount.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка обновления статистики",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        private void Button_Automatic_UpdateTable_Click(object sender, EventArgs e)
        {
            ClearTextboxSMS();
        }
        private void ComboBox_Stats_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
        }
        private void CheckBox_Automatic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Automatic.Checked == true)
            {
                dataGridView_Automatics.Enabled = false;
                dateTimePicker_Automatic.Enabled = false;

                timer_AutomaticSendMessage.Interval = 1000;
                timer_AutomaticSendMessage.Start();

                timer_AutomaticTableUpdate.Interval = 3600000;
                timer_AutomaticTableUpdate.Start();
                textBox_AutomaticStatus.Text = $"Автоматика включена :) Время сейчас: {Helpers.TimeNow()}";
            }
            else if (checkBox_Automatic.Checked == false)
            {
                dataGridView_Automatics.Enabled = true;
                dateTimePicker_Automatic.Enabled = true;

                timer_AutomaticSendMessage.Stop();
                timer_AutomaticTableUpdate.Stop();

                textBox_AutomaticStatus.Text = $"Автоматика отключена :(";
            }
        }
        private async void Timer_AutomaticSendMessage_Tick(object sender, EventArgs e)
        {
            textBox_AutomaticStatus.Text = $"Автоматика включена :) Время сейчас: {Helpers.TimeNow()}";

            TimeOnly selectedTime = TimeOnly.FromDateTime(dateTimePicker_Automatic.Value);

            if (!Automatic.SendingNotify && Helpers.TimeNow().Minute == selectedTime.Minute && Helpers.TimeNow().Hour == selectedTime.Hour)
            {
                textBox_AutomaticStatus.Text = "Отправка...";
                Automatic.SendNotify(dataGridView_Automatics, comboBox_MessageTemplates);

                textBox_AutomaticStatus.Text = "Отправлено! 2 минуты перекур";
            }
        }
        private void Timer_AutomaticTableUpdate_Tick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Automatic.Table_Automatic_Update(ref dataGridView_Automatics, ref label_tableautomaticupdatetime);
            this.Cursor = Cursors.Default;
        }

        private void Timer_StatUpdate_Tick(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        private static void Passchar_change(TextBox textbox, Button button)
        {
            if (textbox.PasswordChar == '*') { button.Image = glyphicons_basic_52_eye; textbox.PasswordChar = '\0'; }
            else { button.Image = glyphicons_basic_53_eye_off; textbox.PasswordChar = '*'; }
        }

        private void Passchar_change_hideall()
        {
            textBox_DataSource.PasswordChar = '*'; button_DataSource_visible.Image = glyphicons_basic_53_eye_off;
            textBox_InitialCatalog.PasswordChar = '*'; button_InitialCatalog_visible.Image = glyphicons_basic_53_eye_off;
            textBox_UserID.PasswordChar = '*'; button_UserID_visible.Image = glyphicons_basic_53_eye_off;
            textBox_Password.PasswordChar = '*'; button_Password_visible.Image = glyphicons_basic_53_eye_off;
            textBox_SMSRUAPI.PasswordChar = '*'; button_SMSRUAPI_visible.Image = glyphicons_basic_53_eye_off;
            textBox_Messagio_SenderCode.PasswordChar = '*'; button_Messagio_SenderCode_visible.Image = glyphicons_basic_53_eye_off;
            textBox_Messagio_Projectlogin.PasswordChar = '*'; button_Messagio_Projectlogin_visible.Image = glyphicons_basic_53_eye_off;
        }

        private void Button_DataSource_visible_Click(object sender, EventArgs e)
        {
            Passchar_change(textBox_DataSource, button_DataSource_visible);
        }

        private void Button_InitialCatalog_visible_Click(object sender, EventArgs e)
        {
            Passchar_change(textBox_InitialCatalog, button_InitialCatalog_visible);
        }

        private void Button_UserID_visible_Click(object sender, EventArgs e)
        {
            Passchar_change(textBox_UserID, button_UserID_visible);
            
        }

        private void Button_Password_visible_Click(object sender, EventArgs e)
        {
            Passchar_change(textBox_Password, button_Password_visible);
        }

        private void Button_SMSRUAPI_visible_Click(object sender, EventArgs e)
        {
            Passchar_change(textBox_SMSRUAPI, button_SMSRUAPI_visible);
        }

        private void Button_Messagio_SenderCode_visible_Click(object sender, EventArgs e)
        {
            Passchar_change(textBox_Messagio_SenderCode, button_Messagio_SenderCode_visible);
        }

        private void Button_Messagio_Projectlogin_visible_Click(object sender, EventArgs e)
        {
            Passchar_change(textBox_Messagio_Projectlogin, button_Messagio_Projectlogin_visible);
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
        }

        private void Form_main_ResizeBegin(object sender, EventArgs e)
        {
            int dtGridAutomaticsHeight = dataGridView_Automatics.Height;
            int dtGridAutomaticsWidth = dataGridView_Automatics.Width;
            dataGridView_Automatics.Dock = DockStyle.None;
            dataGridView_Automatics.Height = dtGridAutomaticsHeight;
            dataGridView_Automatics.Width = dtGridAutomaticsWidth;

            int dtGridUsersTableHeight = dataGridView_UsersTable.Height;
            int dtGridUsersTablesWidth = dataGridView_UsersTable.Width;
            dataGridView_UsersTable.Dock = DockStyle.None;
            dataGridView_UsersTable.Height = dtGridUsersTableHeight;
            dataGridView_UsersTable.Width = dtGridUsersTablesWidth;
        }

        private void Form_main_ResizeEnd(object sender, EventArgs e)
        {
            dataGridView_Automatics.Dock = DockStyle.Fill;
            dataGridView_UsersTable.Dock = DockStyle.Fill;
        }

        private void СтатистикаПоЗаказуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateStatistics();
            Form_statistics form_Statistics = new(DateTimePicker.MinimumDateTime, DateTime.Now, dataGridView_UsersTable.Rows[SelectedRowsIndex].Cells[0].Value.ToString());
            form_Statistics.ShowDialog();
        }
    }
}