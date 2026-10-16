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
    public partial class Patient : UserControl
    {
        private DataAccess Da { get; set; }
        private DataSet Ds { get; set; }
        private string Sql { get; set; }
        public Patient()
        {
            InitializeComponent();
            Da = new DataAccess();
            PopulateGridView();
            GeneratePatientID();
        }
        private void PopulateGridView(string sql = "select * from patientList;")
        {
            this.Ds = this.Da.ExecuteQuery(sql);
            this.dgvPatientList.AutoGenerateColumns = false;
            this.dgvPatientList.DataSource = this.Ds.Tables[0];
        }

        private void GeneratePatientID()
        {
            this.Sql = "select * from patientList order by patientId desc;";
            DataTable Dt = this.Da.ExecuteQueryTable(this.Sql);

            if (Dt.Rows.Count == 0)
            {
                this.txtPatientID.Text = "P0001";
                return;
            }

            string id = Dt.Rows[0]["patientId"].ToString();
            string[] str = id.Split('P');
            int n = Convert.ToInt32(str[1]);
            this.txtPatientID.Text = "P" + (++n).ToString("D4");
        }
        private void ClearAll()
        {
            this.txtPatientID.Clear();
            this.txtPatientID.ReadOnly = true;
            this.txtName.Clear();
            this.txtAge.Clear();
            this.txtContact.Clear();
            this.txtAddress.Clear();
            this.txtBloodGroup.Clear();
            this.txtSearch.Clear();
            GeneratePatientID();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGridView();
            ClearAll();
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.Sql = "select * from patientList where name like '" + this.txtSearch.Text + "%';";
            this.PopulateGridView(this.Sql);
        }


        private void dgvPatientList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPatientList.CurrentRow == null)
            {
                MessageBox.Show("Please select a patient first.");
                return;
            }
            if (dgvPatientList.CurrentRow != null)
            {
                try
                {
                    this.txtPatientID.ReadOnly = true;
                    this.txtPatientID.Text = dgvPatientList.CurrentRow.Cells["patientId"].Value?.ToString();
                    this.txtName.Text = dgvPatientList.CurrentRow.Cells["name"].Value?.ToString();
                    this.txtAge.Text = dgvPatientList.CurrentRow.Cells["age"].Value?.ToString();
                    this.txtContact.Text = dgvPatientList.CurrentRow.Cells["contact"].Value?.ToString();
                    this.txtAddress.Text = dgvPatientList.CurrentRow.Cells["address"].Value?.ToString();
                    this.txtBloodGroup.Text = dgvPatientList.CurrentRow.Cells["bloodGroup"].Value?.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading patient details: " + ex.Message);
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                this.Sql = "select * from patientList where patientId = '" + this.txtPatientID.Text + "'";
                this.Ds = this.Da.ExecuteQuery(this.Sql);
                if (this.Ds.Tables[0].Rows.Count == 1)
                {
                    this.Sql = @"update patientList
                    set name = '" + this.txtName.Text + @"',
                    age = " + this.txtAge.Text + @",
                    contact = '" + this.txtContact.Text + @"',
                    address = '" + this.txtAddress.Text + @"',
                    bloodGroup = '" + this.txtBloodGroup.Text + @"'
                    where patientId = '" + this.txtPatientID.Text + "';";
                    int count = this.Da.ExecuteUpdateQuery(this.Sql);
                    if (count == 1)
                    {
                        MessageBox.Show(this.txtName.Text + " has been updated properly");
                    }
                    else
                    {
                        MessageBox.Show("Patient data updation failed");
                    }
                }
                else
                {
                    this.Sql = @"insert into patientList
                    values ('" + this.txtPatientID.Text + "', '" + this.txtName.Text + "', '" + this.txtAge.Text + "' , '" + this.txtContact.Text + "', '" + this.txtAddress.Text + "', '" + this.txtBloodGroup.Text + "'  );";

                    int count = this.Da.ExecuteUpdateQuery(this.Sql);
                    if (count == 1)
                    {
                        MessageBox.Show(this.txtName.Text + " has been added properly");
                    }
                    else
                    {
                        MessageBox.Show("Patient data insertion failed");
                    }
                }
                this.PopulateGridView();
                this.ClearAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured during saving the patient data\n\n" + exc.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = this.dgvPatientList.CurrentRow.Cells["patientId"].Value.ToString();
                string name = this.dgvPatientList.CurrentRow.Cells["name"].Value.ToString();

                this.Sql = @"delete from patientList
                            where patientId = '" + id + "';";
                int count = this.Da.ExecuteUpdateQuery(this.Sql);
                if (count == 1)
                {
                    MessageBox.Show(name + " has been deleted");
                }
                else
                {
                    MessageBox.Show("Patient data deletion failed");
                }
                this.PopulateGridView();
                this.ClearAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured during deletion\n" + exc.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}