using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroEstudiantesWPF.Entidades;
using RegistroEstudiantesWPF.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroEstudiantesWPF.BLL
{
    public class EstudiantesBLL
    {
        public static bool Guardar(Estudiantes estudiante)
        {
            if (!Existe(estudiante.EstudianteId))
                return Insertar(estudiante);
            else
                return Modificar(estudiante);

        }

        private static bool Insertar(Estudiantes estudiante)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Estudiantes.Add(estudiante);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }

        private static bool Modificar(Estudiantes estudiante)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(estudiante).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;

        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var estudiante = contexto.Estudiantes.Find(id);

                if (estudiante != null)
                {
                    contexto.Estudiantes.Remove(estudiante);
                    paso = contexto.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Estudiantes Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Estudiantes estudiante;

            try
            {
                estudiante = contexto.Estudiantes.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return estudiante;
        }

        public static List<Estudiantes> GetList(Expression<Func<Estudiantes, bool>> criterio)
        {
            List<Estudiantes> lista = new List<Estudiantes>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Estudiantes.Where(criterio).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Estudiantes.Any(e => e.EstudianteId == id);
            }

            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

    }
}
