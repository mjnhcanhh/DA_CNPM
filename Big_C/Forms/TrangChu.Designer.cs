using System;
using System.Windows.Forms;

namespace Big_C
{
    partial class TrangChu
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.btnQuanLyLichLam = new System.Windows.Forms.Button();
            this.btnQLHangKho = new System.Windows.Forms.Button();
            this.btnQLNhanSu = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelMenu.Controls.Add(this.lblTenDangNhap);
            this.panelMenu.Controls.Add(this.btnExit);
            this.panelMenu.Controls.Add(this.btnHome);
            this.panelMenu.Controls.Add(this.btnSupport);
            this.panelMenu.Controls.Add(this.btnChamCong);
            this.panelMenu.Controls.Add(this.btnQuanLyLichLam);
            this.panelMenu.Controls.Add(this.btnQLHangKho);
            this.panelMenu.Controls.Add(this.btnQLNhanSu);
            this.panelMenu.Location = new System.Drawing.Point(1, 1);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(345, 648);
            this.panelMenu.TabIndex = 0;
            this.panelMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMenu_Paint);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightCyan;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(30, 510);
            this.btnExit.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(282, 51);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.LightCyan;
            this.btnHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.Black;
            this.btnHome.Location = new System.Drawing.Point(27, 160);
            this.btnHome.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(283, 51);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnSupport
            // 
            this.btnSupport.BackColor = System.Drawing.Color.LightCyan;
            this.btnSupport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupport.ForeColor = System.Drawing.Color.Black;
            this.btnSupport.Location = new System.Drawing.Point(27, 452);
            this.btnSupport.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(282, 51);
            this.btnSupport.TabIndex = 5;
            this.btnSupport.Text = "Hỗ Trợ";
            this.btnSupport.UseVisualStyleBackColor = false;
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // btnChamCong
            // 
            this.btnChamCong.BackColor = System.Drawing.Color.LightCyan;
            this.btnChamCong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamCong.ForeColor = System.Drawing.Color.Black;
            this.btnChamCong.Location = new System.Drawing.Point(29, 277);
            this.btnChamCong.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Size = new System.Drawing.Size(283, 51);
            this.btnChamCong.TabIndex = 1;
            this.btnChamCong.Text = "Chấm Công";
            this.btnChamCong.UseVisualStyleBackColor = false;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            // 
            // btnQuanLyLichLam
            // 
            this.btnQuanLyLichLam.BackColor = System.Drawing.Color.LightCyan;
            this.btnQuanLyLichLam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLyLichLam.ForeColor = System.Drawing.Color.Black;
            this.btnQuanLyLichLam.Location = new System.Drawing.Point(27, 393);
            this.btnQuanLyLichLam.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnQuanLyLichLam.Name = "btnQuanLyLichLam";
            this.btnQuanLyLichLam.Size = new System.Drawing.Size(282, 51);
            this.btnQuanLyLichLam.TabIndex = 3;
            this.btnQuanLyLichLam.Text = "Quản Lý Lịch Làm";
            this.btnQuanLyLichLam.UseVisualStyleBackColor = false;
            this.btnQuanLyLichLam.Click += new System.EventHandler(this.btnQuanLyLichLam_Click);
            // 
            // btnQLHangKho
            // 
            this.btnQLHangKho.BackColor = System.Drawing.Color.LightCyan;
            this.btnQLHangKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLHangKho.ForeColor = System.Drawing.Color.Black;
            this.btnQLHangKho.Location = new System.Drawing.Point(27, 335);
            this.btnQLHangKho.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnQLHangKho.Name = "btnQLHangKho";
            this.btnQLHangKho.Size = new System.Drawing.Size(283, 51);
            this.btnQLHangKho.TabIndex = 2;
            this.btnQLHangKho.Text = "Quản Lý Hàng Kho";
            this.btnQLHangKho.UseVisualStyleBackColor = false;
            this.btnQLHangKho.Click += new System.EventHandler(this.btnQLHangKho_Click);
            // 
            // btnQLNhanSu
            // 
            this.btnQLNhanSu.BackColor = System.Drawing.Color.LightCyan;
            this.btnQLNhanSu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLNhanSu.ForeColor = System.Drawing.Color.Black;
            this.btnQLNhanSu.Location = new System.Drawing.Point(29, 218);
            this.btnQLNhanSu.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnQLNhanSu.Name = "btnQLNhanSu";
            this.btnQLNhanSu.Size = new System.Drawing.Size(283, 51);
            this.btnQLNhanSu.TabIndex = 4;
            this.btnQLNhanSu.Text = "Quản Lý Nhân Sự";
            this.btnQLNhanSu.UseVisualStyleBackColor = false;
            this.btnQLNhanSu.Click += new System.EventHandler(this.btnQLNhanSu_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 101);
            this.panel2.TabIndex = 0;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDangNhap.ForeColor = System.Drawing.Color.White;
            this.lblTenDangNhap.Location = new System.Drawing.Point(96, 105);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(139, 20);
            this.lblTenDangNhap.TabIndex = 1;
            this.lblTenDangNhap.Text = "Xin chào, User!";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Big_C.Properties.Resources.BigC;
            this.pictureBox1.InitialImage = global::Big_C.Properties.Resources.BigC;
            this.pictureBox1.Location = new System.Drawing.Point(81, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(177, 81);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panelContainer
            // 
            this.panelContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelContainer.Location = new System.Drawing.Point(390, 0);
            this.panelContainer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(953, 648);
            this.panelContainer.TabIndex = 1;
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 648);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "TrangChu";
            this.Text = "TrangChu";
            this.Load += new System.EventHandler(this.TrangChu_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.Button btnQLNhanSu;
        private System.Windows.Forms.Button btnQuanLyLichLam;
        private System.Windows.Forms.Button btnQLHangKho;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTenDangNhap; // KHAI BÁO THÀNH VIÊN MỚI
    }
}