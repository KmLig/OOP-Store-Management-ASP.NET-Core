using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_XoaSanPhamModel : PageModel
    {
        public MatHang sp { get; set; }
        public string Chuoi;
        public bool coSanPham;

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        public ILuuTruLoaiHang luuTruLoaiHang;
        private IXuLySanPham xuLySanPham;
        public void OnGet()
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
        public void OnPost()
        {
            try
            {
                var sp = xuLySanPham.DocSanPham(Id);
                var kq = xuLySanPham.XoaSanPham(sp.Data);
                if (kq.IsSuccess)
                {
                    Chuoi = "Xoa tru thanh cong";
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
        public MH_XoaSanPhamModel()
        {
            xuLySanPham = new XL_SanPham();
            luuTruLoaiHang = new LuuTruLoaiHang();
            sp = new MatHang();
        }
    }
}
