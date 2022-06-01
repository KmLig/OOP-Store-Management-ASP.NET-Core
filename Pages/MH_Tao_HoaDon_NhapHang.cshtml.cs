using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using DAL;
using Services;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_Tao_HoaDon_NhapHangModel : PageModel
    {
        public HoaDon A;
        public string Chuoi;
        public List<MatHang> dsMatHang;
        public List<MatHangHoaDon> mh { get; set; }
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
        private IXL_HoaDonNhapHang xuLyHoaDonNhaphang;

        public void OnGet()
        {
            dsMatHang = luuTruSanPham.DocDanhSachSanPham();
            Chuoi = string.Empty;
        }
        public void OnPost()
        {
            dsMatHang = luuTruSanPham.DocDanhSachSanPham();
            mh = new List<MatHangHoaDon>();
            int nMucNhap = Request.Form["MatHangHoaDon"].Count;
            for (int i = 0; i < nMucNhap; i++)
            {
                string[] Ma_TenMH = Request.Form["MatHangHoaDon"][i].Split('|');
                string MaMH_CT = Ma_TenMH[0];
                string TenMH_CT = Ma_TenMH[1];
                int sl_ct = int.Parse(Request.Form["SL"][i]);
                int gia_ct = int.Parse(Request.Form["Gia"][i]);
                MatHangHoaDon Sp = new MatHangHoaDon(MaMH_CT, TenMH_CT, gia_ct, sl_ct);
                ThanhTien += Sp.SL * Sp.Gia;
                mh.Add(Sp);
            }



            try
            {
                A = new HoaDon(MaHD, mh, NgayLap, ThanhTien);
                var kq = xuLyHoaDonNhaphang.ThemHoaDon(A);
                if (kq.IsSuccess)
                {
                    Chuoi = "Luu tru thanh cong";
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
        public MH_Tao_HoaDon_NhapHangModel()
        {
            luuTruSanPham = new LuuTruSanPham();
            xuLyHoaDonNhaphang = new XL_HoaDonNhapHang();
        }
    }
}
