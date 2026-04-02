using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Big_C
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult exit;
            exit = MessageBox.Show("Bạn có chắc muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (exit == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(
                @"Data Source=.;Initial Catalog=QL_BigC;Integrated Security=True"
            );

            conn.Open();
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM TaiKhoan WHERE TenDangNhap=@user AND MatKhau=@pass",
                conn
            );
            cmd.Parameters.AddWithValue("@user", txtUser.Text);
            cmd.Parameters.AddWithValue("@pass", txtPass.Text);

            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                string role = rd["VaiTro"].ToString();
                string maNV = rd["MaNV"].ToString();

                if (role == "Admin")
                {
                    TrangChu frm = new TrangChu();
                    frm.Show();
                }
                else
                {
                    ChamCongNhanVien frm = new ChamCongNhanVien(maNV);
                    frm.Show();

                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }

            conn.Close();
        }


        private void txtUser_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (txtUser.Text == "")
            {
                this.errorProvider1.SetError(ctr, "Bạn không được để trống trường này!");
            }
            else
            {
                this.errorProvider1.Clear();
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            Control ctr = (Control)sender;
            if (txtPass.Text == "")
            {
                this.errorProvider1.SetError(ctr, "Bạn không được để trống trường này!");
            }
            else
            {
                this.errorProvider1.Clear();
            }
        }

      
    }
}
