using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using DAL;
using Entities;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_XoaHoaDon_NhapModel : PageModel
    {
        public HoaDon hd { get; set; }
        public string Chuoi;
        public bool coSanPham;

        [BindProperty(SupportsGet = true)]
        public string Index { get; set; }
        
        private IXL_HoaDonNhapHang xuLyHoaDonNhapHang;
        public void OnGet()
        {
            var kq = xuLyHoaDonNhapHang.DocHoaDon(Index);
            if (kq.IsSuccess)
            {
                hd = kq.Data;
            }
            else
            {
                hd = null;
                Chuoi = kq.ErrorMessage;
            }
        }
        public void OnPost()
        {
            try
            {
                var sp = xuLyHoaDonNhapHang.DocHoaDon(Index);
                var kq = xuLyHoaDonNhapHang.XoaHoaDon(sp.Data);
                if (kq.IsSuccess)
                {
                    Chuoi = "Xoa tru thanh cong";
                    Response.Redirect("/MH_HoaDonNhapHang");
                }
                else
                {
                    Chuoi = kq.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                Chuoi = ex.Message;
            }

        }
        public MH_XoaHoaDon_NhapModel()
        {
            xuLyHoaDonNhapHang = new XL_HoaDonNhapHang();
        }
    }
}
