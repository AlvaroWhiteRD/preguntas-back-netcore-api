using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.DTO
{
    public class ChangesPasswordDTO
    {
        public string BackPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
