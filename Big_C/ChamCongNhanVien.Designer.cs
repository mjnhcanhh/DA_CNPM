namespace Big_C
{
    partial class ChamCongNhanVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaNVText = new System.Windows.Forms.Label();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.btnVao = new System.Windows.Forms.Button();
            this.btnRa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(230, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(340, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHẤM CÔNG NHÂN VIÊN";
            // 
            // lblMaNVText
            // 
            this.lblMaNVText.AutoSize = true;
            this.lblMaNVText.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblMaNVText.Location = new System.Drawing.Point(260, 110);
            this.lblMaNVText.Name = "lblMaNVText";
            this.lblMaNVText.Size = new System.Drawing.Size(132, 28);
            this.lblMaNVText.TabIndex = 1;
            this.lblMaNVText.Text = "Mã nhân viên:";
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaNV.ForeColor = System.Drawing.Color.Blue;
            this.lblMaNV.Location = new System.Drawing.Point(400, 110);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(0, 28);
            this.lblMaNV.TabIndex = 2;
            // 
            // btnVao
            // 
            this.btnVao.BackColor = System.Drawing.Color.LimeGreen;
            this.btnVao.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnVao.ForeColor = System.Drawing.Color.White;
            this.btnVao.Location = new System.Drawing.Point(220, 200);
            this.btnVao.Name = "btnVao";
            this.btnVao.Size = new System.Drawing.Size(160, 70);
            this.btnVao.TabIndex = 3;
            this.btnVao.Text = "VÀO CA";
            this.btnVao.UseVisualStyleBackColor = false;
            this.btnVao.Click += new System.EventHandler(this.btnVao_Click);
            // 
            // btnRa
            // 
            this.btnRa.BackColor = System.Drawing.Color.IndianRed;
            this.btnRa.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnRa.ForeColor = System.Drawing.Color.White;
            this.btnRa.Location = new System.Drawing.Point(420, 200);
            this.btnRa.Name = "btnRa";
            this.btnRa.Size = new System.Drawing.Size(160, 70);
            this.btnRa.TabIndex = 4;
            this.btnRa.Text = "RA CA";
            this.btnRa.UseVisualStyleBackColor = false;
            this.btnRa.Click += new System.EventHandler(this.btnRa_Click);
            // 
            // ChamCongNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRa);
            this.Controls.Add(this.btnVao);
            this.Controls.Add(this.lblMaNV);
            this.Controls.Add(this.lblMaNVText);
            this.Controls.Add(this.lblTitle);
            this.Name = "ChamCongNhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chấm Công Nhân Viên";
            this.Load += new System.EventHandler(this.ChamCongNhanVien_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaNVText;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.Button btnVao;
        private System.Windows.Forms.Button btnRa;
    }
}
