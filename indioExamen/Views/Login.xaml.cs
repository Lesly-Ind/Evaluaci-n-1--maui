
namespace indioExamen.Views;

public partial class Login : ContentPage
{
    // Matriz de usuarios y contraseñas
    string[,] usuarios =
    {
        { "estudiante", "moviles" },
        { "suda", "2025" }
    };

    public Login()
    {
        InitializeComponent();
    }

    private async void BtnIniciarSesion_Clicked(
        object sender, EventArgs e)
    {
        string usuario = txtUsuario.Text?.Trim() ?? "";
        string contrasena = txtContrasena.Text ?? "";

        bool acceso = false;

        for (int i = 0; i < usuarios.GetLength(0); i++)
        {
            if (usuario == usuarios[i, 0] &&
                contrasena == usuarios[i, 1])
            {
                acceso = true;
                break;
            }
        }

        if (acceso)
        {
            await Navigation.PushAsync(
                new Registro(usuario));
        }
        else
        {
            await DisplayAlertAsync(
                "Error",
                "Usuario o contraseña incorrectos",
                "Aceptar");
        }
    }
}
