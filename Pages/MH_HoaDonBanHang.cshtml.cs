using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_HoaDonBanHangModel : PageModel
    {
        public List<HoaDon> dsHoaDon { get; set; }
        [BindProperty]
        public string TuKhoa { get; set; }
        public IXL_HoaDonBanHang xuLyHoaDonBanHang;
        public void OnGet()
        {
            dsHoaDon = xuLyHoaDonBanHang.TimKiem(string.Empty);
        }
        public void OnPost()
        {
            dsHoaDon = xuLyHoaDonBanHang.TimKiem(TuKhoa);
        }
        public MH_HoaDonBanHangModel()
        {
            dsHoaDon = new List<HoaDon>();
            xuLyHoaDonBanHang = new XL_HoaDonBanHang();
        }
    }
}
