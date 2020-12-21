using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIM_Back.Models.Dto
{
    public class InteractionResult
    {
        public InteractionResult()
        {
        }

        public bool Success { get; set; }

        public string Error { get; set; }
    }
}
