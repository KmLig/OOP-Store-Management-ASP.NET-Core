using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_ThongKe_HetHanModel : PageModel
    {
        public List<MatHang> dsSanPham;
        public string ConHan;
        public string HetHan;
        [BindProperty]
        public string SearchHSD { get; set; }
        [BindProperty]
        public string SearchLoaiHang { get; set; }
        [BindProperty]
        public string? TuKhoa { get; set; }
        public List<string> dsLoaiHang;
        private IXuLySanPham xuLySanPham;
        private IXL_HetHan xuLyHetHan;
        private ILuuTruLoaiHang luuTruLoaiHang;
        public void OnGet()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();

            dsSanPham = xuLySanPham.TimKiem(string.Empty);
            ConHan = string.Empty;
            HetHan = string.Empty;
        }
        public void OnPost()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();
            dsSanPham = xuLySanPham.TimKiem(TuKhoa);

            if (SearchHSD == "conHSD")
            {
                dsSanPham = xuLyHetHan.TimConHSD();
                ConHan = "OK";
            }
            else if (SearchHSD == "hetHSD")
            {
                dsSanPham = xuLyHetHan.TimHetHSD();
                HetHan = "Expired";
            }
            else
            {
                dsSanPham = xuLySanPham.TimKiem(string.Empty);
            }

            if (!string.IsNullOrEmpty(SearchLoaiHang))
            {
                dsSanPham = xuLySanPham.TimKiem(string.Empty);
                dsSanPham = xuLySanPham.TimKiemTheoLoaiHang(SearchLoaiHang);
            }
        }
        public MH_ThongKe_HetHanModel()
        {
            xuLySanPham = new XL_SanPham();
            luuTruLoaiHang = new LuuTruLoaiHang();
            xuLyHetHan = new XL_HetHan();
        }
    }
}
