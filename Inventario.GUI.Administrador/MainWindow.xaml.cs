using Inventario.BIZ;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using Inventario.DAL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Inventario.GUI.Administrador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum accion
        {
            Nuevo,
            Editar

        }
        IManejadorEmpleados manejadorEmpleados;
        IManejadorMateriales manejadorMateriales;
        accion accionEmpleados;
        accion accionMateriales;
        public MainWindow()
        {

            InitializeComponent();
            manejadorEmpleados = new ManejadorEmpleados(new RepositorioDeEmpleados());
            manejadorMateriales = new ManejadorMateriales(new RepositorioDeMaterial());

            PonerBotonesEmpleadosEnEdicion(false);
            LimpiarCamposDeEmpleados();
            ActualizarTablaEmpleados();
            LimpiarCamposDeMateriales();
            ActualizarTablaMateriales();
            PonerBotonesMaterialesEnEdicion(false);
        }

        private void PonerBotonesMaterialesEnEdicion(bool value)
        {
            btnMaterialesCancelar.IsEnabled = value;
            btnMaterialesEditar.IsEnabled = !value;
            btnMaterialesEliminar.IsEnabled = !value;
            btnMaterialesGuardar.IsEnabled = value;
            btnMaterialesNuevo.IsEnabled = !value;
        }

        private void LimpiarCamposDeMateriales()
        {
            txbMaterialesDescripcion.Clear();
            txbMaterialesCategoria.Clear();
            txbMaterialesId.Text = "";
            txbMaterialesNombre.Clear();
        }

        private void ActualizarTablaMateriales()
        {
            dtgMateriales.ItemsSource = null;
            dtgMateriales.ItemsSource = manejadorMateriales.Listar;
        }

        private void ActualizarTablaEmpleados()
        {
            dtgEmpleados.ItemsSource = null;
            dtgEmpleados.ItemsSource = manejadorEmpleados.Listar;
        }

        private void LimpiarCamposDeEmpleados()
        {
            txbEmpleadosApellidos.Clear();
            txbEmpleadosArea.Clear();
            txbEmpleadosId.Text = "";
            txbEmpleadosNombre.Clear();
          }

        private void PonerBotonesEmpleadosEnEdicion(bool value)
        {
            btnEmpleadosCancelar.IsEnabled = value;
            btnEmpleadosEditar.IsEnabled = !value;
            btnEmpleadosEliminar.IsEnabled = !value;
            btnEmpleadosGuardar.IsEnabled = value;
            btnEmpleadosNuevo.IsEnabled = !value;
            }

        private void btnEmpleadosNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeEmpleados();
            PonerBotonesEmpleadosEnEdicion(true);
            accionEmpleados = accion.Nuevo;

        }

        private void btnEmpleadosEditar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if(emp != null)
            {
                txbEmpleadosId.Text = emp.Id;
                txbEmpleadosApellidos.Text = emp.Apellidos;
                txbEmpleadosNombre.Text = emp.Nombre;
                txbEmpleadosArea.Text = emp.Area;
                accionEmpleados = accion.Editar;
                PonerBotonesEmpleadosEnEdicion(true);
            }
           
        }

        private void btnEmpleadosGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionEmpleados == accion.Nuevo)
            {
                Empleado emp = new Empleado()
                {
                    Nombre = txbEmpleadosNombre.Text,
                    Apellidos = txbEmpleadosApellidos.Text,
                    Area = txbEmpleadosArea.Text
                };
                if (manejadorEmpleados.Agregar(emp))
                {
                    MessageBox.Show("Empleado agregado correctamente", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeEmpleados();
                    ActualizarTablaEmpleados();
                    PonerBotonesEmpleadosEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El empleado no se pudo agregar", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                Empleado emp = dtgEmpleados.SelectedItem as Empleado;
                emp.Apellidos = txbEmpleadosApellidos.Text;
                emp.Area = txbEmpleadosArea.Text;
                emp.Nombre = txbEmpleadosNombre.Text;
                if (manejadorEmpleados.Modificar(emp))
                {   
                    MessageBox.Show("Empleado editado correctamente", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCamposDeEmpleados();
                ActualizarTablaEmpleados();
                PonerBotonesEmpleadosEnEdicion(false);
                }
                else
            {
                MessageBox.Show("El empleado no se pudo editar", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);

            }


        }
        }

        private void btnEmpleadosCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeEmpleados();
            PonerBotonesEmpleadosEnEdicion(false);
        }

        private void btnEmpleadosEliminar_Click(object sender, RoutedEventArgs e)
        {
            Empleado emp = dtgEmpleados.SelectedItem as Empleado;
            if(emp!= null)
            {
                if (MessageBox.Show("Realmente desea eliminar este empleado?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                   if(manejadorEmpleados.Eliminar(emp.Id))
                    {
                        MessageBox.Show("Empleado eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaEmpleados();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el empleado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                } 
            }
        }

    
        

        private void btnMaterialesEditar_Click(object sender, RoutedEventArgs e)
        {
            Material mat = dtgMateriales.SelectedItem as Material;
            if (mat != null)
            {
                txbMaterialesId.Text = mat.Id;
                txbMaterialesNombre.Text = mat.Nombre;
                txbMaterialesCategoria.Text = mat.Categoria;
                txbMaterialesDescripcion.Text = mat.Descripcion;
                accionMateriales = accion.Editar;
                PonerBotonesMaterialesEnEdicion(true);
            }
        }

        private void btnMaterialesGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionMateriales == accion.Nuevo)
            {
                Material mat = new Material()
                {
                    Nombre = txbMaterialesNombre.Text,
                    Categoria = txbMaterialesCategoria.Text,
                    Descripcion = txbMaterialesDescripcion.Text
                };
                if (manejadorMateriales.Agregar(mat))
                {
                    MessageBox.Show("Material agregado correctamente", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeMateriales();
                    ActualizarTablaMateriales();
                    PonerBotonesMaterialesEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El material no se pudo agregar", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                Material mat = dtgMateriales.SelectedItem as Material;
                mat.Categoria = txbMaterialesCategoria.Text;
                mat.Descripcion = txbMaterialesDescripcion.Text;
                mat.Nombre = txbMaterialesNombre.Text;
                if (manejadorMateriales.Modificar(mat))
                {
                    MessageBox.Show("Material editado correctamente", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposDeMateriales();
                    ActualizarTablaMateriales();
                    PonerBotonesMaterialesEnEdicion(false);
                }
                else
                {
                    MessageBox.Show("El material no se pudo editar", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);

                }


            }

        }

        private void btnMaterialesCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeMateriales();
            PonerBotonesMaterialesEnEdicion(false);
        }

        private void btnMaterialesEliminar_Click(object sender, RoutedEventArgs e)
        {
            Material mat = dtgMateriales.SelectedItem as Material;
            if (mat != null)
            {
                if (MessageBox.Show("Realmente desea eliminar este material?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorEmpleados.Eliminar(mat.Id))
                    {
                        MessageBox.Show("Material eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaMateriales();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el material", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void btnMaterialesNuevo_Click_1(object sender, RoutedEventArgs e)
        {
            LimpiarCamposDeMateriales();
            PonerBotonesMaterialesEnEdicion(true);
            accionMateriales = accion.Nuevo;
        }
    }
}
