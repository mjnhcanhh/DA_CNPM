using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Big_C.UserControls
{
    public partial class ChamCong : UserControl
    {
        private readonly string strcon =
            "SERVER=DESKTOP-O8DO4M9;DATABASE=QL_BigC;Integrated Security=TRUE";

        private const int LUONG_GIO = 24000;

        public ChamCong()
        {
            InitializeComponent();
        }

        private void ChamCong_Load(object sender, EventArgs e)
        {
            txtLuongTrenGio.Text = $"{LUONG_GIO:N0} VNĐ";
            LoadNhanVienChuaThanhToan();
        }

        // ================= LOAD NHÂN VIÊN CHƯA THANH TOÁN =================
        private void LoadNhanVienChuaThanhToan()
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT DISTINCT nv.MaNhanVien, nv.TenNhanVien
                      FROM NhanVien nv
                      JOIN ChamCong cc ON nv.MaNhanVien = cc.MaNV",
                    conn);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "TenNhanVien";
                cboNhanVien.ValueMember = "MaNhanVien";
            }
        }

        // ================= CHỌN NHÂN VIÊN =================
        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null) return;

            string maNV = cboNhanVien.SelectedValue.ToString();

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();

                SqlCommand cmdInfo = new SqlCommand(
                    "SELECT TenNhanVien, CCCD, SDT FROM NhanVien WHERE MaNhanVien=@ma",
                    conn);
                cmdInfo.Parameters.AddWithValue("@ma", maNV);

                SqlDataReader rd = cmdInfo.ExecuteReader();
                if (rd.Read())
                {
                    txtName.Text = rd["TenNhanVien"].ToString();
                    txtCCCD.Text = rd["CCCD"].ToString();
                    txtPhone.Text = rd["SDT"].ToString();
                }
                rd.Close();

                SqlCommand cmdGio = new SqlCommand(
                    @"SELECT ISNULL(SUM(DATEDIFF(MINUTE, GioVao, GioRa)) / 60.0, 0)
                      FROM ChamCong
                      WHERE MaNV=@ma AND GioRa IS NOT NULL",
                    conn);

                cmdGio.Parameters.AddWithValue("@ma", maNV);
                double gioLam = Convert.ToDouble(cmdGio.ExecuteScalar());

                txtSoGioLam.Text = gioLam.ToString("0.0");
                txtTongLuong.Text = ((int)(gioLam * LUONG_GIO)).ToString();
            }
        }

        // ================= THANH TOÁN =================
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Xác nhận thanh toán lương?",
                "Thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq == DialogResult.No) return;

            string maNV = cboNhanVien.SelectedValue.ToString();

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();

                // 1️⃣ Xuất phiếu lương
                XuatPhieuLuongWord(maNV);

                // 2️⃣ XÓA DỮ LIỆU CHẤM CÔNG → KHÔNG TRẢ TRÙNG
                SqlCommand cmdXoa = new SqlCommand(
                    "DELETE FROM ChamCong WHERE MaNV=@ma",
                    conn);
                cmdXoa.Parameters.AddWithValue("@ma", maNV);
                cmdXoa.ExecuteNonQuery();
            }

            MessageBox.Show("Thanh toán lương thành công!");

            ClearForm();
            LoadNhanVienChuaThanhToan();
        }

        // ================= RESET FORM =================
        private void ClearForm()
        {
            txtName.Clear();
            txtCCCD.Clear();
            txtPhone.Clear();
            txtSoGioLam.Clear();
            txtTongLuong.Clear();
        }

        // ================= PHIẾU LƯƠNG WORD =================
        

        private void XuatPhieuLuongWord(string maNV)
        {
            Word.Application app = new Word.Application();
            app.Visible = true;

            Word.Document doc = app.Documents.Add();

            Word.Paragraph title = doc.Content.Paragraphs.Add();
            title.Range.Text = "PHIẾU THANH TOÁN LƯƠNG";
            title.Range.Font.Size = 18;
            title.Range.Font.Bold = 1;
            title.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            title.Range.InsertParagraphAfter();

            Word.Paragraph body = doc.Content.Paragraphs.Add();
            body.Range.Font.Size = 12;

            body.Range.Text =
                $"\nMã nhân viên: {maNV}" +
                $"\nTên nhân viên: {txtName.Text}" +
                $"\nCCCD: {txtCCCD.Text}" +
                $"\nSĐT: {txtPhone.Text}" +
                $"\n\nSố giờ làm: {txtSoGioLam.Text}" +
                $"\nLương / giờ: {LUONG_GIO:24} VNĐ" +
                $"\nTổng lương: {txtTongLuong.Text} VNĐ" +
                $"\n\nNgày thanh toán: {DateTime.Now:dd/MM/yyyy}";

            Word.Paragraph sign = doc.Content.Paragraphs.Add();
            sign.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            sign.Range.Text = "\n\nNgười lập phiếu\n(Ký & ghi rõ họ tên)";
        }
      
    }
}
