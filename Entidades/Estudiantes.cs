using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RegistroEstudiantesWPF.Entidades
{
   
    public class Estudiantes
    {
        [Key]
        public int EstudianteId { get; set; }

        public string Nombres { get; set; }
        
        public int Semestre { get; set; }
    }
}
