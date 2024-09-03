using Lunacod.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Lunacod
{
    public partial class Form_statistics : Form
    {
        public Form_statistics(DateTimePicker dateTime1, DateTimePicker dateTime2)
        {
            InitializeComponent();
            dateTimePicker1.Value = dateTime1.Value;
            dateTimePicker2.Value = dateTime2.Value;
            comboBox_SearchQuery.SelectedItem = 0;
            UpdateStatistics();
        }
        public Form_statistics(DateTime dateTime1, DateTime dateTime2, string SearchQuery)
        {
            InitializeComponent();
            dateTimePicker1.Value = dateTime1;
            dateTimePicker2.Value = dateTime2;
            textBox_SearchQuery.Text = SearchQuery;
            comboBox_SearchQuery.SelectedItem = 0;
            UpdateStatistics();
        }
        void UpdateStatistics()
        {
            dataGridView_StatisticsTable.Columns.Clear();
            try
            {
                Cursor = Cursors.WaitCursor;
                Statistics.GetStatistics(dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(), ref dataGridView_StatisticsTable, comboBox_SearchQuery.Text, textBox_SearchQuery.Text);

                textBox_StatAtSelectedTime.Text = Statistics.AllMessagesCount.ToString();
                textBox_StatSMSRU.Text = Statistics.SMSRUMessagesCount.ToString();
                textBox_StatMessagio.Text = Statistics.MessagioMessagesCount.ToString();
                textBox_StatAutomatic.Text = Statistics.AutomaticMessagesCount.ToString();
                textBox_StatManually.Text = Statistics.ManuallyMessagesCount.ToString();

                Cursor = Cursors.Default;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка обновления статистики",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
            Statistics.GetStatistics(dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString());
        }
        private void TextBox_SearchQuery_TextChanged(object sender, EventArgs e)
        {
            timer_SearchUpdate.Stop();
            timer_SearchUpdate.Interval = 600;
            timer_SearchUpdate.Start();
        }
        private void Timer_SearchUpdate_Tick(object sender, EventArgs e)
        {
            UpdateStatistics();
            timer_SearchUpdate.Stop();
        }
        private void ComboBox_SearchQuery_TextChanged(object sender, EventArgs e)
        {
            UpdateStatistics();
        }
        private void Form_statistics_ResizeBegin(object sender, EventArgs e)
        {
            int dtGridStatisticsHeight = dataGridView_StatisticsTable.Height;
            int dtGridStaticticsWidth = dataGridView_StatisticsTable.Width;
            dataGridView_StatisticsTable.Dock = DockStyle.None;
            dataGridView_StatisticsTable.Height = dtGridStatisticsHeight;
            dataGridView_StatisticsTable.Width = dtGridStaticticsWidth;
        }
        private void Form_statistics_ResizeEnd(object sender, EventArgs e)
        {
            dataGridView_StatisticsTable.Dock = DockStyle.Fill;
        }
    }
}
