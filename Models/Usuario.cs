using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctrujilloS6A.Models
{
    public class Usuario
    {
        public long Id{ get; set; }

        public string? Nombre { get; set; }

        public string? Correo { get; set; }

        public Boolean Estado { get; set; }
    }
}
