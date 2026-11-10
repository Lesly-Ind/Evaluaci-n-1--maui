
using System.Globalization;

namespace indioExamen.Views;

public partial class Registro : ContentPage
{
    private readonly string usuarioConectado;

    private const decimal CostoCurso = 1500m;
    private decimal pagoMensual;
    private decimal pagoTotal;
    private bool calculoRealizado;

    public Registro(string usuario)
    {
        InitializeComponent();

        usuarioConectado = usuario;

        lblUsuarioConectado.Text =
            "Usuario conectado: " + usuario;
    }

    // SELECCIONAR CIUDADES SEGUN EL PAIS
    private void PkPais_SelectedIndexChanged(
        object sender, EventArgs e)
    {
        pkCiudad.Items.Clear();

        switch (pkPais.SelectedItem?.ToString())
        {
            case "Ecuador":
                pkCiudad.Items.Add("Cuenca");
                pkCiudad.Items.Add("Quito");
                pkCiudad.Items.Add("Guayaquil");
                break;

            case "Colombia":
                pkCiudad.Items.Add("Bogotá");
                pkCiudad.Items.Add("Medellín");
                pkCiudad.Items.Add("Cali");
                break;

            case "Perú":
                pkCiudad.Items.Add("Lima");
                pkCiudad.Items.Add("Arequipa");
                pkCiudad.Items.Add("Cusco");
                break;
        }

        pkCiudad.SelectedIndex = -1;
    }

    // INVALIDAR CALCULO SI CAMBIA EL MONTO
    private void TxtMontoInicial_TextChanged(
        object sender, TextChangedEventArgs e)
    {
        calculoRealizado = false;
        pagoMensual = 0m;
        pagoTotal = 0m;

        txtPagoMensual.Text = "";
    }

    // CALCULAR PAGO MENSUAL
    private async void BtnCalcular_Clicked(
        object sender, EventArgs e)
    {
        calculoRealizado = false;
        txtPagoMensual.Text = "";

        if (!decimal.TryParse(
            txtMontoInicial.Text,
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out decimal montoInicial))
        {
            await DisplayAlertAsync(
                "Error",
                "Ingrese un monto inicial válido.",
                "Aceptar");
            return;
        }

        if (montoInicial < 0 ||
            montoInicial > CostoCurso)
        {
            await DisplayAlertAsync(
                "Error",
                "El monto debe estar entre 0 y 1500.",
                "Aceptar");
            return;
        }

        // SALDO PENDIENTE
        decimal saldo = CostoCurso - montoInicial;

        // DIVIDIR EN 4 CUOTAS
        decimal cuotaBase = saldo / 4m;

        // APLICAR 4% A CADA CUOTA
        decimal interes = cuotaBase * 0.04m;

        // PAGO MENSUAL
        pagoMensual = Math.Round(
            cuotaBase + interes, 2);

        // PAGO TOTAL
        pagoTotal = montoInicial +
                    pagoMensual * 4m;

        // MOSTRAR RESULTADO
        txtPagoMensual.Text =
            pagoMensual.ToString("F2");

        calculoRealizado = true;
    }

    // VER RESUMEN
    private async void BtnResumen_Clicked(
        object sender, EventArgs e)
    {
        // VALIDAR DATOS DEL ESTUDIANTE
        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
            string.IsNullOrWhiteSpace(txtApellido.Text) ||
            !int.TryParse(txtEdad.Text, out int edad) ||
            edad <= 0 ||
            pkPais.SelectedIndex == -1 ||
            pkCiudad.SelectedIndex == -1 ||
            !calculoRealizado)
        {
            await DisplayAlertAsync(
                "Datos incompletos",
                "Complete todos los campos y calcule el pago mensual.",
                "Aceptar");
            return;
        }

        // OBTENER DATOS PERSONALES
        string nombre = txtNombre.Text.Trim();
        string apellido = txtApellido.Text.Trim();

        int edadEstudiante = edad;

        // OBTENER FECHA
        DateTime fecha = dpFecha.Date ?? DateTime.Today;

        // OBTENER PAIS Y CIUDAD
        string pais =
            pkPais.SelectedItem!.ToString()!;

        string ciudad =
            pkCiudad.SelectedItem!.ToString()!;

        // OBTENER MONTO INICIAL
        decimal montoInicial = decimal.Parse(
            txtMontoInicial.Text!,
            NumberStyles.Number,
            CultureInfo.CurrentCulture);

        // ENVIAR DATOS A RESUMEN
        await Navigation.PushAsync(
            new Resumen(
                usuarioConectado,
                nombre,
                apellido,
                edadEstudiante,
                fecha,
                ciudad,
                pais,
                montoInicial,
                pagoMensual,
                pagoTotal
            )
        );
    }
}
