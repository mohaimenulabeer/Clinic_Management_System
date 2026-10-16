namespace Group2_Clinic
{
    partial class Admin
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnMedicalRecord = new System.Windows.Forms.Button();
            this.btnBill = new System.Windows.Forms.Button();
            this.btnAppointment = new System.Windows.Forms.Button();
            this.btnDoctor = new System.Windows.Forms.Button();
            this.btnPatient = new System.Windows.Forms.Button();
            this.patient1 = new Group2_Clinic.Patient();
            this.medicalRecord1 = new Group2_Clinic.MedicalRecord();
            this.doctor1 = new Group2_Clinic.Doctor();
            this.bill1 = new Group2_Clinic.Bill();
            this.appointment1 = new Group2_Clinic.Appointment();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnLogOut);
            this.panel1.Controls.Add(this.btnMedicalRecord);
            this.panel1.Controls.Add(this.btnBill);
            this.panel1.Controls.Add(this.btnAppointment);
            this.panel1.Controls.Add(this.btnDoctor);
            this.panel1.Controls.Add(this.btnPatient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 610);
            this.panel1.TabIndex = 1;
            // 
            // btnLogOut
            // 
            this.btnLogOut.Location = new System.Drawing.Point(63, 542);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(75, 23);
            this.btnLogOut.TabIndex = 11;
            this.btnLogOut.Text = "Logout";
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnMedicalRecord
            // 
            this.btnMedicalRecord.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnMedicalRecord.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedicalRecord.Location = new System.Drawing.Point(19, 406);
            this.btnMedicalRecord.Margin = new System.Windows.Forms.Padding(2);
            this.btnMedicalRecord.Name = "btnMedicalRecord";
            this.btnMedicalRecord.Size = new System.Drawing.Size(172, 37);
            this.btnMedicalRecord.TabIndex = 9;
            this.btnMedicalRecord.Text = "Medical Record";
            this.btnMedicalRecord.UseVisualStyleBackColor = false;
            this.btnMedicalRecord.Click += new System.EventHandler(this.btnMedicalRecord_Click);
            // 
            // btnBill
            // 
            this.btnBill.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnBill.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBill.Location = new System.Drawing.Point(19, 325);
            this.btnBill.Margin = new System.Windows.Forms.Padding(2);
            this.btnBill.Name = "btnBill";
            this.btnBill.Size = new System.Drawing.Size(172, 37);
            this.btnBill.TabIndex = 8;
            this.btnBill.Text = "Bill";
            this.btnBill.UseVisualStyleBackColor = false;
            this.btnBill.Click += new System.EventHandler(this.btnBill_Click);
            // 
            // btnAppointment
            // 
            this.btnAppointment.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnAppointment.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppointment.Location = new System.Drawing.Point(19, 244);
            this.btnAppointment.Margin = new System.Windows.Forms.Padding(2);
            this.btnAppointment.Name = "btnAppointment";
            this.btnAppointment.Size = new System.Drawing.Size(172, 37);
            this.btnAppointment.TabIndex = 7;
            this.btnAppointment.Text = "Appointment";
            this.btnAppointment.UseVisualStyleBackColor = false;
            this.btnAppointment.Click += new System.EventHandler(this.btnAppointment_Click);
            // 
            // btnDoctor
            // 
            this.btnDoctor.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnDoctor.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoctor.Location = new System.Drawing.Point(19, 162);
            this.btnDoctor.Margin = new System.Windows.Forms.Padding(2);
            this.btnDoctor.Name = "btnDoctor";
            this.btnDoctor.Size = new System.Drawing.Size(172, 37);
            this.btnDoctor.TabIndex = 6;
            this.btnDoctor.Text = "Doctor";
            this.btnDoctor.UseVisualStyleBackColor = false;
            this.btnDoctor.Click += new System.EventHandler(this.btnDoctor_Click);
            // 
            // btnPatient
            // 
            this.btnPatient.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnPatient.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatient.Location = new System.Drawing.Point(19, 81);
            this.btnPatient.Margin = new System.Windows.Forms.Padding(2);
            this.btnPatient.Name = "btnPatient";
            this.btnPatient.Size = new System.Drawing.Size(172, 37);
            this.btnPatient.TabIndex = 5;
            this.btnPatient.Text = "Patient";
            this.btnPatient.UseVisualStyleBackColor = false;
            this.btnPatient.Click += new System.EventHandler(this.btnPatient_Click);
            // 
            // patient1
            // 
            this.patient1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.patient1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patient1.Location = new System.Drawing.Point(210, 0);
            this.patient1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.patient1.Name = "patient1";
            this.patient1.Size = new System.Drawing.Size(634, 610);
            this.patient1.TabIndex = 2;
            // 
            // medicalRecord1
            // 
            this.medicalRecord1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.medicalRecord1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.medicalRecord1.Location = new System.Drawing.Point(210, 0);
            this.medicalRecord1.Name = "medicalRecord1";
            this.medicalRecord1.Size = new System.Drawing.Size(634, 610);
            this.medicalRecord1.TabIndex = 3;
            // 
            // doctor1
            // 
            this.doctor1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.doctor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doctor1.Location = new System.Drawing.Point(210, 0);
            this.doctor1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.doctor1.Name = "doctor1";
            this.doctor1.Size = new System.Drawing.Size(634, 610);
            this.doctor1.TabIndex = 4;
            // 
            // bill1
            // 
            this.bill1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bill1.Location = new System.Drawing.Point(210, 0);
            this.bill1.Name = "bill1";
            this.bill1.Size = new System.Drawing.Size(634, 610);
            this.bill1.TabIndex = 5;
            // 
            // appointment1
            // 
            this.appointment1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.appointment1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appointment1.Location = new System.Drawing.Point(210, 0);
            this.appointment1.Margin = new System.Windows.Forms.Padding(2);
            this.appointment1.Name = "appointment1";
            this.appointment1.Size = new System.Drawing.Size(634, 610);
            this.appointment1.TabIndex = 6;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 610);
            this.Controls.Add(this.appointment1);
            this.Controls.Add(this.bill1);
            this.Controls.Add(this.doctor1);
            this.Controls.Add(this.medicalRecord1);
            this.Controls.Add(this.patient1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.Admin_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMedicalRecord;
        private System.Windows.Forms.Button btnBill;
        private System.Windows.Forms.Button btnAppointment;
        private System.Windows.Forms.Button btnDoctor;
        private System.Windows.Forms.Button btnPatient;
        private System.Windows.Forms.Button btnLogOut;
        private Patient patient1;
        private MedicalRecord medicalRecord1;
        private Doctor doctor1;
        private Bill bill1;
        private Appointment appointment1;
    }
}