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
    public partial class MedicalRecord : UserControl
    {
        private DataAccess Da { get; set; }
        private DataSet Ds { get; set; }
        private string Sql { get; set; }

        public MedicalRecord()
        {
            InitializeComponent();
            Da = new DataAccess();

            PopulatePatientCombo();
            PopulateDoctorCombo();
            PopulateGridView();
            GenerateMedicalRecordID();
        }

        private void MedicalRecord_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                PopulatePatientCombo();
                PopulateDoctorCombo();
                PopulateGridView();
            }
        }
        private void PopulateGridView(string sql = @"SELECT
                m.medicalId,
                m.patientId,
                m.doctorId,
                p.name AS patientName,
                d.doctorName,
                m.diagnosis,
                m.prescription,
                m.treatment
            FROM medicalRecord m
            JOIN patientList p ON m.patientId = p.patientId
            JOIN doctorList d ON m.doctorId = d.doctorId")
        {
            Ds = Da.ExecuteQuery(sql);
            dgvMedicalRecordList.AutoGenerateColumns = false;
            dgvMedicalRecordList.DataSource = Ds.Tables[0];
        }
        private void GenerateMedicalRecordID()
        {
            Sql = "SELECT medicalId FROM medicalRecord ORDER BY medicalId DESC";
            DataTable dt = Da.ExecuteQueryTable(Sql);

            if (dt.Rows.Count == 0)
            {
                txtMedicalID.Text = "M0001";
                return;
            }

            string id = dt.Rows[0]["medicalId"].ToString();
            int num = int.Parse(id.Substring(1));
            txtMedicalID.Text = "M" + (++num).ToString("D4");
        }
        private void PopulatePatientCombo()
        {
            this.cmbPatient.DataSource = null;
            this.cmbPatient.Items.Clear();

            Sql = "SELECT patientId FROM patientList";
            DataTable dt = Da.ExecuteQueryTable(Sql);

            this.cmbPatient.DataSource = dt;
            this.cmbPatient.DisplayMember = "patientId";
            this.cmbPatient.ValueMember = "patientId";
            this.cmbPatient.SelectedIndex = -1;
        }

        private void PopulateDoctorCombo()
        {
            this.cmbDoctor.DataSource = null;
            this.cmbDoctor.Items.Clear();

            Sql = "SELECT doctorId FROM doctorList";
            DataTable dt = Da.ExecuteQueryTable(Sql);

            this.cmbDoctor.DataSource = dt;
            this.cmbDoctor.DisplayMember = "doctorId";
            this.cmbDoctor.ValueMember = "doctorId";
            this.cmbDoctor.SelectedIndex = -1;
        }
        private void ClearAll()
        {
            this.txtMedicalID.Clear();
            this.txtMedicalID.ReadOnly = true;
            this.cmbPatient.SelectedIndex = -1;
            this.cmbDoctor.SelectedIndex = -1;
            this.txtDiagnosis.Clear();
            this.txtPrescription.Clear();
            this.txtTreatment.Clear();

            GenerateMedicalRecordID();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Sql = "SELECT * FROM medicalRecord WHERE medicalId = '" + txtMedicalID.Text + "'";
                Ds = Da.ExecuteQuery(Sql);

                if (Ds.Tables[0].Rows.Count == 1)
                {
                    Sql = @"UPDATE medicalRecord SET
                            patientId = '" + cmbPatient.SelectedValue + @"',
                            doctorId = '" + cmbDoctor.SelectedValue + @"',
                            diagnosis = '" + txtDiagnosis.Text + @"',
                            prescription = '" + txtPrescription.Text + @"',
                            treatment = '" + txtTreatment.Text + @"'
                            WHERE medicalId = '" + txtMedicalID.Text + "'";

                    Da.ExecuteUpdateQuery(Sql);
                    MessageBox.Show("Medical record updated successfully");
                }
                else
                {
                    Sql = @"INSERT INTO medicalRecord VALUES(
                            '" + txtMedicalID.Text + @"',
                            '" + cmbPatient.SelectedValue + @"',
                            '" + cmbDoctor.SelectedValue + @"',
                            '" + txtDiagnosis.Text + @"',
                            '" + txtPrescription.Text + @"',
                            '" + txtTreatment.Text + @"')";

                    Da.ExecuteUpdateQuery(Sql);
                    MessageBox.Show("Medical record added successfully");
                }

                PopulateGridView();
                PopulatePatientCombo();
                PopulateDoctorCombo();
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving medical record\n" + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMedicalRecordList.CurrentRow == null) return;

            string id = dgvMedicalRecordList.CurrentRow.Cells["medicalId"].Value.ToString();

            Sql = "DELETE FROM medicalRecord WHERE medicalId = '" + id + "'";
            Da.ExecuteUpdateQuery(Sql);

            MessageBox.Show("Medical record deleted");

            PopulateGridView();
            PopulatePatientCombo();
            PopulateDoctorCombo();
            ClearAll();
        }
        private void dgvMedicalRecordList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMedicalRecordList.CurrentRow == null) return;

            this.txtMedicalID.Text = dgvMedicalRecordList.CurrentRow.Cells["medicalId"].Value.ToString();
            this.cmbPatient.SelectedValue = dgvMedicalRecordList.CurrentRow.Cells["patientId"].Value.ToString();
            this.cmbDoctor.SelectedValue = dgvMedicalRecordList.CurrentRow.Cells["doctorId"].Value.ToString();
            this.txtDiagnosis.Text = dgvMedicalRecordList.CurrentRow.Cells["diagnosis"].Value.ToString();
            this.txtPrescription.Text = dgvMedicalRecordList.CurrentRow.Cells["prescription"].Value.ToString();
            this.txtTreatment.Text = dgvMedicalRecordList.CurrentRow.Cells["treatment"].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Sql = @"SELECT
                    m.medicalId,
                    m.patientId,
                    m.doctorId,
                    p.name AS patientName,
                    d.doctorName,
                    m.diagnosis,
                    m.prescription,
                    m.treatment
                FROM medicalRecord m
                JOIN patientList p ON m.patientId = p.patientId
                JOIN doctorList d ON m.doctorId = d.doctorId
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
