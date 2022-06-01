using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Entities;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_LoaiHangModel : PageModel
    {
        public List<string> dsLoaiHang { get; set; }    
        public List<MatHang> dsSanPham { get; set; }
        //[BindProperty(SupportsGet = true)]
        //public string ID { get; set; }
        public int[] countMH;
        [BindProperty]
        public string TuKhoa { get; set; }
        public ILuuTruLoaiHang luuTruLoaiHang;
        private ILuuTruSanPham luuTruSanPham;
        private IXuLyLoaiHang xuLyLoaiHang;
        public void OnGet()
        {
            dsLoaiHang = xuLyLoaiHang.TimKiemLoaiHang(string.Empty);
            dsSanPham = luuTruSanPham.DocDanhSachSanPham();
            countMH = xuLyLoaiHang.DemSoLuongMatHang(dsLoaiHang, dsSanPham);
        }
        public void OnPost()
        {
            dsLoaiHang = xuLyLoaiHang.TimKiemLoaiHang(TuKhoa);
            dsSanPham = luuTruSanPham.DocDanhSachSanPham();
            countMH = xuLyLoaiHang.DemSoLuongMatHang(dsLoaiHang, dsSanPham);
        }
        public MH_LoaiHangModel()
        {
            xuLyLoaiHang = new XL_LoaiHang();
            luuTruSanPham = new LuuTruSanPham();
            luuTruLoaiHang = new LuuTruLoaiHang();
            
        }
    }
}
