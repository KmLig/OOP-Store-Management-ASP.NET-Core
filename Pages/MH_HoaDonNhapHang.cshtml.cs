using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_HoaDonNhapHangModel : PageModel
    {
        public List<HoaDon> dsHoaDon { get; set; }        
        [BindProperty]
        public string TuKhoa { get; set; }
        public IXL_HoaDonNhapHang xuLyHoaDonNhapHang;
        public void OnGet()
        {
            dsHoaDon = xuLyHoaDonNhapHang.TimKiem(string.Empty);
        }
        public void OnPost()
        {
            dsHoaDon = xuLyHoaDonNhapHang.TimKiem(TuKhoa);
        }
        public MH_HoaDonNhapHangModel()
        {
            dsHoaDon = new List<HoaDon>();
            xuLyHoaDonNhapHang = new XL_HoaDonNhapHang();
        }
    }
}
