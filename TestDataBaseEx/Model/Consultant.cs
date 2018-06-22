using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDataBaseEx.Model
{
    [Table("Consultant")]
    public class Consultant
    {
        public const int Maxlength = 50;
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 销售顾问名称
        /// </summary>
        [Required]
        [MaxLength(Maxlength)]
        public string ConsultantName { get; set; }
    }
}
