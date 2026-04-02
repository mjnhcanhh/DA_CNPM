using System.Drawing;
using System.Windows.Forms;

namespace Big_C.UserControls
{
    partial class ChamCong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.cboNhanVien = new ComboBox();
            this.txtName = new TextBox();
            this.txtCCCD = new TextBox();
            this.txtPhone = new TextBox();
            this.txtLuongTrenGio = new TextBox();
            this.txtSoGioLam = new TextBox();
            this.txtTongLuong = new TextBox();
            this.btnThanhToan = new Button();

            this.lblMa = new Label();
            this.lblTen = new Label();
            this.lblCCCD = new Label();
            this.lblPhone = new Label();
            this.lblLuong = new Label();
            this.lblGio = new Label();
            this.lblTong = new Label();

            this.panel1.SuspendLayout();
            this.SuspendLayout();

            Font labelFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font textFont = new Font("Segoe UI", 10F);

            // panel1
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.BackColor = Color.White;
            this.panel1.Padding = new Padding(40);

            // cboNhanVien
            this.cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboNhanVien.Font = textFont;
            this.cboNhanVien.Location = new Point(260, 40);
            this.cboNhanVien.Size = new Size(400, 28);
            this.cboNhanVien.SelectedIndexChanged += new System.EventHandler(this.cboNhanVien_SelectedIndexChanged);

            // txtName
            this.txtName.Font = textFont;
            this.txtName.Enabled = false;
            this.txtName.Location = new Point(260, 90);
            this.txtName.Size = new Size(400, 28);

            // txtCCCD
            this.txtCCCD.Font = textFont;
            this.txtCCCD.Enabled = false;
            this.txtCCCD.Location = new Point(260, 140);
            this.txtCCCD.Size = new Size(400, 28);

            // txtPhone
            this.txtPhone.Font = textFont;
            this.txtPhone.Enabled = false;
            this.txtPhone.Location = new Point(260, 190);
            this.txtPhone.Size = new Size(400, 28);

            // txtLuongTrenGio
            this.txtLuongTrenGio.Font = textFont;
            this.txtLuongTrenGio.Enabled = false;
            this.txtLuongTrenGio.Location = new Point(260, 240);
            this.txtLuongTrenGio.Size = new Size(400, 28);

            // txtSoGioLam
            this.txtSoGioLam.Font = textFont;
            this.txtSoGioLam.Enabled = false;
            this.txtSoGioLam.Location = new Point(260, 290);
            this.txtSoGioLam.Size = new Size(400, 28);

            // txtTongLuong
            this.txtTongLuong.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.txtTongLuong.Enabled = false;
            this.txtTongLuong.Location = new Point(260, 340);
            this.txtTongLuong.Size = new Size(400, 30);

            // btnThanhToan
            this.btnThanhToan.Text = "THANH TOÁN LƯƠNG";
            this.btnThanhToan.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnThanhToan.BackColor = Color.SeaGreen;
            this.btnThanhToan.ForeColor = Color.White;
            this.btnThanhToan.FlatStyle = FlatStyle.Flat;
            this.btnThanhToan.Location = new Point(260, 400);
            this.btnThanhToan.Size = new Size(400, 55);
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);

            // Labels
            Label[] labels = { lblMa, lblTen, lblCCCD, lblPhone, lblLuong, lblGio, lblTong };
            string[] texts =
            {
                "Nhân viên:",
                "Tên nhân viên:",
                "CCCD:",
                "Số điện thoại:",
                "Lương / giờ:",
                "Số giờ làm:",
                "Tổng lương:"
            };

            int y = 45;
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].Text = texts[i];
                labels[i].Font = labelFont;
                labels[i].AutoSize = true;
                labels[i].Location = new Point(80, y);
                y += 50;
            }

            // Add controls
            this.panel1.Controls.AddRange(new Control[]
            {
                cboNhanVien, txtName, txtCCCD, txtPhone,
                txtLuongTrenGio, txtSoGioLam, txtTongLuong,
                btnThanhToan,
                lblMa, lblTen, lblCCCD, lblPhone, lblLuong, lblGio, lblTong
            });

            this.Controls.Add(this.panel1);
            this.Size = new Size(800, 600);
            this.Load += new System.EventHandler(this.ChamCong_Load);

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ComboBox cboNhanVien;
        private TextBox txtName;
        private TextBox txtCCCD;
        private TextBox txtPhone;
        private TextBox txtLuongTrenGio;
        private TextBox txtSoGioLam;
        private TextBox txtTongLuong;
        private Button btnThanhToan;

        private Label lblMa;
        private Label lblTen;
        private Label lblCCCD;
        private Label lblPhone;
        private Label lblLuong;
        private Label lblGio;
        private Label lblTong;
    }
}
