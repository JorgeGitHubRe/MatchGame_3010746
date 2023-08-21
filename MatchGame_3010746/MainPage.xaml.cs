using System.Timers;

namespace MatchGame_3010746
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            //Cuando se ejecute el programa se inicializara lo siguiente:
            InitializeComponent();

            Grid1.IsVisible = false;
            Grid1.IsEnabled = false;
            SetUpGame();
            lblTem.IsVisible = false;
            lblCom.IsVisible = false;
            btnreinicar.IsVisible = false;
        }

        //Metodo que asigna aleateriamente los emojis
        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
        {
            "🐺","🐺",
            "🦓","🦓",
            "🐗","🐗",
            "🦆","🦆",
            "🦧","🦧",
            "🙀","🙀",
            "🕷","🕷",
            "😺","😺",
        };
            
            Random random = new Random();

            //A cada boton del grid se le coloca un emoji aleatorio
            foreach (Button view in Grid1.Children)
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                view.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

       
        Button ultimoButtonClicked;
        bool encontradoMatch = false;
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (encontradoMatch == false)
            {
                button.IsVisible = false;
                ultimoButtonClicked = button;
                encontradoMatch = true;
            }
            else if (button.Text == ultimoButtonClicked.Text)
            {
                button.IsVisible = false;
                encontradoMatch = false;
            }
            else
            {
                ultimoButtonClicked.IsVisible = true;
                encontradoMatch = false;
            }
            if (!EmojisVisibles())
            {
                tiempoJuegoCompletado();
                btnreinicar.IsVisible = true;
            }
        }

        //esta función determina si hay al menos un botón visible en la cuadrícula
        private bool EmojisVisibles()
        {
            foreach (Button view in Grid1.Children)
            {
                if (view.IsVisible)
                {
                    return true;
                }
            }
            return false;
        }

        //Este fragmento de código define algunas variables y una función en el contexto de un juego o una aplicación
        private System.Timers.Timer temJuego;
        private int segTranscurridos;
        private void iniciarTem()
        {
            //inicia que mostrara el tiempo que nos tardamos
            temJuego = new System.Timers.Timer(1000);
            temJuego.Elapsed += contarTiempo;
            temJuego.AutoReset = true;
            temJuego.Enabled = true;
        }


        private void contarTiempo(object sender, ElapsedEventArgs e)
        {
            //Este fragmento de código describe un manejador de eventos que se ejecuta cuando el temporizador temJuego activa un evento de intervalo
            if (EmojisVisibles())
            {
                segTranscurridos++;
                Device.BeginInvokeOnMainThread(() =>
                {
                    VerTiempo();

                });

            }
            else
            {
                temJuego.Stop();
                tiempoJuegoCompletado();

            }
        }

        //Mettodo que muestra cuanto se tardo en completar el juego
        private void tiempoJuegoCompletado()
        {
            //Muestra el tiempo de cuanto se tardo
            Device.BeginInvokeOnMainThread(() =>
            {
                lblCom.Text = $"Game Completed in: {segTranscurridos} s";
                lblCom.IsVisible = true;
            });
        }

        //Metodo para mostrar el temporizador 
        private void VerTiempo()
        {
            lblTem.Text = $"Timer: {segTranscurridos} s";
        }

        //Se ejecutara al presionar el boton iniciar 
        private void btniniciar_Clicked(object sender, EventArgs e)
        {
            //al presionar el boton se inicia el temporizado, se hacen visibles el grid,
            //el label y los botones con emojis tambien el mismo boton
            btniniciar.IsVisible = false;
            iniciarTem();
            btniniciar.Text = "Start Game";
            Grid1.IsEnabled = true;
            Grid1.IsVisible = true;
            lblTem.IsVisible = true;

            foreach (Button view in Grid1.Children)
            {
                view.IsVisible = true;
            }
        }

        //se ejecuta el presionar el boton reiniciar
        private void btnreinicar_Clicked(object sender, EventArgs e)
        {
            //Detiene el temporizador, restablece los segudnos a cero, oculta los label y el mismo boton y se muestra de nuevo el boton iniciar
            //para que el judador empieze otra vez
            temJuego.Stop();
            segTranscurridos = 0;
            lblTem.IsVisible = false;
            lblTem.Text = "Timer: 0 s";
            lblCom.IsVisible = false;
            encontradoMatch = false;
            btniniciar.IsVisible = true;
            btniniciar.Text = "Start Game";

            foreach (Button view in Grid1.Children)
            {
                view.IsVisible = false;
            }


            btnreinicar.IsVisible = false;
        }
    }
}