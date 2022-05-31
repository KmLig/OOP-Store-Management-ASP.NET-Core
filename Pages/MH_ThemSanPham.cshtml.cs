using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Entities;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_ThemSanPhamModel : PageModel
    {
        public string Chuoi { get; set; }
        [BindProperty]
        public string MaMH { get; set; }
        [BindProperty]
        public string TenMH { get; set; }
        [BindProperty]
        public DateTime HanSD { get; set; }
        [BindProperty]
        public string CtySX { get; set; }
        [BindProperty]
        public DateTime NgaySX { get; set; }
        [BindProperty]
        public string LoaiHang { get; set; }
        [BindProperty]
        public int Gia { get; set; }
        [BindProperty]
        public int TonKho { get; set; }
        public List<string> dsLoaiHang { get; set; }
        public MatHang sp { get; set; }
        public ILuuTruLoaiHang luuTruLoaiHang;
        private IXuLySanPham xuLySanPham;
        public void OnGet()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();
            Chuoi = string.Empty;
        }
        public void OnPost()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();
            try
            {
                sp = new MatHang(MaMH, TenMH, HanSD, CtySX, NgaySX, LoaiHang, Gia, TonKho);
                var kq = xuLySanPham.ThemSanPham(sp);
                if (kq.IsSuccess)
                {
                    Chuoi = "Luu tru thanh cong";
                    Response.Redirect("/MH_MatHang");
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
        public MH_ThemSanPhamModel()
        {
            xuLySanPham = new XL_SanPham();
            luuTruLoaiHang = new LuuTruLoaiHang();
            sp = new MatHang();
        }
    }
}
