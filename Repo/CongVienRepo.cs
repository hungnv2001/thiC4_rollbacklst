using hungnv_ph30098.Context;
using hungnv_ph30098.Models;
using hungnv_ph30098.Repo.IRepo;

namespace hungnv_ph30098.Repo
{
    public class CongVienRepo : ICVRepo
    {
        MyContext _context;
        public CongVienRepo() { _context = new MyContext(); }

        public bool Create(CongVien congVien)
        {
            try
            {
                _context.Add(congVien);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

               return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var congVien= _context.congViens.Find(id);
                _context.Remove(congVien);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public CongVien Find(Guid id)
        {
            try
            {
                return _context.congViens.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CongVien> GetAll()
        {
            return _context.congViens.ToList();
        }

        public bool Update(CongVien congVien)
        {
            try
            {
                var congVienu = _context.congViens.Find(congVien.Id);
                if (congVienu != null)
                {
                    congVienu.DienTich=congVien.DienTich;
                    congVienu.ThoiGianMo = congVien.ThoiGianMo;
                    congVienu.Status = congVien.Status;
                    congVienu.Address = congVien.Address;
                    congVienu.Name = congVien.Name;
                    congVienu.Type = congVien.Type;
                    _context.Update(congVienu);
                    _context.SaveChanges();
                }
              
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
