using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_SuaLoaiHangModel : PageModel
    {
        public string LoaiHang { get; set; }
        public string Chuoi;
        [BindProperty]
        public string LoaiHangSua { get; set; }
        public bool coLoaiHang;

        [BindProperty(SupportsGet = true)]
        public int Index { get; set; }
        public ILuuTruLoaiHang luuTruLoaiHang;
        private IXuLyLoaiHang xuLyLoaiHang;
        public void OnGet()
        {
            var kq = xuLyLoaiHang.DocLoaiHang(Index);
            if (kq.IsSuccess)
            {
                LoaiHang = kq.Data;
            }
            else
            {
                LoaiHang = null;
                Chuoi = kq.ErrorMessage;
            }            
            coLoaiHang = (LoaiHang != null);
        }
        public void OnPost()
        {
            var Lhc = xuLyLoaiHang.DocLoaiHang(Index);
            var kq = xuLyLoaiHang.SuaLoaiHang(Lhc.Data, LoaiHangSua);
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
        public MH_SuaLoaiHangModel()
        {
            xuLyLoaiHang = new XL_LoaiHang();
            luuTruLoaiHang = new LuuTruLoaiHang();
        }
    }
}
