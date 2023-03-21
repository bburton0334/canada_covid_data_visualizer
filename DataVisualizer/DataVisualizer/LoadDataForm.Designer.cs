namespace DataVisualizer
{
    partial class LoadDataForm
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
            this.btnTestingData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCaseData = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTestingLoadStatus = new System.Windows.Forms.Label();
            this.lblCaseLoadStatus = new System.Windows.Forms.Label();
            this.btnLoadContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestingData
            // 
            this.btnTestingData.Location = new System.Drawing.Point(229, 94);
            this.btnTestingData.Name = "btnTestingData";
            this.btnTestingData.Size = new System.Drawing.Size(138, 32);
            this.btnTestingData.TabIndex = 0;
            this.btnTestingData.Text = "Select File";
            this.btnTestingData.UseVisualStyleBackColor = true;
            this.btnTestingData.Click += new System.EventHandler(this.btnTestingData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Black", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(155, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "COVID-19 Data Loader";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCaseData
            // 
            this.btnCaseData.Location = new System.Drawing.Point(229, 147);
            this.btnCaseData.Name = "btnCaseData";
            this.btnCaseData.Size = new System.Drawing.Size(138, 32);
            this.btnCaseData.TabIndex = 2;
            this.btnCaseData.Text = "Select File";
            this.btnCaseData.UseVisualStyleBackColor = true;
            this.btnCaseData.Click += new System.EventHandler(this.btnCaseData_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "COVID-19 Testing Data";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "COVID-19 Case Data";
            // 
            // lblTestingLoadStatus
            // 
            this.lblTestingLoadStatus.AutoSize = true;
            this.lblTestingLoadStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestingLoadStatus.ForeColor = System.Drawing.Color.Red;
            this.lblTestingLoadStatus.Location = new System.Drawing.Point(399, 102);
            this.lblTestingLoadStatus.Name = "lblTestingLoadStatus";
            this.lblTestingLoadStatus.Size = new System.Drawing.Size(133, 17);
            this.lblTestingLoadStatus.TabIndex = 5;
            this.lblTestingLoadStatus.Text = "Status: Unloaded";
            // 
            // lblCaseLoadStatus
            // 
            this.lblCaseLoadStatus.AutoSize = true;
            this.lblCaseLoadStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaseLoadStatus.ForeColor = System.Drawing.Color.Red;
            this.lblCaseLoadStatus.Location = new System.Drawing.Point(399, 155);
            this.lblCaseLoadStatus.Name = "lblCaseLoadStatus";
            this.lblCaseLoadStatus.Size = new System.Drawing.Size(133, 17);
            this.lblCaseLoadStatus.TabIndex = 6;
            this.lblCaseLoadStatus.Text = "Status: Unloaded";
            // 
            // btnLoadContinue
            // 
            this.btnLoadContinue.Location = new System.Drawing.Point(425, 222);
            this.btnLoadContinue.Name = "btnLoadContinue";
            this.btnLoadContinue.Size = new System.Drawing.Size(153, 32);
            this.btnLoadContinue.TabIndex = 7;
            this.btnLoadContinue.Text = "Continue";
            this.btnLoadContinue.UseVisualStyleBackColor = true;
            this.btnLoadContinue.Click += new System.EventHandler(this.btnLoadContinue_Click);
            // 
            // LoadDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(590, 266);
            this.Controls.Add(this.btnLoadContinue);
            this.Controls.Add(this.lblCaseLoadStatus);
            this.Controls.Add(this.lblTestingLoadStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCaseData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTestingData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoadDataForm";
            this.Text = "Load COVID-19 Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestingData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCaseData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTestingLoadStatus;
        private System.Windows.Forms.Label lblCaseLoadStatus;
        private System.Windows.Forms.Button btnLoadContinue;
    }
}