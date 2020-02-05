using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NewBPMSApp.Models
{
    public class DetailsContract
    {
        public string StatusMessage { get; set; }

        /// <summary>
        /// 已分配产值比例
        /// </summary>
        [Display(Name = "产值已分配")]
        public decimal PdtPercent { get; set; }

        public Contract ContractViewModel { get; set; }

        public IEnumerable<UserProductValue> UserProductValueViewModels { get; set; }
    }
}
