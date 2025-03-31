namespace Hospital
{
    partial class PatientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientsForm));
            this.dataGridViewPatients = new System.Windows.Forms.DataGridView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.maskedTextBoxPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.labelPhoneNumber = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.dateTimePickerDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.labelDateOfBirth = new System.Windows.Forms.Label();
            this.textBoxPatro = new System.Windows.Forms.TextBox();
            this.labelMiddleName = new System.Windows.Forms.Label();
            this.textBoxSurName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPolicy = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPatients
            // 
            this.dataGridViewPatients.AllowUserToAddRows = false;
            this.dataGridViewPatients.AllowUserToDeleteRows = false;
            this.dataGridViewPatients.AllowUserToResizeColumns = false;
            this.dataGridViewPatients.AllowUserToResizeRows = false;
            this.dataGridViewPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPatients.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPatients.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPatients.Location = new System.Drawing.Point(-3, -4);
            this.dataGridViewPatients.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewPatients.MultiSelect = false;
            this.dataGridViewPatients.Name = "dataGridViewPatients";
            this.dataGridViewPatients.ReadOnly = true;
            this.dataGridViewPatients.Size = new System.Drawing.Size(997, 437);
            this.dataGridViewPatients.TabIndex = 0;
            this.dataGridViewPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPatients_CellClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 442);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(448, 30);
            this.richTextBox1.TabIndex = 40;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // maskedTextBoxPhoneNumber
            // 
            this.maskedTextBoxPhoneNumber.Location = new System.Drawing.Point(632, 599);
            this.maskedTextBoxPhoneNumber.Margin = new System.Windows.Forms.Padding(4);
            this.maskedTextBoxPhoneNumber.Mask = "+7 (999) 000-00-00";
            this.maskedTextBoxPhoneNumber.Name = "maskedTextBoxPhoneNumber";
            this.maskedTextBoxPhoneNumber.Size = new System.Drawing.Size(181, 31);
            this.maskedTextBoxPhoneNumber.TabIndex = 54;
            this.maskedTextBoxPhoneNumber.Leave += new System.EventHandler(this.maskedTextBoxPhoneNumber_Leave);
            // 
            // labelPhoneNumber
            // 
            this.labelPhoneNumber.AutoSize = true;
            this.labelPhoneNumber.Location = new System.Drawing.Point(456, 605);
            this.labelPhoneNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPhoneNumber.Name = "labelPhoneNumber";
            this.labelPhoneNumber.Size = new System.Drawing.Size(163, 24);
            this.labelPhoneNumber.TabIndex = 53;
            this.labelPhoneNumber.Text = "Номер телефона:";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(530, 545);
            this.textBoxAddress.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(451, 31);
            this.textBoxAddress.TabIndex = 52;
            this.textBoxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAddress_KeyPress);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(454, 549);
            this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(68, 24);
            this.labelAddress.TabIndex = 51;
            this.labelAddress.Text = "Адрес:";
            // 
            // dateTimePickerDateOfBirth
            // 
            this.dateTimePickerDateOfBirth.Location = new System.Drawing.Point(632, 491);
            this.dateTimePickerDateOfBirth.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerDateOfBirth.Name = "dateTimePickerDateOfBirth";
            this.dateTimePickerDateOfBirth.Size = new System.Drawing.Size(265, 31);
            this.dateTimePickerDateOfBirth.TabIndex = 50;
            // 
            // labelDateOfBirth
            // 
            this.labelDateOfBirth.AutoSize = true;
            this.labelDateOfBirth.Location = new System.Drawing.Point(454, 495);
            this.labelDateOfBirth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDateOfBirth.Name = "labelDateOfBirth";
            this.labelDateOfBirth.Size = new System.Drawing.Size(146, 24);
            this.labelDateOfBirth.TabIndex = 49;
            this.labelDateOfBirth.Text = "Дата рождения:";
            // 
            // textBoxPatro
            // 
            this.textBoxPatro.Location = new System.Drawing.Point(139, 599);
            this.textBoxPatro.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPatro.Name = "textBoxPatro";
            this.textBoxPatro.Size = new System.Drawing.Size(265, 31);
            this.textBoxPatro.TabIndex = 48;
            this.textBoxPatro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPatro_KeyPress);
            // 
            // labelMiddleName
            // 
            this.labelMiddleName.AutoSize = true;
            this.labelMiddleName.Location = new System.Drawing.Point(15, 607);
            this.labelMiddleName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMiddleName.Name = "labelMiddleName";
            this.labelMiddleName.Size = new System.Drawing.Size(98, 24);
            this.labelMiddleName.TabIndex = 47;
            this.labelMiddleName.Text = "Отчество:";
            // 
            // textBoxSurName
            // 
            this.textBoxSurName.Location = new System.Drawing.Point(139, 543);
            this.textBoxSurName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSurName.Name = "textBoxSurName";
            this.textBoxSurName.Size = new System.Drawing.Size(265, 31);
            this.textBoxSurName.TabIndex = 46;
            this.textBoxSurName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSurName_KeyPress);
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(15, 551);
            this.labelLastName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(92, 24);
            this.labelLastName.TabIndex = 45;
            this.labelLastName.Text = "Фамилия:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(139, 493);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(265, 31);
            this.textBoxName.TabIndex = 44;
            this.textBoxName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxName_KeyPress);
            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(15, 501);
            this.labelFirstName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(52, 24);
            this.labelFirstName.TabIndex = 43;
            this.labelFirstName.Text = "Имя:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Enabled = false;
            this.button1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button1.Location = new System.Drawing.Point(19, 687);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 53);
            this.button1.TabIndex = 42;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.Enabled = false;
            this.button2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button2.Location = new System.Drawing.Point(798, 687);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 53);
            this.button2.TabIndex = 41;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 651);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 24);
            this.label1.TabIndex = 56;
            this.label1.Text = "Номер страхового полиса:";
            // 
            // textBoxPolicy
            // 
            this.textBoxPolicy.Location = new System.Drawing.Point(549, 648);
            this.textBoxPolicy.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxPolicy.Mask = "00000000000";
            this.textBoxPolicy.Name = "textBoxPolicy";
            this.textBoxPolicy.Size = new System.Drawing.Size(127, 31);
            this.textBoxPolicy.TabIndex = 54;
            this.textBoxPolicy.Leave += new System.EventHandler(this.textBoxPolicy_Leave);
            // 
            // PatientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 481);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPolicy);
            this.Controls.Add(this.maskedTextBoxPhoneNumber);
            this.Controls.Add(this.labelPhoneNumber);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.dateTimePickerDateOfBirth);
            this.Controls.Add(this.labelDateOfBirth);
            this.Controls.Add(this.textBoxPatro);
            this.Controls.Add(this.labelMiddleName);
            this.Controls.Add(this.textBoxSurName);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelFirstName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridViewPatients);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1006, 787);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1006, 520);
            this.Name = "PatientsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление пациентами";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatientsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPatients;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxPhoneNumber;
        private System.Windows.Forms.Label labelPhoneNumber;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.DateTimePicker dateTimePickerDateOfBirth;
        private System.Windows.Forms.Label labelDateOfBirth;
        private System.Windows.Forms.TextBox textBoxPatro;
        private System.Windows.Forms.Label labelMiddleName;
        private System.Windows.Forms.TextBox textBoxSurName;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox textBoxPolicy;
    }
}