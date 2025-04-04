namespace Hospital
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.labelDatabaseServer = new System.Windows.Forms.Label();
            this.textBoxDatabaseServer = new System.Windows.Forms.TextBox();
            this.labelDatabaseName = new System.Windows.Forms.Label();
            this.textBoxDatabaseName = new System.Windows.Forms.TextBox();
            this.labelDatabaseUser = new System.Windows.Forms.Label();
            this.textBoxDatabaseUser = new System.Windows.Forms.TextBox();
            this.labelDatabasePassword = new System.Windows.Forms.Label();
            this.textBoxDatabasePassword = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDatabaseServer
            // 
            this.labelDatabaseServer.AutoSize = true;
            this.labelDatabaseServer.Location = new System.Drawing.Point(11, 11);
            this.labelDatabaseServer.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDatabaseServer.Name = "labelDatabaseServer";
            this.labelDatabaseServer.Size = new System.Drawing.Size(192, 24);
            this.labelDatabaseServer.TabIndex = 0;
            this.labelDatabaseServer.Text = "Сервер базы данных:";
            // 
            // textBoxDatabaseServer
            // 
            this.textBoxDatabaseServer.Location = new System.Drawing.Point(215, 4);
            this.textBoxDatabaseServer.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxDatabaseServer.Name = "textBoxDatabaseServer";
            this.textBoxDatabaseServer.Size = new System.Drawing.Size(352, 31);
            this.textBoxDatabaseServer.TabIndex = 0;
            this.textBoxDatabaseServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDatabaseServer_KeyPress);
            // 
            // labelDatabaseName
            // 
            this.labelDatabaseName.AutoSize = true;
            this.labelDatabaseName.Location = new System.Drawing.Point(11, 70);
            this.labelDatabaseName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDatabaseName.Name = "labelDatabaseName";
            this.labelDatabaseName.Size = new System.Drawing.Size(167, 24);
            this.labelDatabaseName.TabIndex = 2;
            this.labelDatabaseName.Text = "Имя базы данных:";
            // 
            // textBoxDatabaseName
            // 
            this.textBoxDatabaseName.Location = new System.Drawing.Point(215, 63);
            this.textBoxDatabaseName.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxDatabaseName.Name = "textBoxDatabaseName";
            this.textBoxDatabaseName.Size = new System.Drawing.Size(352, 31);
            this.textBoxDatabaseName.TabIndex = 1;
            this.textBoxDatabaseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDatabaseName_KeyPress);
            // 
            // labelDatabaseUser
            // 
            this.labelDatabaseUser.AutoSize = true;
            this.labelDatabaseUser.Location = new System.Drawing.Point(11, 127);
            this.labelDatabaseUser.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDatabaseUser.Name = "labelDatabaseUser";
            this.labelDatabaseUser.Size = new System.Drawing.Size(167, 24);
            this.labelDatabaseUser.TabIndex = 4;
            this.labelDatabaseUser.Text = "Пользователь БД:";
            // 
            // textBoxDatabaseUser
            // 
            this.textBoxDatabaseUser.Location = new System.Drawing.Point(215, 120);
            this.textBoxDatabaseUser.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxDatabaseUser.Name = "textBoxDatabaseUser";
            this.textBoxDatabaseUser.Size = new System.Drawing.Size(352, 31);
            this.textBoxDatabaseUser.TabIndex = 2;
            this.textBoxDatabaseUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDatabaseUser_KeyPress);
            // 
            // labelDatabasePassword
            // 
            this.labelDatabasePassword.AutoSize = true;
            this.labelDatabasePassword.Location = new System.Drawing.Point(11, 186);
            this.labelDatabasePassword.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelDatabasePassword.Name = "labelDatabasePassword";
            this.labelDatabasePassword.Size = new System.Drawing.Size(113, 24);
            this.labelDatabasePassword.TabIndex = 6;
            this.labelDatabasePassword.Text = "Пароль БД:";
            // 
            // textBoxDatabasePassword
            // 
            this.textBoxDatabasePassword.Location = new System.Drawing.Point(215, 179);
            this.textBoxDatabasePassword.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxDatabasePassword.Name = "textBoxDatabasePassword";
            this.textBoxDatabasePassword.PasswordChar = '*';
            this.textBoxDatabasePassword.Size = new System.Drawing.Size(352, 31);
            this.textBoxDatabasePassword.TabIndex = 3;
            this.textBoxDatabasePassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDatabasePassword_KeyPress);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonSave.Location = new System.Drawing.Point(433, 280);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(134, 39);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonCancel.Location = new System.Drawing.Point(6, 280);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(134, 39);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 227);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "Таймер бездействия:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(215, 225);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 31);
            this.numericUpDown1.TabIndex = 4;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(573, 324);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxDatabasePassword);
            this.Controls.Add(this.labelDatabasePassword);
            this.Controls.Add(this.textBoxDatabaseUser);
            this.Controls.Add(this.labelDatabaseUser);
            this.Controls.Add(this.textBoxDatabaseName);
            this.Controls.Add(this.labelDatabaseName);
            this.Controls.Add(this.textBoxDatabaseServer);
            this.Controls.Add(this.labelDatabaseServer);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(589, 363);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(589, 363);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDatabaseServer;
        private System.Windows.Forms.TextBox textBoxDatabaseServer;
        private System.Windows.Forms.Label labelDatabaseName;
        private System.Windows.Forms.TextBox textBoxDatabaseName;
        private System.Windows.Forms.Label labelDatabaseUser;
        private System.Windows.Forms.TextBox textBoxDatabaseUser;
        private System.Windows.Forms.Label labelDatabasePassword;
        private System.Windows.Forms.TextBox textBoxDatabasePassword;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}