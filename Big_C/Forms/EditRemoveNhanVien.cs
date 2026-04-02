using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Big_C.Model;

namespace Big_C.Forms
{
    public partial class EditRemoveNhanVien : Form
    {
        public EditRemoveNhanVien()
        {
            InitializeComponent();
        }

        List<NhanVien> lnv = new List<NhanVien>();
        string strcon = "SERVER = DESKTOP-O8DO4M9; DATABASE = QL_BigC; Integrated Security = TRUE";
        SqlConnection connection = null;

        private void EditRemoveNhanVien_Load(object sender, EventArgs e)
        {
            if (connection == null)
            {
                connection = new SqlConnection(strcon);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "Select * from NhanVien";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                NhanVien nv = new NhanVien();
                nv.MaNhanVien = reader.GetString(0);
                nv.TenNhanVien = reader.GetString(1);
                nv.NgaySinh = reader.GetDateTime(2);
                nv.DiaChi = reader.GetString(3);
                nv.SDT = reader.GetString(4);
                nv.CCCD = reader.GetString(5);
                nv.NgayVaoLam = reader.GetDateTime(6);
                nv.SoNgayLam = reader.GetInt32(7);
                nv.MaQuanLy = reader.GetString(8);
                lnv.Add(nv);
            }

            foreach(NhanVien nv in lnv)
            {
                ListViewItem item = new ListViewItem(nv.MaNhanVien);
                item.SubItems.Add(nv.TenNhanVien);
                item.SubItems.Add(nv.NgaySinh.ToString());
                item.SubItems.Add(nv.DiaChi);
                item.SubItems.Add(nv.SDT);
                item.SubItems.Add(nv.CCCD);
                item.SubItems.Add(nv.NgayVaoLam.ToString());
                item.SubItems.Add(nv.SoNgayLam.ToString());
                item.SubItems.Add(nv.MaQuanLy);
                listView1.Items.Add(item);
            }
            reader.Close();

        }

        public void hienThiNhanVien()
        {
            lnv = new List<NhanVien>();
            if (connection == null)
            {
                connection = new SqlConnection(strcon);
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "Select * from NhanVien";
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                NhanVien nv = new NhanVien();
                nv.MaNhanVien = reader.GetString(0);
                nv.TenNhanVien = reader.GetString(1);
                nv.NgaySinh = reader.GetDateTime(2);
                nv.DiaChi = reader.GetString(3);
                nv.SDT = reader.GetString(4);
                nv.CCCD = reader.GetString(5);
                nv.NgayVaoLam = reader.GetDateTime(6);
                nv.SoNgayLam = reader.GetInt32(7);
                nv.MaQuanLy = reader.GetString(8);
                lnv.Add(nv);
            }

            foreach (NhanVien nv in lnv)
            {
                ListViewItem item = new ListViewItem(nv.MaNhanVien);
                item.SubItems.Add(nv.TenNhanVien);
                item.SubItems.Add(nv.NgaySinh.ToString());
                item.SubItems.Add(nv.DiaChi);
                item.SubItems.Add(nv.SDT);
                item.SubItems.Add(nv.CCCD);
                item.SubItems.Add(nv.NgayVaoLam.ToString());
                item.SubItems.Add(nv.SoNgayLam.ToString());
                item.SubItems.Add(nv.MaQuanLy);
                listView1.Items.Add(item);
            }
            reader.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string MaNhanVien = listView1.SelectedItems[0].SubItems[0].Text;
                foreach(NhanVien nv in lnv)
                {
                    if(nv.MaNhanVien == MaNhanVien)
                    {
                        txtMa.Text = nv.MaNhanVien;
                        txtName.Text = nv.TenNhanVien;
                        txtDiaChi.Text = nv.DiaChi;
                        txtPhone.Text = nv.SDT;
                        txtCCCD.Text = nv.CCCD;
                        txtSoGioLam.Text = nv.SoNgayLam + "";
                    }    
                }    
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtMa.Text.Trim() == "")
            {
                MessageBox.Show("Chọn nhân viên cần xóa!");
                return;
            }

            // Xác nhận trước khi xóa để tránh nhầm lẫn
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này và tất cả dữ liệu liên quan (Lịch làm, Tài khoản)?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.No) return;

            string maNV = txtMa.Text.Trim();

            if (connection == null)
                connection = new SqlConnection(strcon);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlTransaction tran = connection.BeginTransaction();

            try
            {
                // 1. Xóa Lịch làm việc trước (Đây là nguyên nhân gây lỗi của bạn)
                SqlCommand cmdLich = new SqlCommand(
                    "DELETE FROM LichLam WHERE MaNhanVien = @ma",
                    connection, tran
                );
                cmdLich.Parameters.Add("@ma", SqlDbType.Char).Value = maNV;
                cmdLich.ExecuteNonQuery();

                // 2. Xóa tài khoản
                SqlCommand cmdTK = new SqlCommand(
                    "DELETE FROM TaiKhoan WHERE MaNV = @ma",
                    connection, tran
                );
                cmdTK.Parameters.Add("@ma", SqlDbType.Char).Value = maNV;
                cmdTK.ExecuteNonQuery();

                // 3. Cuối cùng mới xóa nhân viên
                SqlCommand cmdNV = new SqlCommand(
                    "DELETE FROM NhanVien WHERE MaNhanVien = @ma",
                    connection, tran
                );
                cmdNV.Parameters.Add("@ma", SqlDbType.Char).Value = maNV;
                int ret = cmdNV.ExecuteNonQuery();

                tran.Commit();

                if (ret > 0)
                {
                    listView1.Items.Clear();
                    hienThiNhanVien();
                    // Xóa trắng các textbox sau khi xóa thành công
                    txtMa.Clear();
                    txtName.Clear();
                    MessageBox.Show("Xóa nhân viên và các dữ liệu liên quan thành công!");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên để xóa!");
                }
            }
            catch (SqlException ex)
            {
                tran.Rollback();
                MessageBox.Show("Lỗi hệ thống khi xóa:\n" + ex.Message, "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtMa.Text == "")
            {
                MessageBox.Show("Nhập mã nhân viên cần sửa đi má ơi!");
                return;
            }
            else
            {
                if (connection == null)
                {
                    connection = new SqlConnection(strcon);
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "Update NhanVien set TenNhanVien = @ten, DiaChi = @DiaChi, SDT = @sdt, CCCD = @cccd, SoNgayLam = @SoNgayLam where MaNhanVien = @ma";
                    command.Connection = connection;

                    command.Parameters.Add("@ma", SqlDbType.Char).Value = txtMa.Text.TrimEnd();
                    command.Parameters.Add("@ten", SqlDbType.NVarChar).Value = txtName.Text.TrimEnd();
                    command.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = txtDiaChi.Text.TrimEnd();
                    command.Parameters.Add("@sdt", SqlDbType.Char).Value = txtPhone.Text.TrimEnd();
                    command.Parameters.Add("@cccd", SqlDbType.Char).Value = txtCCCD.Text.TrimEnd();
                    command.Parameters.Add("@SoNgayLam", SqlDbType.Int).Value = int.Parse(txtSoGioLam.Text.TrimEnd());



                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        listView1.Items.Clear();
                        hienThiNhanVien();
                        MessageBox.Show("Sửa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Sửa thất bại");
                    }
                }

            }
        }
    }
}
