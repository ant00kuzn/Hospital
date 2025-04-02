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
            this.buttonBeds = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEmployees
            // 
            this.buttonEmployees.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonEmployees.FlatAppearance.BorderSize = 0;
            this.buttonEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEmployees.ForeColor = System.Drawing.Color.Black;
            this.buttonEmployees.Location = new System.Drawing.Point(15, 13);
            this.buttonEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEmployees.Name = "buttonEmployees";
            this.buttonEmployees.Size = new System.Drawing.Size(177, 38);
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
            this.buttonPatients.Location = new System.Drawing.Point(15, 68);
            this.buttonPatients.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPatients.Name = "buttonPatients";
            this.buttonPatients.Size = new System.Drawing.Size(177, 38);
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
            this.buttonHospitalizations.Location = new System.Drawing.Point(15, 123);
            this.buttonHospitalizations.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHospitalizations.Name = "buttonHospitalizations";
            this.buttonHospitalizations.Size = new System.Drawing.Size(177, 38);
            this.buttonHospitalizations.TabIndex = 2;
            this.buttonHospitalizations.Text = "Госпитализации";
            this.buttonHospitalizations.UseVisualStyleBackColor = false;
            this.buttonHospitalizations.Visible = false;
            this.buttonHospitalizations.Click += new System.EventHandler(this.buttonHospitalizations_Click);
            // 
            // buttonBeds
            // 
            this.buttonBeds.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonBeds.FlatAppearance.BorderSize = 0;
            this.buttonBeds.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBeds.ForeColor = System.Drawing.Color.Black;
            this.buttonBeds.Location = new System.Drawing.Point(15, 178);
            this.buttonBeds.Margin = new System.Windows.Forms.Padding(4);
            this.buttonBeds.Name = "buttonBeds";
            this.buttonBeds.Size = new System.Drawing.Size(177, 38);
            this.buttonBeds.TabIndex = 3;
            this.buttonBeds.Text = "Коечные места";
            this.buttonBeds.UseVisualStyleBackColor = false;
            this.buttonBeds.Visible = false;
            this.buttonBeds.Click += new System.EventHandler(this.buttonBeds_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(15, 313);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "Выход";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(15, 233);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 38);
            this.button2.TabIndex = 3;
            this.button2.Text = "Справочники";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Hospital.Properties.Resources.settings;
            this.pictureBox1.Location = new System.Drawing.Point(617, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Hospital.Properties.Resources.isometric_hospital_building_and_;
            this.pictureBox2.Location = new System.Drawing.Point(256, 32);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(330, 300);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(649, 365);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonEmployees);
            this.Controls.Add(this.buttonPatients);
            this.Controls.Add(this.buttonHospitalizations);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonBeds);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(665, 404);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(665, 404);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главная форма";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonEmployees;
        private System.Windows.Forms.Button buttonPatients;
        private System.Windows.Forms.Button buttonHospitalizations;
        private System.Windows.Forms.Button buttonBeds;
        private System.Windows.Forms.Button button1;
        private Button button2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}