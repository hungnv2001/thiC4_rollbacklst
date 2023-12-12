using System.ComponentModel.DataAnnotations;

namespace hungnv_ph30098.Models
{
    public class CongVien
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address {  get; set; }    
        public string Type {  get; set; }
        public int DienTich {  get; set; }
        public DateTime ThoiGianMo {  get; set; }   
        public bool Status {  get; set; }
    }
}
