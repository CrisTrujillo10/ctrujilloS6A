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
		mostrarUsuario();
	}

	public async void mostrarUsuario()
	{
		var content = await cliente.GetStringAsync(URL);
		List<Usuario> lista = JsonConvert.DeserializeObject<List<Usuario>>(content);
		_usuarioTem = new ObservableCollection<Usuario>(lista);
		lvUsuarios.ItemsSource = _usuarioTem;

        lvUsuarios.ItemsSource = new List<Usuario>
		{
			new Usuario { id = 1, nombre = "Cris", correo = "cris@gmail.com", estado = true },
			new Usuario { id = 2, nombre = "Luis", correo = "luis@gmail.com", estado = true },
		};
    }
}