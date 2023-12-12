using hungnv_ph30098.Models;
using Microsoft.EntityFrameworkCore;

namespace hungnv_ph30098.Context
{
    public class MyContext : DbContext
    {
        public MyContext()
        {
            
        }
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=hungnv_ph30098;Integrated Security=True;Trust Server Certificate=True");
        }
        public DbSet<CongVien> congViens { get; set; }
    }
}
