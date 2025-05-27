using HomeRentApp_MAUI.Models;
using HomeRentApp_MAUI.Services;

namespace HomeRentApp_MAUI.Views;

public partial class LoginPage : ContentPage
{
    private bool esModoRegistro = false;
    private readonly UsuarioService _usuarioService = new UsuarioService();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginOrRegisterClicked(object sender, EventArgs e)
    {
        string correo = usuarioEntry.Text?.Trim();
        string contrasena = contrasenaEntry.Text;
        string nombre = nombreEntry.Text?.Trim();
        string apellido = apellidoEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena) || (esModoRegistro && string.IsNullOrWhiteSpace(nombre) || (esModoRegistro && string.IsNullOrWhiteSpace(apellido))))
        {
            await DisplayAlert("Error", "Completa todos los campos.", "OK");
            return;
        }

        if (esModoRegistro)
        {
            // REGISTRO
            var nuevoUsuario = new Usuario
            {
                UsuarioId = Guid.NewGuid().ToString(),
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Contraseña = contrasena
            };

            try
            {
                await _usuarioService.RegistrarUsuarioAsync(nuevoUsuario);
                await DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
                CambiarModo(false);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        else
        {
            // LOGIN SIMULADO
            var usuario = await _usuarioService.AutenticarUsuarioAsync(correo, contrasena);
            if (usuario != null)
            {
                Application.Current.MainPage = new AppShell();
                await Navigation.PushAsync(new DepartamentosPage());
            }
            else
            {
                await DisplayAlert("Error", "Credenciales incorrectas", "OK");
            }
        }

    }

    private void OnToggleMode(object sender, EventArgs e)
    {
        CambiarModo(!esModoRegistro);
    }

    private void CambiarModo(bool registro)
    {
        esModoRegistro = registro;

        nombreEntry.IsVisible = registro;
        apellidoEntry.IsVisible = registro;
        loginButton.Text = registro ? "Registrarse" : "Iniciar Sesión";
        toggleLabel.Text = registro ? "¿Ya tienes cuenta? Inicia sesión" : "¿No tienes cuenta? Regístrate";
    }
}