namespace Hospital
{
    partial class BedsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BedsForm));
            this.dataGridViewBeds = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBoxTypeBed = new System.Windows.Forms.ComboBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeds)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewBeds
            // 
            this.dataGridViewBeds.AllowUserToAddRows = false;
            this.dataGridViewBeds.AllowUserToDeleteRows = false;
            this.dataGridViewBeds.AllowUserToResizeColumns = false;
            this.dataGridViewBeds.AllowUserToResizeRows = false;
            this.dataGridViewBeds.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBeds.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridViewBeds.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBeds.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewBeds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBeds.Location = new System.Drawing.Point(-3, -2);
            this.dataGridViewBeds.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewBeds.MultiSelect = false;
            this.dataGridViewBeds.Name = "dataGridViewBeds";
            this.dataGridViewBeds.ReadOnly = true;
            this.dataGridViewBeds.RowHeadersVisible = false;
            this.dataGridViewBeds.Size = new System.Drawing.Size(1152, 351);
            this.dataGridViewBeds.TabIndex = 0;
            this.dataGridViewBeds.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBeds_CellClick);
            this.dataGridViewBeds.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBeds_CellContentClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(391, 358);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(743, 168);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.buttonCancel);
            this.tabPage1.Controls.Add(this.comboBoxType);
            this.tabPage1.Controls.Add(this.labelType);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(735, 131);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Добавление";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button2.Location = new System.Drawing.Point(597, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 34);
            this.button2.TabIndex = 24;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.Desktop;
            this.buttonCancel.Location = new System.Drawing.Point(597, 61);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(129, 35);
            this.buttonCancel.TabIndex = 22;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.ForeColor = System.Drawing.SystemColors.Desktop;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Общее",
            "ВИП",
            "Специализированное"});
            this.comboBoxType.Location = new System.Drawing.Point(128, 63);
            this.comboBoxType.Margin = new System.Windows.Forms.Padding(5);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(314, 32);
            this.comboBoxType.TabIndex = 18;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.BackColor = System.Drawing.Color.White;
            this.labelType.ForeColor = System.Drawing.SystemColors.Desktop;
            this.labelType.Location = new System.Drawing.Point(8, 71);
            this.labelType.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(77, 24);
            this.labelType.TabIndex = 17;
            this.labelType.Text = "Палата:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.comboBox4);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.comboBox2);
            this.tabPage2.Controls.Add(this.comboBox3);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(735, 131);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Редактирование";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button1.Location = new System.Drawing.Point(583, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 34);
            this.button1.TabIndex = 32;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PowderBlue;
            this.button3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button3.Location = new System.Drawing.Point(583, 62);
            this.button3.Margin = new System.Windows.Forms.Padding(5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 35);
            this.button3.TabIndex = 30;
            this.button3.Text = "Отмена";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBoxTypeBed
            // 
            this.comboBoxTypeBed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeBed.FormattingEnabled = true;
            this.comboBoxTypeBed.Items.AddRange(new object[] {
            "Фильтрация по типу палаты",
            "Общая",
            "ВИП",
            "Специализированная"});
            this.comboBoxTypeBed.Location = new System.Drawing.Point(13, 403);
            this.comboBoxTypeBed.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxTypeBed.Name = "comboBoxTypeBed";
            this.comboBoxTypeBed.Size = new System.Drawing.Size(346, 32);
            this.comboBoxTypeBed.TabIndex = 28;
            this.comboBoxTypeBed.SelectedIndexChanged += new System.EventHandler(this.comboBoxSorting_SelectedIndexChanged);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Фильтрация по статусу койко места",
            "Свободна",
            "Занята",
            "Санитарная обработка"});
            this.comboBoxStatus.Location = new System.Drawing.Point(13, 358);
            this.comboBoxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(346, 32);
            this.comboBoxStatus.TabIndex = 29;
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxDepartment_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.PowderBlue;
            this.button4.Location = new System.Drawing.Point(13, 489);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(346, 37);
            this.button4.TabIndex = 30;
            this.button4.Text = "Сформировать отчет";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Общее",
            "ВИП",
            "Специализированное"});
            this.comboBox1.Location = new System.Drawing.Point(128, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(314, 32);
            this.comboBox1.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label3.Location = new System.Drawing.Point(9, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 24);
            this.label3.TabIndex = 25;
            this.label3.Text = "Отделение:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 24);
            this.label2.TabIndex = 27;
            this.label2.Text = "Палата:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(6, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "Статус:";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.ForeColor = System.Drawing.SystemColors.Desktop;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Общее",
            "ВИП",
            "Специализированное"});
            this.comboBox3.Location = new System.Drawing.Point(125, 50);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(319, 32);
            this.comboBox3.TabIndex = 33;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Свободна",
            "Занята",
            "Санитарная обработка"});
            this.comboBox2.Location = new System.Drawing.Point(125, 93);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(319, 32);
            this.comboBox2.TabIndex = 34;
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "Общее",
            "ВИП",
            "Специализированное"});
            this.comboBox4.Location = new System.Drawing.Point(125, 8);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(319, 32);
            this.comboBox4.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Location = new System.Drawing.Point(6, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 24);
            this.label4.TabIndex = 35;
            this.label4.Text = "Отделение:";
            // 
            // BedsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1147, 538);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBoxTypeBed);
            this.Controls.Add(this.comboBoxStatus);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dataGridViewBeds);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1163, 577);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1163, 577);
            this.Name = "BedsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление коечными местами";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BedsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeds)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBeds;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBoxTypeBed;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}