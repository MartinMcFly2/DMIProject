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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        private async void Button_Login(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailLog.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir un Email en el Campo", "✅OK");
                return;
            }
            if (string.IsNullOrEmpty(txtContraLog.Text))
            {
                await DisplayAlert("❌AVISO", "Debe Escribir la Contraseña", "✅OK");
                return;
            }
            var resultado = await App.SQLiteDB.GetUsersValidate(txtEmailLog.Text, txtContraLog.Text);

            if (resultado.Count > 0)
            {
                txtEmailLog.Text = "";
                txtContraLog.Text = "";

                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("❌AVISO", "El Email o la Contaseña esta Incorretco", "✅OK");
                txtEmailLog.Text = "";
                txtContraLog.Text = "";
                await Navigation.PushAsync(new Registro());
            }
        }

        private async void Button_Registrarse(object sender, EventArgs e)
        {
            txtEmailLog.Text = "";
            txtContraLog.Text = "";

            await Navigation.PushAsync(new Registro());
        }
    }
}