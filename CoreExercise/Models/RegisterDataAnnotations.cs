using System;
using System.ComponentModel.DataAnnotations;

namespace CoreExercise.Models
{
    public class RegisterDataAnnotations
    {
        [Display(Name = "編號")]
        //[HiddenInput(DisplayValue =false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name不得為空白")]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Display(Name = "密碼")]
        [Required(ErrorMessage = "Password不得為空白")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "電郵")]
        [Required(ErrorMessage = "Email不得為空白")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "首頁")]
        [DataType(DataType.Url)]
        public string HomePage { get; set; }
        [Display(Name = "性別")]
        public Gender? Gender { get; set; }
        //[DataType(DataType.Date)]
        [Display(Name = "生日")]
        public DateTime Birthday { get; set; }
        [Display(Name = "生日")]
        //[DataType(DataType.DateTime)]
        public DateTime Birthday2 { get; set; }
        [Display(Name = "存款")]
        //[DataType(DataType.Currency)]
        public decimal Money { get; set; }
        [Required(ErrorMessage = "不得為空白")]
        [Display(Name = "城市")]
        [Range(1, 10)]
        public int City { get; set; }
        [Display(Name = "通勤")]
        public string Commutermode { get; set; }
        [Display(Name = "意見")]
        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public string Comment { get; set; }
        [Display(Name = "條款")]
        public bool Terms { get; set; }
    }

    public enum Gender
    {
        Female = 0,
        Male = 1,
        Other = 2
    }
}
