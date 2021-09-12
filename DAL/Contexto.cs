using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroEstudiantesWPF.Entidades;

namespace RegistroEstudiantesWPF.DAL
{
    public class Contexto : DbContext 
    {
       public DbSet<Estudiantes> Estudiantes { get; set; }

       public DbSet<Roles> Roles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= DATA\TeacherControl.db");
        }
    }

    
}
