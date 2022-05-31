using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Services;
using DAL;

namespace DoAn_LTHDT_21880067.Pages
{
    public class MH_ThongKe_TonKhoModel : PageModel
    {
        public List<MatHang> dsSanPham { get; set; }    
        public int[] Nhap;
        public int[] Xuat;
        public int[] TonKho;
        [BindProperty]
        public string? SearchLoaiHang { get; set; }
        [BindProperty]
        public string SearchTonKho { get; set; }
        [BindProperty]
        public string? TuKhoa { get; set; }
        public List<string> dsLoaiHang;
        private IXuLySanPham xuLySanPham;
        private ILuuTruLoaiHang luuTruLoaiHang;
        private IXL_TonKho xuLyTonKho;
        public void OnGet()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();

            dsSanPham = xuLySanPham.TimKiem(string.Empty);
            Nhap = xuLyTonKho.KiemTraSLNhap(dsSanPham);
            Xuat = xuLyTonKho.KiemTraSLXuat(dsSanPham);
            TonKho = xuLyTonKho.KiemTraTonKho(Nhap, Xuat);
        }
        public void OnPost()
        {
            dsLoaiHang = luuTruLoaiHang.DocDanhSachLoaiHang();

            dsSanPham = xuLySanPham.TimKiem(TuKhoa);
            Nhap = xuLyTonKho.KiemTraSLNhap(dsSanPham);
            Xuat = xuLyTonKho.KiemTraSLXuat(dsSanPham);
            TonKho = xuLyTonKho.KiemTraTonKho(Nhap, Xuat);
            if (SearchTonKho == "ConHang")
            {
                dsSanPham = xuLyTonKho.ConHang(TonKho);
                Nhap = xuLyTonKho.KiemTraSLNhap(dsSanPham);
                Xuat = xuLyTonKho.KiemTraSLXuat(dsSanPham);
                TonKho = xuLyTonKho.KiemTraTonKho(Nhap, Xuat);
            }
            else if (SearchTonKho == "HetHang")
            {
                dsSanPham = xuLyTonKho.HetHang(TonKho);
                Nhap = xuLyTonKho.KiemTraSLNhap(dsSanPham);
                Xuat = xuLyTonKho.KiemTraSLXuat(dsSanPham);
                TonKho = xuLyTonKho.KiemTraTonKho(Nhap, Xuat);
            }

            if (!string.IsNullOrEmpty(SearchLoaiHang))
            {
                dsSanPham = xuLySanPham.TimKiem(string.Empty);
                dsSanPham = xuLySanPham.TimKiemTheoLoaiHang(SearchLoaiHang);
                Nhap = xuLyTonKho.KiemTraSLNhap(dsSanPham);
                Xuat = xuLyTonKho.KiemTraSLXuat(dsSanPham);
                TonKho = xuLyTonKho.KiemTraTonKho(Nhap, Xuat);
            }
        }
        public MH_ThongKe_TonKhoModel()
        {
            xuLySanPham = new XL_SanPham();
            luuTruLoaiHang = new LuuTruLoaiHang();
            xuLyTonKho = new XL_TonKho();
            
        }
    }
}
