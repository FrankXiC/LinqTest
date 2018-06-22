using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestDataBaseEx.Model {
    [Table("ReturnVisitTask")]
    public class ReturnVisitTask
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
        public int CustomerId { get; set; }

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
        /// 预计回访时间
        /// </summary>
        [Required]
        public DateTime ExpectedTime { get; set; }

        /// <summary>
        /// 实际回访时间
        /// </summary>
        public DateTime? ActualTime { get; set; }

    }
}
