using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group2_Clinic
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            patient1.Hide();
            doctor1.Hide();
            appointment1.Hide();
            bill1.Hide();
            medicalRecord1.Hide();
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            patient1.Show();
            patient1.BringToFront();
            doctor1.Hide();
            appointment1.Hide();
            bill1.Hide();
            medicalRecord1.Hide();
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            patient1.Hide();
            doctor1.Show();
            doctor1.BringToFront();
            appointment1.Hide();
            bill1.Hide();
            medicalRecord1.Hide();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            patient1.Hide();
            doctor1.Hide();
            appointment1.Show();
            appointment1.BringToFront();
            bill1.Hide();
            medicalRecord1.Hide();
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            patient1.Hide();
            doctor1.Hide();
            appointment1.Hide();
            bill1.Show();
            bill1.BringToFront();
            medicalRecord1.Hide();
        }

        private void btnMedicalRecord_Click(object sender, EventArgs e)
        {
            patient1.Hide();
            doctor1.Hide();
            appointment1.Hide();
            bill1.Hide();
            medicalRecord1.Show();
            medicalRecord1.BringToFront();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            this.Close();
            l.Show();
        }
    }
}
