using Hospital.Справочники;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting.Lifetime;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hospital
{
    public partial class GuidesForm : Form
    {
        public GuidesForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //отделения
            Department department = new Department();
            this.Hide();
            department.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //роли
            Role role = new Role();
            this.Hide();
            role.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //льготы
            Benefit benefit = new Benefit();
            this.Hide();
            benefit.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //диагнозы
            Diagnosis diagnosis = new Diagnosis();
            this.Hide();
            diagnosis.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //статусы
            PatientStatus status = new PatientStatus();
            this.Hide();
            status.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GuidesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["MainForm"] is MainForm main)
                main.Show();
        }
    }
}