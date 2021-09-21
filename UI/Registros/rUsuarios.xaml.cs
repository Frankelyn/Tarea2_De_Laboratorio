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
using RegistroEstudiantesWPF.Entidades;
using RegistroEstudiantesWPF.BLL;

namespace RegistroEstudiantesWPF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
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

        private Usuarios usuario = new Usuarios();

        private void Limpiar()
        {
            this.usuario = new Usuarios();
            this.DataContext = usuario;

            ClavePasswordbox.Password = string.Empty;
            ConfirmarClavePasswordbox.Password = string.Empty;
        }

        private bool Validar()
        {
            bool esValido = true;

            if (UsuarioIdTextbox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Transaccion Fallida", "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (string.IsNullOrWhiteSpace(UsuarioIdTextbox.Text))
            {
                esValido = false;
                MessageBox.Show("UsuarioID no valido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (UsuariosBLL.Existe(usuario.UsuarioID))
            {
                esValido = false;
                MessageBox.Show("Este Usuario ya existe", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (UsuariosBLL.ExisteAlias(usuario.Alias))
            {
                esValido = false;
                MessageBox.Show("Este Alias ya existe", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (UsuariosBLL.ExisteEmail(usuario.Email))
            {
                esValido = false;
                MessageBox.Show("Este Email ya existe", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (ConfirmarClavePasswordbox.Password != ClavePasswordbox.Password)
            {
                esValido = false;
                GuardarButton.IsEnabled = false;
                MessageBox.Show("Las contraseñas no coinciden", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                ConfirmarClavePasswordbox.Focus();
                GuardarButton.IsEnabled = true;
            }


            return esValido;
        }

        public rUsuarios()
        {
            InitializeComponent();
            this.DataContext = usuario;

            //Cargar Roles
            RolCombox.ItemsSource = RolesBLL.GetRoles();
            RolCombox.SelectedValuePath = "RolId";
            RolCombox.DisplayMemberPath = "Descripcion";
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var Usuario = UsuariosBLL.Buscar(Utilidades.ToInt(UsuarioIdTextbox.Text));

            if (usuario != null)
                this.usuario = Usuario;
            else
                this.usuario = new Usuarios();

            this.DataContext = this.usuario;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();

            //this.usuario.RolId = 1;
            UsuarioIdTextbox.Clear();
            AliasTextbox.Clear();
            NombresTextBox.Clear();
            EmailTextBox.Clear();
            ClavePasswordbox.Clear();
            ConfirmarClavePasswordbox.Clear();
            ActivoCheckBox.IsChecked = false;
            UsuarioIdTextbox.Focus();
           
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            var paso = UsuariosBLL.Guardar(usuario);

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
            if (UsuariosBLL.Eliminar(Utilidades.ToInt(UsuarioIdTextbox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

            UsuarioIdTextbox.Clear();
            AliasTextbox.Clear();
            NombresTextBox.Clear();
            EmailTextBox.Clear();
            ClavePasswordbox.Clear();
            ConfirmarClavePasswordbox.Clear();
            ActivoCheckBox.IsChecked = false;
            UsuarioIdTextbox.Focus();
        }
    }
}
