using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Big_C.Model;

namespace Big_C.UserControls
{
    public partial class ViewOrder : UserControl
    {
        // Chuỗi kết nối Database
        string strcon = "SERVER = DESKTOP-O8DO4M9; DATABASE = QL_BigC; Integrated Security = TRUE";

        // 1. Tạo biến ToolTip riêng để tự quản lý hiển thị
        ToolTip toolTip1 = new ToolTip();
        // Biến lưu dòng đang được di chuột để tránh nhấp nháy
        ListViewItem currentHoverItem = null;

        public ViewOrder()
        {
            InitializeComponent();

            // 2. QUAN TRỌNG: Tắt tooltip mặc định của ListView đi (vì nó chỉ hiện ở cột 1)
            listView1.ShowItemToolTips = false;

            // 3. Gán sự kiện di chuyển chuột
            listView1.MouseMove += ListView1_MouseMove;
        }

        // =======================================================
        // SỰ KIỆN MỚI: XỬ LÝ DI CHUỘT ĐỂ HIỆN TOOLTIP
        // =======================================================
        private void ListView1_MouseMove(object sender, MouseEventArgs e)
        {
            // Lấy dòng (Item) tại vị trí con trỏ chuột
            ListViewItem item = listView1.GetItemAt(e.X, e.Y);

            if (item != null)
            {
                // Nếu chuột chuyển sang một dòng mới khác với dòng cũ
                if (item != currentHoverItem)
                {
                    currentHoverItem = item;
                    // Nếu dòng đó có nội dung Tooltip thì hiện lên
                    if (!string.IsNullOrEmpty(item.ToolTipText))
                    {
                        // Hiện Tooltip ngay cạnh con chuột, giữ trong 3 giây (3000ms)
                        toolTip1.Show(item.ToolTipText, listView1, e.Location.X + 15, e.Location.Y + 10, 3000);
                    }
                }
            }
            else
            {
                // Nếu di chuột ra vùng trắng (không có dòng nào) thì ẩn Tooltip
                if (currentHoverItem != null)
                {
                    toolTip1.Hide(listView1);
                    currentHoverItem = null;
                }
            }
        }

        private void ViewOrder_Load(object sender, EventArgs e)
        {
            LoadPhieuMuaHang();
        }

