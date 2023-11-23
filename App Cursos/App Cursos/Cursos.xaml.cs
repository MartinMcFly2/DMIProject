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
    public partial class Cursos : ContentPage
    {

        public Cursos()
        {
            InitializeComponent();
            txtTipo_de_Curso.Items.Add("Interno");
            txtTipo_de_Curso.Items.Add("Externo");
            LlenarDatos();
        }
        private async void Guardar_Curso_Button_Clicked(object sender, EventArgs a)
        {
            if (ValidarDatos())
            {
                CursosE cso = new CursosE
                {
                    Nombre_del_Curso = txtNombre_del_Curso.Text,
                    Tipo_de_Curso = txtTipo_de_Curso.SelectedItem.ToString(),
                    Descripción_del_Curso = txtDescripción_del_Curso.Text,
                    Horas_del_Curso = int.Parse(txtHoras_del_Curso.Text)
                };

                await App.SQLiteDB.SaveCursosAsync(cso);

                txtNombre_del_Curso.Text = "";
                txtTipo_de_Curso.SelectedItem = "";
                txtDescripción_del_Curso.Text = "";
                txtHoras_del_Curso.Text = "";
                await DisplayAlert("❌AVISO", "Se Registro Curso", "✅Ok");
                LlenarDatos();

                var CursosList = await App.SQLiteDB.GetCursosAsync();
                if (CursosList != null)
                {
                    lsCursos.ItemsSource = CursosList;
                }

            }
            else
            {
                await DisplayAlert("❌AVISO", "Ingresar los Datos", "✅Ok");
            }
        }
        private async void Button_Actualizar_Curso_Clicked(object sender, EventArgs a)
        {
            if (!string.IsNullOrEmpty(txtIdCso.Text))
            {
                CursosE curso = new CursosE()
                {
                    IDCso = int.Parse(txtIdCso.Text),
                    Nombre_del_Curso = txtNombre_del_Curso.Text,
                    Tipo_de_Curso = txtTipo_de_Curso.SelectedItem.ToString(),
                    Descripción_del_Curso = txtDescripción_del_Curso.Text,
                    Horas_del_Curso = int.Parse(txtHoras_del_Curso.Text)
                };

                await App.SQLiteDB.SaveCursosAsync(curso);
                txtIdCso.Text = "";
                txtNombre_del_Curso.Text = "";
                txtTipo_de_Curso.SelectedItem = "";
                txtDescripción_del_Curso.Text = "";
                txtHoras_del_Curso.Text = "";

                txtIdCso.IsVisible = false;
                btnRegistrar_Curso.IsVisible = true;
                btnActualizar_Curso.IsVisible = false;

                await DisplayAlert("❌AVISO", "Se Actualizo Registro de Manera Exitosa", "✅OK");
                LlenarDatos();
            }
        }
        public async void Borrar_Button_Curso_Clicked(object sender, EventArgs a)
        {
            var curso = await App.SQLiteDB.GetCursosByIdAsync(int.Parse(txtIdCso.Text));
            if (curso != null)
            {
                await App.SQLiteDB.DeleteCursosAsync(curso);
                await DisplayAlert("❌AVISO", "Se Elimino el Registro de Manera Exitosa", "✅OK");
                txtIdCso.Text = "";
                txtNombre_del_Curso.Text = "";
                txtTipo_de_Curso.SelectedItem = "";
                txtDescripción_del_Curso.Text = "";
                txtHoras_del_Curso.Text = "";

                txtIdCso.IsVisible = false;
                btnRegistrar_Curso.IsVisible = true;
                btnActualizar_Curso.IsVisible = false;
                btnEliminar_Curso.IsVisible = false;
                LlenarDatos();
            }
        }
        public async void LlenarDatos()
        {
            var CursosList = await App.SQLiteDB.GetCursosAsync();
            if (CursosList != null)
            {
                lsCursos.ItemsSource = CursosList;
            }
        }

        private async void lstCursos_ItemSelected(object sender, SelectedItemChangedEventArgs a)
        {
            var Obj = (CursosE)a.SelectedItem;

            btnRegistrar_Curso.IsVisible = false;
            txtIdCso.IsVisible = true;
            btnActualizar_Curso.IsVisible = true;
            btnEliminar_Curso.IsVisible = true;

            if (!string.IsNullOrEmpty(Obj.IDCso.ToString()))
            {
                var cso = await App.SQLiteDB.GetCursosByIdAsync(Obj.IDCso);

                if (cso != null)
                {
                    txtIdCso.Text = cso.IDCso.ToString();
                    txtNombre_del_Curso.Text = cso.Nombre_del_Curso;
                    txtTipo_de_Curso.SelectedItem = cso.Tipo_de_Curso;
                    txtDescripción_del_Curso.Text = cso.Descripción_del_Curso;
                    txtHoras_del_Curso.Text = cso.Horas_del_Curso.ToString();
                }
            }
        }

        public bool ValidarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre_del_Curso.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTipo_de_Curso.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDescripción_del_Curso.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtHoras_del_Curso.Text))
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