using App_Cursos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_Cursos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            txtTipo_de_Empleado.Items.Add("Planta");
            txtTipo_de_Empleado.Items.Add("Temporal");
            llenarDatos();
        }

        private async void Registrar_Button_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Empleados emple = new Empleados
                {
                    Nombre_de_Empleado = txtNombre_de_Empleado.Text,
                    Dirección = txtDirección.Text,
                    Teléfono = double.Parse(txtTeléfono.Text),
                    Edad = int.Parse(txtEdad.Text),
                    CURP = txtCURP.Text,
                    Tipo_de_Empleado = txtTipo_de_Empleado.SelectedItem.ToString(),
                };

                await App.SQLiteDB.SaveEmpleadoAsync(emple);

                txtNombre_de_Empleado.Text = "";
                txtDirección.Text = "";
                txtTeléfono.Text = "";
                txtEdad.Text = "";
                txtCURP.Text = "";
                txtTipo_de_Empleado.SelectedItem = "";
                await DisplayAlert("❌AVISO", "Se Registro Empleado", "✅Ok");
                llenarDatos();

                var EmpleadosList = await App.SQLiteDB.GetEmpleadosAsync();
                if (EmpleadosList != null)
                {
                    lsEmpleados.ItemsSource = EmpleadosList;
                }

            }
            else
            {
                await DisplayAlert("❌AVISO", "Ingresar los Datos", "✅Ok");
            }
        }
        private async void Button_Actualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmp.Text))
            {
                Empleados empleado = new Empleados()
                {
                    IDEmp = int.Parse(txtIdEmp.Text),
                    Nombre_de_Empleado = txtNombre_de_Empleado.Text,
                    Dirección = txtDirección.Text,
                    Teléfono = double.Parse(txtTeléfono.Text),
                    Edad = int.Parse(txtEdad.Text),
                    CURP = txtCURP.Text,
                    Tipo_de_Empleado = txtTipo_de_Empleado.SelectedItem.ToString()
                };

                await App.SQLiteDB.SaveEmpleadoAsync(empleado);
                txtIdEmp.Text = "";
                txtNombre_de_Empleado.Text = "";
                txtDirección.Text = "";
                txtTeléfono.Text = "";
                txtEdad.Text = "";
                txtCURP.Text = "";
                txtTipo_de_Empleado.SelectedItem = "";

                txtIdEmp.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnActualizar.IsVisible = false;

                await DisplayAlert("❌AVISO", "Se Actualizo Registro de Manera Exitosa", "✅OK");
                llenarDatos();
            }
        }
        public async void Borrar_Button_Clicked(object sender, EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(int.Parse(txtIdEmp.Text));
            if (empleado != null)
            {
                await App.SQLiteDB.DeleteEmpleadosAsync(empleado);
                await DisplayAlert("❌AVISO", "Se Elimino el Registro de Manera Exitosa", "✅OK");
                txtIdEmp.Text = "";
                txtNombre_de_Empleado.Text = "";
                txtDirección.Text = "";
                txtTeléfono.Text = "";
                txtEdad.Text = "";
                txtCURP.Text = "";
                txtTipo_de_Empleado.SelectedItem = "";

                txtIdEmp.IsVisible = false;
                btnRegistrar.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                llenarDatos();
            }
        }
        public async void llenarDatos()
        {
            var EmpleadoList = await App.SQLiteDB.GetEmpleadosAsync();
            if (EmpleadoList != null)
            {
                lsEmpleados.ItemsSource = EmpleadoList;
            }
        }

        private async void lstEmpleados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleados)e.SelectedItem;

            btnRegistrar.IsVisible = false;
            txtIdEmp.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IDEmp.ToString()))
            {
                var emplea = await App.SQLiteDB.GetEmpleadoByIdAsync(obj.IDEmp);

                if (emplea != null)
                {
                    txtIdEmp.Text = emplea.IDEmp.ToString();
                    txtNombre_de_Empleado.Text = emplea.Nombre_de_Empleado;
                    txtDirección.Text = emplea.Dirección;
                    txtTeléfono.Text = emplea.Teléfono.ToString();
                    txtEdad.Text = emplea.Edad.ToString();
                    txtCURP.Text = emplea.CURP.ToString();
                    txtTipo_de_Empleado.SelectedItem = emplea.Tipo_de_Empleado.ToString();
                }
            }
        }

        public bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre_de_Empleado.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDirección.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTeléfono.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCURP.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTipo_de_Empleado.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}