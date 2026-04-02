using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Big_C.UserControls;
using Big_C.Model;
using System.Data.SqlClient;

namespace Big_C
{
    public partial class TrangChu : Form
    {

        public string TenDangNhap { get; set; }
        public TrangChu()
        {
            InitializeComponent();
        }

        public void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TenDangNhap))
            {
                lblTenDangNhap.Text = "Xin chào, " + this.TenDangNhap;
            }
            Home hm = new Home();
            addUserControl(hm);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            if (btnChamCong.BackColor == Color.Tan)
            {
                btnHome.BackColor = Color.Tan;
                btnChamCong.BackColor = Color.PaleTurquoise;
                btnQLHangKho.BackColor = Color.Tan;
                btnQuanLyLichLam.BackColor = Color.Tan;
                btnQLNhanSu.BackColor = Color.Tan;
                btnSupport.BackColor = Color.Tan;
            }
            ChamCong cc = new ChamCong();
            addUserControl(cc);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if(btnHome.BackColor == Color.Tan)
            {
                btnHome.BackColor = Color.PaleTurquoise;
                btnChamCong.BackColor = Color.Tan;
                btnQLHangKho.BackColor = Color.Tan;
                btnQuanLyLichLam.BackColor = Color.Tan;
                btnQLNhanSu.BackColor = Color.Tan;
                btnSupport.BackColor = Color.Tan;
            }
            Home hm = new Home();
            addUserControl(hm);
        }

        private void btnQLHangKho_Click(object sender, EventArgs e)
        {
            if (btnQLHangKho.BackColor == Color.Tan)
            {
                btnHome.BackColor = Color.Tan;
                btnChamCong.BackColor = Color.Tan;
                btnQLHangKho.BackColor = Color.PaleTurquoise;
                btnQuanLyLichLam.BackColor = Color.Tan;
                btnQLNhanSu.BackColor = Color.Tan;
                btnSupport.BackColor = Color.Tan;
            }
            QLHangKho qlhk = new QLHangKho();
            addUserControl(qlhk);

        }

        private void btnQuanLyLichLam_Click(object sender, EventArgs e)
        {
            if (btnQuanLyLichLam.BackColor == Color.Tan)
            {
                btnHome.BackColor = Color.Tan;
                btnChamCong.BackColor = Color.Tan;
                btnQLHangKho.BackColor = Color.Tan;
                btnQuanLyLichLam.BackColor = Color.PaleTurquoise;
                btnQLNhanSu.BackColor = Color.Tan;
                btnSupport.BackColor = Color.Tan;
            }
            LichLam lm = new LichLam();
            addUserControl(lm);
        }

        private void btnQLNhanSu_Click(object sender, EventArgs e)
        {
            if (btnQLNhanSu.BackColor == Color.Tan)
            {
                btnHome.BackColor = Color.Tan;
                btnChamCong.BackColor = Color.Tan;
                btnQLHangKho.BackColor = Color.Tan;
                btnQuanLyLichLam.BackColor = Color.Tan;
                btnQLNhanSu.BackColor = Color.PaleTurquoise;
                btnSupport.BackColor = Color.Tan;
            }
            QLNhanSu qlns = new QLNhanSu();
            addUserControl(qlns);
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            if (btnSupport.BackColor == Color.Tan)
            {
                btnHome.BackColor = Color.Tan;
                btnChamCong.BackColor = Color.Tan;
                btnQLHangKho.BackColor = Color.Tan;
                btnQuanLyLichLam.BackColor = Color.Tan;
                btnQLNhanSu.BackColor = Color.Tan;
                btnSupport.BackColor = Color.PaleTurquoise;
            }
            Suppport sp = new Suppport();
            addUserControl(sp);
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }


        List<NhanVien> lnv = new List<NhanVien>();

        // DÙNG SERVER ĐÃ TEST THÀNH CÔNG
        string strcon = "Data Source = DESKTOP-O8DO4M9;Initial Catalog=QL_BigC;Integrated Security=True;Connection Timeout=30";

        SqlConnection connection = null;

        private void QLNhanSu_Load(object sender, EventArgs e)
        {
            try
            {
                if (connection == null)
                {
                    connection = new SqlConnection(strcon);
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    MessageBox.Show("✅ Kết nối SQL Server thành công!", "Database Connected");
                }

                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT COUNT(*) FROM NhanVien"; // Test đơn giản trước
                int count = (int)command.ExecuteScalar();

                MessageBox.Show($"Database có {count} nhân viên", "Thông tin Database");

            }
            catch (SqlException sqlEx)
            {
                // HIỂN THỊ HƯỚNG DẪN CỤ THỂ
                string errorMessage = $"Lỗi SQL: {sqlEx.Message}\n\n" +
                                     "HÃY LÀM THEO CÁC BƯỚC SAU:\n" +
                                     "1. Mở SQL Server Configuration Manager\n" +
                                     "2. Vào SQL Server Services\n" +
                                     "3. Start 'SQL Server (SQLEXPRESS)'\n" +
                                     "4. Start 'SQL Server Browser'";

                MessageBox.Show(errorMessage, "CẦN START SQL SERVER", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi ứng dụng");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
