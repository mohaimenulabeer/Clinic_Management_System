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
    public partial class Doctor : UserControl
    {
        private DataAccess Da { get; set; }
        private DataSet Ds { get; set; }
        private string Sql { get; set; }
        public Doctor()
        {
            InitializeComponent();
            Da = new DataAccess();
            PopulateGridView();
            GenerateDoctorID();
        }
        private void PopulateGridView(string sql = "select * from doctorList;")
        {
            this.Ds = this.Da.ExecuteQuery(sql);
            this.dgvDoctorList.AutoGenerateColumns = false;
            this.dgvDoctorList.DataSource = this.Ds.Tables[0];
        }
        private void GenerateDoctorID()
        {
            this.Sql = "select * from doctorList order by doctorId desc;";
            DataTable Dt = this.Da.ExecuteQueryTable(this.Sql);

            if (Dt.Rows.Count == 0)
            {
                this.txtDoctorID.Text = "D0001";
                return;
            }

            string id = Dt.Rows[0]["doctorId"].ToString();
            string[] str = id.Split('D');
            int n = Convert.ToInt32(str[1]);
            this.txtDoctorID.Text = "D" + (++n).ToString("D4");
        }
        private void ClearAll()
        {
            this.txtDoctorID.Clear();
            this.txtDoctorID.ReadOnly = true;
            this.txtName.Clear();
            this.txtAge.Clear();
            this.txtDegree.Clear();
            this.txtSpecialist.Clear();
            this.txtContact.Clear();
            this.txtSearch.Clear();
            GenerateDoctorID();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGridView();
            ClearAll();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.Sql = "select * from doctorList where doctorName like '" + this.txtSearch.Text + "%';";
            this.PopulateGridView(this.Sql);
        }
        private void dgvDoctorList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvDoctorList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDoctorList.CurrentRow == null)
            {
                MessageBox.Show("Please select a doctor first.");
                return;
            }
            if (dgvDoctorList.CurrentRow != null)
            {
                try
                {
                    this.txtDoctorID.ReadOnly = true;
                    this.txtDoctorID.Text = dgvDoctorList.CurrentRow.Cells["doctorId"].Value?.ToString();
                    this.txtName.Text = dgvDoctorList.CurrentRow.Cells["doctorName"].Value?.ToString();
                    this.txtAge.Text = dgvDoctorList.CurrentRow.Cells["age"].Value?.ToString();
                    this.txtDegree.Text = dgvDoctorList.CurrentRow.Cells["degree"].Value?.ToString();
                    this.txtSpecialist.Text = dgvDoctorList.CurrentRow.Cells["specialist"].Value?.ToString();
                    this.txtContact.Text = dgvDoctorList.CurrentRow.Cells["contact"].Value?.ToString();
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
                this.Sql = "select * from doctorList where doctorId = '" + this.txtDoctorID.Text + "'";
                this.Ds = this.Da.ExecuteQuery(this.Sql);
                if (this.Ds != null && this.Ds.Tables.Count > 0 && this.Ds.Tables[0].Rows.Count == 1)
                {
                    this.Sql = @"update doctorList
                    set doctorName = '" + this.txtName.Text + @"',
                    age = " + this.txtAge.Text + @",
                    degree = '" + this.txtDegree.Text + @"',
                    specialist = '" + this.txtSpecialist.Text + @"',
                    contact = '" + this.txtContact.Text + @"'
                    where doctorId = '" + this.txtDoctorID.Text + "';";
                    int count = this.Da.ExecuteUpdateQuery(this.Sql);
                    if (count == 1)
                    {
                        MessageBox.Show(this.txtName.Text + " has been updated properly");
                    }
                    else
                    {
                        MessageBox.Show("Doctor data updation failed");
                    }
                }
                else
                {
                    this.Sql = @"insert into doctorList
                    values ('" + this.txtDoctorID.Text + "', '" + this.txtName.Text + "', '" + this.txtAge.Text + "' , '" + this.txtDegree.Text + "', '" + this.txtSpecialist.Text + "', '" + this.txtContact.Text + "'  );";

                    int count = this.Da.ExecuteUpdateQuery(this.Sql);
                    if (count == 1)
                    {
                        MessageBox.Show(this.txtName.Text + " has been added properly");
                    }
                    else
                    {
                        MessageBox.Show("Doctor data insertion failed");
                    }
                }
                this.PopulateGridView();
                this.ClearAll();
            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured during saving the doctor data\n\n" + exc.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = this.dgvDoctorList.CurrentRow.Cells["doctorId"].Value.ToString();
                string name = this.dgvDoctorList.CurrentRow.Cells["doctorName"].Value.ToString();

                this.Sql = @"delete from doctorList
                            where doctorId = '" + id + "';";
                int count = this.Da.ExecuteUpdateQuery(this.Sql);
                if (count == 1)
                {
                    MessageBox.Show(name + " has been deleted");
                }
                else
                {
                    MessageBox.Show("doctor data deletion failed");
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
