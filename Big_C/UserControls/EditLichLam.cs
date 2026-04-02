using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Big_C.UserControls
{
    public partial class EditLichLam : UserControl
    {
        string strcon = "SERVER = DESKTOP-O8DO4M9; DATABASE = QL_BigC; Integrated Security = TRUE";

        private string currentMaNV = null;
        private DateTime currentNgayLam;
        private bool isEditing = false;

        // =======================================================
        // CÁC CONSTRUCTOR (KHỞI TẠO)
        // =======================================================

        // 1. Constructor mặc định (Dùng cho Thêm mới hoàn toàn)
        public EditLichLam()
        {
            InitializeComponent();
            isEditing = false;
        }

        // 2. Constructor dùng khi Click Phân Ca cho 1 người (Chưa có lịch)
        public EditLichLam(string maNV) : this()
        {
            this.currentMaNV = maNV.Trim();
            this.isEditing = false; // Vẫn là thêm mới nhưng có sẵn mã NV
        }

        // 3. Constructor dùng khi Sửa lịch đã tồn tại
        public EditLichLam(string maNV, DateTime ngayLam) : this()
        {
            this.currentMaNV = maNV.Trim();
            this.currentNgayLam = ngayLam;
            this.isEditing = true;
        }

        // =======================================================
        // SỰ KIỆN LOAD FORM
        // =======================================================
        private void EditLichLam_Load(object sender, EventArgs e)
        {
            LoadNhanVienToComboBox();

            // Đổi tên nút bấm tùy theo chế độ
            btnSave.Text = isEditing ? "CẬP NHẬT" : "THÊM MỚI";

            if (isEditing)
            {
                // Chế độ Sửa: Load dữ liệu cũ và khóa các trường định danh
                LoadExistingSchedule(currentMaNV, currentNgayLam);
                cboMaNV.Enabled = false;
                dtNgayLam.Enabled = false;
            }
            else
            {
                // Chế độ Thêm
                dtNgayLam.Value = DateTime.Today;
                if (!string.IsNullOrEmpty(currentMaNV))
                {
                    cboMaNV.SelectedItem = currentMaNV;
                    cboMaNV.Enabled = false; // Nếu truyền MaNV từ ngoài vào thì khóa lại
                }
            }
        }

        private void LoadNhanVienToComboBox()
        {
            cboMaNV.Items.Clear();
            string sql = "SELECT MaNhanVien FROM NhanVien";
            try
            {
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cboMaNV.Items.Add(reader["MaNhanVien"].ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải NV: " + ex.Message); }
        }

        private void LoadExistingSchedule(string maNV, DateTime ngayLam)
        {
            string sql = "SELECT * FROM LichLam WHERE MaNhanVien = @maNV AND NgayLam = @ngayLam";
            try
            {
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@maNV", maNV);
                        command.Parameters.AddWithValue("@ngayLam", ngayLam.Date);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cboMaNV.SelectedItem = reader["MaNhanVien"].ToString().Trim();
                                dtNgayLam.Value = Convert.ToDateTime(reader["NgayLam"]);
                                txtCaLam.Text = reader["CaLam"].ToString();
                                txtGhiChu.Text = reader["GhiChu"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu cũ: " + ex.Message); }
        }

        // =======================================================
        // CHỨC NĂNG LƯU
        // =======================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboMaNV.SelectedItem == null || string.IsNullOrWhiteSpace(txtCaLam.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo");
                return;
            }

            string sql = isEditing
                ? "UPDATE LichLam SET CaLam = @caLam, GhiChu = @ghiChu WHERE MaNhanVien = @maNV AND NgayLam = @ngayLam"
                : "INSERT INTO LichLam (MaNhanVien, NgayLam, CaLam, GhiChu) VALUES (@maNV, @ngayLam, @caLam, @ghiChu)";

            try
            {
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@maNV", cboMaNV.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@ngayLam", dtNgayLam.Value.Date);
                        command.Parameters.AddWithValue("@caLam", txtCaLam.Text.Trim());
                        command.Parameters.AddWithValue("@ghiChu", txtGhiChu.Text.Trim());

                        if (command.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Thao tác thành công!");
                            this.ParentForm.DialogResult = DialogResult.OK;
                            this.ParentForm.Close();
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi DB: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }
    }
}