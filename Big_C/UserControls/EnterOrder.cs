using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Big_C.Model; // Giả định các class Model (NhaCungCap, HangHoa, ChiTietPhieu) đã định nghĩa

namespace Big_C.UserControls
{
    public partial class EnterOrder : UserControl
    {
        // ---------------------------------------------------------
        // KHAI BÁO BIẾN TOÀN CỤC VÀ KẾT NỐI
        // ---------------------------------------------------------
        List<NhaCungCap> lncc = new List<NhaCungCap>();
        List<HangHoa> lhh = new List<HangHoa>();
        List<ChiTietPhieu> dsChiTiet = new List<ChiTietPhieu>();

        // LƯU Ý QUAN TRỌNG: CẦN THAY ĐỔI CHUỖI KẾT NỐI NÀY VỀ MÁY CỦA BẠN HOẶC ĐẶT TRONG App.config
        private const string ConnectionString = "SERVER = DESKTOP-O8DO4M9; DATABASE = QL_BigC; Integrated Security = TRUE";

        // Không cần khai báo biến SqlConnection toàn cục nữa, dùng 'using' để quản lý an toàn hơn.

        private string currentMaPhieu = null;
        private bool isEditing = false;
        private int currentTrangThaiPhieu = 0; // Thêm biến lưu trạng thái: 0=Chưa nhập kho, 1=Đã nhập kho

        public EnterOrder()
        {
            InitializeComponent();
        }

        public EnterOrder(string maPhieu) : this()
        {
            this.currentMaPhieu = maPhieu?.Trim();
            this.isEditing = true;
        }

        // =======================================================
        // 1. KẾT NỐI DATABASE - KHÔNG CẦN HÀM Open/Close NỮA, DÙNG USING
        // =======================================================
        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        // =======================================================
        // 2. TẠO MÃ PHIẾU TỰ ĐỘNG (Đã tối ưu dùng using)
        // =======================================================
        private string GenerateNewMaPhieu()
        {
            int maxIdNumber = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(CAST(SUBSTRING(MaPhieu, 4, LEN(MaPhieu) - 3) AS INT)), 0) FROM PhieuMuaHang WHERE MaPhieu LIKE 'PMH%'", connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            maxIdNumber = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo mã: {ex.Message}", "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            maxIdNumber++;
            return $"PMH{maxIdNumber:D3}";
        }

        // =======================================================
        // 3. LOAD DỮ LIỆU CHUNG (NCC, HÀNG HÓA) (Đã tối ưu dùng using)
        // =======================================================
        private void LoadNhaCungCap()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Select MaNhaCungCap, TenNhaCungCap, DiaChi from NhaCungCap", connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            lncc.Clear();
                            cbo_NhaCC.Items.Clear();

