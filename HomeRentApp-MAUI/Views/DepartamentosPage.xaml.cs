using HomeRentApp_MAUI.Models;
using HomeRentApp_MAUI.Services;

namespace HomeRentApp_MAUI.Views;

public partial class DepartamentosPage : ContentPage
{
    private DepartamentoService _service = new DepartamentoService();

    public DepartamentosPage()
    {
        InitializeComponent();
        CargarHabitaciones();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarHabitaciones(); // Llama de nuevo a la API
    }

    private async Task CargarHabitaciones()
    {
        try
        {
            var lista = await _service.ObtenerDepartamentosAsync();
            DepartamentosList.ItemsSource = lista;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudieron cargar los departamentos: {ex.Message}", "OK");
        }
    }

    private async void OnEliminarClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var departamento = button?.CommandParameter as Departamento;

        if (departamento != null)
        {
            bool confirm = await DisplayAlert("Confirmar", $"¿Eliminar '{departamento.Nombre}'?", "Sí", "No");
            if (confirm)
            {
                await _service.EliminarDepartamentoAsync(departamento.DepartamentoId);
                await DisplayAlert("Éxito", "Habitación eliminada", "OK");
                CargarHabitaciones();
            }
        }
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var departamento = button?.CommandParameter as Departamento;

        if (departamento != null)
        {
            await Navigation.PushAsync(new EditarDepartamentoPage(departamento));
        }
    }
}