// Big_C-master/Big_C/UserControls/Suppport.cs

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Cần thiết cho SQL

namespace Big_C.UserControls
{
    public partial class Suppport : UserControl
    {
        public Suppport()
        {
            InitializeComponent();
        }

        // Khai báo kết nối
        string strcon = "SERVER = DESKTOP-O8DO4M9; DATABASE = QL_BigC; Integrated Security = TRUE";
        SqlConnection connection = null;

        private void btnPhanHoi_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra dữ liệu nhập vào (Validation)
            if (string.IsNullOrWhiteSpace(txtMaNhanVien.Text) || string.IsNullOrWhiteSpace(txtTieuDe.Text) || string.IsNullOrWhiteSpace(txtNoiDungHoTro.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Mã Nhân Viên, Tiêu Đề và Nội Dung.", "Lỗi Thiếu Thông Tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (connection == null)
                {
                    connection = new SqlConnection(strcon);
                }
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                // 2. Định nghĩa câu lệnh INSERT
                string sqlInsert = @"INSERT INTO PhanHoiHoTro (MaNhanVien, TieuDe, NoiDung) 
                                     VALUES (@MaNV, @TieuDe, @NoiDung)";

                SqlCommand command = new SqlCommand(sqlInsert, connection);

                // 3. Sử dụng Parameter để tránh lỗi SQL Injection
                command.Parameters.AddWithValue("@MaNV", txtMaNhanVien.Text.Trim());
                command.Parameters.AddWithValue("@TieuDe", txtTieuDe.Text.Trim());
                command.Parameters.AddWithValue("@NoiDung", txtNoiDungHoTro.Text.Trim());

                // 4. Thực thi câu lệnh
                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("✅ Phản hồi của bạn đã được gửi thành công và lưu vào hệ thống. Chúng tôi sẽ xử lý sớm nhất có thể!", "Gửi Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Xóa nội dung sau khi gửi thành công
                    txtTieuDe.Text = "";
                    txtNoiDungHoTro.Text = "";
                    txtMaNhanVien.Focus();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi gửi phản hồi. Vui lòng thử lại.", "Lỗi Gửi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                // Xử lý trường hợp Mã Nhân Viên không tồn tại hoặc lỗi SQL khác
                MessageBox.Show($"Lỗi Database: {sqlEx.Message}\n\nKiểm tra Mã Nhân Viên và kết nối SQL Server.", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi Ứng Dụng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}