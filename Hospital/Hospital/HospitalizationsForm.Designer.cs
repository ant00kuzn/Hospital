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
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.comboBoxSorting = new System.Windows.Forms.ComboBox();
            this.textBoxSearch = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.labelRecordCount = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
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
            this.dataGridViewHospitalizations.Location = new System.Drawing.Point(10, 68);
            this.dataGridViewHospitalizations.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewHospitalizations.MultiSelect = false;
            this.dataGridViewHospitalizations.Name = "dataGridViewHospitalizations";
            this.dataGridViewHospitalizations.ReadOnly = true;
            this.dataGridViewHospitalizations.RowHeadersVisible = false;
            this.dataGridViewHospitalizations.Size = new System.Drawing.Size(1775, 680);
            this.dataGridViewHospitalizations.TabIndex = 0;
            this.dataGridViewHospitalizations.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHospitalizations_CellClick);
            this.dataGridViewHospitalizations.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridViewHospitalizations_RowPrePaint);
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(412, 13);
            this.comboBoxFilter.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(303, 32);
            this.comboBoxFilter.TabIndex = 27;
            this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDepartment_SelectedIndexChanged);
            // 
            // comboBoxSorting
            // 
            this.comboBoxSorting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSorting.FormattingEnabled = true;
            this.comboBoxSorting.Location = new System.Drawing.Point(723, 13);
            this.comboBoxSorting.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSorting.Name = "comboBoxSorting";
            this.comboBoxSorting.Size = new System.Drawing.Size(252, 32);
            this.comboBoxSorting.TabIndex = 27;
            this.comboBoxSorting.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSorting_SelectedIndexChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(10, 13);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(394, 32);
            this.textBoxSearch.TabIndex = 28;
            this.textBoxSearch.Text = "";
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearch_KeyPress);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Location = new System.Drawing.Point(12, 780);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(321, 49);
            this.button1.TabIndex = 29;
            this.button1.Text = "Просмотр записи";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.Location = new System.Drawing.Point(896, 780);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(321, 49);
            this.button2.TabIndex = 29;
            this.button2.Text = "Формирование отчета";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PowderBlue;
            this.button3.Location = new System.Drawing.Point(1464, 780);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(321, 49);
            this.button3.TabIndex = 29;
            this.button3.Text = "В меню";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelRecordCount
            // 
            this.labelRecordCount.AutoSize = true;
            this.labelRecordCount.Location = new System.Drawing.Point(12, 753);
            this.labelRecordCount.Name = "labelRecordCount";
            this.labelRecordCount.Size = new System.Drawing.Size(59, 24);
            this.labelRecordCount.TabIndex = 36;
            this.labelRecordCount.Text = "label2";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.PowderBlue;
            this.button4.Location = new System.Drawing.Point(556, 780);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(321, 49);
            this.button4.TabIndex = 37;
            this.button4.Text = "Печать направления";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // HospitalizationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1800, 832);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.labelRecordCount);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.comboBoxSorting);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.dataGridViewHospitalizations);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "HospitalizationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Учет госпитализаций";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HospitalizationsForm_FormClosed);
            this.Load += new System.EventHandler(this.HospitalizationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHospitalizations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewHospitalizations;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.ComboBox comboBoxSorting;
        private System.Windows.Forms.RichTextBox textBoxSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelRecordCount;
        private System.Windows.Forms.Button button4;
    }
}