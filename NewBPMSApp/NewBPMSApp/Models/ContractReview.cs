using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NewBPMSApp.Models
{
    public class ContractReview
    {
        public string StatusMessage { get; set; }
        public IEnumerable<DetailsContract> DetailsContractViewModels { get; set; }
    }
}
