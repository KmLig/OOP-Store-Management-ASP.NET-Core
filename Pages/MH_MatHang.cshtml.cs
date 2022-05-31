using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_MatHangModel : PageModel
    {
        [BindProperty]
        public string TuKhoa { get; set; }
        [BindProperty]
        public string SearchLoaiHang { get; set; }
        public List<MatHang> dsSanPham;

        public List<string> dsLoaiHang;
        private IXuLySanPham xuLySanPham;
        public ILuuTruLoaiHang luuTruLoaiHang;
        public void OnGet()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();
            dsSanPham = xuLySanPham.TimKiem(string.Empty);
        }
        public void OnPost()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();
            dsSanPham = xuLySanPham.TimKiem(TuKhoa);
            if (!string.IsNullOrEmpty(SearchLoaiHang))
            {
                dsSanPham = xuLySanPham.TimKiem(string.Empty);
                dsSanPham = xuLySanPham.TimKiemTheoLoaiHang(SearchLoaiHang);
            }
        }
        public MH_MatHangModel()
        {
            xuLySanPham = new XL_SanPham();
            luuTruLoaiHang = new LuuTruLoaiHang();
            dsSanPham = new List<MatHang>();
        }
    }
}
