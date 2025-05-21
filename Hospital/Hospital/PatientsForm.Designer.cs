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
            this.phone = new System.Windows.Forms.MaskedTextBox();
            this.labelPhoneNumber = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.data = new System.Windows.Forms.DateTimePicker();
            this.labelDateOfBirth = new System.Windows.Forms.Label();
            this.surname = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.policy = new System.Windows.Forms.MaskedTextBox();
            this.snils = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.passport = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxGender = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxBenefit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.diagnos = new System.Windows.Forms.TextBox();
            this.FIOrod = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.phoneRod = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.rab = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.patronymic = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.labelRowCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPatients
            // 
            this.dataGridViewPatients.AllowUserToAddRows = false;
            this.dataGridViewPatients.AllowUserToDeleteRows = false;
            this.dataGridViewPatients.AllowUserToResizeColumns = false;
            this.dataGridViewPatients.AllowUserToResizeRows = false;
            this.dataGridViewPatients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPatients.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPatients.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPatients.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPatients.Location = new System.Drawing.Point(5, 358);
            this.dataGridViewPatients.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewPatients.MultiSelect = false;
            this.dataGridViewPatients.Name = "dataGridViewPatients";
            this.dataGridViewPatients.ReadOnly = true;
            this.dataGridViewPatients.Size = new System.Drawing.Size(1267, 445);
            this.dataGridViewPatients.TabIndex = 0;
            this.dataGridViewPatients.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPatients_CellClick);
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(767, 120);
            this.phone.Margin = new System.Windows.Forms.Padding(4);
            this.phone.Mask = "+7 999 000-00-00";
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(210, 31);
            this.phone.TabIndex = 54;
            this.phone.Leave += new System.EventHandler(this.maskedTextBoxPhoneNumber_Leave);
            // 
            // labelPhoneNumber
            // 
            this.labelPhoneNumber.AutoSize = true;
            this.labelPhoneNumber.Location = new System.Drawing.Point(635, 123);
            this.labelPhoneNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPhoneNumber.Name = "labelPhoneNumber";
            this.labelPhoneNumber.Size = new System.Drawing.Size(89, 24);
            this.labelPhoneNumber.TabIndex = 53;
            this.labelPhoneNumber.Text = "Телефон";
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(92, 120);
            this.address.Margin = new System.Windows.Forms.Padding(4);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(495, 31);
            this.address.TabIndex = 52;
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(21, 123);
            this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(63, 24);
            this.labelAddress.TabIndex = 51;
            this.labelAddress.Text = "Адрес";
            // 
            // data
            // 
            this.data.Location = new System.Drawing.Point(1054, 169);
            this.data.Margin = new System.Windows.Forms.Padding(4);
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(188, 31);
            this.data.TabIndex = 50;
            // 
            // labelDateOfBirth
            // 
            this.labelDateOfBirth.AutoSize = true;
            this.labelDateOfBirth.Location = new System.Drawing.Point(900, 174);
            this.labelDateOfBirth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDateOfBirth.Name = "labelDateOfBirth";
            this.labelDateOfBirth.Size = new System.Drawing.Size(141, 24);
            this.labelDateOfBirth.TabIndex = 49;
            this.labelDateOfBirth.Text = "Дата рождения";
            // 
            // surname
            // 
            this.surname.Location = new System.Drawing.Point(111, 71);
            this.surname.Margin = new System.Windows.Forms.Padding(4);
            this.surname.Name = "surname";
            this.surname.Size = new System.Drawing.Size(345, 31);
            this.surname.TabIndex = 46;
            this.surname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSurName_KeyPress);
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(21, 71);
            this.labelLastName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(87, 24);
            this.labelLastName.TabIndex = 45;
            this.labelLastName.Text = "Фамилия";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.ForeColor = System.Drawing.SystemColors.Desktop;
            this.buttonAdd.Location = new System.Drawing.Point(5, 843);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(177, 53);
            this.buttonAdd.TabIndex = 41;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(635, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 24);
            this.label1.TabIndex = 56;
            this.label1.Text = "Номер мед. полиса";
            // 
            // policy
            // 
            this.policy.Location = new System.Drawing.Point(824, 18);
            this.policy.Margin = new System.Windows.Forms.Padding(4);
            this.policy.Mask = "00000000000";
            this.policy.Name = "policy";
            this.policy.Size = new System.Drawing.Size(153, 31);
            this.policy.TabIndex = 54;
            this.policy.Leave += new System.EventHandler(this.textBoxPolicy_Leave);
            // 
            // snils
            // 
            this.snils.Location = new System.Drawing.Point(111, 18);
            this.snils.Margin = new System.Windows.Forms.Padding(4);
            this.snils.Name = "snils";
            this.snils.Size = new System.Drawing.Size(185, 31);
            this.snils.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 57;
            this.label2.Text = "СНИЛС";
            // 
            // passport
            // 
            this.passport.Location = new System.Drawing.Point(421, 18);
            this.passport.Margin = new System.Windows.Forms.Padding(4);
            this.passport.Name = "passport";
            this.passport.Size = new System.Drawing.Size(185, 31);
            this.passport.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 24);
            this.label3.TabIndex = 59;
            this.label3.Text = "Паспорт";
            // 
            // comboBoxGender
            // 
            this.comboBoxGender.FormattingEnabled = true;
            this.comboBoxGender.Location = new System.Drawing.Point(92, 171);
            this.comboBoxGender.Name = "comboBoxGender";
            this.comboBoxGender.Size = new System.Drawing.Size(85, 32);
            this.comboBoxGender.TabIndex = 61;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 179);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 24);
            this.label4.TabIndex = 62;
            this.label4.Text = "Пол";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 174);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 24);
            this.label5.TabIndex = 64;
            this.label5.Text = "Льгота";
            // 
            // comboBoxBenefit
            // 
            this.comboBoxBenefit.FormattingEnabled = true;
            this.comboBoxBenefit.Location = new System.Drawing.Point(274, 171);
            this.comboBoxBenefit.Name = "comboBoxBenefit";
            this.comboBoxBenefit.Size = new System.Drawing.Size(313, 32);
            this.comboBoxBenefit.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(636, 174);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 24);
            this.label6.TabIndex = 66;
            this.label6.Text = "Код диагноза";
            // 
            // diagnos
            // 
            this.diagnos.Location = new System.Drawing.Point(767, 172);
            this.diagnos.Margin = new System.Windows.Forms.Padding(4);
            this.diagnos.Name = "diagnos";
            this.diagnos.Size = new System.Drawing.Size(108, 31);
            this.diagnos.TabIndex = 67;
            // 
            // FIOrod
            // 
            this.FIOrod.Location = new System.Drawing.Point(211, 270);
            this.FIOrod.Margin = new System.Windows.Forms.Padding(4);
            this.FIOrod.Name = "FIOrod";
            this.FIOrod.Size = new System.Drawing.Size(495, 31);
            this.FIOrod.TabIndex = 69;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 273);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 24);
            this.label7.TabIndex = 68;
            this.label7.Text = "ФИО родственника";
            // 
            // phoneRod
            // 
            this.phoneRod.Location = new System.Drawing.Point(246, 318);
            this.phoneRod.Margin = new System.Windows.Forms.Padding(4);
            this.phoneRod.Mask = "+7 999 000-00-00";
            this.phoneRod.Name = "phoneRod";
            this.phoneRod.Size = new System.Drawing.Size(210, 31);
            this.phoneRod.TabIndex = 71;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 321);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(213, 24);
            this.label8.TabIndex = 70;
            this.label8.Text = "Телефон родственника";
            // 
            // rab
            // 
            this.rab.Location = new System.Drawing.Point(163, 221);
            this.rab.Margin = new System.Windows.Forms.Padding(4);
            this.rab.Name = "rab";
            this.rab.Size = new System.Drawing.Size(543, 31);
            this.rab.TabIndex = 73;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 224);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 24);
            this.label9.TabIndex = 72;
            this.label9.Text = "Место работы";
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonEdit.Enabled = false;
            this.buttonEdit.ForeColor = System.Drawing.SystemColors.Desktop;
            this.buttonEdit.Location = new System.Drawing.Point(202, 843);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(177, 53);
            this.buttonEdit.TabIndex = 41;
            this.buttonEdit.Text = "Редактировать";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonExit.Enabled = false;
            this.buttonExit.ForeColor = System.Drawing.SystemColors.Desktop;
            this.buttonExit.Location = new System.Drawing.Point(1095, 843);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(177, 53);
            this.buttonExit.TabIndex = 74;
            this.buttonExit.Text = "В меню";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(521, 71);
            this.name.Margin = new System.Windows.Forms.Padding(4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(290, 31);
            this.name.TabIndex = 77;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(475, 71);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 24);
            this.label10.TabIndex = 76;
            this.label10.Text = "Имя";
            // 
            // patronymic
            // 
            this.patronymic.Location = new System.Drawing.Point(920, 71);
            this.patronymic.Margin = new System.Windows.Forms.Padding(4);
            this.patronymic.Name = "patronymic";
            this.patronymic.Size = new System.Drawing.Size(334, 31);
            this.patronymic.TabIndex = 79;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(828, 71);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 24);
            this.label11.TabIndex = 78;
            this.label11.Text = "Отчество";
            // 
            // labelRowCount
            // 
            this.labelRowCount.AutoSize = true;
            this.labelRowCount.Location = new System.Drawing.Point(1, 812);
            this.labelRowCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRowCount.Name = "labelRowCount";
            this.labelRowCount.Size = new System.Drawing.Size(0, 24);
            this.labelRowCount.TabIndex = 80;
            // 
            // PatientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1276, 899);
            this.Controls.Add(this.labelRowCount);
            this.Controls.Add(this.patronymic);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.rab);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.phoneRod);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.FIOrod);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.diagnos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxBenefit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxGender);
            this.Controls.Add(this.passport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.snils);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.policy);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.labelPhoneNumber);
            this.Controls.Add(this.address);
            this.Controls.Add(this.labelAddress);
            this.Controls.Add(this.data);
            this.Controls.Add(this.labelDateOfBirth);
            this.Controls.Add(this.surname);
            this.Controls.Add(this.labelLastName);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridViewPatients);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "PatientsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пациенты";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PatientsForm_FormClosed);
            this.Load += new System.EventHandler(this.PatientsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPatients;
        private System.Windows.Forms.MaskedTextBox phone;
        private System.Windows.Forms.Label labelPhoneNumber;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.DateTimePicker data;
        private System.Windows.Forms.Label labelDateOfBirth;
        private System.Windows.Forms.TextBox surname;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox policy;
        private System.Windows.Forms.TextBox snils;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxGender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxBenefit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox diagnos;
        private System.Windows.Forms.TextBox FIOrod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox phoneRod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rab;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox patronymic;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelRowCount;
    }
}