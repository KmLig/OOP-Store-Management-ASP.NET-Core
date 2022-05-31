using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using DAL;
using Entities;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_XoaHoaDon_BanModel : PageModel
    {
        public HoaDon hd { get; set; }
        public string Chuoi;
        public bool coSanPham;

        [BindProperty(SupportsGet = true)]
        public string Index { get; set; }
        
        private IXL_HoaDonBanHang xuLyHoaDonBanhang;
        public void OnGet()
        {
            var kq = xuLyHoaDonBanhang.DocHoaDon(Index);
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
                var sp = xuLyHoaDonBanhang.DocHoaDon(Index);
                var kq = xuLyHoaDonBanhang.XoaHoaDon(sp.Data);
                if (kq.IsSuccess)
                {
                    Chuoi = "Xoa tru thanh cong";
                    Response.Redirect("/MH_HoaDonBanHang");
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
        public MH_XoaHoaDon_BanModel()
        {
            xuLyHoaDonBanhang = new XL_HoaDonBanHang();
        }
    }
}
