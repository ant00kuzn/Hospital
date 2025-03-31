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
            this.searchTextBox = new System.Windows.Forms.RichTextBox();
            this.roleFilterComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.AllowUserToAddRows = false;
            this.dataGridViewEmployees.AllowUserToDeleteRows = false;
            this.dataGridViewEmployees.AllowUserToResizeColumns = false;
            this.dataGridViewEmployees.AllowUserToResizeRows = false;
            this.dataGridViewEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmployees.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewEmployees.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewEmployees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(-6, -4);
            this.dataGridViewEmployees.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridViewEmployees.MultiSelect = false;
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(1191, 507);
            this.dataGridViewEmployees.TabIndex = 0;
            this.dataGridViewEmployees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEmployees_CellClick);
            this.dataGridViewEmployees.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEmployees_CellDoubleClick);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(2, 506);
            this.searchTextBox.MaxLength = 30;
            this.searchTextBox.Multiline = false;
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(610, 34);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.Text = "";
            this.searchTextBox.WordWrap = false;
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // roleFilterComboBox
            // 
            this.roleFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roleFilterComboBox.FormattingEnabled = true;
            this.roleFilterComboBox.Location = new System.Drawing.Point(618, 506);
            this.roleFilterComboBox.Name = "roleFilterComboBox";
            this.roleFilterComboBox.Size = new System.Drawing.Size(382, 32);
            this.roleFilterComboBox.TabIndex = 2;
            this.roleFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.RoleFilterComboBox_SelectedIndexChanged);
            // 
            // EmployeesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 543);
            this.Controls.Add(this.roleFilterComboBox);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.dataGridViewEmployees);
            this.Font = new System.Drawing.Font("Garamond", 15.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление сотрудниками";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmployeesForm_FormClosed);
            this.Load += new System.EventHandler(this.EmployeesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.RichTextBox searchTextBox;
        private System.Windows.Forms.ComboBox roleFilterComboBox;
    }
}