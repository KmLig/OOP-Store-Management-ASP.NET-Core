using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using DAL;
using Services;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_Tao_HoaDon_BanHangModel : PageModel
    {
        public HoaDon A;
        public string Chuoi;
        public List<MatHang> dsMatHang;
        public MatHangHoaDon mh { get; set; }
        [BindProperty]
        public string MaHD { get; set; }
        [BindProperty]
        public string MatHangHoaDon { get; set; }
        [BindProperty]
        public int Gia { get; set; }
        [BindProperty]
        public int SL { get; set; }

        [BindProperty]
        public DateTime NgayLap { get; set; }
        [BindProperty]
        public int ThanhTien { get; set; }
        
        private ILuuTruSanPham luuTruSanPham;
        private IXL_HoaDonBanHang xuLyHoaDonBanhang;
        
        public void OnGet()
        {
            dsMatHang = luuTruSanPham.DocDanhSachSanPham();
            Chuoi = string.Empty;
        }
        public void OnPost()
        {
            dsMatHang = luuTruSanPham.DocDanhSachSanPham();
            string[] Ma_TenMH = MatHangHoaDon.Split('|');            
            mh = new MatHangHoaDon(Ma_TenMH[0], Ma_TenMH[1], Gia, SL);
            int thanhTien = SL * Gia;

            try
            {
                A = new HoaDon(MaHD, mh, NgayLap, thanhTien);
                var kq = xuLyHoaDonBanhang.ThemHoaDon(A);
                if (kq.IsSuccess)
                {
                    Chuoi = "Luu tru thanh cong";
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
        public MH_Tao_HoaDon_BanHangModel()
        {            
            luuTruSanPham = new LuuTruSanPham();
            xuLyHoaDonBanhang = new XL_HoaDonBanHang();
        }
    }
}
