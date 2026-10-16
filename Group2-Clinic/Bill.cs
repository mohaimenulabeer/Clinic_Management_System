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
    public partial class Bill : UserControl
    {
        private DataAccess Da { get; set; }
        private DataSet Ds { get; set; }
        private string Sql { get; set; }

        public Bill()
        {
            InitializeComponent();
            Da = new DataAccess();

            PopulatePatientCombo();
            PopulateGridView();
            GenerateBillsID();

            cmbStatus.SelectedIndex = -1;
            cmbPaymentMethod.SelectedIndex = -1;
        }
        private void Bill_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                PopulatePatientCombo();
                PopulateGridView();
            }
        }
        private void PopulateGridView(string sql = @" SELECT
                    b.billsId,
                    b.patientId, 
                    p.name AS patientName,
                    b.amount,
                    b.status,
                    b.paymentMethod,
                 CONVERT(NVARCHAR(10), b.paymentDate, 120) AS paymentDate
                 FROM bills b
                 JOIN PatientList p ON b.patientId = p.patientId")
        {
            Ds = Da.ExecuteQuery(sql);
            dgvBillsList.AutoGenerateColumns = false;
            dgvBillsList.DataSource = Ds.Tables[0];
        }
        private void GenerateBillsID()
        {
            Sql = "SELECT billsId FROM bills ORDER BY billsId DESC;";
            DataTable dt = Da.ExecuteQueryTable(Sql);

            if (dt.Rows.Count == 0)
            {
                txtBillsID.Text = "B0001";
                return;
            }

            string id = dt.Rows[0]["billsId"].ToString();
            int num = int.Parse(id.Substring(1));
            txtBillsID.Text = "B" + (++num).ToString("D4");
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
        private void ClearAll()
        {
            this.txtBillsID.Clear();
            this.txtBillsID.ReadOnly = true;
            this.cmbPatient.SelectedIndex = -1;
            this.cmbStatus.SelectedIndex = -1;
            this.txtAmount.Clear();
            this.dtpBillsDate.Value = DateTime.Today;

            GenerateBillsID();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Sql = "SELECT * FROM bills WHERE billsId = '" + txtBillsID.Text + "'";
                Ds = Da.ExecuteQuery(Sql);

                if (Ds.Tables[0].Rows.Count == 1)
                {
                    Sql = @"UPDATE bills SET
                    patientId = '" + cmbPatient.SelectedValue + @"',
                    amount = '" + txtAmount.Text + @"',
                    status = '" + cmbStatus.Text + @"',
                    paymentMethod = '" + cmbPaymentMethod.Text + @"',
                    paymentDate = '" + dtpBillsDate.Value.ToString("yyyy-MM-dd") + @"'
                    WHERE billsId = '" + txtBillsID.Text + "';";

                    Da.ExecuteUpdateQuery(Sql);
                    MessageBox.Show("bills updated successfully");
                }
                else
                {
                    Sql = @"INSERT INTO bills
                    (billsId, patientId, amount, status, paymentMethod, paymentDate)
                    VALUES (
                    '" + txtBillsID.Text + @"',
                    '" + cmbPatient.SelectedValue + @"',
                    '" + txtAmount.Text + @"',
                    '" + cmbStatus.Text + @"',
                    '" + cmbPaymentMethod.Text + @"',
                    '" + dtpBillsDate.Value.ToString("yyyy-MM-dd") + @"');";

                    Da.ExecuteUpdateQuery(Sql);
                    MessageBox.Show("Bills added successfully");
                }

                PopulateGridView();
                PopulatePatientCombo();
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving bills\n" + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBillsList.CurrentRow == null) return;

            string id = dgvBillsList.CurrentRow.Cells["billsId"].Value.ToString();

            Sql = "DELETE FROM bills WHERE billsId = '" + id + "'";
            Da.ExecuteUpdateQuery(Sql);

            MessageBox.Show("Bill deleted");
            PopulateGridView();
            PopulatePatientCombo();
            ClearAll();
        }
        private void dgvBillsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBillsList.CurrentRow == null) return;

            this.txtBillsID.Text =dgvBillsList.CurrentRow.Cells["billsId"].Value.ToString();
            this.cmbPatient.SelectedValue =dgvBillsList.CurrentRow.Cells["patientId"].Value.ToString();
            this.txtAmount.Text = dgvBillsList.CurrentRow.Cells["amount"].Value.ToString();
            this.cmbStatus.Text =dgvBillsList.CurrentRow.Cells["status"].Value.ToString();
            this.cmbPaymentMethod.Text =dgvBillsList.CurrentRow.Cells["paymentMethod"].Value.ToString();
            this.dtpBillsDate.Value = DateTime.Parse(
                dgvBillsList.CurrentRow.Cells["paymentDate"].Value.ToString()
            );
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Sql = @" SELECT
                        b.billsId,
                        b.patientId,
                        p.name AS patientName,
                        b.amount,
                        b.status,
                        b.paymentMethod,
                        b.paymentDate
                   FROM bills b
                   JOIN PatientList p ON b.patientId = p.patientId
                   WHERE p.name LIKE '" + txtSearch.Text + "%'";

            PopulateGridView(Sql);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PopulateGridView();
            PopulatePatientCombo();
            ClearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
