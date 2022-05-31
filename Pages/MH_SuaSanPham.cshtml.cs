using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_SuaSanPhamModel : PageModel
    {
        public string Chuoi { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public MatHang sp { get; set; }
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
        public ILuuTruLoaiHang luuTruLoaiHang;
        private IXuLySanPham xuLySanPham;
        public void OnGet()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();
            if (string.IsNullOrEmpty(Id))
            {
                Chuoi = "Ma san pham khong hop le";
            }
            else
            {
                var kq = xuLySanPham.DocSanPham(Id);
                if (kq.IsSuccess)
                {
                    sp = kq.Data;
                }
                else
                {
                    sp = null;
                    Chuoi = kq.ErrorMessage;
                }
            }
        }
        public void OnPost()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();
            try
            {
                MatHang sp = new MatHang(Id, TenMH, HanSD, CtySX, NgaySX, LoaiHang, Gia, TonKho);
                var kq = xuLySanPham.SuaSanPham(sp);
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
        public MH_SuaSanPhamModel()
        {
            xuLySanPham = new XL_SanPham();
            luuTruLoaiHang = new LuuTruLoaiHang();
            sp = new MatHang();
        }
    }
}
