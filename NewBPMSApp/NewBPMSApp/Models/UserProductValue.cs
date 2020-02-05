using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewBPMSApp.Models
{
    public class UserProductValue
    {
        /// <summary>
        /// 指的是UserContract表中对应的Id
        /// </summary>
        public Guid Id { get; set; }

        [Display(Name = "分工")]
        public Labor Labor { get; set; }

        [Display(Name = "产值比例")]
        public decimal Ratio { get; set; }

        [Display(Name = "产值")]
        public decimal Amount { get; set; }

        public string UserId { get; set; }
        [Display(Name = "姓名")]
        public string StaffName { get; set; }

        public Guid ContractId { get; set; }

        [Display(Name = "项目名称")]
        public string ContractName { get; set; }
    }

    public enum Labor
    {
        [Display(Name = "项目负责")]
        response = 1,
        [Display(Name = "现场检测")]
        siteWorking = 2,
        [Display(Name = "报告撰写")]
        reportWriting = 3,
        [Display(Name = "报告校核")]
        reportChecking = 4,
        [Display(Name = "数据分析")]
        dataAnalyse = 5,
    }
}