        // =======================================================
        // PHƯƠNG THỨC: Tải và hiển thị danh sách Đơn hàng
        // =======================================================
        public void LoadPhieuMuaHang()
        {
            listView1.Items.Clear();

            string sql = @"
            SELECT PMH.MaPhieu, 
                STUFF((
                    SELECT ', ' + HH.TenHangHoa
                    FROM PhieuMuaHangChiTiet CT_HH
                    JOIN HangHoa HH ON CT_HH.MaHangHoa = HH.MaHangHoa
                    WHERE CT_HH.MaPhieu = PMH.MaPhieu
                    FOR XML PATH('')
                ), 1, 2, '') AS DanhSachHangHoa, 
                NCC.TenNhaCungCap AS TenNCC,        
                PMH.NgayDat,                        
                PMH.NgayGiao,                       
                SUM(CT.SoLuong) AS TongSoLuong,     
                PMH.TongTien,                       
                PMH.MoTa,                           
                ISNULL(PMH.TrangThai,0) AS TrangThai 
            FROM PhieuMuaHang PMH
            JOIN NhaCungCap NCC ON PMH.MaNhaCungCap = NCC.MaNhaCungCap
            LEFT JOIN PhieuMuaHangChiTiet CT ON PMH.MaPhieu = CT.MaPhieu
            GROUP BY PMH.MaPhieu, NCC.TenNhaCungCap, PMH.NgayDat, PMH.NgayGiao, PMH.TongTien, PMH.MoTa, PMH.TrangThai
            ORDER BY PMH.MaPhieu";

            try
            {
                using (SqlConnection connection = new SqlConnection(strcon))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader.GetString(0)); // 0. Mã phiếu
                                item.SubItems.Add(reader.IsDBNull(1) ? "" : reader.GetString(1)); // 1. Tên hàng
                                item.SubItems.Add(reader.GetString(2)); // 2. NCC
                                item.SubItems.Add(reader.GetDateTime(3).ToShortDateString()); // 3. Ngày đặt

                                // 4. NGÀY GIAO
                                DateTime ngayGiao = reader.GetDateTime(4);
                                item.SubItems.Add(ngayGiao.ToShortDateString());

                                item.SubItems.Add(reader.IsDBNull(5) ? "0" : reader.GetInt32(5).ToString()); // 5. Số lượng
                                item.SubItems.Add(reader.IsDBNull(6) ? "0" : reader.GetInt32(6).ToString("N0")); // 6. Tổng tiền
                                item.SubItems.Add(reader.IsDBNull(7) ? "" : reader.GetString(7)); // 7. Mô tả

                                // =======================================================
                                // TÔ MÀU VÀ GÁN NỘI DUNG TOOLTIP
                                // =======================================================
                                if (ngayGiao.Date < DateTime.Now.Date)
                                {
                                    item.BackColor = Color.OrangeRed;
                                    item.ForeColor = Color.White;
                                    // Nội dung này sẽ được hiện bởi hàm MouseMove ở trên
                                    item.ToolTipText = "Cảnh báo: Đơn hàng TỒN KHO (Đã quá hạn ngày giao)";
                                }
                                else
                                {
                                    item.BackColor = Color.LightGreen;
                                    item.ForeColor = Color.Black;
                                    item.ToolTipText = "Trạng thái: Ổn định (Chưa đến hạn hoặc giao hôm nay)";
                                }

                                item.UseItemStyleForSubItems = true;
                                // =======================================================

                                listView1.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải đơn hàng: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // CHỨC NĂNG: XÓA ĐƠN HÀNG
        // =======================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = listView1.SelectedItems[0].SubItems[0].Text;

            DialogResult dialogResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa đơn hàng {maPhieu}?\nTất cả chi tiết liên quan cũng sẽ bị xóa.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                string sqlDelete = @"
                    DELETE FROM PhieuMuaHangChiTiet WHERE MaPhieu = @ma;
                    DELETE FROM PhieuMuaHang WHERE MaPhieu = @ma;";

                try
                {
                    using (SqlConnection connection = new SqlConnection(strcon))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sqlDelete, connection))
                        {
                            command.Parameters.AddWithValue("@ma", maPhieu.Trim());
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPhieuMuaHang();
                            }
                            else
                            {
                                MessageBox.Show("Xóa thất bại. Không tìm thấy mã phiếu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa đơn hàng: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =======================================================
        // CHỨC NĂNG: SỬA ĐƠN HÀNG
        // =======================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = listView1.SelectedItems[0].SubItems[0].Text;

            try
            {
                Form editForm = new Form();
                EnterOrder ucEdit = new EnterOrder(maPhieu);

                editForm.Controls.Add(ucEdit);
                ucEdit.Dock = DockStyle.Fill;
                editForm.Text = $"Sửa Đơn Hàng: {maPhieu.Trim()}";
                editForm.Width = 700;
                editForm.Height = 500;
                editForm.StartPosition = FormStartPosition.CenterScreen;

                editForm.ShowDialog();
                LoadPhieuMuaHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form sửa: {ex.Message}", "Lỗi Ứng Dụng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // =======================================================
        // CHỨC NĂNG: XUẤT HÀNG
        // =======================================================
        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = listView1.SelectedItems[0].SubItems[0].Text;

            DialogResult result = MessageBox.Show(
                $"Xác nhận xuất hàng cho đơn {maPhieu}?\nSau khi xuất, đơn hàng sẽ bị xóa khỏi hệ thống.",
                "Xác nhận xuất hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            string sqlXuatHang = @"
                DELETE FROM PhieuMuaHangChiTiet WHERE MaPhieu = @ma;
                DELETE FROM PhieuMuaHang WHERE MaPhieu = @ma;";

            try
            {
                using (SqlConnection conn = new SqlConnection(strcon))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlXuatHang, conn))
                    {
                        cmd.Parameters.AddWithValue("@ma", maPhieu.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Xuất hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuMuaHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}