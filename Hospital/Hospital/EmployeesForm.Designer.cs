namespace Hospital
{
    partial class EmployeesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeesForm));
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.tabNumber = new System.Windows.Forms.RichTextBox();
            this.labRowCount = new System.Windows.Forms.Label();
            this.back = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.edit = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fio = new System.Windows.Forms.RichTextBox();
            this.phone = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.post = new System.Windows.Forms.RichTextBox();
            this.dbrd = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.photo = new System.Windows.Forms.PictureBox();
            this.changePhoto = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.AllowUserToAddRows = false;
            this.dataGridViewEmployees.AllowUserToDeleteRows = false;
            this.dataGridViewEmployees.AllowUserToResizeColumns = false;
            this.dataGridViewEmployees.AllowUserToResizeRows = false;
            this.dataGridViewEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmployees.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewEmployees.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEmployees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(6, 234);
            this.dataGridViewEmployees.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewEmployees.MultiSelect = false;
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(1251, 507);
            this.dataGridViewEmployees.TabIndex = 0;
            this.dataGridViewEmployees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEmployees_CellClick);
            this.dataGridViewEmployees.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEmployees_CellDoubleClick);
            // 
            // tabNumber
            // 
            this.tabNumber.Location = new System.Drawing.Point(178, 12);
            this.tabNumber.MaxLength = 8;
            this.tabNumber.Multiline = false;
            this.tabNumber.Name = "tabNumber";
            this.tabNumber.Size = new System.Drawing.Size(193, 34);
            this.tabNumber.TabIndex = 1;
            this.tabNumber.Text = "";
            this.tabNumber.WordWrap = false;
            this.tabNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tabNumber_KeyPress);
            // 
            // labRowCount
            // 
            this.labRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labRowCount.AutoSize = true;
            this.labRowCount.Location = new System.Drawing.Point(2, 746);
            this.labRowCount.Name = "labRowCount";
            this.labRowCount.Size = new System.Drawing.Size(197, 24);
            this.labRowCount.TabIndex = 3;
            this.labRowCount.Text = "Количество записей: ";
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.PowderBlue;
            this.back.Location = new System.Drawing.Point(1024, 790);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(173, 49);
            this.back.TabIndex = 19;
            this.back.Text = "В меню";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.button1_Click);
            // 
            // add
            // 
            this.add.BackColor = System.Drawing.Color.PowderBlue;
            this.add.Location = new System.Drawing.Point(6, 790);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(173, 49);
            this.add.TabIndex = 21;
            this.add.Text = "Добавить";
            this.add.UseVisualStyleBackColor = false;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // edit
            // 
            this.edit.BackColor = System.Drawing.Color.PowderBlue;
            this.edit.Enabled = false;
            this.edit.Location = new System.Drawing.Point(198, 790);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(173, 49);
            this.edit.TabIndex = 21;
            this.edit.Text = "Редактировать";
            this.edit.UseVisualStyleBackColor = false;
            this.edit.Click += new System.EventHandler(this.edit_Click);
            // 
            // delete
            // 
            this.delete.BackColor = System.Drawing.Color.PowderBlue;
            this.delete.Enabled = false;
            this.delete.Location = new System.Drawing.Point(394, 790);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(173, 49);
            this.delete.TabIndex = 21;
            this.delete.Text = "Удалить";
            this.delete.UseVisualStyleBackColor = false;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1182, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 24);
            this.label1.TabIndex = 22;
            this.label1.Text = "Табельный номер";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 24;
            this.label2.Text = "ФИО";
            // 
            // fio
            // 
            this.fio.Location = new System.Drawing.Point(70, 70);
            this.fio.MaxLength = 30;
            this.fio.Multiline = false;
            this.fio.Name = "fio";
            this.fio.Size = new System.Drawing.Size(749, 34);
            this.fio.TabIndex = 3;
            this.fio.Text = "";
            this.fio.WordWrap = false;
            this.fio.Leave += new System.EventHandler(this.fio_Leave);
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(104, 127);
            this.phone.Mask = "+7 000 000-00-00";
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(218, 31);
            this.phone.TabIndex = 4;
            this.phone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.phone_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "Телефон";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(381, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 24);
            this.label6.TabIndex = 32;
            this.label6.Text = "Должность";
            // 
            // post
            // 
            this.post.Location = new System.Drawing.Point(494, 126);
            this.post.MaxLength = 30;
            this.post.Multiline = false;
            this.post.Name = "post";
            this.post.Size = new System.Drawing.Size(325, 34);
            this.post.TabIndex = 5;
            this.post.Text = "";
            this.post.WordWrap = false;
            // 
            // dbrd
            // 
            this.dbrd.Location = new System.Drawing.Point(149, 183);
            this.dbrd.MaxDate = new System.DateTime(2007, 1, 1, 0, 0, 0, 0);
            this.dbrd.Name = "dbrd";
            this.dbrd.Size = new System.Drawing.Size(200, 31);
            this.dbrd.TabIndex = 6;
            this.dbrd.Value = new System.DateTime(2007, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 24);
            this.label4.TabIndex = 37;
            this.label4.Text = "Дата рождения";
            // 
            // photo
            // 
            this.photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.photo.Image = global::Hospital.Properties.Resources.doctor_linear_icon_vector_285420161;
            this.photo.InitialImage = null;
            this.photo.Location = new System.Drawing.Point(857, 15);
            this.photo.Name = "photo";
            this.photo.Size = new System.Drawing.Size(202, 172);
            this.photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.photo.TabIndex = 38;
            this.photo.TabStop = false;
            // 
            // changePhoto
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Location = new System.Drawing.Point(1027, 583);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 33);
            this.button1.TabIndex = 19;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EmployeesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1212, 628);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.add);
            this.Controls.Add(this.back);
            this.Controls.Add(this.labRowCount);
            this.Controls.Add(this.tabNumber);
            this.Controls.Add(this.dataGridViewEmployees);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximumSize = new System.Drawing.Size(1920, 1280);
            this.MinimumSize = new System.Drawing.Size(1290, 663);
            this.Name = "EmployeesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сотрудники";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EmployeesForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmployeesForm_FormClosed);
            this.Load += new System.EventHandler(this.EmployeesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.RichTextBox tabNumber;
        private System.Windows.Forms.Label labRowCount;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button edit;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox fio;
        private System.Windows.Forms.MaskedTextBox phone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox post;
        private System.Windows.Forms.DateTimePicker dbrd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox photo;
        private System.Windows.Forms.Button changePhoto;
    }
}