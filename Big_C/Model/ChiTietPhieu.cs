using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Big_C.Model
{
    class ChiTietPhieu
    {
        
            public string MaHangHoa { get; set; }
            public string TenHangHoa { get; set; }
            public int? SoLuong { get; set; } // Giả sử là int?
            public int? DonGia { get; set; }   // Giả sử là int?

            public long ThanhTien => (long)(SoLuong ?? 0) * (DonGia ?? 0);
           

    }
}
