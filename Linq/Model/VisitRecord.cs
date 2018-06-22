using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Linq.Model {
    [Table("VisitRecord")]
    public class VisitRecord
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// 销售顾问Id
        /// </summary>
        [Required]
        public int ConsultantId { get; set; }

        /// <summary>
        /// 确度
        /// </summary>
        [Required]
        public int Intent { get; set; }

        /// <summary>
        /// 来访时间
        /// </summary>
        [Required]
        public DateTime VisitTime { get; set; }
    }
}
