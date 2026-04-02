using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Big_C.Model;
using Big_C.Forms;

namespace Big_C.UserControls
{
    public partial class QLNhanSu : UserControl
    {
        private readonly string strcon =
            "SERVER=DESKTOP-O8DO4M9;DATABASE=QL_BigC;Integrated Security=TRUE";

        private List<NhanVien> lnv = new List<NhanVien>();

        public QLNhanSu()
        {
            InitializeComponent();
        }

        private void QLNhanSu_Load(object sender, EventArgs e)
        {
            LoadNhanVien(); // load sẵn khi mở
        }

        // ================= LOAD NHÂN VIÊN =================
        private void LoadNhanVien()
        {
            lnv.Clear();

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                conn.Open();

                string sql = "SELECT * FROM NhanVien";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NhanVien nv = new NhanVien
                    {
                        MaNhanVien = reader.GetString(0),
                        TenNhanVien = reader.GetString(1),
                        NgaySinh = reader.GetDateTime(2),
                        DiaChi = reader.GetString(3),
                        SDT = reader.GetString(4),
                        CCCD = reader.GetString(5),
                        NgayVaoLam = reader.GetDateTime(6),
                        SoNgayLam = reader.GetInt32(7),
                        MaQuanLy = reader.GetString(8)
                    };

                    lnv.Add(nv);
                }
            }

            dtGrView_NhanVien.DataSource = null;
            dtGrView_NhanVien.DataSource = lnv;
        }

        // ================= NÚT LOAD =================
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        // ================= THÊM NHÂN VIÊN =================
        private void btnCreateNV_Click(object sender, EventArgs e)
        {
            CreateNV cr = new CreateNV();
            cr.FormClosed += (s, args) => LoadNhanVien(); // tự reload
            cr.Show();
        }

        // ================= SỬA / XÓA =================
        private void btnEditDeleteNV_Click(object sender, EventArgs e)
        {
            EditRemoveNhanVien form = new EditRemoveNhanVien();
            form.FormClosed += (s, args) => LoadNhanVien();
            form.Show();
        }

        private void dtGrView_NhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể xử lý click nếu cần
        }
    }
}
