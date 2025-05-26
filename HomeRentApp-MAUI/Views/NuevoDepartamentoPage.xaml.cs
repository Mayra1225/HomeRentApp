using HomeRentApp_MAUI.Models;
using HomeRentApp_MAUI.Services;

namespace HomeRentApp_MAUI.Views;

public partial class NuevoDepartamentoPage : ContentPage
{
    private FileResult imagenSeleccionada;
    private readonly DepartamentoService _service = new DepartamentoService();

    public NuevoDepartamentoPage()
    {
        InitializeComponent();
    }

    private async void OnSeleccionarImagen(object sender, EventArgs e)
    {
        imagenSeleccionada = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Selecciona una imagen"
        });

        if (imagenSeleccionada != null)
        {
            var stream = await imagenSeleccionada.OpenReadAsync();
            imagenPreview.Source = ImageSource.FromStream(() => stream);
        }
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        try
        {
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(nombreEntry.Text), "Nombre");
            content.Add(new StringContent(direccionEntry.Text), "Direccion");
            content.Add(new StringContent(precioEntry.Text), "Precio");
            content.Add(new StringContent(cuartosEntry.Text), "CuartosDisponibles");
            content.Add(new StringContent("5"), "UsuarioId"); // puedes hacerlo dinámico

            if (imagenSeleccionada != null)
            {
                var stream = await imagenSeleccionada.OpenReadAsync();
                content.Add(new StreamContent(stream), "Imagen", imagenSeleccionada.FileName);
            }

            await _service.CrearDepartamentoMultipartAsync(content);

            await DisplayAlert("Éxito", "Departamento creado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}