using hungnv_ph30098.Models;
using hungnv_ph30098.Repo;
using hungnv_ph30098.Repo.IRepo;
using hungnv_ph30098.Service.IService;

namespace hungnv_ph30098.Service
{
    public class CongVienService : ICVService
    {
        ICVRepo repo;
        public CongVienService()
        {
            repo=new CongVienRepo();
        }
        public bool Create(CongVien congVien)
        {
           return  repo.Create(congVien);
        }

        public bool Delete(Guid id)
        {
            return repo.Delete(id);
        }

        public CongVien Find(Guid id)
        {
            return repo.Find(id);
        }

        public List<CongVien> GetAll()
        {
            return repo.GetAll();
        }

        public bool Update(CongVien congVien)
        {
            return repo.Update(congVien);
        }
    }
}
