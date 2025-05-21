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

    private async void btnAñadir_Clicked(object sender, EventArgs e)
    {
        try
        {
            Usuario nuevoUsuario = new Usuario
            {
                id = long.Parse(txtId.Text),
                nombre = txtNombre.Text,
                correo = txtCorreo.Text,
                estado = bool.Parse(txtEstado.Text.ToLower())
            };

            string json = JsonConvert.SerializeObject(nuevoUsuario);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync("http://172.22.224.1:8086/api/usuario/add", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Usuario añadido correctamente", "OK");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    await DisplayAlert("Error", $"Código de respuesta: {response.StatusCode}", "OK");
                }
            }
        }
        catch (FormatException)
        {
            await DisplayAlert("Error", "ID debe ser numérico y Estado debe ser 'true' o 'false'", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error inesperado", ex.Message, "OK");
        }
    }
}