                            while (reader.Read())
                            {
                                NhaCungCap ncc = new NhaCungCap
                                {
                                    MaNhaCungCap = reader.GetString(0),
                                    TenNhaCungCap = reader.GetString(1),
                                    DiaChi = reader.GetString(2)
                                };

                                cbo_NhaCC.Items.Add(ncc.TenNhaCungCap);
                                lncc.Add(ncc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải NCC: " + ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHangHoaTheoNhaCC(string maNcc)
        {
            cbo_TenHH.Items.Clear();
            lhh.Clear();
            cbo_TenHH.Enabled = false;

            if (string.IsNullOrEmpty(maNcc)) return;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("Select MaHangHoa, TenHangHoa, DonGia, MaNhaCungCap from HangHoa where MaNhaCungCap = @mancc", connection))
                    {
                        command.Parameters.AddWithValue("@mancc", maNcc);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HangHoa hh = new HangHoa
                                {
                                    MaHangHoa = reader.GetString(0),
                                    TenHangHoa = reader.GetString(1),
                                    DonGia = reader.GetInt32(2),
                                    MaNhaCungCap = reader.GetString(3)
                                };

                                cbo_TenHH.Items.Add(hh.TenHangHoa);
                                lhh.Add(hh);
                            }
                        }
                    }
                }
                cbo_TenHH.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải Hàng hóa: " + ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 4. Cập nhật hiển thị chi tiết lên DataGridView
        // =======================================================
        private void UpdateDataGridView()
        {
            dgvChiTiet.SuspendLayout(); // Tối ưu: Ngăn DGV nhấp nháy khi cập nhật
            dgvChiTiet.Rows.Clear();

            foreach (var item in dsChiTiet)
            {
                long donGiaLong = (long)(item.DonGia ?? 0);
                long thanhTienLong = (long)item.ThanhTien;

                dgvChiTiet.Rows.Add(
                    item.MaHangHoa,
                    item.TenHangHoa,
                    item.SoLuong,
                    donGiaLong.ToString("N0"),
                    thanhTienLong.ToString("N0")
                );
            }

            dgvChiTiet.ResumeLayout();
            UpdateTongTien();
        }

        // =======================================================
        // 4. LOAD CHI TIẾT ĐỂ SỬA (Đã tối ưu dùng using)
        // =======================================================
        private void LoadOrderData(string maPhieu)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string maNCC = "";

                    // BƯỚC 1: Load thông tin chung (Header) - Thêm TrangThai
                    string sqlHeader = "SELECT MaNhaCungCap, NgayGiao, MoTa, ISNULL(TrangThai, 0) FROM PhieuMuaHang WHERE MaPhieu = @maPhieu";
                    using (SqlCommand cmdHeader = new SqlCommand(sqlHeader, connection))
                    {
                        cmdHeader.Parameters.AddWithValue("@maPhieu", maPhieu);

                        using (SqlDataReader reader = cmdHeader.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                maNCC = reader.GetString(0);
                                dt_NgayGiao.Value = reader.GetDateTime(1);
                                txtMoTa.Text = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                currentTrangThaiPhieu = reader.GetInt32(3);
                            }
                        }
                    }

                    // >>> LOGIC HIỂN THỊ NÚT DỰA TRÊN TRẠNG THÁI <<<
                    bool isLocked = currentTrangThaiPhieu == 1;

                    btnThanhToan.Visible = !isLocked; // Cập nhật/Thanh toán
                    btnThemHang.Enabled = !isLocked;
                    btnXoaHang.Enabled = !isLocked;

                    if (isLocked)
                    {
                        MessageBox.Show($"Phiếu {maPhieu} ĐÃ HOÀN TẤT NHẬP KHO. Mọi thao tác chỉnh sửa đều bị vô hiệu hóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    var nccObj = lncc.FirstOrDefault(n => n.MaNhaCungCap?.Trim() == maNCC?.Trim());
                    if (nccObj != null) cbo_NhaCC.SelectedItem = nccObj.TenNhaCungCap;

                    LoadHangHoaTheoNhaCC(maNCC);

                    // BƯỚC 2: Load danh sách hàng hóa (Details)
                    string sqlDetail = @"
                        SELECT ct.MaHangHoa, hh.TenHangHoa, ct.SoLuong, ct.DonGia
                        FROM PhieuMuaHangChiTiet ct
                        JOIN HangHoa hh ON ct.MaHangHoa = hh.MaHangHoa
                        WHERE ct.MaPhieu = @maPhieu";

                    using (SqlCommand cmdDetail = new SqlCommand(sqlDetail, connection))
                    {
                        cmdDetail.Parameters.AddWithValue("@maPhieu", maPhieu);

                        using (SqlDataReader readerDetail = cmdDetail.ExecuteReader())
                        {
                            dsChiTiet.Clear();

                            while (readerDetail.Read())
                            {
                                ChiTietPhieu item = new ChiTietPhieu
                                {
                                    MaHangHoa = readerDetail.GetString(0),
                                    TenHangHoa = readerDetail.GetString(1),
                                    SoLuong = readerDetail.GetInt32(2),
                                    DonGia = readerDetail.GetInt32(3)
                                };
                                dsChiTiet.Add(item);
                            }
                        }
                    }

                    UpdateDataGridView(); // Cập nhật DGV từ dsChiTiet
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phiếu: " + ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm cập nhật hiển thị tổng tiền
        private void UpdateTongTien()
        {
            long tong = dsChiTiet.Sum(x => (long)x.ThanhTien);
            txtTongTien.Text = tong.ToString("N0");
        }

        // =======================================================
        // 5. SỰ KIỆN LOAD FORM
        // =======================================================
        private void EnterOrder_Load(object sender, EventArgs e)
        {
            LoadNhaCungCap();

            txtMaPhieu.Enabled = false;
            txtSoLuong.Text = "1";

            if (isEditing)
            {
                txtMaPhieu.Text = currentMaPhieu;
                btnThanhToan.Text = "CẬP NHẬT";
                cbo_NhaCC.Enabled = false;

                LoadOrderData(currentMaPhieu);
            }
            else
            {
                string newMaPhieu = GenerateNewMaPhieu();
                if (newMaPhieu == null) return;
                txtMaPhieu.Text = newMaPhieu;
                btnThanhToan.Text = "THANH TOÁN";
                cbo_TenHH.Enabled = false;
            }
        }

        // =======================================================
        // 6. CHỌN NHÀ CUNG CẤP -> LOAD HÀNG HÓA
        // =======================================================
        private void cbo_NhaCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maNcc = lncc.FirstOrDefault(x => x.TenNhaCungCap == cbo_NhaCC.Text)?.MaNhaCungCap;
            LoadHangHoaTheoNhaCC(maNcc);
        }

        // =======================================================
        // 7. THÊM HÀNG VÀO DANH SÁCH (LƯỚI)
        // =======================================================
        private void btnThemHang_Click(object sender, EventArgs e)
        {
            if (currentTrangThaiPhieu == 1)
            {
                MessageBox.Show("Phiếu đã hoàn tất nhập kho, không thể thêm hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbo_TenHH.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn hàng hóa", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbo_TenHH.Focus();
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương (> 0)", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            // Dùng null-coalescing operator để kiểm tra null an toàn hơn
            HangHoa hh = lhh.FirstOrDefault(x => x.TenHangHoa == cbo_TenHH.Text);
            if (hh == null) return;

            try
            {
                var existingItem = dsChiTiet.FirstOrDefault(x => x.MaHangHoa?.Trim() == hh.MaHangHoa?.Trim());

                if (existingItem != null)
                {
                    // Cập nhật và cộng dồn trong Model
                    existingItem.SoLuong = (existingItem.SoLuong ?? 0) + soLuong;

                    // Cập nhật lại Grid View hiển thị bằng cách tìm theo MaHangHoa
                    foreach (DataGridViewRow row in dgvChiTiet.Rows)
                    {
                        if (row.Cells["colMaHang"].Value != null && row.Cells["colMaHang"].Value.ToString().Trim() == hh.MaHangHoa.Trim())
                        {
                            row.Cells["colSoLuong"].Value = existingItem.SoLuong;
                            row.Cells["colThanhTien"].Value = existingItem.ThanhTien.ToString("N0");
                            break;
                        }
                    }
                }
                else
                {
                    // Thêm mới vào Model
                    ChiTietPhieu ct = new ChiTietPhieu
                    {
                        MaHangHoa = hh.MaHangHoa,
                        TenHangHoa = hh.TenHangHoa,
                        SoLuong = soLuong,
                        DonGia = hh.DonGia
                    };
                    dsChiTiet.Add(ct);

                    // Đổ lên lưới DataGridView
                    long donGiaFormatted = (long)(ct.DonGia ?? 0);
                    dgvChiTiet.Rows.Add(ct.MaHangHoa, ct.TenHangHoa, ct.SoLuong, donGiaFormatted.ToString("N0"), ct.ThanhTien.ToString("N0"));
                }

                UpdateTongTien();

                // Tối ưu hóa UI
                txtSoLuong.Text = "1";
                cbo_TenHH.SelectedIndex = -1;
                cbo_TenHH.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hàng: " + ex.Message, "Lỗi thêm chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 8. XÓA HÀNG KHỎI DANH SÁCH (LƯỚI)
        // =======================================================
        private void btnXoaHang_Click(object sender, EventArgs e)
        {
            if (currentTrangThaiPhieu == 1)
            {
                MessageBox.Show("Phiếu đã hoàn tất nhập kho, không thể xóa hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvChiTiet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hàng này khỏi phiếu mua hàng?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            try
            {
                // Sử dụng List tạm để lưu các mã hàng cần xóa
                List<string> maHangHoaToRemove = new List<string>();

                for (int i = dgvChiTiet.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dgvChiTiet.SelectedRows[i];

                    if (row.Cells["colMaHang"].Value == null) continue;
                    string maHangHoa = row.Cells["colMaHang"].Value.ToString().Trim();

                    maHangHoaToRemove.Add(maHangHoa);
                    dgvChiTiet.Rows.RemoveAt(row.Index);
                }

                // Xóa khỏi danh sách Model (dsChiTiet)
                dsChiTiet.RemoveAll(x => maHangHoaToRemove.Contains(x.MaHangHoa?.Trim()));

                UpdateTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =======================================================
        // 9. LƯU (THANH TOÁN / CẬP NHẬT) (Đã tối ưu dùng using và Transaction)
        // =======================================================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (currentTrangThaiPhieu == 1)
            {
                MessageBox.Show("Phiếu đã hoàn tất nhập kho, không thể cập nhật thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dsChiTiet.Count == 0)
            {
                MessageBox.Show("Phiếu chưa có hàng nào. Vui lòng thêm hàng trước khi Thanh Toán/Cập Nhật.");
                return;
            }

            string maNCC = lncc.FirstOrDefault(x => x.TenNhaCungCap == cbo_NhaCC.Text)?.MaNhaCungCap;
            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Vui lòng chọn Nhà Cung Cấp.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            long tongTien = dsChiTiet.Sum(x => (long)x.ThanhTien);

            // Bắt đầu Transaction
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();

                try
                {
                    // A. XỬ LÝ PHIẾU MUA HÀNG (HEADER)
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = tran;

                        if (isEditing)
                        {
                            // Chế độ Sửa: UPDATE
                            cmd.CommandText = @"UPDATE PhieuMuaHang SET NgayGiao = @ngaygiao, TongTien = @tongtien, MoTa = @mota WHERE MaPhieu = @maphieu";

                            // Xóa chi tiết cũ
                            using (SqlCommand cmdDel = new SqlCommand("DELETE FROM PhieuMuaHangChiTiet WHERE MaPhieu = @maphieu", connection, tran))
                            {
                                cmdDel.Parameters.AddWithValue("@maphieu", txtMaPhieu.Text);
                                cmdDel.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Chế độ Thêm mới: INSERT
                            cmd.CommandText = @"INSERT INTO PhieuMuaHang(MaPhieu, MaNhaCungCap, NgayDat, NgayGiao, TongTien, MoTa, TrangThai)
                                                VALUES(@maphieu, @mancc, GETDATE(), @ngaygiao, @tongtien, @mota, 0)"; // Thêm TrangThai=0
                            cmd.Parameters.AddWithValue("@mancc", maNCC);
                        }

                        cmd.Parameters.AddWithValue("@maphieu", txtMaPhieu.Text);
                        cmd.Parameters.AddWithValue("@ngaygiao", dt_NgayGiao.Value);
                        cmd.Parameters.AddWithValue("@tongtien", tongTien);
                        cmd.Parameters.AddWithValue("@mota", txtMoTa.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // B. XỬ LÝ CHI TIẾT (INSERT LẠI TẤT CẢ)
                    string sqlCT = @"INSERT INTO PhieuMuaHangChiTiet(MaPhieu, MaHangHoa, SoLuong, DonGia)
                                     VALUES(@maphieu, @mahh, @soluong, @dongia)";

                    using (SqlCommand cmdCT = new SqlCommand(sqlCT, connection, tran))
                    {
                        cmdCT.Parameters.Add("@maphieu", SqlDbType.NVarChar, 10).Value = txtMaPhieu.Text;
                        cmdCT.Parameters.Add("@mahh", SqlDbType.NVarChar, 10);
                        cmdCT.Parameters.Add("@soluong", SqlDbType.Int);
                        cmdCT.Parameters.Add("@dongia", SqlDbType.Int);

                        foreach (var ct in dsChiTiet)
                        {
                            cmdCT.Parameters["@mahh"].Value = ct.MaHangHoa;
                            cmdCT.Parameters["@soluong"].Value = ct.SoLuong ?? 0;
                            cmdCT.Parameters["@dongia"].Value = ct.DonGia ?? 0;
                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    // C. HOÀN TẤT
                    tran.Commit();
                    MessageBox.Show(isEditing ? "Cập nhật thành công!" : "Thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset form nếu là thêm mới
                    if (!isEditing)
                    {
                        dsChiTiet.Clear();
                        dgvChiTiet.Rows.Clear();
                        txtTongTien.Text = "0";
                        txtMaPhieu.Text = GenerateNewMaPhieu();
                        cbo_NhaCC.SelectedIndex = -1;
                        cbo_TenHH.Items.Clear();
                        cbo_TenHH.Enabled = false;
                        txtMoTa.Clear();
                        dt_NgayGiao.Value = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        }
    }