using System;
using System.Collections.Generic;
using System.Text;

namespace NewBPMSApp.Models
{
    public class ContractCheck
    {
        public string StatusMessage { get; set; }

        public IEnumerable<Contract> ContractViewModels { get; set; }
    }
}
