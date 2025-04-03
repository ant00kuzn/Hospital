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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeesForm));
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.searchTextBox = new System.Windows.Forms.RichTextBox();
            this.roleFilterComboBox = new System.Windows.Forms.ComboBox();
            this.labRowCount = new System.Windows.Forms.Label();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            this.dataGridViewEmployees.Location = new System.Drawing.Point(11, 54);
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
            this.searchTextBox.Location = new System.Drawing.Point(11, 10);
            this.searchTextBox.MaxLength = 30;
            this.searchTextBox.Multiline = false;
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(610, 34);
            this.searchTextBox.TabIndex = 1;
            this.searchTextBox.Text = "";
            this.searchTextBox.WordWrap = false;
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            this.searchTextBox.Enter += new System.EventHandler(this.searchTextBox_Enter);
            this.searchTextBox.Leave += new System.EventHandler(this.searchTextBox_Leave);
            // 
            // roleFilterComboBox
            // 
            this.roleFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roleFilterComboBox.FormattingEnabled = true;
            this.roleFilterComboBox.Location = new System.Drawing.Point(627, 10);
            this.roleFilterComboBox.Name = "roleFilterComboBox";
            this.roleFilterComboBox.Size = new System.Drawing.Size(382, 32);
            this.roleFilterComboBox.TabIndex = 2;
            this.roleFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.RoleFilterComboBox_SelectedIndexChanged);
            // 
            // labRowCount
            // 
            this.labRowCount.AutoSize = true;
            this.labRowCount.Location = new System.Drawing.Point(7, 566);
            this.labRowCount.Name = "labRowCount";
            this.labRowCount.Size = new System.Drawing.Size(197, 24);
            this.labRowCount.TabIndex = 3;
            this.labRowCount.Text = "Количество записей: ";
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.Image = ((System.Drawing.Image)(resources.GetObject("buttonNextPage.Image")));
            this.buttonNextPage.Location = new System.Drawing.Point(171, 590);
            this.buttonNextPage.MaximumSize = new System.Drawing.Size(33, 33);
            this.buttonNextPage.MinimumSize = new System.Drawing.Size(33, 33);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(33, 33);
            this.buttonNextPage.TabIndex = 18;
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.BackColor = System.Drawing.SystemColors.Window;
            this.buttonPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("buttonPreviousPage.Image")));
            this.buttonPreviousPage.Location = new System.Drawing.Point(12, 590);
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.Size = new System.Drawing.Size(33, 33);
            this.buttonPreviousPage.TabIndex = 17;
            this.buttonPreviousPage.UseVisualStyleBackColor = false;
            this.buttonPreviousPage.Click += new System.EventHandler(this.buttonPreviousPage_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.BackColor = System.Drawing.Color.PowderBlue;
            this.toolTip1.ForeColor = System.Drawing.Color.Black;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Помощь";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1182, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 24);
            this.label1.TabIndex = 20;
            this.label1.Text = "?";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // button1
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonNextPage);
            this.Controls.Add(this.buttonPreviousPage);
            this.Controls.Add(this.labRowCount);
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
            this.Text = "Сотрудники";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EmployeesForm_FormClosed);
            this.Load += new System.EventHandler(this.EmployeesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.RichTextBox searchTextBox;
        private System.Windows.Forms.ComboBox roleFilterComboBox;
        private System.Windows.Forms.Label labRowCount;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}