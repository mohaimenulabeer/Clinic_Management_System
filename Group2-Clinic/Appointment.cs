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
    public partial class Appointment : UserControl
    {
        private DataAccess Da { get; set; }
        private DataSet Ds { get; set; }
        private string Sql { get; set; }

        public Appointment()
        {
            InitializeComponent();
            Da = new DataAccess();

            PopulatePatientCombo();
            PopulateDoctorCombo();
            PopulateGridView();
            GenerateAppointmentID();

            cmbStatus.SelectedIndex = -1;
        }
        private void Appointment_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                PopulatePatientCombo();
                PopulateDoctorCombo();
                PopulateGridView();
            }
        }

        private void PopulateGridView(string sql = @"SELECT 
                    a.appointmentId,
                    a.patientId,
                    a.doctorId,
                    p.name AS patientName,
                    d.doctorName,
                    CONVERT(varchar(10), a.appointmentDate, 120) AS appointmentDate,
                    CONVERT(varchar(8), a.appointmentTime, 108) AS appointmentTime,
                    a.status
                FROM appointment a
                JOIN patientList p ON a.patientId = p.patientId
                JOIN doctorList d ON a.doctorId = d.doctorId;")
        {
            Ds = Da.ExecuteQuery(sql);
            dgvAppointmentList.AutoGenerateColumns = false;
            dgvAppointmentList.DataSource = Ds.Tables[0];
        }
        private void GenerateAppointmentID()
        {
            Sql = "SELECT appointmentId FROM appointment ORDER BY appointmentId DESC;";
            DataTable dt = Da.ExecuteQueryTable(Sql);

            if (dt.Rows.Count == 0)
            {
                txtAppointmentID.Text = "A0001";
                return;
            }

            string id = dt.Rows[0]["appointmentId"].ToString();
            int num = int.Parse(id.Substring(1));
            txtAppointmentID.Text = "A" + (++num).ToString("D4");
        }
        private void PopulatePatientCombo()
        {
            this.cmbPatientID.DataSource = null;
            this.cmbPatientID.Items.Clear();

            Sql = "SELECT patientId FROM patientList";
            DataTable dt = Da.ExecuteQueryTable(Sql);

            this.cmbPatientID.DataSource = dt;
            this.cmbPatientID.DisplayMember = "patientId";
            this.cmbPatientID.ValueMember = "patientId";
            this.cmbPatientID.SelectedIndex = -1;
        }

        private void PopulateDoctorCombo()
        {
            this.cmbDoctorID.DataSource = null;
            this.cmbDoctorID.Items.Clear();

            Sql = "SELECT doctorId FROM doctorList";
            DataTable dt = Da.ExecuteQueryTable(Sql);

            this.cmbDoctorID.DataSource = dt;
            this.cmbDoctorID.DisplayMember = "doctorId";
            this.cmbDoctorID.ValueMember = "doctorId";
            this.cmbDoctorID.SelectedIndex = -1;
        }
        private void ClearAll()
        {
            this.txtAppointmentID.Clear();
            this.txtAppointmentID.ReadOnly = true;
            this.cmbPatientID.SelectedIndex = -1;
            this.cmbDoctorID.SelectedIndex = -1;
            this.cmbStatus.SelectedIndex = -1;
            this.dtpAppointmentDate.Value = DateTime.Today;
            this.dtpAppointmentTime.Value = DateTime.Now;

            GenerateAppointmentID();
        }

        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                Sql = "SELECT * FROM appointment WHERE appointmentId = '" + txtAppointmentID.Text + "'";
                Ds = Da.ExecuteQuery(Sql);

                if (Ds.Tables[0].Rows.Count == 1)
                {
                    Sql = @"UPDATE appointment SET
                            patientId = '" + this.cmbPatientID.SelectedValue + @"',
                            doctorId = '" + this.cmbDoctorID.SelectedValue + @"',
                            appointmentDate = '" + dtpAppointmentDate.Value.ToString("yyyy-MM-dd") + @"',
                            appointmentTime = '" + dtpAppointmentTime.Value.ToString("HH:mm:ss") + @"',
                            status = '" + cmbStatus.Text + @"'
                            WHERE appointmentId = '" + txtAppointmentID.Text + "';";

                    Da.ExecuteUpdateQuery(Sql);
                    MessageBox.Show("Appointment updated successfully");
                }
                else
                {
                    Sql = @"INSERT INTO appointment VALUES(
                            '" + this.txtAppointmentID.Text + @"',
                            '" + this.cmbPatientID.SelectedValue + @"',
                            '" + this.cmbDoctorID.SelectedValue + @"',
                            '" + this.dtpAppointmentDate.Value.ToString("yyyy-MM-dd") + @"',
                            '" + this.dtpAppointmentTime.Value.ToString("HH:mm:ss") + @"',
                            '" + this.cmbStatus.Text + @"');";

                    Da.ExecuteUpdateQuery(Sql);
                    MessageBox.Show("Appointment added successfully");
                }

                PopulateGridView();
                PopulatePatientCombo();
                PopulateDoctorCombo();
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving appointment\n" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvAppointmentList.CurrentRow == null) return;

            string id = dgvAppointmentList.CurrentRow.Cells["appointmentId"].Value.ToString();

            Sql = "DELETE FROM appointment WHERE appointmentId = '" + id + "'";
            Da.ExecuteUpdateQuery(Sql);

            MessageBox.Show("Appointment deleted");
            PopulatePatientCombo();
            PopulateDoctorCombo();
            PopulateGridView();
            ClearAll();
        }
        private void dgvAppointmentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAppointmentList.CurrentRow == null) return;

            this.txtAppointmentID.Text = dgvAppointmentList.CurrentRow.Cells["appointmentId"].Value.ToString();
            this.cmbPatientID.SelectedValue = dgvAppointmentList.CurrentRow.Cells["patientId"].Value.ToString();
            this.cmbDoctorID.SelectedValue = dgvAppointmentList.CurrentRow.Cells["doctorId"].Value.ToString();
            this.dtpAppointmentDate.Value =DateTime.Parse(dgvAppointmentList.CurrentRow.Cells["appointmentDate"].Value.ToString());

            this.dtpAppointmentTime.Value = DateTime.ParseExact(
                    dgvAppointmentList.CurrentRow.Cells["appointmentTime"].Value.ToString(),
                    "HH:mm:ss",
                    null
                );
            this.cmbStatus.Text = dgvAppointmentList.CurrentRow.Cells["status"].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Sql = @" SELECT 
                         a.appointmentId,
                         a.patientId,
                         a.doctorId,
                         p.name AS patientName,
                         d.doctorName,
                         a.appointmentDate,
                         a.appointmentTime,
                         a.status
                 FROM appointment a
                 JOIN patientList p ON a.patientId = p.patientId
                 JOIN doctorList d ON a.doctorId = d.doctorId
                 WHERE p.name LIKE '" + txtSearch.Text + "%'";

            PopulateGridView(Sql);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulatePatientCombo();   
            PopulateDoctorCombo();
            PopulateGridView();
            ClearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
 }
        


