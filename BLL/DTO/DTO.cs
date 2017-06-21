using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL.DTO
{
   public abstract class DTO:IDTO
    {
        public int Id { get; }
    }
}
