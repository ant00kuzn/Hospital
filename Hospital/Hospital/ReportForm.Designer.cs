namespace Hospital
{
    partial class ReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.labelStartDate = new System.Windows.Forms.Label();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.buttonGenerateReport = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(21, 78);
            this.labelStartDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(150, 24);
            this.labelStartDate.TabIndex = 0;
            this.labelStartDate.Text = "Начальная дата:";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(213, 73);
            this.dateTimePickerStartDate.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(352, 31);
            this.dateTimePickerStartDate.TabIndex = 1;
            this.dateTimePickerStartDate.Value = new System.DateTime(2025, 3, 10, 0, 0, 0, 0);
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Location = new System.Drawing.Point(21, 137);
            this.labelEndDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(142, 24);
            this.labelEndDate.TabIndex = 2;
            this.labelEndDate.Text = "Конечная дата:";
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(213, 133);
            this.dateTimePickerEndDate.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(352, 31);
            this.dateTimePickerEndDate.TabIndex = 3;
            this.dateTimePickerEndDate.Value = new System.DateTime(2025, 3, 10, 0, 0, 0, 0);
            // 
            // buttonGenerateReport
            // 
            this.buttonGenerateReport.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonGenerateReport.Location = new System.Drawing.Point(400, 229);
            this.buttonGenerateReport.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonGenerateReport.Name = "buttonGenerateReport";
            this.buttonGenerateReport.Size = new System.Drawing.Size(179, 39);
            this.buttonGenerateReport.TabIndex = 6;
            this.buttonGenerateReport.Text = "Сформировать";
            this.buttonGenerateReport.UseVisualStyleBackColor = false;
            this.buttonGenerateReport.Click += new System.EventHandler(this.buttonGenerateReport_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonCancel.Location = new System.Drawing.Point(15, 229);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(134, 39);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(530, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Выберите даты для отчета об использовании коечных мест:";
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(594, 275);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonGenerateReport);
            this.Controls.Add(this.dateTimePickerEndDate);
            this.Controls.Add(this.labelEndDate);
            this.Controls.Add(this.dateTimePickerStartDate);
            this.Controls.Add(this.labelStartDate);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(610, 314);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(610, 164);
            this.Name = "ReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Формирование отчета";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReportForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.Button buttonGenerateReport;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
    }
}