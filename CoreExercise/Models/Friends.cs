using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreExercise.Models
{
    // using System.ComponentModel.DataAnnotations.Schema; => Table
    [Table("Friends")]
    public class Friends
    {
        // using System.ComponentModel.DataAnnotations; => Key
        [Key]
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string Name { get; set; }

        [Display(Name = "電話")]
        [Required(ErrorMessage = "請輸入電話")]
        public string Phone { get; set; }

        [Display(Name = "信箱")]
        [Required(ErrorMessage = "請輸入信箱")]
        public string Email { get; set; }

        [Display(Name = "縣市")]
        [Required(ErrorMessage = "請輸入縣市")]
        public string City { get; set; }
    }
}
