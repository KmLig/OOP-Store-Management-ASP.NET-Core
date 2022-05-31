using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_ThemLoaiHangModel : PageModel
    {
        public string? Chuoi;
        [BindProperty]
        public string LoaiHang { get; set; }
        private IXuLyLoaiHang xuLyLoaiHang;
        public void OnGet()
        {
            Chuoi = string.Empty;
        }
        public void OnPost()
        {
            try
            {                
                var kq = xuLyLoaiHang.ThemLoaiHang(LoaiHang);
                if (kq.IsSuccess)
                {
                    Chuoi = "Luu tru thanh cong";
                    Response.Redirect("/MH_LoaiHang");
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
        public MH_ThemLoaiHangModel()
        {
            xuLyLoaiHang = new XL_LoaiHang();
        }
    }
}
