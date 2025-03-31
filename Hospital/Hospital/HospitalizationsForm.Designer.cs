namespace Hospital
{
    partial class HospitalizationsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HospitalizationsForm));
            this.dataGridViewHospitalizations = new System.Windows.Forms.DataGridView();
            this.dateTimePickerDischargeDate = new System.Windows.Forms.DateTimePicker();
            this.labelDischargeDate = new System.Windows.Forms.Label();
            this.dateTimePickerAdmissionDate = new System.Windows.Forms.DateTimePicker();
            this.labelAdmissionDate = new System.Windows.Forms.Label();
            this.comboBoxBed = new System.Windows.Forms.ComboBox();
            this.labelBed = new System.Windows.Forms.Label();
            this.comboBoxPatient = new System.Windows.Forms.ComboBox();
            this.labelPatient = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxDepartment = new System.Windows.Forms.ComboBox();
            this.comboBoxSorting = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHospitalizations)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHospitalizations
            // 
            this.dataGridViewHospitalizations.AllowUserToAddRows = false;
            this.dataGridViewHospitalizations.AllowUserToDeleteRows = false;
            this.dataGridViewHospitalizations.AllowUserToResizeColumns = false;
            this.dataGridViewHospitalizations.AllowUserToResizeRows = false;
            this.dataGridViewHospitalizations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHospitalizations.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewHospitalizations.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewHospitalizations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHospitalizations.Location = new System.Drawing.Point(-6, -4);
            this.dataGridViewHospitalizations.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewHospitalizations.MultiSelect = false;
            this.dataGridViewHospitalizations.Name = "dataGridViewHospitalizations";
            this.dataGridViewHospitalizations.ReadOnly = true;
            this.dataGridViewHospitalizations.RowHeadersVisible = false;
            this.dataGridViewHospitalizations.Size = new System.Drawing.Size(1911, 437);
            this.dataGridViewHospitalizations.TabIndex = 0;
            this.dataGridViewHospitalizations.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHospitalizations_CellClick);
            this.dataGridViewHospitalizations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHospitalizations_CellDoubleClick);
            this.dataGridViewHospitalizations.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewHospitalizations_RowPrePaint);
            // 
            // dateTimePickerDischargeDate
            // 
            this.dateTimePickerDischargeDate.Location = new System.Drawing.Point(904, 562);
            this.dateTimePickerDischargeDate.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dateTimePickerDischargeDate.MaxDate = new System.DateTime(2025, 3, 10, 0, 0, 0, 0);
            this.dateTimePickerDischargeDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerDischargeDate.Name = "dateTimePickerDischargeDate";
            this.dateTimePickerDischargeDate.Size = new System.Drawing.Size(215, 31);
            this.dateTimePickerDischargeDate.TabIndex = 23;
            this.dateTimePickerDischargeDate.Value = new System.DateTime(2025, 3, 10, 0, 0, 0, 0);
            // 
            // labelDischargeDate
            // 
            this.labelDischargeDate.AutoSize = true;
            this.labelDischargeDate.Location = new System.Drawing.Point(688, 568);
            this.labelDischargeDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDischargeDate.Name = "labelDischargeDate";
            this.labelDischargeDate.Size = new System.Drawing.Size(134, 24);
            this.labelDischargeDate.TabIndex = 22;
            this.labelDischargeDate.Text = "Дата выписки:";
            // 
            // dateTimePickerAdmissionDate
            // 
            this.dateTimePickerAdmissionDate.Location = new System.Drawing.Point(904, 510);
            this.dateTimePickerAdmissionDate.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dateTimePickerAdmissionDate.MaxDate = new System.DateTime(2025, 3, 10, 0, 0, 0, 0);
            this.dateTimePickerAdmissionDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerAdmissionDate.Name = "dateTimePickerAdmissionDate";
            this.dateTimePickerAdmissionDate.Size = new System.Drawing.Size(215, 31);
            this.dateTimePickerAdmissionDate.TabIndex = 21;
            this.dateTimePickerAdmissionDate.Value = new System.DateTime(2025, 3, 5, 23, 59, 0, 0);
            // 
            // labelAdmissionDate
            // 
            this.labelAdmissionDate.AutoSize = true;
            this.labelAdmissionDate.Location = new System.Drawing.Point(688, 510);
            this.labelAdmissionDate.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelAdmissionDate.Name = "labelAdmissionDate";
            this.labelAdmissionDate.Size = new System.Drawing.Size(171, 24);
            this.labelAdmissionDate.TabIndex = 20;
            this.labelAdmissionDate.Text = "Дата поступления:";
            // 
            // comboBoxBed
            // 
            this.comboBoxBed.FormattingEnabled = true;
            this.comboBoxBed.Location = new System.Drawing.Point(239, 575);
            this.comboBoxBed.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.comboBoxBed.Name = "comboBoxBed";
            this.comboBoxBed.Size = new System.Drawing.Size(398, 32);
            this.comboBoxBed.TabIndex = 19;
            // 
            // labelBed
            // 
            this.labelBed.AutoSize = true;
            this.labelBed.Location = new System.Drawing.Point(23, 577);
            this.labelBed.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelBed.Name = "labelBed";
            this.labelBed.Size = new System.Drawing.Size(146, 24);
            this.labelBed.TabIndex = 18;
            this.labelBed.Text = "Коечное место:";
            // 
            // comboBoxPatient
            // 
            this.comboBoxPatient.FormattingEnabled = true;
            this.comboBoxPatient.Location = new System.Drawing.Point(239, 509);
            this.comboBoxPatient.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.comboBoxPatient.Name = "comboBoxPatient";
            this.comboBoxPatient.Size = new System.Drawing.Size(398, 32);
            this.comboBoxPatient.TabIndex = 17;
            // 
            // labelPatient
            // 
            this.labelPatient.AutoSize = true;
            this.labelPatient.Location = new System.Drawing.Point(23, 519);
            this.labelPatient.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPatient.Name = "labelPatient";
            this.labelPatient.Size = new System.Drawing.Size(91, 24);
            this.labelPatient.TabIndex = 16;
            this.labelPatient.Text = "Пациент:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button2.Location = new System.Drawing.Point(1182, 501);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 53);
            this.button2.TabIndex = 26;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button1.Location = new System.Drawing.Point(1182, 608);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 53);
            this.button1.TabIndex = 22;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxDepartment
            // 
            this.comboBoxDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDepartment.FormattingEnabled = true;
            this.comboBoxDepartment.Location = new System.Drawing.Point(600, 442);
            this.comboBoxDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDepartment.Name = "comboBoxDepartment";
            this.comboBoxDepartment.Size = new System.Drawing.Size(481, 32);
            this.comboBoxDepartment.TabIndex = 27;
            this.comboBoxDepartment.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDepartment_SelectedIndexChanged);
            // 
            // comboBoxSorting
            // 
            this.comboBoxSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSorting.FormattingEnabled = true;
            this.comboBoxSorting.Location = new System.Drawing.Point(1089, 442);
            this.comboBoxSorting.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSorting.Name = "comboBoxSorting";
            this.comboBoxSorting.Size = new System.Drawing.Size(481, 32);
            this.comboBoxSorting.TabIndex = 27;
            this.comboBoxSorting.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSorting_SelectedIndexChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(13, 442);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(579, 32);
            this.textBoxSearch.TabIndex = 28;
            this.textBoxSearch.Text = "";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 632);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Врач";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(239, 629);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(398, 32);
            this.comboBox1.TabIndex = 30;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.LightCyan;
            this.button3.Enabled = false;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.LightCyan;
            this.button3.Location = new System.Drawing.Point(1582, 462);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(22, 17);
            this.button3.TabIndex = 31;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.LightGreen;
            this.button4.Enabled = false;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.LightGreen;
            this.button4.Location = new System.Drawing.Point(1582, 481);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(22, 17);
            this.button4.TabIndex = 31;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(1610, 458);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 21);
            this.label2.TabIndex = 32;
            this.label2.Text = "Пациент госпитализирован";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(1610, 477);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 21);
            this.label3.TabIndex = 32;
            this.label3.Text = "Пациент выписан";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(1610, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(256, 21);
            this.label4.TabIndex = 34;
            this.label4.Text = "Пациент выписывается сегодня";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LightSalmon;
            this.button5.Enabled = false;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Garamond", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.LightGreen;
            this.button5.Location = new System.Drawing.Point(1582, 441);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(22, 17);
            this.button5.TabIndex = 33;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // HospitalizationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1898, 500);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxSorting);
            this.Controls.Add(this.comboBoxDepartment);
            this.Controls.Add(this.dateTimePickerDischargeDate);
            this.Controls.Add(this.labelDischargeDate);
            this.Controls.Add(this.dataGridViewHospitalizations);
            this.Controls.Add(this.dateTimePickerAdmissionDate);
            this.Controls.Add(this.labelPatient);
            this.Controls.Add(this.labelAdmissionDate);
            this.Controls.Add(this.comboBoxPatient);
            this.Controls.Add(this.comboBoxBed);
            this.Controls.Add(this.labelBed);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1914, 708);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1914, 539);
            this.Name = "HospitalizationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление госпитализациями";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HospitalizationsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHospitalizations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewHospitalizations;
        private System.Windows.Forms.DateTimePicker dateTimePickerDischargeDate;
        private System.Windows.Forms.Label labelDischargeDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerAdmissionDate;
        private System.Windows.Forms.Label labelAdmissionDate;
        private System.Windows.Forms.ComboBox comboBoxBed;
        private System.Windows.Forms.Label labelBed;
        private System.Windows.Forms.ComboBox comboBoxPatient;
        private System.Windows.Forms.Label labelPatient;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBoxDepartment;
        private System.Windows.Forms.ComboBox comboBoxSorting;
        private System.Windows.Forms.RichTextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button5;
    }
}