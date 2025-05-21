namespace Hospital.Справочники
{
    partial class Benefit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Benefit));
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridViewBenefit = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRecordCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBenefit)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.PowderBlue;
            this.button4.Location = new System.Drawing.Point(14, 529);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(341, 49);
            this.button4.TabIndex = 45;
            this.button4.Text = "В справочники";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PowderBlue;
            this.button3.Location = new System.Drawing.Point(14, 466);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(341, 49);
            this.button3.TabIndex = 46;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Location = new System.Drawing.Point(14, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(341, 49);
            this.button1.TabIndex = 47;
            this.button1.Text = "Редактировать";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.Location = new System.Drawing.Point(15, 356);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(341, 49);
            this.button2.TabIndex = 44;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridViewBenefit
            // 
            this.dataGridViewBenefit.AllowUserToAddRows = false;
            this.dataGridViewBenefit.AllowUserToDeleteRows = false;
            this.dataGridViewBenefit.AllowUserToResizeColumns = false;
            this.dataGridViewBenefit.AllowUserToResizeRows = false;
            this.dataGridViewBenefit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBenefit.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewBenefit.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBenefit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewBenefit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBenefit.Location = new System.Drawing.Point(15, 46);
            this.dataGridViewBenefit.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewBenefit.MultiSelect = false;
            this.dataGridViewBenefit.Name = "dataGridViewBenefit";
            this.dataGridViewBenefit.ReadOnly = true;
            this.dataGridViewBenefit.Size = new System.Drawing.Size(340, 276);
            this.dataGridViewBenefit.TabIndex = 43;
            this.dataGridViewBenefit.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBenefit_CellClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(86, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(269, 31);
            this.textBox1.TabIndex = 42;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 24);
            this.label1.TabIndex = 41;
            this.label1.Text = "Льгота";
            // 
            // labelRecordCount
            // 
            this.labelRecordCount.AutoSize = true;
            this.labelRecordCount.Location = new System.Drawing.Point(10, 327);
            this.labelRecordCount.Name = "labelRecordCount";
            this.labelRecordCount.Size = new System.Drawing.Size(59, 24);
            this.labelRecordCount.TabIndex = 48;
            this.labelRecordCount.Text = "label2";
            // 
            // Benefit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(369, 583);
            this.Controls.Add(this.labelRecordCount);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridViewBenefit);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Benefit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Льготы";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.benefit_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBenefit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridViewBenefit;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelRecordCount;
    }
}