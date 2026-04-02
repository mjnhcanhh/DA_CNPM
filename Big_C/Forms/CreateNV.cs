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
    public partial class CreateNV : Form
    {
        public CreateNV()
        {
            InitializeComponent();
        }

        // Khai báo chuỗi kết nối
        private string strcon = "SERVER = DESKTOP-O8DO4M9; DATABASE = QL_BigC; Integrated Security = TRUE";

        // Phương thức TẠO MÃ NHÂN VIÊN TỰ ĐỘNG (ví dụ: NV001, NV002,...)
        private string GenerateNewMaNhanVien()
        {
            int maxIdNumber = 0;
            try
            {
                // Sử dụng 'using' để đảm bảo kết nối được đóng ngay cả khi có lỗi
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.Text;

                    // Truy vấn lấy số thứ tự lớn nhất từ MaNhanVien (cắt bỏ 'NV' và chuyển sang INT)
                    command.CommandText = "SELECT MAX(CAST(SUBSTRING(MaNhanVien, 3, LEN(MaNhanVien) - 2) AS INT)) FROM NhanVien WHERE MaNhanVien LIKE 'NV%'";
                    command.Connection = connection;

                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        maxIdNumber = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo mã nhân viên tự động: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "ERROR"; // Trả về mã lỗi nếu có vấn đề
            }

            // Tăng số thứ tự lên 1
            maxIdNumber++;

            // Format lại mã mới (D3 đảm bảo có 3 chữ số, ví dụ: NV001)
            return $"NV{maxIdNumber:D3}";
        }

        // Phương thức hiển thị danh sách nhân viên lên ListView
        public void HienThiNhanVien()
        {
            listView1.Items.Clear();

            try
            {
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    string sql = "SELECT MaNhanVien, TenNhanVien, NgaySinh, DiaChi, SDT, CCCD, NgayVaoLam, SoNgayLam, MaQuanLy FROM NhanVien";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader.GetString(0));
                                item.SubItems.Add(reader.GetString(1));
                                item.SubItems.Add(reader.GetDateTime(2).ToShortDateString());
                                item.SubItems.Add(reader.GetString(3));
                                item.SubItems.Add(reader.GetString(4));
                                item.SubItems.Add(reader.GetString(5));
                                item.SubItems.Add(reader.GetDateTime(6).ToShortDateString());
                                item.SubItems.Add(reader.GetInt32(7).ToString());
                                item.SubItems.Add(reader.GetString(8));
                                listView1.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện Load Form
        private void CreateNV_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu nhân viên hiện có
            HienThiNhanVien();

            // GÁN MÃ NHÂN VIÊN TỰ ĐỘNG KHI FORM MỞ
            txtMa.Text = GenerateNewMaNhanVien();

            // Vô hiệu hóa textbox Mã nhân viên để người dùng không chỉnh sửa
            txtMa.Enabled = false;

            // TODO: Thêm logic tải danh sách MaQuanLy vào Combobox/TextBox nếu cần
        }

        // Sự kiện click nút Thêm Nhân Viên
        private void btnAddNV_Click(object sender, EventArgs e)
        {
            // 1. VALIDATION
            if (!ValidateInput())
            {
                return; // Ngừng nếu dữ liệu không hợp lệ
            }

            // Lấy và xác thực giá trị số ngày làm
            if (!int.TryParse(txtSoGioLam.Text.Trim(), out int soNgayLam))
            {
                MessageBox.Show("Số Ngày Làm không hợp lệ (phải là số nguyên).", "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. THỰC HIỆN INSERT VÀ BẮT LỖI SQL
            try
            {
                InsertNhanVienToDatabase(soNgayLam);
            }
            catch (SqlException sqlEx)
            {
                // Bắt lỗi trùng lặp (2627: Primary key violation, 2601: Unique constraint violation)
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    MessageBox.Show("Thêm thất bại. SĐT hoặc CCCD đã bị trùng.", "Lỗi Dữ Liệu Trùng Lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (sqlEx.Number == 547) // Lỗi khóa ngoại (MaQuanLy không tồn tại)
                {
                    MessageBox.Show("Thêm thất bại. Mã Quản Lý không tồn tại trong hệ thống.", "Lỗi Khóa Ngoại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Lỗi SQL không xác định: {sqlEx.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Thêm thất bại: {ex.Message}", "Lỗi Ứng Dụng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Phương thức tách biệt: Thực hiện validation đầu vào
        private bool ValidateInput()
        {
            // Loại bỏ kiểm tra txtMa.Text vì Mã đã được tạo tự động
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChi.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtCCCD.Text) || string.IsNullOrWhiteSpace(txtSoGioLam.Text) ||
                string.IsNullOrWhiteSpace(txtMaQuanLy.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tất cả các trường thông tin (ngoại trừ Mã Nhân Viên đã được tạo tự động).", "Lỗi Thiếu Thông Tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Thêm validation khác tại đây (VD: độ dài SĐT, định dạng CCCD, v.v.)
            return true;
        }


        // Phương thức tách biệt: Logic truy vấn và thêm dữ liệu vào DB
        private void InsertNhanVienToDatabase(int soNgayLam)
        {
            using (SqlConnection connection = new SqlConnection(strcon))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("sp_ThemNhanVienVaTaiKhoan", connection);
                command.CommandType = CommandType.StoredProcedure;

                string maNV = txtMa.Text.Trim(); // LƯU LẠI MÃ CŨ

                command.Parameters.Add("@MaNV", SqlDbType.Char, 10).Value = maNV;
                command.Parameters.Add("@TenNV", SqlDbType.NVarChar, 50).Value = txtName.Text.Trim();
                command.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = dt_NgaySinh.Value;
                command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 100).Value = txtDiaChi.Text.Trim();
                command.Parameters.Add("@SDT", SqlDbType.Char, 13).Value = txtPhone.Text.Trim();
                command.Parameters.Add("@CCCD", SqlDbType.Char, 15).Value = txtCCCD.Text.Trim();
                command.Parameters.Add("@NgayVaoLam", SqlDbType.Date).Value = dt_NgayVaoLam.Value;
                command.Parameters.Add("@SoNgayLam", SqlDbType.Int).Value = soNgayLam;
                command.Parameters.Add("@MaQuanLy", SqlDbType.Char, 10).Value = txtMaQuanLy.Text.Trim();

                command.ExecuteNonQuery(); // CHẠY ĐƯỢC = THÀNH CÔNG

                // Reload
                HienThiNhanVien();

                // Tạo mã mới cho lần thêm tiếp
                txtMa.Text = GenerateNewMaNhanVien();

                MessageBox.Show(
                    "✅ Thêm nhân viên & tạo tài khoản thành công!\n\n" +
                    $"🔐 Tên đăng nhập: {maNV}\n" +
                    "🔐 Mật khẩu mặc định: 123",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

    }

}

