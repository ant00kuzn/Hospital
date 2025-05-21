
namespace Hospital
{
    partial class DatabaseImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseImport));
            this.label1 = new System.Windows.Forms.Label();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRepairStructure = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.btnImportTable = new System.Windows.Forms.Button();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.btnExportTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "Таблица";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(198, 151);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(278, 31);
            this.tbFileName.TabIndex = 9;
            this.tbFileName.TextChanged += new System.EventHandler(this.tbFileName_TextChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.PowderBlue;
            this.btnExit.Location = new System.Drawing.Point(12, 225);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 39);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRepairStructure
            // 
            this.btnRepairStructure.BackColor = System.Drawing.Color.PowderBlue;
            this.btnRepairStructure.Location = new System.Drawing.Point(12, 59);
            this.btnRepairStructure.Name = "btnRepairStructure";
            this.btnRepairStructure.Size = new System.Drawing.Size(470, 39);
            this.btnRepairStructure.TabIndex = 7;
            this.btnRepairStructure.Text = "Восстановить структуру базы данных";
            this.btnRepairStructure.UseVisualStyleBackColor = false;
            this.btnRepairStructure.Click += new System.EventHandler(this.btnRepairStructure_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackColor = System.Drawing.Color.PowderBlue;
            this.btnSelectFile.Enabled = false;
            this.btnSelectFile.Location = new System.Drawing.Point(12, 151);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(170, 31);
            this.btnSelectFile.TabIndex = 10;
            this.btnSelectFile.Text = "Выбрать файл";
            this.btnSelectFile.UseVisualStyleBackColor = false;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnImportTable
            // 
            this.btnImportTable.BackColor = System.Drawing.Color.PowderBlue;
            this.btnImportTable.Enabled = false;
            this.btnImportTable.Location = new System.Drawing.Point(312, 225);
            this.btnImportTable.Name = "btnImportTable";
            this.btnImportTable.Size = new System.Drawing.Size(170, 39);
            this.btnImportTable.TabIndex = 11;
            this.btnImportTable.Text = "Импортировать";
            this.btnImportTable.UseVisualStyleBackColor = false;
            this.btnImportTable.Click += new System.EventHandler(this.btnImportTable_Click);
            // 
            // cbTables
            // 
            this.cbTables.Enabled = false;
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(135, 104);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(341, 32);
            this.cbTables.TabIndex = 8;
            this.cbTables.SelectedIndexChanged += new System.EventHandler(this.cbTables_SelectedIndexChanged);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(470, 39);
            this.button1.TabIndex = 7;
            this.button1.Text = "Выполнить резервное копирование";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExportTable
            // 
            this.btnExportTable.BackColor = System.Drawing.Color.PowderBlue;
            this.btnExportTable.Enabled = false;
            this.btnExportTable.Location = new System.Drawing.Point(136, 225);
            this.btnExportTable.Name = "btnExportTable";
            this.btnExportTable.Size = new System.Drawing.Size(170, 39);
            this.btnExportTable.TabIndex = 11;
            this.btnExportTable.Text = "Экспортировать";
            this.btnExportTable.UseVisualStyleBackColor = false;
            this.btnExportTable.Click += new System.EventHandler(this.btnExportTable_Click);
            // 
            // DatabaseImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 269);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnRepairStructure);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnExportTable);
            this.Controls.Add(this.btnImportTable);
            this.Controls.Add(this.cbTables);
            this.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "DatabaseImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление базой данных";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DatabseImport_FormClosed);
            this.Load += new System.EventHandler(this.DatabseImport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRepairStructure;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Button btnImportTable;
        private System.Windows.Forms.ComboBox cbTables;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnExportTable;
    }
}