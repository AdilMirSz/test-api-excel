using System.ComponentModel.DataAnnotations;

namespace ApiExcel.DAL
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Position { get; set; }
    }
}