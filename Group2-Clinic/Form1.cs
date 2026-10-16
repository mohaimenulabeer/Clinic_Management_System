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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string sql = @"select * from login where userName = '" + this.txtUsername.Text + "' and password = '" + this.txtPassword.Text + "';";

            DataAccess da = new DataAccess();
            DataSet ds = da.ExecuteQuery(sql);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
            {
                int role = Convert.ToInt32(ds.Tables[0].Rows[0]["Role"]);
                if (role == 1)
                {
                    Admin admin = new Admin();
                    this.Hide();
                    admin.Show();
                }
                else if (role == 2)
                {
                    Receptionist receptionist = new Receptionist();
                    this.Hide();
                    receptionist.Show();
                }
                else if (role == 3)
                {
                    Form doctorForm = new Form();
                    doctorForm.Text = "Doctor Panel";
                    doctorForm.WindowState = FormWindowState.Maximized;
                    Doctor doctorUC = new Doctor();
                    doctorUC.Dock = DockStyle.Fill;
                    doctorForm.Controls.Add(doctorUC);
                    this.Hide();
                    doctorForm.Show();
                }
                else
                {
                    MessageBox.Show("Access Denied!", "Unauthorized",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password!","Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

    }

}