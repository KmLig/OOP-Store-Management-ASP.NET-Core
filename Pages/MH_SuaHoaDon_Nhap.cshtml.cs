using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_SuaHoaDon_NhapModel : PageModel
    {
        public HoaDon A;
        public List <MatHangHoaDon> mh { get; set; }
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
        private IXL_HoaDonNhapHang xuLyHoaDonNhaphang;
        public void OnGet()
        {
            var HD = xuLyHoaDonNhaphang.DocHoaDon(Index);
            dsMatHang = luuTruSanPham.DocDanhSachSanPham();

            if (string.IsNullOrEmpty(Index))
            {
                Chuoi = "Ma hoa don khong hop le";
            }
            else
            {
                var kq = xuLyHoaDonNhaphang.DocHoaDon(Index);
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
            var hdd = xuLyHoaDonNhaphang.DocHoaDon(Index);
            A = hdd.Data;

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
                A = new HoaDon(Index, mh, NgayLap, ThanhTien);
                var kq = xuLyHoaDonNhaphang.SuaHoaDon(A);
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
        public MH_SuaHoaDon_NhapModel()        {

            luuTruSanPham = new LuuTruSanPham();
            xuLyHoaDonNhaphang = new XL_HoaDonNhapHang();
        }
    }
    }
