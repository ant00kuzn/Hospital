
namespace Hospital
{
    partial class EmployeeManagment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeManagment));
            this.button2 = new System.Windows.Forms.Button();
            this.buttonBrowsePhoto = new System.Windows.Forms.Button();
            this.comboBoxRole = new System.Windows.Forms.ComboBox();
            this.labelRole = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.photoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button2.Location = new System.Drawing.Point(468, 272);
            this.button2.Margin = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(157, 45);
            this.button2.TabIndex = 72;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // buttonBrowsePhoto
            // 
            this.buttonBrowsePhoto.Location = new System.Drawing.Point(423, 205);
            this.buttonBrowsePhoto.Margin = new System.Windows.Forms.Padding(8);
            this.buttonBrowsePhoto.Name = "buttonBrowsePhoto";
            this.buttonBrowsePhoto.Size = new System.Drawing.Size(202, 32);
            this.buttonBrowsePhoto.TabIndex = 71;
            this.buttonBrowsePhoto.Text = "Обзор";
            this.buttonBrowsePhoto.UseVisualStyleBackColor = true;
            this.buttonBrowsePhoto.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // comboBoxRole
            // 
            this.comboBoxRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRole.FormattingEnabled = true;
            this.comboBoxRole.Items.AddRange(new object[] {
            "Администратор",
            "Главный врач",
            "Врач",
            "Регистратор"});
            this.comboBoxRole.Location = new System.Drawing.Point(125, 204);
            this.comboBoxRole.Margin = new System.Windows.Forms.Padding(8);
            this.comboBoxRole.Name = "comboBoxRole";
            this.comboBoxRole.Size = new System.Drawing.Size(255, 32);
            this.comboBoxRole.TabIndex = 69;
            // 
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.Location = new System.Drawing.Point(6, 207);
            this.labelRole.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(62, 24);
            this.labelRole.TabIndex = 68;
            this.labelRole.Text = "Роль :";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(125, 111);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(8);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(255, 31);
            this.textBoxPassword.TabIndex = 67;
            this.textBoxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPassword_KeyPress);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(6, 115);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(86, 24);
            this.labelPassword.TabIndex = 66;
            this.labelPassword.Text = "Пароль :";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(125, 64);
            this.textBoxLogin.Margin = new System.Windows.Forms.Padding(8);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(255, 31);
            this.textBoxLogin.TabIndex = 65;
            this.textBoxLogin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLogin_KeyPress);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(6, 68);
            this.labelLogin.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(74, 24);
            this.labelLogin.TabIndex = 64;
            this.labelLogin.Text = "Логин :";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(125, 17);
            this.textBox3.Margin = new System.Windows.Forms.Padding(8);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(255, 31);
            this.textBox3.TabIndex = 59;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox3_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 24);
            this.label3.TabIndex = 58;
            this.label3.Text = "ФИО :";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button1.Location = new System.Drawing.Point(10, 272);
            this.button1.Margin = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 45);
            this.button1.TabIndex = 57;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 161);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 24);
            this.label1.TabIndex = 66;
            this.label1.Text = "Должность :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 158);
            this.textBox1.Margin = new System.Windows.Forms.Padding(8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(255, 31);
            this.textBox1.TabIndex = 67;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // photoPictureBox
            // 
            this.photoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.photoPictureBox.Image = global::Hospital.Properties.Resources.doctor_linear_icon_vector_285420161;
            this.photoPictureBox.Location = new System.Drawing.Point(423, 17);
            this.photoPictureBox.Margin = new System.Windows.Forms.Padding(8);
            this.photoPictureBox.Name = "photoPictureBox";
            this.photoPictureBox.Size = new System.Drawing.Size(202, 172);
            this.photoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.photoPictureBox.TabIndex = 70;
            this.photoPictureBox.TabStop = false;
            // 
            // EmployeeManagment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(634, 324);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonBrowsePhoto);
            this.Controls.Add(this.photoPictureBox);
            this.Controls.Add(this.comboBoxRole);
            this.Controls.Add(this.labelRole);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeManagment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление сотрудником";
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonBrowsePhoto;
        private System.Windows.Forms.PictureBox photoPictureBox;
        private System.Windows.Forms.ComboBox comboBoxRole;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}