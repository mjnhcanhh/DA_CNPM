using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Big_C.UserControls;
using Big_C.Model;
using System.Data.SqlClient;

namespace Big_C.UserControls
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        // Phương thức tìm Form TrangChu cha để gọi addUserControl
        private Big_C.TrangChu GetParentTrangChu()
        {
            Control parent = this.Parent;
            while (parent != null && !(parent is Big_C.TrangChu))
            {
                parent = parent.Parent;
            }
            return parent as Big_C.TrangChu;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Không làm gì
        }

        // 1. Chấm Công (btnChamCong)
        private void btnChamCong_Click(object sender, EventArgs e)
        {
            Big_C.TrangChu parentForm = GetParentTrangChu();
            if (parentForm != null)
            {
                parentForm.addUserControl(new ChamCong());
            }
        }

        // 2. Quản lý kho (btnHangHoa)
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            Big_C.TrangChu parentForm = GetParentTrangChu();
            if (parentForm != null)
            {
                parentForm.addUserControl(new QLHangKho());
            }
        }

        // 3. Lịch Làm (btnLichLam)
        private void btnLichLam_Click(object sender, EventArgs e) // PHƯƠNG THỨC MỚI
        {
            Big_C.TrangChu parentForm = GetParentTrangChu();
            if (parentForm != null)
            {
                parentForm.addUserControl(new LichLam());
            }
        }

        // 4. Nhân Sự (btnNhanSu)
        private void btnNhanSu_Click(object sender, EventArgs e) // PHƯƠNG THỨC MỚI
        {
            Big_C.TrangChu parentForm = GetParentTrangChu();
            if (parentForm != null)
            {
                parentForm.addUserControl(new QLNhanSu());
            }
        }

        // 5. Hỗ Trợ (btnSupport)
        private void btnSupport_Click(object sender, EventArgs e) // PHƯƠNG THỨC MỚI
        {
            Big_C.TrangChu parentForm = GetParentTrangChu();
            if (parentForm != null)
            {
                parentForm.addUserControl(new Suppport());
            }
        }

        // 6. Thoát (btnExit)
        private void btnExit_Click(object sender, EventArgs e)
        {
            Big_C.TrangChu parentForm = GetParentTrangChu();
            if (parentForm != null)
            {
                parentForm.Close(); // Đóng Form TrangChu
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Không làm gì
        }
    }
}