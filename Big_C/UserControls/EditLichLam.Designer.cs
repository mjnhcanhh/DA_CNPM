namespace Big_C.UserControls
{
    partial class EditLichLam
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.labelMaNV = new System.Windows.Forms.Label();
            this.labelNgayLam = new System.Windows.Forms.Label();
            this.labelCaLam = new System.Windows.Forms.Label();
            this.labelGhiChu = new System.Windows.Forms.Label();
            this.cboMaNV = new System.Windows.Forms.ComboBox();
            this.dtNgayLam = new System.Windows.Forms.DateTimePicker();
            this.txtCaLam = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(140, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÊM / SỬA LỊCH LÀM VIỆC";
            // 
            // labelMaNV
            // 
            this.labelMaNV.AutoSize = true;
            this.labelMaNV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMaNV.Location = new System.Drawing.Point(50, 80);
            this.labelMaNV.Name = "labelMaNV";
            this.labelMaNV.Size = new System.Drawing.Size(95, 16);
            this.labelMaNV.TabIndex = 1;
            this.labelMaNV.Text = "Mã Nhân Viên:";
            // 
            // labelNgayLam
            // 
            this.labelNgayLam.AutoSize = true;
            this.labelNgayLam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNgayLam.Location = new System.Drawing.Point(50, 130);
            this.labelNgayLam.Name = "labelNgayLam";
            this.labelNgayLam.Size = new System.Drawing.Size(69, 16);
            this.labelNgayLam.TabIndex = 2;
            this.labelNgayLam.Text = "Ngày Làm:";
            // 
            // labelCaLam
            // 
            this.labelCaLam.AutoSize = true;
            this.labelCaLam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaLam.Location = new System.Drawing.Point(50, 180);
            this.labelCaLam.Name = "labelCaLam";
            this.labelCaLam.Size = new System.Drawing.Size(56, 16);
            this.labelCaLam.TabIndex = 3;
            this.labelCaLam.Text = "Ca Làm:";
            // 
            // labelGhiChu
            // 
            this.labelGhiChu.AutoSize = true;
            this.labelGhiChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGhiChu.Location = new System.Drawing.Point(50, 230);
            this.labelGhiChu.Name = "labelGhiChu";
            this.labelGhiChu.Size = new System.Drawing.Size(59, 16);
            this.labelGhiChu.TabIndex = 4;
            this.labelGhiChu.Text = "Ghi Chú:";
            // 
            // cboMaNV
            // 
            this.cboMaNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMaNV.FormattingEnabled = true;
            this.cboMaNV.Location = new System.Drawing.Point(180, 78);
            this.cboMaNV.Name = "cboMaNV";
            this.cboMaNV.Size = new System.Drawing.Size(280, 21);
            this.cboMaNV.TabIndex = 5;
            // 
            // dtNgayLam
            // 
            this.dtNgayLam.CustomFormat = "dd/MM/yyyy";
            this.dtNgayLam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayLam.Location = new System.Drawing.Point(180, 127);
            this.dtNgayLam.Name = "dtNgayLam";
            this.dtNgayLam.Size = new System.Drawing.Size(280, 20);
            this.dtNgayLam.TabIndex = 6;
            // 
            // txtCaLam
            // 
            this.txtCaLam.Location = new System.Drawing.Point(180, 177);
            this.txtCaLam.Name = "txtCaLam";
            this.txtCaLam.Size = new System.Drawing.Size(280, 20);
            this.txtCaLam.TabIndex = 7;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(180, 227);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(280, 80);
            this.txtGhiChu.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(280, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "LƯU";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(380, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "HỦY";
            this.btnCancel.UseVisualStyleBackColor = true;
            // Sự kiện này sẽ được xử lý trong code (Close Form)
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditLichLam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.txtCaLam);
            this.Controls.Add(this.dtNgayLam);
            this.Controls.Add(this.cboMaNV);
            this.Controls.Add(this.labelGhiChu);
            this.Controls.Add(this.labelCaLam);
            this.Controls.Add(this.labelNgayLam);
            this.Controls.Add(this.labelMaNV);
            this.Controls.Add(this.label1);
            this.Name = "EditLichLam";
            this.Size = new System.Drawing.Size(550, 400);
            this.Load += new System.EventHandler(this.EditLichLam_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMaNV;
        private System.Windows.Forms.Label labelNgayLam;
        private System.Windows.Forms.Label labelCaLam;
        private System.Windows.Forms.Label labelGhiChu;
        private System.Windows.Forms.ComboBox cboMaNV;
        private System.Windows.Forms.DateTimePicker dtNgayLam;
        private System.Windows.Forms.TextBox txtCaLam;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}