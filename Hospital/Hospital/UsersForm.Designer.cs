namespace Hospital
{
    partial class UsersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersForm));
            this.label2 = new System.Windows.Forms.Label();
            this.fio = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.delete = new System.Windows.Forms.Button();
            this.edit = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.labRowCount = new System.Windows.Forms.Label();
            this.tabNumber = new System.Windows.Forms.RichTextBox();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.role = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.login = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(591, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 42;
            this.label2.Text = "ФИО";
            // 
            // fio
            // 
            this.fio.Enabled = false;
            this.fio.Location = new System.Drawing.Point(654, 6);
            this.fio.MaxLength = 30;
            this.fio.Multiline = false;
            this.fio.Name = "fio";
            this.fio.Size = new System.Drawing.Size(525, 34);
            this.fio.TabIndex = 41;
            this.fio.Text = "";
            this.fio.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 24);
            this.label1.TabIndex = 40;
            this.label1.Text = "Табельный номер";
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.PowderBlue;
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(401, 671);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(173, 49);
            this.delete.TabIndex = 39;
            this.delete.Text = "Удалить";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // edit
            // 
            this.edit.BackColor = System.Drawing.Color.PowderBlue;
            this.edit.Enabled = false;
            this.edit.Location = new System.Drawing.Point(205, 671);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(173, 49);
            this.edit.TabIndex = 38;
            this.edit.Text = "Редактировать";
            this.edit.UseVisualStyleBackColor = false;
            this.edit.Click += new System.EventHandler(this.edit_Click);
            // 
            // add
            // 
            this.add.BackColor = System.Drawing.Color.PowderBlue;
            this.add.Location = new System.Drawing.Point(13, 671);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(173, 49);
            this.add.TabIndex = 37;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = false;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.PowderBlue;
            this.back.Location = new System.Drawing.Point(1005, 671);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(173, 49);
            this.back.TabIndex = 36;
            this.back.Text = "В меню";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // labRowCount
            // 
            this.labRowCount.AutoSize = true;
            this.labRowCount.Location = new System.Drawing.Point(9, 627);
            this.labRowCount.Name = "labRowCount";
            this.labRowCount.Size = new System.Drawing.Size(197, 24);
            this.labRowCount.TabIndex = 35;
            this.labRowCount.Text = "Количество записей: ";
            // 
            // tabNumber
            // 
            this.tabNumber.Enabled = false;
            this.tabNumber.Location = new System.Drawing.Point(372, 6);
            this.tabNumber.MaxLength = 30;
            this.tabNumber.Multiline = false;
            this.tabNumber.Name = "tabNumber";
            this.tabNumber.Size = new System.Drawing.Size(193, 34);
            this.tabNumber.TabIndex = 34;
            this.tabNumber.Text = "";
            this.tabNumber.WordWrap = false;
            this.tabNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tabNumber_KeyPress);
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AllowUserToAddRows = false;
            this.dataGridViewUsers.AllowUserToDeleteRows = false;
            this.dataGridViewUsers.AllowUserToResizeColumns = false;
            this.dataGridViewUsers.AllowUserToResizeRows = false;
            this.dataGridViewUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUsers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewUsers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewUsers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(13, 115);
            this.dataGridViewUsers.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewUsers.MultiSelect = false;
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.ReadOnly = true;
            this.dataGridViewUsers.Size = new System.Drawing.Size(1165, 507);
            this.dataGridViewUsers.TabIndex = 33;
            this.dataGridViewUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUsers_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 24);
            this.label3.TabIndex = 43;
            this.label3.Text = "Роль";
            // 
            // role
            // 
            this.role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.role.FormattingEnabled = true;
            this.role.Location = new System.Drawing.Point(71, 60);
            this.role.Name = "role";
            this.role.Size = new System.Drawing.Size(266, 32);
            this.role.TabIndex = 44;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 24);
            this.label4.TabIndex = 46;
            this.label4.Text = "Логин";
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(433, 58);
            this.login.MaxLength = 30;
            this.login.Multiline = false;
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(293, 34);
            this.login.TabIndex = 45;
            this.login.Text = "";
            this.login.WordWrap = false;
            this.login.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.login_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(747, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 24);
            this.label5.TabIndex = 48;
            this.label5.Text = "Пароль";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(829, 58);
            this.password.MaxLength = 30;
            this.password.Multiline = false;
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(351, 34);
            this.password.TabIndex = 47;
            this.password.Text = "";
            this.password.WordWrap = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Location = new System.Drawing.Point(12, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 34);
            this.button1.TabIndex = 49;
            this.button1.Text = "Сотрудник";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1193, 733);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.login);
            this.Controls.Add(this.role);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.fio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.add);
            this.Controls.Add(this.back);
            this.Controls.Add(this.labRowCount);
            this.Controls.Add(this.tabNumber);
            this.Controls.Add(this.dataGridViewUsers);
            this.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "UsersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Пользователи";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UsersForm_FormClosed);
            this.Load += new System.EventHandler(this.UsersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox fio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button edit;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Label labRowCount;
        private System.Windows.Forms.RichTextBox tabNumber;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox role;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox login;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox password;
        private System.Windows.Forms.Button button1;
    }
}