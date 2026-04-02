namespace Big_C.UserControls
{
    partial class ViewOrder
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.clhMaPhieu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhTenHangHoa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhTenNhaCungCap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhNgayDat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhNgayGiao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhSoLuong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhTongTien = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clhMoTa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnXuatHang = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhMaPhieu,
            this.clhTenHangHoa,
            this.clhTenNhaCungCap,
            this.clhNgayDat,
            this.clhNgayGiao,
            this.clhSoLuong,
            this.clhTongTien,
            this.clhMoTa});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 62);
            this.listView1.Name = "listView1";
            // QUAN TRỌNG: Bật hiển thị ToolTip cho Item
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(1360, 553);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // clhMaPhieu
            // 
            this.clhMaPhieu.Text = "Mã Phiếu";
            this.clhMaPhieu.Width = 90;
            // 
            // clhTenHangHoa
            // 
            this.clhTenHangHoa.Text = "Hàng Hóa";
            this.clhTenHangHoa.Width = 150;
            // 
            // clhTenNhaCungCap
            // 
            this.clhTenNhaCungCap.Text = "Nhà Cung Cấp";
            this.clhTenNhaCungCap.Width = 150;
            // 
            // clhNgayDat
            // 
            this.clhNgayDat.Text = "Ngày Đặt";
            this.clhNgayDat.Width = 100;
            // 
            // clhNgayGiao
            // 
            this.clhNgayGiao.Text = "Ngày Giao";
            this.clhNgayGiao.Width = 100;
            // 
            // clhSoLuong
            // 
            this.clhSoLuong.Text = "Số Lượng";
            this.clhSoLuong.Width = 80;
            // 
            // clhTongTien
            // 
            this.clhTongTien.Text = "Tổng Tiền";
            this.clhTongTien.Width = 120;
            // 
            // clhMoTa
            // 
            this.clhMoTa.Text = "Mô Tả";
            this.clhMoTa.Width = 200;
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.btnXuatHang);
            this.panelControl.Controls.Add(this.btnXoa);
            this.panelControl.Controls.Add(this.btnSua);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(1360, 62);
            this.panelControl.TabIndex = 0;
            // 
            // btnXuatHang
            // 
            this.btnXuatHang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatHang.Location = new System.Drawing.Point(1237, 16);
            this.btnXuatHang.Name = "btnXuatHang";
            this.btnXuatHang.Size = new System.Drawing.Size(110, 30);
            this.btnXuatHang.TabIndex = 3;
            this.btnXuatHang.Text = "Xuất hàng";
            this.btnXuatHang.UseVisualStyleBackColor = true;
            this.btnXuatHang.Click += new System.EventHandler(this.btnXuatHang_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.Location = new System.Drawing.Point(1141, 16);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(90, 30);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSua.Location = new System.Drawing.Point(1045, 16);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(90, 30);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "QUẢN LÝ PHIẾU MUA HÀNG";
            // 
            // ViewOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panelControl);
            this.Name = "ViewOrder";
            this.Size = new System.Drawing.Size(1360, 615);
            this.Load += new System.EventHandler(this.ViewOrder_Load);
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnXuatHang;
        private System.Windows.Forms.ColumnHeader clhMaPhieu;
        private System.Windows.Forms.ColumnHeader clhTenHangHoa;
        private System.Windows.Forms.ColumnHeader clhTenNhaCungCap;
        private System.Windows.Forms.ColumnHeader clhNgayDat;
        private System.Windows.Forms.ColumnHeader clhNgayGiao;
        private System.Windows.Forms.ColumnHeader clhSoLuong;
        private System.Windows.Forms.ColumnHeader clhTongTien;
        private System.Windows.Forms.ColumnHeader clhMoTa;
    }
}