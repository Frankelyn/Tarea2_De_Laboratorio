using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiantesWPF.Entidades
{
   public class Roles
    {
        [Key]
        public int RolId { get; set; }

        public DateTime FechaDeCreacion { get; set; } = DateTime.Now;

        public string Descripcion { get; set; }

    }

    
}
