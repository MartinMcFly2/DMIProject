using App_Cursos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_Cursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeguimientoCursos : ContentPage
    {
        public SeguimientoCursos()
        {
            InitializeComponent();
            txtEstatus.Items.Add("Programado");
            txtEstatus.Items.Add("En Progreso");
            txtEstatus.Items.Add("Completado");
            llenarDatos();
            llenarDatos2();
            llenarDatos3();
        }

        private void LimpiarControles()
        {
            txtNombreEmp.SelectedItem = null;
            txtNombreCso.SelectedItem = null;
            txtEstatus.SelectedItem = null;
            txtLugarCurso.Text = string.Empty;
            txtFecha.Date = DateTime.Now.Date;
            txtHora.Time = TimeSpan.Zero;
            txtCalificación.Text = string.Empty;
        }

        public async void llenarDatos()
        {
            var SeguimientoList = await App.SQLiteDB.GetSeguimientoAsync();
            if (SeguimientoList != null)
            {
                lsSeguimiento.ItemsSource = SeguimientoList;
            }
        }

        public async void llenarDatos2()
        {
            var empleado = await App.SQLiteDB.GetEmpleadosAsync();
            if (empleado != null)
            {
                txtIdEmp.SelectedItem = empleado;
                txtNombreEmp.ItemsSource = empleado;
            }
        }

        public async void llenarDatos3()
        {
            var NombreCurso = await App.SQLiteDB.GetCursosAsync();
            if (NombreCurso != null)
            {
                txtNombreCso.ItemsSource = NombreCurso;
            }
        }

        public bool validarDatos2()
        {
            bool respuesta;

            //if (string.IsNullOrEmpty(txtNombreEmp.Items[txtNombreEmp.SelectedIndex]))
            if (txtNombreEmp.SelectedIndex == -1 && txtNombreEmp.SelectedIndex <= txtNombreEmp.Items.Count)
            {
                respuesta = false;
            }
            //else if (string.IsNullOrEmpty(txtNombreCso.Items[txtNombreCso.SelectedIndex]))
            else if (txtNombreCso.SelectedIndex == -1 && txtNombreCso.SelectedIndex <= txtNombreCso.Items.Count)
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtLugarCurso.Text))
            {
                respuesta = false;
            }
            else if (txtFecha.Date == null || txtFecha.Date < DateTime.Today)
            {
                respuesta = false;
            }
            else if (txtHora.Time == null || txtHora.Time < TimeSpan.Zero)
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEstatus.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCalificación.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;

        }
        private void txtEstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEstatus.SelectedItem.ToString();
        }

        private async void btnGuardarSeg_Clicked(object sender, EventArgs e)
        {
            if (validarDatos2())
            {
                await DisplayAlert("AVISO", "Paso Validacion", "ok");
                Seguimiento SeguimientoG = new Seguimiento
                {
                    Nombre_de_Empleado_2 = txtNombreEmp.Items[txtNombreEmp.SelectedIndex],
                    Nombre_de_Curso_2 = txtNombreCso.Items[txtNombreCso.SelectedIndex],
                    Lugar_del_Curso = txtLugarCurso.Text,
                    Fecha = txtFecha.Date,
                    Hora = txtHora.Time,
                    Estatus = txtEstatus.SelectedItem.ToString(),
                    Calificación = int.Parse(txtCalificación.Text),

                };

                await App.SQLiteDB.SaveSeguimientoAsync(SeguimientoG);

                
                txtNombreEmp.SelectedItem = "";
                txtNombreCso.SelectedItem = "";
                txtLugarCurso.Text = "";
                txtFecha.Date = DateTime.Now.Date;
                txtHora.Time = TimeSpan.Zero;
                txtEstatus.SelectedItem = "";
                txtCalificación.Text = "";
                

                await DisplayAlert("❌AVISO", "Seguimiento Guardado con Exito", "✅OK");

                llenarDatos();

                /*
                var SeguimientoList = await App.SQLiteDB.GetSeguimientoAsync();
                if (SeguimientoList != null)
                {
                    lsSeguimiento.ItemsSource = SeguimientoList;
                }
                else
                {
                    await DisplayAlert("❌AVISO", "Ingresar los Datos", "✅OK");
                }
                */

            }else
            {
                await DisplayAlert("❌AVISO", "Ingresar los Datos", "✅OK");

            }
        }

        

        private async void lsSeguimiento_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Seguimiento)e.SelectedItem;

            txtIdSto.IsVisible = true;
            btnActualizarSeg.IsVisible = true;
            btnGuardarSeg.IsVisible = false;
            btnEliminarSeg.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IDSto.ToString()))
            {
                var seguimientols = await App.SQLiteDB.GetSeguimientoByIdAsync(obj.IDSto);

                if (seguimientols != null)
                {
                    txtIdSto.Text = seguimientols.IDSto.ToString();
                    txtNombreEmp.SelectedIndex = txtNombreEmp.Items.IndexOf(seguimientols.Nombre_de_Empleado_2);
                    txtNombreCso.SelectedIndex = txtNombreCso.Items.IndexOf(seguimientols.Nombre_de_Curso_2);
                    txtLugarCurso.Text = seguimientols.Lugar_del_Curso;
                    txtEstatus.SelectedItem = seguimientols.Estatus;
                    txtFecha.Date = seguimientols.Fecha;
                    txtHora.Time = seguimientols.Hora;
                    txtCalificación.Text = seguimientols.Calificación.ToString();
                }
            }
        }
        private async void btnActualizarSeg_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdSto.Text))
            {
                Seguimiento seguimientoA = new Seguimiento
                {
                    IDSto = int.Parse(txtIdSto.Text),
                    Nombre_de_Empleado_2 = txtNombreEmp.Items[txtNombreEmp.SelectedIndex],
                    Nombre_de_Curso_2 = txtNombreCso.Items[txtNombreCso.SelectedIndex],
                    Lugar_del_Curso = txtLugarCurso.Text,
                    Fecha = txtFecha.Date,
                    Hora = txtHora.Time,
                    Estatus = txtEstatus.SelectedItem as string,
                    Calificación = int.Parse(txtCalificación.Text),
                };

                await App.SQLiteDB.SaveSeguimientoAsync(seguimientoA);

                txtNombreEmp.SelectedItem = seguimientoA.Nombre_de_Empleado_2;
                txtNombreCso.SelectedItem = seguimientoA.Nombre_de_Curso_2;
                txtLugarCurso.Text = "";
                txtFecha.Date = DateTime.Now.Date;
                txtHora.Time = TimeSpan.Zero;
                txtEstatus.SelectedItem = "";
                txtCalificación.Text = "";

                await DisplayAlert("❌AVISO", "Se Actualizo Seguimiento de Manera Exitosa", "✅OK");
                LimpiarControles();
                llenarDatos();

                txtIdSto.IsVisible = false;
                btnActualizarSeg.IsVisible = false;
                btnGuardarSeg.IsVisible = true;
                btnEliminarSeg.IsVisible = true;
            }
        }

        private async void btnEliminarSeg_Clicked(object sender, EventArgs e)
        {
            var seguimientoB = await App.SQLiteDB.GetSeguimientoByIdAsync(int.Parse(txtIdSto.Text));
            if (seguimientoB != null)
            {
                await App.SQLiteDB.DeleteSeguimientoAsync(seguimientoB);
                await DisplayAlert("❌AVISO", "Se Elimino Seguimiento de Manera Exitosa", "✅OK");
                //LimpiarControles();
                llenarDatos();

                txtIdSto.Text = "";
                txtNombreEmp.SelectedItem = "";
                txtNombreCso.SelectedItem = "";
                txtLugarCurso.Text = "";
                txtFecha.Date = DateTime.Now.Date;
                txtHora.Time = TimeSpan.Zero;
                txtEstatus.SelectedItem = "";
                txtCalificación.Text = "";

                txtIdSto.IsVisible = false;
                btnGuardarSeg.IsVisible = true;
                btnActualizarSeg.IsVisible = false;
                btnEliminarSeg.IsVisible = false;
            }
        }
    }
}
  