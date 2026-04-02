using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Big_C.UserControls
{
    public partial class LichLam : UserControl
    {
        string strcon = "SERVER = DESKTOP-O8DO4M9; DATABASE = QL_BigC; Integrated Security = TRUE";

        enum TrangThaiLich { ChuaCo, DaCo }

        public LichLam()
        {
            InitializeComponent();
            // Đăng ký sự kiện double click
            listView1.DoubleClick += listView1_DoubleClick;
        }

        private void LichLam_Load(object sender, EventArgs e)
        {
            LoadLichLam();
        }

        public void LoadLichLam()
        {
            listView1.Items.Clear();
            string sql = @"
                SELECT NV.MaNhanVien, NV.TenNhanVien, LL.NgayLam, LL.CaLam, LL.GhiChu
                FROM NhanVien NV
                LEFT JOIN LichLam LL ON NV.MaNhanVien = LL.MaNhanVien
                ORDER BY ISNULL(LL.NgayLam, GETDATE()) DESC";

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
                            bool coLich = !reader.IsDBNull(2);
                            ListViewItem item = new ListViewItem(reader.GetString(0)); // MaNV
                            item.SubItems.Add(reader.GetString(1)); // TenNV

                            item.SubItems.Add(coLich ? reader.GetDateTime(2).ToShortDateString() : "— Chưa có —");
                            item.SubItems.Add(coLich ? reader.GetString(3) : "Chưa phân ca");
                            item.SubItems.Add(reader.IsDBNull(4) ? "" : reader.GetString(4));

                            if (coLich)
                            {
                                item.SubItems.Add("Đã có lịch");
                                item.BackColor = Color.FromArgb(220, 248, 220);
                                item.Tag = TrangThaiLich.DaCo;
                            }
                            else
                            {
                                item.SubItems.Add("Chưa có lịch");
                                item.BackColor = Color.FromArgb(255, 230, 230);
                                item.Tag = TrangThaiLich.ChuaCo;
                            }
                            listView1.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            var item = listView1.SelectedItems[0];
            string maNV = item.SubItems[0].Text.Trim();
            string tenNV = item.SubItems[1].Text.Trim();
            TrangThaiLich trangThai = (TrangThaiLich)item.Tag;

            Form editForm = new Form();
            EditLichLam ucEdit;

            if (trangThai == TrangThaiLich.ChuaCo)
            {
                ucEdit = new EditLichLam(maNV);
                editForm.Text = $"Phân ca cho: {tenNV}";
            }
            else
            {
                DateTime ngayLam = DateTime.Parse(item.SubItems[2].Text);
                ucEdit = new EditLichLam(maNV, ngayLam);
                editForm.Text = $"Sửa lịch: {tenNV}";
            }

            HienThiFormEdit(editForm, ucEdit);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Form editForm = new Form();
            EditLichLam ucEdit = new EditLichLam(); // Chế độ thêm mới hoàn toàn
            editForm.Text = "Thêm Lịch Làm Việc Mới";
            HienThiFormEdit(editForm, ucEdit);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            listView1_DoubleClick(sender, e); // Tái sử dụng logic double click
        }

        // Hàm phụ để hiển thị Form chứa UserControl Edit
        private void HienThiFormEdit(Form f, EditLichLam uc)
        {
            f.Controls.Add(uc);
            uc.Dock = DockStyle.Fill;
            f.Width = 550;
            f.Height = 450;
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadLichLam();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên ListView chưa
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lịch làm việc cần xóa!", "Thông báo");
                return;
            }

            var item = listView1.SelectedItems[0];

            // 2. Kiểm tra xem dòng được chọn có lịch để xóa không (dựa vào Tag đã set lúc Load)
            if ((TrangThaiLich)item.Tag == TrangThaiLich.ChuaCo)
            {
                MessageBox.Show("Nhân viên này hiện chưa có lịch làm để xóa!", "Thông báo");
                return;
            }

            string maNV = item.SubItems[0].Text.Trim();
            string tenNV = item.SubItems[1].Text.Trim();
            string ngayLamStr = item.SubItems[2].Text; // Lấy ngày làm từ cột thứ 3

            // 3. Xác nhận xóa
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa lịch làm ngày {ngayLamStr} của nhân viên {tenNV}?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                // 4. Thực hiện lệnh xóa trong SQL
                // Lưu ý: Chỉ xóa trong bảng LichLam, không đụng đến bảng NhanVien
                string sql = "DELETE FROM LichLam WHERE MaNhanVien = @maNV AND NgayLam = @ngayLam";

                try
                {
                    using (SqlConnection connection = new SqlConnection(strcon))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // Truyền tham số để tránh lỗi định dạng ngày tháng và SQL Injection
                            command.Parameters.Add("@maNV", SqlDbType.Char).Value = maNV;
                            command.Parameters.Add("@ngayLam", SqlDbType.Date).Value = DateTime.Parse(ngayLamStr);

                            int result = command.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Xóa lịch làm việc thành công!", "Thành công");
                                LoadLichLam(); // Tải lại danh sách sau khi xóa
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy lịch làm việc để xóa hoặc đã có lỗi xảy ra.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi Database");
                }
            }
        }
    }
}