using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroEstudiantesWPF.Entidades
{
    public class Usuarios
    {
        [Key]

        public int UsuarioID { get; set; }

        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        public string Alias { get; set; }

        public string Nombres { get; set; }

        public string Email { get; set; }

        public string Clave { get; set; }

        public bool Activo { get; set; }

        public int RolId { get; set; }

        [ForeignKey("RolId")]

        public virtual Roles Rol { get; set; }


    }
}
