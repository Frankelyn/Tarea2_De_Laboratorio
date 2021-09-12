using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegistroEstudiantesWPF.BLL;
using RegistroEstudiantesWPF.Entidades;

namespace RegistroEstudiantesWPF.UI.Registros
{
    public partial class rRoles : Window
    {
        public class DateFormat : System.Windows.Data.IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value == null) return null;

                return ((DateTime)value).ToString("dd/MM/yyyy");
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        private Roles Rol = new Roles();

       
        private void Limpiar()
        {
            this.Rol = new Roles();
            this.DataContext = Rol;


        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Roles esValido = RolesBLL.Buscar(Rol.Descripcion);

            return (esValido != null);
        }

        private bool Validar()
        {
            bool esValido = true;

            if (DescripcionTextbox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (string.IsNullOrWhiteSpace(DescripcionTextbox.Text))
            {
                esValido = false;
                MessageBox.Show("Descripcion no valida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(ExisteEnLaBaseDeDatos())
            {
                esValido = false;
                MessageBox.Show("Este rol ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return esValido;
        }
        public rRoles()
        {
            InitializeComponent();
            this.DataContext = Rol;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var rol = RolesBLL.Buscar(Utilidades.ToInt(RolIdTextbox.Text));

            if (Rol != null)
                this.Rol = rol;
            else
                this.Rol = new Roles();

            this.DataContext = this.Rol;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();

            RolIdTextbox.Clear();
            DescripcionTextbox.Clear();
            RolIdTextbox.Focus();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = RolesBLL.Guardar(Rol);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccion exitosa!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Eliminar(Utilidades.ToInt(RolIdTextbox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

            RolIdTextbox.Clear();
            DescripcionTextbox.Clear();
            RolIdTextbox.Focus();
        }
    }
}
