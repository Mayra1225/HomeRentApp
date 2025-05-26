using HomeRentApp_MAUI.Models;
using HomeRentApp_MAUI.Services;

namespace HomeRentApp_MAUI.Views;

public partial class EditarDepartamentoPage : ContentPage
{
    private Departamento _departamento;
    private FileResult imagenSeleccionada;
    private readonly DepartamentoService _service = new DepartamentoService();
    public EditarDepartamentoPage(Departamento departamento)
    {
        InitializeComponent();
        _departamento = departamento;
        BindingContext = _departamento;
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
            content.Add(new StringContent(_departamento.DepartamentoId.ToString()), "DepartamentoId");
            content.Add(new StringContent(nombreEntry.Text), "Nombre");
            content.Add(new StringContent(direccionEntry.Text), "Direccion");
            content.Add(new StringContent(precioEntry.Text), "Precio");
            content.Add(new StringContent(cuartosEntry.Text), "CuartosDisponibles");
            content.Add(new StringContent("5"), "UsuarioId");

            if (imagenSeleccionada != null)
            {
                var stream = await imagenSeleccionada.OpenReadAsync();
                content.Add(new StreamContent(stream), "Imagen", imagenSeleccionada.FileName);
            }

            await _service.ActualizarDepartamentoMultipartAsync(_departamento.DepartamentoId, content);
            await DisplayAlert("�xito", "Departamento actualizado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}