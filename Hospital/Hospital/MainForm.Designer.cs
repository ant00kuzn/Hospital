using System.Drawing;
using System.Windows.Forms;

namespace Hospital
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonEmployees = new System.Windows.Forms.Button();
            this.buttonPatients = new System.Windows.Forms.Button();
            this.buttonHospitalizations = new System.Windows.Forms.Button();
            this.buttonWards = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonGuides = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonUsers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEmployees
            // 
            this.buttonEmployees.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonEmployees.FlatAppearance.BorderSize = 0;
            this.buttonEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEmployees.ForeColor = System.Drawing.Color.Black;
            this.buttonEmployees.Location = new System.Drawing.Point(13, 300);
            this.buttonEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployees.Name = "buttonEmployees";
            this.buttonEmployees.Size = new System.Drawing.Size(198, 38);
            this.buttonEmployees.TabIndex = 0;
            this.buttonEmployees.Text = "Сотрудники";
            this.buttonEmployees.UseVisualStyleBackColor = false;
            this.buttonEmployees.Visible = false;
            this.buttonEmployees.Click += new System.EventHandler(this.buttonEmployees_Click);
            // 
            // buttonPatients
            // 
            this.buttonPatients.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonPatients.FlatAppearance.BorderSize = 0;
            this.buttonPatients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPatients.ForeColor = System.Drawing.Color.Black;
            this.buttonPatients.Location = new System.Drawing.Point(13, 24);
            this.buttonPatients.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPatients.Name = "buttonPatients";
            this.buttonPatients.Size = new System.Drawing.Size(198, 38);
            this.buttonPatients.TabIndex = 1;
            this.buttonPatients.Text = "Пациенты";
            this.buttonPatients.UseVisualStyleBackColor = false;
            this.buttonPatients.Visible = false;
            this.buttonPatients.Click += new System.EventHandler(this.buttonPatients_Click);
            // 
            // buttonHospitalizations
            // 
            this.buttonHospitalizations.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonHospitalizations.FlatAppearance.BorderSize = 0;
            this.buttonHospitalizations.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHospitalizations.ForeColor = System.Drawing.Color.Black;
            this.buttonHospitalizations.Location = new System.Drawing.Point(13, 79);
            this.buttonHospitalizations.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHospitalizations.Name = "buttonHospitalizations";
            this.buttonHospitalizations.Size = new System.Drawing.Size(198, 38);
            this.buttonHospitalizations.TabIndex = 2;
            this.buttonHospitalizations.Text = "Госпитализации";
            this.buttonHospitalizations.UseVisualStyleBackColor = false;
            this.buttonHospitalizations.Visible = false;
            this.buttonHospitalizations.Click += new System.EventHandler(this.buttonHospitalizations_Click);
            // 
            // buttonWards
            // 
            this.buttonWards.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonWards.FlatAppearance.BorderSize = 0;
            this.buttonWards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWards.ForeColor = System.Drawing.Color.Black;
            this.buttonWards.Location = new System.Drawing.Point(13, 134);
            this.buttonWards.Margin = new System.Windows.Forms.Padding(4);
            this.buttonWards.Name = "buttonWards";
            this.buttonWards.Size = new System.Drawing.Size(198, 38);
            this.buttonWards.TabIndex = 3;
            this.buttonWards.Text = "Палаты";
            this.buttonWards.UseVisualStyleBackColor = false;
            this.buttonWards.Visible = false;
            this.buttonWards.Click += new System.EventHandler(this.buttonBeds_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Location = new System.Drawing.Point(13, 384);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(198, 61);
            this.buttonExit.TabIndex = 4;
            this.buttonExit.Text = "Выйти из учетной записи";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonGuides
            // 
            this.buttonGuides.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonGuides.FlatAppearance.BorderSize = 0;
            this.buttonGuides.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGuides.ForeColor = System.Drawing.Color.Black;
            this.buttonGuides.Location = new System.Drawing.Point(13, 189);
            this.buttonGuides.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGuides.Name = "buttonGuides";
            this.buttonGuides.Size = new System.Drawing.Size(198, 38);
            this.buttonGuides.TabIndex = 3;
            this.buttonGuides.Text = "Справочники";
            this.buttonGuides.UseVisualStyleBackColor = false;
            this.buttonGuides.Visible = false;
            this.buttonGuides.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Hospital.Properties.Resources.isometric_hospital_building_and_;
            this.pictureBox2.Location = new System.Drawing.Point(273, 91);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(330, 300);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // buttonUsers
            // 
            this.buttonUsers.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonUsers.FlatAppearance.BorderSize = 0;
            this.buttonUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUsers.ForeColor = System.Drawing.Color.Black;
            this.buttonUsers.Location = new System.Drawing.Point(13, 245);
            this.buttonUsers.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(198, 38);
            this.buttonUsers.TabIndex = 7;
            this.buttonUsers.Text = "Пользователи";
            this.buttonUsers.UseVisualStyleBackColor = false;
            this.buttonUsers.Visible = false;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(273, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 24);
            this.label1.TabIndex = 11;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(273, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 24);
            this.label2.TabIndex = 10;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(13, 300);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 38);
            this.button1.TabIndex = 9;
            this.button1.Text = "Настройки";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(13, 300);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(198, 38);
            this.button2.TabIndex = 7;
            this.button2.Text = "Спец. возможности";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(659, 450);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonUsers);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonEmployees);
            this.Controls.Add(this.buttonPatients);
            this.Controls.Add(this.buttonHospitalizations);
            this.Controls.Add(this.buttonGuides);
            this.Controls.Add(this.buttonWards);
            this.Controls.Add(this.buttonExit);
            this.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(665, 404);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная форма";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEmployees;
        private System.Windows.Forms.Button buttonPatients;
        private System.Windows.Forms.Button buttonHospitalizations;
        private System.Windows.Forms.Button buttonWards;
        private System.Windows.Forms.Button buttonExit;
        private Button buttonGuides;
        private PictureBox pictureBox2;
        private Button buttonUsers;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
    }
}