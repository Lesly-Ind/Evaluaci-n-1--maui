
namespace indioExamen.Views;

public partial class Resumen : ContentPage
{
    public Resumen(
        string usuario,
        string nombre,
        string apellido,
        int edad,
        DateTime fecha,
        string ciudad,
        string pais,
        decimal montoInicial,
        decimal pagoMensual,
        decimal pagoTotal)
    {
        InitializeComponent();

        // USUARIO CONECTADO
        lblUsuarioConectado.Text =
            "Usuario conectado: " + usuario;

        // DATOS PERSONALES
        lblNombre.Text = nombre;
        lblApellido.Text = apellido;
        lblEdad.Text = edad.ToString();

        // FECHA
        lblFecha.Text = fecha.ToString("dd/MM/yyyy");

        // UBICACION
        lblCiudad.Text = ciudad;
        lblPais.Text = pais;

        // INFORMACION DE PAGOS
        lblMontoInicial.Text =
            "$ " + montoInicial.ToString("F2");

        lblPagoMensual.Text =
            "$ " + pagoMensual.ToString("F2");

        lblPagoTotal.Text =
            "$ " + pagoTotal.ToString("F2");
    }

    // REGRESAR AL REGISTRO
    private async void BtnVolver_Clicked(
        object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
