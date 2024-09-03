namespace Lunacod
{
    partial class Form_statistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_statistics));
            panel_statistics = new System.Windows.Forms.Panel();
            dataGridView_StatisticsTable = new System.Windows.Forms.DataGridView();
            panel_statistics_content_top = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            comboBox_SearchQuery = new System.Windows.Forms.ComboBox();
            textBox_SearchQuery = new System.Windows.Forms.TextBox();
            label15 = new System.Windows.Forms.Label();
            dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            label11 = new System.Windows.Forms.Label();
            dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            textBox_StatAtSelectedTime = new System.Windows.Forms.TextBox();
            textBox_StatManually = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            textBox_StatAutomatic = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            textBox_StatSMSRU = new System.Windows.Forms.TextBox();
            textBox_StatMessagio = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            timer_SearchUpdate = new System.Windows.Forms.Timer(components);
            panel_statistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_StatisticsTable).BeginInit();
            panel_statistics_content_top.SuspendLayout();
            SuspendLayout();
            // 
            // panel_statistics
            // 
            panel_statistics.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            panel_statistics.Controls.Add(dataGridView_StatisticsTable);
            panel_statistics.Controls.Add(panel_statistics_content_top);
            panel_statistics.Dock = System.Windows.Forms.DockStyle.Fill;
            panel_statistics.Location = new System.Drawing.Point(0, 0);
            panel_statistics.Name = "panel_statistics";
            panel_statistics.Size = new System.Drawing.Size(1184, 561);
            panel_statistics.TabIndex = 2;
            // 
            // dataGridView_StatisticsTable
            // 
            dataGridView_StatisticsTable.AllowUserToAddRows = false;
            dataGridView_StatisticsTable.AllowUserToDeleteRows = false;
            dataGridView_StatisticsTable.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(19, 56, 140);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView_StatisticsTable.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView_StatisticsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_StatisticsTable.BackgroundColor = System.Drawing.Color.White;
            dataGridView_StatisticsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridView_StatisticsTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView_StatisticsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView_StatisticsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_StatisticsTable.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(19, 56, 140);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView_StatisticsTable.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView_StatisticsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView_StatisticsTable.EnableHeadersVisualStyles = false;
            dataGridView_StatisticsTable.GridColor = System.Drawing.Color.White;
            dataGridView_StatisticsTable.Location = new System.Drawing.Point(0, 80);
            dataGridView_StatisticsTable.MultiSelect = false;
            dataGridView_StatisticsTable.Name = "dataGridView_StatisticsTable";
            dataGridView_StatisticsTable.ReadOnly = true;
            dataGridView_StatisticsTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(19, 56, 140);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView_StatisticsTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView_StatisticsTable.RowHeadersVisible = false;
            dataGridView_StatisticsTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(19, 56, 140);
            dataGridView_StatisticsTable.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridView_StatisticsTable.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            dataGridView_StatisticsTable.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(19, 56, 140);
            dataGridView_StatisticsTable.RowTemplate.Height = 20;
            dataGridView_StatisticsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView_StatisticsTable.Size = new System.Drawing.Size(1184, 481);
            dataGridView_StatisticsTable.TabIndex = 39;
            // 
            // panel_statistics_content_top
            // 
            panel_statistics_content_top.Controls.Add(label2);
            panel_statistics_content_top.Controls.Add(label1);
            panel_statistics_content_top.Controls.Add(comboBox_SearchQuery);
            panel_statistics_content_top.Controls.Add(textBox_SearchQuery);
            panel_statistics_content_top.Controls.Add(label15);
            panel_statistics_content_top.Controls.Add(dateTimePicker2);
            panel_statistics_content_top.Controls.Add(label11);
            panel_statistics_content_top.Controls.Add(dateTimePicker1);
            panel_statistics_content_top.Controls.Add(textBox_StatAtSelectedTime);
            panel_statistics_content_top.Controls.Add(textBox_StatManually);
            panel_statistics_content_top.Controls.Add(label5);
            panel_statistics_content_top.Controls.Add(label9);
            panel_statistics_content_top.Controls.Add(textBox_StatAutomatic);
            panel_statistics_content_top.Controls.Add(label8);
            panel_statistics_content_top.Controls.Add(textBox_StatSMSRU);
            panel_statistics_content_top.Controls.Add(textBox_StatMessagio);
            panel_statistics_content_top.Controls.Add(label7);
            panel_statistics_content_top.Dock = System.Windows.Forms.DockStyle.Top;
            panel_statistics_content_top.Location = new System.Drawing.Point(0, 0);
            panel_statistics_content_top.Name = "panel_statistics_content_top";
            panel_statistics_content_top.Size = new System.Drawing.Size(1184, 80);
            panel_statistics_content_top.TabIndex = 37;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(483, 20);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(45, 15);
            label2.TabIndex = 53;
            label2.Text = "Поиск:";
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(980, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(84, 15);
            label1.TabIndex = 52;
            label1.Text = "Тип отправки:";
            // 
            // comboBox_SearchQuery
            // 
            comboBox_SearchQuery.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            comboBox_SearchQuery.FormattingEnabled = true;
            comboBox_SearchQuery.Items.AddRange(new object[] { "", "SMS.RU", "Messagio", "Automatic", "Manual" });
            comboBox_SearchQuery.Location = new System.Drawing.Point(980, 46);
            comboBox_SearchQuery.Name = "comboBox_SearchQuery";
            comboBox_SearchQuery.Size = new System.Drawing.Size(192, 23);
            comboBox_SearchQuery.TabIndex = 51;
            comboBox_SearchQuery.SelectedIndexChanged += ComboBox_SearchQuery_TextChanged;
            // 
            // textBox_SearchQuery
            // 
            textBox_SearchQuery.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            textBox_SearchQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox_SearchQuery.Location = new System.Drawing.Point(483, 46);
            textBox_SearchQuery.Name = "textBox_SearchQuery";
            textBox_SearchQuery.Size = new System.Drawing.Size(491, 23);
            textBox_SearchQuery.TabIndex = 49;
            textBox_SearchQuery.TextChanged += TextBox_SearchQuery_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.ForeColor = System.Drawing.Color.Black;
            label15.Location = new System.Drawing.Point(330, 17);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(12, 15);
            label15.TabIndex = 48;
            label15.Text = "-";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CalendarMonthBackground = System.Drawing.SystemColors.InactiveBorder;
            dateTimePicker2.Font = new System.Drawing.Font("Segoe UI", 8F);
            dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dateTimePicker2.Location = new System.Drawing.Point(343, 13);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new System.Drawing.Size(82, 22);
            dateTimePicker2.TabIndex = 47;
            dateTimePicker2.ValueChanged += DateTimePicker_ValueChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = System.Drawing.Color.Black;
            label11.Location = new System.Drawing.Point(365, 49);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(50, 15);
            label11.TabIndex = 40;
            label11.Text = "Manual:";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarMonthBackground = System.Drawing.Color.FromArgb(236, 236, 236);
            dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 8F);
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            dateTimePicker1.Location = new System.Drawing.Point(250, 13);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new System.Drawing.Size(78, 22);
            dateTimePicker1.TabIndex = 46;
            dateTimePicker1.ValueChanged += DateTimePicker_ValueChanged;
            // 
            // textBox_StatAtSelectedTime
            // 
            textBox_StatAtSelectedTime.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            textBox_StatAtSelectedTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_StatAtSelectedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            textBox_StatAtSelectedTime.Location = new System.Drawing.Point(431, 17);
            textBox_StatAtSelectedTime.Multiline = true;
            textBox_StatAtSelectedTime.Name = "textBox_StatAtSelectedTime";
            textBox_StatAtSelectedTime.ReadOnly = true;
            textBox_StatAtSelectedTime.Size = new System.Drawing.Size(46, 14);
            textBox_StatAtSelectedTime.TabIndex = 45;
            textBox_StatAtSelectedTime.TabStop = false;
            // 
            // textBox_StatManually
            // 
            textBox_StatManually.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            textBox_StatManually.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_StatManually.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            textBox_StatManually.Location = new System.Drawing.Point(421, 49);
            textBox_StatManually.Multiline = true;
            textBox_StatManually.Name = "textBox_StatManually";
            textBox_StatManually.ReadOnly = true;
            textBox_StatManually.Size = new System.Drawing.Size(46, 14);
            textBox_StatManually.TabIndex = 39;
            textBox_StatManually.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label5.ForeColor = System.Drawing.Color.Black;
            label5.Location = new System.Drawing.Point(14, 13);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(209, 21);
            label5.TabIndex = 28;
            label5.Text = "Статистика по отправкам";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = System.Drawing.Color.Black;
            label9.Location = new System.Drawing.Point(241, 49);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(66, 15);
            label9.TabIndex = 36;
            label9.Text = "Automatic:";
            // 
            // textBox_StatAutomatic
            // 
            textBox_StatAutomatic.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            textBox_StatAutomatic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_StatAutomatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            textBox_StatAutomatic.Location = new System.Drawing.Point(313, 49);
            textBox_StatAutomatic.Multiline = true;
            textBox_StatAutomatic.Name = "textBox_StatAutomatic";
            textBox_StatAutomatic.ReadOnly = true;
            textBox_StatAutomatic.Size = new System.Drawing.Size(46, 14);
            textBox_StatAutomatic.TabIndex = 35;
            textBox_StatAutomatic.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = System.Drawing.Color.Black;
            label8.Location = new System.Drawing.Point(123, 49);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(60, 15);
            label8.TabIndex = 34;
            label8.Text = "Messagio:";
            // 
            // textBox_StatSMSRU
            // 
            textBox_StatSMSRU.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            textBox_StatSMSRU.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_StatSMSRU.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            textBox_StatSMSRU.Location = new System.Drawing.Point(71, 49);
            textBox_StatSMSRU.Multiline = true;
            textBox_StatSMSRU.Name = "textBox_StatSMSRU";
            textBox_StatSMSRU.ReadOnly = true;
            textBox_StatSMSRU.Size = new System.Drawing.Size(46, 14);
            textBox_StatSMSRU.TabIndex = 31;
            textBox_StatSMSRU.TabStop = false;
            // 
            // textBox_StatMessagio
            // 
            textBox_StatMessagio.BackColor = System.Drawing.Color.FromArgb(236, 236, 236);
            textBox_StatMessagio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox_StatMessagio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            textBox_StatMessagio.Location = new System.Drawing.Point(189, 49);
            textBox_StatMessagio.Multiline = true;
            textBox_StatMessagio.Name = "textBox_StatMessagio";
            textBox_StatMessagio.ReadOnly = true;
            textBox_StatMessagio.Size = new System.Drawing.Size(46, 14);
            textBox_StatMessagio.TabIndex = 33;
            textBox_StatMessagio.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = System.Drawing.Color.Black;
            label7.Location = new System.Drawing.Point(14, 49);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(51, 15);
            label7.TabIndex = 32;
            label7.Text = "SMS.RU:";
            // 
            // timer_SearchUpdate
            // 
            timer_SearchUpdate.Tick += Timer_SearchUpdate_Tick;
            // 
            // Form_statistics
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1184, 561);
            Controls.Add(panel_statistics);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(1200, 600);
            Name = "Form_statistics";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Lunacod - Статистика по отправкам";
            ResizeBegin += Form_statistics_ResizeBegin;
            ResizeEnd += Form_statistics_ResizeEnd;
            panel_statistics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView_StatisticsTable).EndInit();
            panel_statistics_content_top.ResumeLayout(false);
            panel_statistics_content_top.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel_statistics;
        private System.Windows.Forms.Panel panel_statistics_content_top;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_StatManually;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_StatAutomatic;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_StatSMSRU;
        private System.Windows.Forms.TextBox textBox_StatMessagio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView_StatisticsTable;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox_StatAtSelectedTime;
        private System.Windows.Forms.TextBox textBox_SearchQuery;
        private System.Windows.Forms.ComboBox comboBox_SearchQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer_SearchUpdate;
    }
}