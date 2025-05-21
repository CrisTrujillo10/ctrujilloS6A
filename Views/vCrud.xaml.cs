using System.Collections.ObjectModel;
using ctrujilloS6A.Models;
using Newtonsoft.Json;

namespace ctrujilloS6A.Views;

public partial class vCrud : ContentPage
{
	private const string URL = "http://172.22.224.1:8086/api/usuario/views";

    private HttpClient cliente = new HttpClient();
	private ObservableCollection<Usuario> _usuarioTem;
	public vCrud()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await mostrarUsuario();
    }

    public async Task mostrarUsuario()
    {
        try
        {
            var content = await cliente.GetStringAsync(URL);
            List<Usuario> lista = JsonConvert.DeserializeObject<List<Usuario>>(content);
            _usuarioTem = new ObservableCollection<Usuario>(lista);
            lvUsuarios.ItemsSource = _usuarioTem;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los usuarios: {ex.Message}", "OK");
        }
    }

    private void btnAñadir_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new vAñadirUsuario());
    }

	private async void lvUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
        if (e.SelectedItem == null) return;

        var usuario = e.SelectedItem as Usuario;

        lvUsuarios.SelectedItem = null;

        await Navigation.PushAsync(new vEliminarUsuario(usuario));
    }
}