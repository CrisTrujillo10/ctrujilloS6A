using ctrujilloS6A.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ctrujilloS6A.Views;

public partial class vEliminarUsuario : ContentPage
{
    private Usuario usuario;
	public vEliminarUsuario(Usuario usuario)
	{
		InitializeComponent();
        this.usuario = usuario;

        txtId.Text = usuario.id.ToString();
        txtNombre.Text = usuario.nombre;
        txtCorreo.Text = usuario.correo;
        txtEstado.Text = usuario.estado.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            Usuario usuarioActualizado = new Usuario
            {
                id = usuario.id,  
                nombre = txtNombre.Text,
                correo = txtCorreo.Text,
                estado = bool.Parse(txtEstado.Text.ToLower())  
            };

            string json = JsonConvert.SerializeObject(usuarioActualizado);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PutAsync($"http://172.22.224.1:8086/api/usuario/update", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Usuario actualizado correctamente", "OK");
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
            await DisplayAlert("Error", "Estado debe ser 'true' o 'false'", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error inesperado", ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmacion = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres eliminar este usuario?", "Sí", "No");
            if (!confirmacion)
            {
                return; 
            }

            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"http://172.22.224.1:8086/api/usuario/delete/{usuario.id}");

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Usuario eliminado correctamente", "OK");
                    
                    await Navigation.PopAsync(); 
                }
                else
                {
                    await DisplayAlert("Error", $"Código de respuesta: {response.StatusCode}", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
