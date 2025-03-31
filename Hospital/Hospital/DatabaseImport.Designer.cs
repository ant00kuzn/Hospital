
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "Таблица";
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(198, 129);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(278, 31);
            this.tbFileName.TabIndex = 9;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.PowderBlue;
            this.btnExit.Location = new System.Drawing.Point(12, 203);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 39);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // btnRepairStructure
            // 
            this.btnRepairStructure.BackColor = System.Drawing.Color.PowderBlue;
            this.btnRepairStructure.Location = new System.Drawing.Point(12, 12);
            this.btnRepairStructure.Name = "btnRepairStructure";
            this.btnRepairStructure.Size = new System.Drawing.Size(470, 39);
            this.btnRepairStructure.TabIndex = 7;
            this.btnRepairStructure.Text = "Восстановить структуру базы данных";
            this.btnRepairStructure.UseVisualStyleBackColor = false;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackColor = System.Drawing.Color.PowderBlue;
            this.btnSelectFile.Enabled = false;
            this.btnSelectFile.Location = new System.Drawing.Point(12, 129);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(170, 31);
            this.btnSelectFile.TabIndex = 10;
            this.btnSelectFile.Text = "Выбрать файл";
            this.btnSelectFile.UseVisualStyleBackColor = false;
            // 
            // btnImportTable
            // 
            this.btnImportTable.BackColor = System.Drawing.Color.PowderBlue;
            this.btnImportTable.Enabled = false;
            this.btnImportTable.Location = new System.Drawing.Point(312, 203);
            this.btnImportTable.Name = "btnImportTable";
            this.btnImportTable.Size = new System.Drawing.Size(170, 39);
            this.btnImportTable.TabIndex = 11;
            this.btnImportTable.Text = "Импортировать";
            this.btnImportTable.UseVisualStyleBackColor = false;
            // 
            // cbTables
            // 
            this.cbTables.Enabled = false;
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(135, 82);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(341, 32);
            this.cbTables.TabIndex = 8;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // DatabaseImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 252);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRepairStructure);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnImportTable);
            this.Controls.Add(this.cbTables);
            this.Font = new System.Drawing.Font("Garamond", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(504, 291);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(504, 291);
            this.Name = "DatabaseImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DatabaseImport";
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
    }
}