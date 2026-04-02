using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Big_C
{
    public partial class ChamCongNhanVien : Form
    {
        // Lưu mã nhân viên đăng nhập
        private readonly string _maNV;

        // Chuỗi kết nối CSDL
        private readonly SqlConnection conn = new SqlConnection(
            @"Data Source = DESKTOP-O8DO4M9;Initial Catalog=QL_BigC;Integrated Security=True"
        );

        // Constructor nhận mã nhân viên từ form Login
        public ChamCongNhanVien(string maNV)
        {
            InitializeComponent();
            _maNV = maNV;
        }

        // Khi form load → hiển thị mã nhân viên
        private void ChamCongNhanVien_Load(object sender, EventArgs e)
        {
            lblMaNV.Text = _maNV;
        }

        // Nút VÀO CA
        private void btnVao_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO ChamCong (MaNV, Ngay, GioVao)
                      VALUES (@MaNV, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME))",
                    conn
                );

                cmd.Parameters.AddWithValue("@MaNV", _maNV);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Chấm công vào ca thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chấm công vào:\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        // Nút RA CA
        private void btnRa_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    @"UPDATE ChamCong
                      SET GioRa = CAST(GETDATE() AS TIME)
                      WHERE MaNV = @MaNV
                        AND Ngay = CAST(GETDATE() AS DATE)",
                    conn
                );

                cmd.Parameters.AddWithValue("@MaNV", _maNV);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Chấm công ra ca thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chấm công ra:\n" + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
