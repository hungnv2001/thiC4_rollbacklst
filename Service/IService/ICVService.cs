using hungnv_ph30098.Models;

namespace hungnv_ph30098.Service.IService
{
    public interface ICVService
    {
        public List<CongVien> GetAll();
        public bool Create(CongVien congVien);
        public bool Update(CongVien congVien);
        public bool Delete(Guid id);
        public CongVien Find(Guid id);
    }
}
