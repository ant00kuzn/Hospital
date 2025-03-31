namespace Hospital
{
    partial class GuidesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuidesForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.radioButtonRole = new System.Windows.Forms.RadioButton();
            this.radioButtonDepartment = new System.Windows.Forms.RadioButton();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelBedID = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.radioButtonWard = new System.Windows.Forms.RadioButton();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.labelDepartment = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(22, 64);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(581, 450);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // radioButtonRole
            // 
            this.radioButtonRole.AutoSize = true;
            this.radioButtonRole.Location = new System.Drawing.Point(22, 21);
            this.radioButtonRole.Margin = new System.Windows.Forms.Padding(5);
            this.radioButtonRole.Name = "radioButtonRole";
            this.radioButtonRole.Size = new System.Drawing.Size(72, 28);
            this.radioButtonRole.TabIndex = 1;
            this.radioButtonRole.TabStop = true;
            this.radioButtonRole.Text = "Роли";
            this.radioButtonRole.UseVisualStyleBackColor = true;
            this.radioButtonRole.CheckedChanged += new System.EventHandler(this.radioButtonRole_CheckedChanged);
            // 
            // radioButtonDepartment
            // 
            this.radioButtonDepartment.AutoSize = true;
            this.radioButtonDepartment.Location = new System.Drawing.Point(206, 21);
            this.radioButtonDepartment.Margin = new System.Windows.Forms.Padding(5);
            this.radioButtonDepartment.Name = "radioButtonDepartment";
            this.radioButtonDepartment.Size = new System.Drawing.Size(122, 28);
            this.radioButtonDepartment.TabIndex = 2;
            this.radioButtonDepartment.TabStop = true;
            this.radioButtonDepartment.Text = "Отделения";
            this.radioButtonDepartment.UseVisualStyleBackColor = true;
            this.radioButtonDepartment.CheckedChanged += new System.EventHandler(this.radioButtonDepartment_CheckedChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(613, 64);
            this.labelName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(97, 24);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Название:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(750, 64);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxName.MaxLength = 10;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(293, 31);
            this.textBoxName.TabIndex = 6;
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonSave.ForeColor = System.Drawing.Color.Black;
            this.buttonSave.Location = new System.Drawing.Point(910, 232);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(133, 39);
            this.buttonSave.TabIndex = 7;
            this.buttonSave.Text = "Добавить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Location = new System.Drawing.Point(617, 229);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(133, 39);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelBedID
            // 
            this.labelBedID.AutoSize = true;
            this.labelBedID.Location = new System.Drawing.Point(613, 115);
            this.labelBedID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelBedID.Name = "labelBedID";
            this.labelBedID.Size = new System.Drawing.Size(94, 24);
            this.labelBedID.TabIndex = 9;
            this.labelBedID.Text = "ID койки:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Location = new System.Drawing.Point(750, 115);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(293, 32);
            this.comboBox1.TabIndex = 10;
            // 
            // radioButtonWard
            // 
            this.radioButtonWard.AutoSize = true;
            this.radioButtonWard.Location = new System.Drawing.Point(102, 21);
            this.radioButtonWard.Margin = new System.Windows.Forms.Padding(5);
            this.radioButtonWard.Name = "radioButtonWard";
            this.radioButtonWard.Size = new System.Drawing.Size(94, 28);
            this.radioButtonWard.TabIndex = 11;
            this.radioButtonWard.TabStop = true;
            this.radioButtonWard.Text = "Палаты";
            this.radioButtonWard.UseVisualStyleBackColor = true;
            this.radioButtonWard.CheckedChanged += new System.EventHandler(this.radioButtonWard_CheckedChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Location = new System.Drawing.Point(750, 165);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(293, 32);
            this.comboBox2.TabIndex = 10;
            // 
            // labelDepartment
            // 
            this.labelDepartment.AutoSize = true;
            this.labelDepartment.Location = new System.Drawing.Point(613, 165);
            this.labelDepartment.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelDepartment.Name = "labelDepartment";
            this.labelDepartment.Size = new System.Drawing.Size(132, 24);
            this.labelDepartment.TabIndex = 9;
            this.labelDepartment.Text = "ID отделения:";
            // 
            // GuidesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(617, 531);
            this.Controls.Add(this.radioButtonWard);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.labelDepartment);
            this.Controls.Add(this.labelBedID);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.radioButtonDepartment);
            this.Controls.Add(this.radioButtonRole);
            this.Controls.Add(this.dataGridView);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1073, 570);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(633, 570);
            this.Name = "GuidesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление справочниками";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GuidesForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.RadioButton radioButtonRole;
        private System.Windows.Forms.RadioButton radioButtonDepartment;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelBedID;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.RadioButton radioButtonWard;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label labelDepartment;
    }
}