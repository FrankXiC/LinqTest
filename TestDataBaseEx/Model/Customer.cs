using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestDataBaseEx.Model {
    [Table("Customer")]
    public class Customer
    {
        public const int Maxlength = 50;
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [Required]
        [MaxLength(Maxlength)]
        public string CustomerName { get; set; }

        /// <summary>
        /// 当前确度
        /// </summary>
        [Required]
        public int CurrentIntent { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

    }
}
