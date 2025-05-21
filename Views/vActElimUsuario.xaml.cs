using ctrujilloS6A.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ctrujilloS6A.Views;

public partial class vActElimUsuario : ContentPage
{
    private Usuario usuario;
	public vActElimUsuario(Usuario usuario)
	{
		InitializeComponent();
        this.usuario = usuario;

        txtId.Text = usuario.Id.ToString();
        txtNombre.Text = usuario.Nombre;
        txtCorreo.Text = usuario.Correo;
        txtEstado.Text = usuario.Estado.ToString();
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            Usuario usuarioActualizar = new Usuario
            {
                Id = long.Parse(txtId.Text),
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text,
                Estado = bool.Parse(txtEstado.Text.ToLower())
            };

            string json = JsonConvert.SerializeObject(usuarioActualizar);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5070/api/usuario/{usuarioActualizar.Id}";
                var response = client.PutAsync(url, content).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    this.Dispatcher.Dispatch(() =>
                    {
                        DisplayAlert("Éxito", "Usuario actualizado correctamente", "OK");
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

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            long idUsuario = long.Parse(txtId.Text);

            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:5070/api/usuario/{idUsuario}";
                var response = client.DeleteAsync(url).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    this.Dispatcher.Dispatch(() =>
                    {
                        DisplayAlert("Éxito", "Usuario eliminado correctamente", "OK");
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
                DisplayAlert("Error", "ID debe ser numérico", "OK");
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
