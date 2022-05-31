using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_SuaHoaDon_BanModel : PageModel
    {
        public HoaDon A;
        public MatHangHoaDon mh { get; set; }
        public string? Chuoi;
        public bool coHoaDon;
        public List<MatHang> dsMatHang { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Index { get; set; }       
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
            var HD = xuLyHoaDonBanhang.DocHoaDon(Index);
            dsMatHang = luuTruSanPham.DocDanhSachSanPham();

            if (string.IsNullOrEmpty(Index))
            {
                Chuoi = "Ma hoa don khong hop le";
            }
            else
            {
                var kq = xuLyHoaDonBanhang.DocHoaDon(Index);
                if (kq.IsSuccess)
                {
                    A = kq.Data;
                }
                else
                {
                    A = null;
                    Chuoi = kq.ErrorMessage;
                }
            }


        }
        public void OnPost()
        {
            var hdd = xuLyHoaDonBanhang.DocHoaDon(Index);
            A = hdd.Data;

            dsMatHang = luuTruSanPham.DocDanhSachSanPham();
            string[] Ma_TenMH = MatHangHoaDon.Split('|');
            mh = new MatHangHoaDon(Ma_TenMH[0], Ma_TenMH[1], Gia, SL);
            int thanhTien = SL * Gia;

            try
            {
                A = new HoaDon(Index, mh, NgayLap, thanhTien);
                var kq = xuLyHoaDonBanhang.SuaHoaDon(A);
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
        public MH_SuaHoaDon_BanModel()        {

            luuTruSanPham = new LuuTruSanPham();
            xuLyHoaDonBanhang = new XL_HoaDonBanHang();
        }
    }
    }
