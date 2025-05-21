using System.Net.Http.Headers;
using ctrujilloS6A.Models;
using Newtonsoft.Json;
using System.Text;


namespace ctrujilloS6A.Views;

public partial class vAñadirUsuario : ContentPage
{
    public vAñadirUsuario()
    {
        InitializeComponent();
    }

    private void btnAñadir_Clicked(object sender, EventArgs e)
    {
        try
        {
            Usuario nuevoUsuario = new Usuario
            {
                Id = long.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text,
                Estado = bool.Parse(txtEstado.Text.ToLower())
            };

            string json = JsonConvert.SerializeObject(nuevoUsuario);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsync("http://localhost:5070/api/usuario", content).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    this.Dispatcher.Dispatch(() =>
                    {
                        DisplayAlert("Éxito", "Usuario añadido correctamente", "OK");
                        Navigation.PushAsync(new vCrud());
                    });
                }
                else
                {
                    this.Dispatcher.Dispatch(() =>
                    {
                        DisplayAlert("Error", $"Código de respuesta: {response.StatusCode}", "OK");
                    });
                }
            }
        }
        catch (FormatException)
        {
            this.Dispatcher.Dispatch(() =>
            {
                DisplayAlert("Error", "ID debe ser numérico y Estado debe ser 'true' o 'false'", "OK");
            });
        }
        catch (Exception ex)
        {
            this.Dispatcher.Dispatch(() =>
            {
                DisplayAlert("Error inesperado", ex.Message, "OK");
            });
        }
    }
}