
using System;
using System.Collections.Generic;
using Spectre.Console;

public class Juegos
{
    private static Personaje personajeJugador1Seleccionado;
    private static Personaje personajeJugador2Seleccionado;
    private static string dificultad = "Fácil";

    // Controles Jugador 1
    const ConsoleKey J1Arriba = ConsoleKey.W;
    const ConsoleKey J1Abajo = ConsoleKey.S;
    const ConsoleKey J1Izquierda = ConsoleKey.A;
    const ConsoleKey J1Derecha = ConsoleKey.D;
    const ConsoleKey J1Atacar = ConsoleKey.F;
    const ConsoleKey J1Habilidad = ConsoleKey.G;

    // Controles Jugador 2
    const ConsoleKey J2Arriba = ConsoleKey.UpArrow;
    const ConsoleKey J2Abajo = ConsoleKey.DownArrow;
    const ConsoleKey J2Izquierda = ConsoleKey.LeftArrow;
    const ConsoleKey J2Derecha = ConsoleKey.RightArrow;
    const ConsoleKey J2Atacar = ConsoleKey.M;
    const ConsoleKey J2Habilidad = ConsoleKey.N;
    static void Main()
    {
        MostrarPantallaTitulo();
        EjecutarBucleJuego();
    }

    private static void MostrarPantallaTitulo()
    {
        AnsiConsole.Write(
            new FigletText("MAZE RUNNERS")
            .Centered()
            .Color(Color.Red));
        Thread.Sleep(5000);
        Console.Clear();
    }

    private static void EjecutarBucleJuego()
    {
       Console.Clear();
        while (true)
        {
            string opcion = MostrarMenu();
            ProcesarOpcionMenu(opcion);
        }
    }

    private static void ProcesarOpcionMenu(string opcion)
    {
        switch (opcion)
        {
            case "1":
                IniciarJuego();
                break;
            case "2":
                SeleccionarPersonajes();
                break;
            case "3":
                SeleccionarDificultad();
                break;
            case "4":
                if (ConfirmarSalida())
                {
                    Environment.Exit(0);
                }
                break;
            default:
                AnsiConsole.MarkupLine("[blue]Opción no válida. Por favor seleccione de nuevo.[/]");
                break;
        }
    }

    private static void IniciarJuego()
    {
        if (personajeJugador1Seleccionado != null && personajeJugador2Seleccionado != null)
        {
            AnsiConsole.MarkupLine($"[blue]Comenzando el juego con {personajeJugador1Seleccionado.Nombre} y {personajeJugador2Seleccionado.Nombre} en dificultad {dificultad}.[/]");
            Jugar();
        }
        else
        {
            AnsiConsole.MarkupLine("[blue]Por favor selecciona personajes para ambos jugadores antes de jugar.[/]");
        }
    }

    static string MostrarMenu()
    {
        var opciones = new[] { "Jugar", "Seleccionar Personajes", "Seleccionar Dificultad", "Salir" };
        int seleccion = 0;

        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold cyan]=== MENÚ INICIAL ===[/]");
            MostrarOpcionesMenu(opciones, seleccion);

            var key = Console.ReadKey(intercept: true).Key;
            seleccion = ActualizarSeleccion(key, seleccion, opciones.Length);

            if (key == ConsoleKey.Enter)
            {
                return (seleccion + 1).ToString();
            }
        }
    }

    private static void MostrarOpcionesMenu(string[] opciones, int seleccion)
    {
        for (int i = 0; i < opciones.Length; i++)
        {
            if (i == seleccion)
            {
                AnsiConsole.MarkupLine($"[red]< {opciones[i]} >[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[blue]{opciones[i]}[/]");
            }
        }
    }

    private static int ActualizarSeleccion(ConsoleKey key, int seleccion, int maxOpciones)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                return (seleccion > 0) ? seleccion - 1 : maxOpciones - 1;
            case ConsoleKey.DownArrow:
                return (seleccion < maxOpciones - 1) ? seleccion + 1 : 0;
            default:
                return seleccion;
        }
    }

    private static bool ConfirmarSalida()
    {
        while (true)
        {
            AnsiConsole.Markup("[blue]¿Está seguro que desea salir? (s/n): [/]");
            string respuesta = Console.ReadLine().ToLower();

            if (respuesta == "s") return true;
            if (respuesta == "n") return false;
            
            AnsiConsole.MarkupLine("[red]Entrada no válida. Por favor ingrese 's' o 'n'.[/]");
        }
    }

    static void SeleccionarPersonajes()
    {
        var personajes = new List<Personaje>
        {
            new Personaje("Guerrero", "🗡️", 30, 1000, "Corte Poderoso", 4, 0, 0, 0, 0),
            new Personaje("Mago", "🔮", 25, 800, "Rayo Mágico", 3, 0, 0, 0, 0),
            new Personaje("Arquero", "🏹", 20, 900, "Disparo Rápido", 2, 0, 0, 0, 0),
            new Personaje("Asesino", "🥷", 35, 700, "Ataque Sorpresa", 5, 0, 0, 0, 0),
            new Personaje("Sanador", "💖", 15, 1000, "Curación Rápida", 6, 0, 0, 0, 0)
        };
        Console.Clear();
        personajeJugador1Seleccionado = SeleccionarPersonajeParaJugador(1, personajes);
        personajeJugador2Seleccionado = SeleccionarPersonajeParaJugador(2, personajes);

        while (personajeJugador1Seleccionado == personajeJugador2Seleccionado)
        {
            AnsiConsole.MarkupLine("[red]Los jugadores no pueden seleccionar el mismo personaje. Jugador 2, selecciona otro personaje.[/]");
            Console.ReadLine();
            Console.Clear();
            personajeJugador2Seleccionado = SeleccionarPersonajeParaJugador(2, personajes);
        }

        AnsiConsole.MarkupLine("[blue]Pulse cualquier tecla para continuar...[/]");
        Console.ReadKey();
    }

    static Personaje SeleccionarPersonajeParaJugador(int numeroJugador, List<Personaje> personajes)
    {
        int seleccion = 0;

        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[bold cyan]=== SELECCIONAR PERSONAJE PARA JUGADOR {numeroJugador} ===[/]");

            for (int i = 0; i < personajes.Count; i++)
            {
                string formato = (i == seleccion) ? "[blue]< {0}: {1} >[/]" : "[red]{0}: {1}[/]";
                AnsiConsole.MarkupLine(formato, personajes[i].Nombre, personajes[i].Descripcion);
            }

            var key = Console.ReadKey(intercept: true).Key;
            seleccion = ActualizarSeleccion(key, seleccion, personajes.Count);

            if (key == ConsoleKey.Enter)
            {
                return personajes[seleccion];
            }
        }
    }
    static void SeleccionarDificultad()
    {
        int seleccion = 0;
        string[] dificultades = { "Fácil", "Difícil" };

        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold cyan]=== SELECCIONAR DIFICULTAD ===[/]");

            for (int i = 0; i < dificultades.Length; i++)
            {
                if (i == seleccion)
                {
                    AnsiConsole.MarkupLine($"[red]< {dificultades[i]} >[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[blue]{dificultades[i]}[/]"); 
                }
            }

            var key = Console.ReadKey(intercept: true).Key;
            
            if (key == ConsoleKey.UpArrow)
            {
                seleccion = (seleccion > 0) ? seleccion - 1 : dificultades.Length - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                seleccion = (seleccion < dificultades.Length - 1) ? seleccion + 1 : 0;
            }
            else if (key == ConsoleKey.Enter)
            {
                dificultad = dificultades[seleccion];
                AnsiConsole.MarkupLine($"[red]Dificultad seleccionada: {dificultad}.[/]");
                break;
            }
        }

        AnsiConsole.MarkupLine("[blue]Pulse cualquier tecla para continuar...[/]");
        Console.ReadKey();
    }
      static void Jugar()
{
    var laberinto = new Laberinto(31, 31);
    var juego = new Laberinto.Juego();
    juego.jugador1 = personajeJugador1Seleccionado;
    juego.jugador2 = personajeJugador2Seleccionado;
    juego.jugador1.PosX = 1;
    juego.jugador1.PosY = 1;
    juego.jugador2.PosX = 29;
    juego.jugador2.PosY = 29;
    
    if (dificultad == "Fácil")
    {
        juego.Iniciar(); // Remove invalid JuegoFacil call
    }
    else
    {
        juego.ConfigurarModoDificil();
        juego.Iniciar();
    }
}
    public class Personaje
    {
        public string Nombre { get; set; }
        public string Emoji { get; set; }
        public int Danio { get; set; }
        public int Vidas { get; set; }
        public string Descripcion { get; set; }
        public string HabilidadEspecial { get; set; }
        public int TiempoEnfriamiento { get; set; }
        public int TiempoEnfriamientoOriginal { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int TurnosAdicionales { get; set; }
        public int DañosReducidosPorTurnos { get; set; }
        public int TurnosPerdidos { get; set; }
        public int dañoInicial { get; set; }

        public Personaje(string nombre, string emoji, int danio, int vidas, string habilidadEspecial, 
                        int tiempoEnfriamiento, int dañoInicial, int inicioX, int inicioY, int TurnosPerdidos)
        {
            Nombre = nombre;
            Emoji = emoji;
            Danio = danio;
            Vidas = vidas;
            HabilidadEspecial = habilidadEspecial;
            TiempoEnfriamientoOriginal = tiempoEnfriamiento;
            Descripcion = $"Daño: {danio}, Vidas: {vidas}, Habilidad: {habilidadEspecial}, Tiempo de Enfriamiento: {tiempoEnfriamiento} turnos";
            PosX = inicioX;
            PosY = inicioY;
            this.TurnosPerdidos = TurnosPerdidos;
            DañosReducidosPorTurnos = 0;
        }
        static void Jugar()
        {
           var laberinto = new Laberinto(31, 31);
           var juego = new Laberinto.Juego();
           juego.jugador1 = personajeJugador1Seleccionado;
           juego.jugador2 = personajeJugador2Seleccionado;
           juego.jugador1.PosX = 1;
           juego.jugador1.PosY = 1;
           juego.jugador2.PosX = 29;
           juego.jugador2.PosY = 29;
           if (dificultad == "Fácil")
 {
            juego.Iniciar();
 }
            else
  {
            juego.ConfigurarModoDificil();
            juego.Iniciar();
  }
        }
        public void ReducirEfectos()
        {
            if (TurnosPerdidos > 0)
                TurnosPerdidos--;

            if (DañosReducidosPorTurnos > 0)
            {
                DañosReducidosPorTurnos--;
                if (DañosReducidosPorTurnos == 0)
                {
                    dañoInicial += 20;
                    AnsiConsole.MarkupLine($"[green]{Nombre} ha recuperado su daño original. Daño actual: {dañoInicial}.[/]");
                }
            }
        }
    }

    public class Laberinto
    {
    public int GetAncho() => ancho;
    public int GetAlto() => alto;
    public int GetValorCelda(int x, int y) => mapa[y, x];
    public void SetValorCelda(int x, int y, int valor) => mapa[y, x] = valor;
    public void AumentarTrampas() => ColocarTrampas(5); // Aumenta el número de trampas en modo difícil       
        private int ancho, alto;
        private int[,] mapa;
        private Random random = new Random();
        private void Shuffle<T>(List<T> list)
{
    int n = list.Count;
    while (n > 1)
    {
        n--;
        int k = random.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
    }
}
        public Laberinto(int ancho, int alto)
        {
            if (ancho % 2 == 0 || alto % 2 == 0 || ancho != alto)
            {
                throw new ArgumentException("El ancho y el alto del laberinto deben ser números impares e iguales.");
            }

            this.ancho = ancho;
            this.alto = alto;
            mapa = new int[alto, ancho];
            InicializarMapa();
            GenerarLaberinto();
            ColocarTrampas(3);
        }

        public bool EsMovimientoValido(int x, int y)
        {
            if (x < 0 || x >= ancho || y < 0 || y >= alto)
                return false;
            return mapa[y, x] == 1;
        }

        private void InicializarMapa()
        {
            for (int y = 0; y < alto; y++)
                for (int x = 0; x < ancho; x++)
                    mapa[y, x] = 0;
        }

        public void GenerarLaberinto()
        {
            Backtrack(1, 1);
            EstablecerPuntosImportantes();
            CrearCaminosAdicionales();
            AjustarCaminos();
        }
           private void CrearCaminoAdicional(int jugador)
    {
        int maxIntentos = 100;
        int intentosActuales = 0;
        bool caminoExitoso = false;

        while (!caminoExitoso && intentosActuales < maxIntentos)
        {
            int inicioX = jugador == 1 ? 1 : ancho/2;
            int finX = jugador == 1 ? ancho/2 : ancho-1;
            int inicioY = jugador == 1 ? 1 : alto/2;
            int finY = jugador == 1 ? alto/2 : alto-1;

            int x = random.Next(inicioX, finX);
            int y = random.Next(inicioY, finY);

            if (mapa[y, x] == 0)
            {
                bool tieneConexion = false;
                
                if (y > 1 && mapa[y-1, x] == 1) tieneConexion = true;
                if (y < alto-2 && mapa[y+1, x] == 1) tieneConexion = true;
                if (x > 1 && mapa[y, x-1] == 1) tieneConexion = true;
                if (x < ancho-2 && mapa[y, x+1] == 1) tieneConexion = true;

                if (tieneConexion)
                {
                    mapa[y, x] = 1;
                    caminoExitoso = true;
                }
            }
            intentosActuales++;
        }
    }
        private void EstablecerPuntosImportantes()
        {
            mapa[1, 1] = 1;  // Punto inicial jugador 1
            mapa[alto - 2, ancho - 2] = 1;  // Punto inicial jugador 2
            mapa[alto/2, ancho/2] = 1;  // Meta central
        }
        private void Backtrack(int x, int y)
        {
            if (x > 0 && x < ancho - 1 && y > 0 && y < alto - 1)
                mapa[y, x] = 1;

            int[] dx = { 0, +2, 0, -2 };
            int[] dy = { -2, 0, +2, 0 };
            List<int> direcciones = new List<int> { 0, 1, 2, 3 };
            Shuffle(direcciones);

            foreach (int dir in direcciones)
            {
                int nx = x + dx[dir];
                int ny = y + dy[dir];

                if (nx > 0 && nx < ancho - 1 && ny > 0 && ny < alto - 1 && mapa[ny, nx] == 0)
                {
                    mapa[y + dy[dir] / 2, x + dx[dir] / 2] = 1;
                    Backtrack(nx, ny);
                }
            }
        }

        private void CrearCaminosAdicionales()
        {
            int caminosAdicionales = random.Next(1, 3);
            for (int i = 0; i < caminosAdicionales; i++)
            {
                int randomX = random.Next(1, ancho - 1);
                int randomY = random.Next(1, alto - 1);

                if (mapa[randomY, randomX] == 0)
                {
                    mapa[randomY, randomX] = 1;
                    
                    // Crear conexiones con caminos existentes
                    if (randomY > 1)
                        mapa[randomY - 1, randomX] = random.Next(2) == 0 ? 1 : mapa[randomY - 1, randomX];
                    if (randomY < alto - 2)
                        mapa[randomY + 1, randomX] = random.Next(2) == 0 ? 1 : mapa[randomY + 1, randomX];
                    if (randomX > 1)
                        mapa[randomY, randomX - 1] = random.Next(2) == 0 ? 1 : mapa[randomY, randomX - 1];
                    if (randomX < ancho - 2)
                        mapa[randomY, randomX + 1] = random.Next(2) == 0 ? 1 : mapa[randomY, randomX + 1];
                }
            }
        }

        private void ColocarTrampas(int cantidadPorTipo)
        {
            for (int tipo = 1; tipo <= 4; tipo++)
            {
                for (int i = 0; i < cantidadPorTipo;)
                {
                    int x = random.Next(1, ancho - 1);
                    int y = random.Next(1, alto - 1);

                    if (mapa[y, x] == 1 && 
                        !(x == 1 && y == 1) && 
                        !(x == ancho-2 && y == alto-2) && 
                        !(x == ancho/2 && y == alto/2))
                    {
                        mapa[y, x] = tipo + 10; // Usar valores >10 para trampas
                        i++;
                    }
                }
            }
        }
        public void ImprimirLaberinto()
{
    var juego = new Laberinto.Juego();
    juego.jugador1 = personajeJugador1Seleccionado;
    juego.jugador2 = personajeJugador2Seleccionado;
    var jugadorActual = juego.jugador1; // Inicializar con jugador1
    jugadorActual.PosX = juego.jugador1.PosX; // Guardar posición X
    jugadorActual.PosY = juego.jugador1.PosY; // Guardar posición Y
    // Borde superior decorativo
    Console.WriteLine("🎮 " + new string('━', ancho * 2) + " 🎮");
    
    for (int y = 0; y < alto; y++)
    {
        Console.Write("┃ "); // Borde izquierdo decorativo
        for (int x = 0; x < ancho; x++)
        {
            // Verifica posición de jugadores
            if (jugadorActual != null && x == jugadorActual.PosX && y == jugadorActual.PosY)
            {
                string colorJugador = jugadorActual == juego.jugador1 ? "blue" : "red";
                AnsiConsole.Markup($"[{colorJugador}]{jugadorActual.Emoji}[/]");
            }
            else if (juego.jugador2 != null && x == juego.jugador2.PosX && y == juego.jugador2.PosY)
            {
                AnsiConsole.Markup($"[red]{juego.jugador2.Emoji}[/]");
            }
            else
            {
                string simbolo = ObtenerSimbolo(x, y);
                AnsiConsole.Markup(simbolo);
            }
            Console.Write(" "); // Espacio para mejor visualización
        }
        Console.WriteLine("┃"); // Borde derecho decorativo
    }
    
    // Borde inferior decorativo
    Console.WriteLine("🎮 " + new string('━', ancho * 2) + " 🎮");
}

        private string ObtenerSimbolo(int x, int y)
        {
            if (mapa[y, x] == 0) return "🔳";
            if (y == 1 && x == 1) return "🚪";
            if (y == alto - 2 && x == ancho - 2) return "🚪";
            if (y == alto / 2 && x == ancho / 2) return "🏆";
            if (mapa[y, x] > 10) return ObtenerSimboloTrampa(mapa[y, x] - 10);
            return "🟦";
        }

        private string ObtenerSimboloTrampa(int tipoTrampa)
        {
            return tipoTrampa switch
            {
                1 => "💀",  // Trampa de daño
                2 => "↩️",  // Trampa de retroceso
                3 => "⏸️",  // Trampa de turno perdido
                4 => "⚔️",  // Trampa de reducción de daño
                0 => "   "    
            };
        }

        private void AjustarCaminos()
        {
            int distanciaJugador1 = CalcularDistancia(1, 1);
            int distanciaJugador2 = CalcularDistancia(alto - 2, ancho - 2);

            while (Math.Abs(distanciaJugador1 - distanciaJugador2) > Math.Max(alto / 10, 3))
            {
                if (distanciaJugador1 > distanciaJugador2)
                {
                    CrearCaminoAdicional(1);
                    distanciaJugador1 = CalcularDistancia(1, 1);
                }
                else
                {
                    CrearCaminoAdicional(2);
                    distanciaJugador2 = CalcularDistancia(alto - 2, ancho - 2);
                }
            }
        }
        private int CalcularDistancia(int startY, int startX)
        {
            Queue<(int Y, int X, int Distancia)> queue = new();
            bool[,] visitado = new bool[alto, ancho];
            
            queue.Enqueue((startY, startX, 0));
            visitado[startY, startX] = true;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var (y, x, distancia) = queue.Dequeue();

                if (y == alto/2 && x == ancho/2) 
                    return distancia;

                foreach (int dir in Enumerable.Range(0, 4))
                {
                    int nx = x + dx[dir];
                    int ny = y + dy[dir];

                    if (nx >= 0 && nx < ancho && ny >= 0 && ny < alto && 
                        !visitado[ny, nx] && mapa[ny, nx] == 1)
                    {
                        visitado[ny, nx] = true;
                        queue.Enqueue((ny, nx, distancia + 1));
                    }
                }
            }
            return int.MaxValue;
        }

        public class Juego
        {
            public Laberinto laberinto;
            public Personaje jugador1;
            public Personaje jugador2;
            public bool turnoJugador1;
            private List<(int X, int Y)> historialPosiciones;
            private const int MAX_HISTORIAL = 3;

            public Juego()
            {
                laberinto = new Laberinto(21, 21);
                turnoJugador1 = true;
                historialPosiciones = new List<(int X, int Y)>();
            }

            public void Iniciar()
            {
                while (jugador1.Vidas > 0 && jugador2.Vidas > 0)
                {
                    Console.Clear();
                    laberinto.ImprimirLaberinto();
                    MostrarEstadoJugadores();
                    ProcesarTurno();
                }
                MostrarResultadoFinal();
            }
            private void MostrarEstadoJugadores()
            {
                AnsiConsole.MarkupLine($"""
                    [blue]Estado Actual:[/]
                    {jugador1.Nombre}: {jugador1.Emoji} 
                    Vida: {jugador1.Vidas} | Daño: {jugador1.dañoInicial} | Posición: ({jugador1.PosX}, {jugador1.PosY})
                    
                    {jugador2.Nombre}: {jugador2.Emoji}
                    Vida: {jugador2.Vidas} | Daño: {jugador2.dañoInicial} | Posición: ({jugador2.PosX}, {jugador2.PosY})
                    
                    Turno de: {(turnoJugador1 ? jugador1.Nombre : jugador2.Nombre)}
                    """);
            }

            private void ProcesarTurno()
            {
                var jugadorActual = turnoJugador1 ? jugador1 : jugador2;
                
                if (jugadorActual.TurnosPerdidos > 0)
                {
                    AnsiConsole.MarkupLine($"[red]{jugadorActual.Nombre} pierde este turno.[/]");
                    jugadorActual.TurnosPerdidos--;
                    turnoJugador1 = !turnoJugador1;
                    return;
                }

                MostrarOpcionesJugador();
                var accion = Console.ReadKey(true).Key;
                
                switch (accion)
                {
                    case ConsoleKey.D1:
                        RealizarMovimiento(jugadorActual);
                        break;
                    case ConsoleKey.D2:
                        RealizarAtaque(jugadorActual);
                        break;
                    case ConsoleKey.D3:
                        UsarHabilidadEspecial(jugadorActual);
                        break;
                }

                VerificarTrampas(jugadorActual);
                ActualizarHistorialPosiciones(jugadorActual);
                turnoJugador1 = !turnoJugador1;
            }
            private void RealizarMovimiento(Personaje jugador)
            {
                bool movimientoRealizado = false;
                while (!movimientoRealizado)
                {
                    AnsiConsole.MarkupLine($"[blue]{jugador.Nombre}, usa las teclas de movimiento:[/]");
                    AnsiConsole.MarkupLine(jugador == jugador1 ? 
                        "[green]W/A/S/D para moverte[/]" : 
                        "[green]↑/←/↓/→ para moverte[/]");

                    var tecla = Console.ReadKey(true).Key;
                    (int dx, int dy) = ObtenerDireccionMovimiento(tecla, jugador);
                    
                    int nuevaPosX = jugador.PosX + dx;
                    int nuevaPosY = jugador.PosY + dy;

                    if (laberinto.EsMovimientoValido(nuevaPosX, nuevaPosY))
                    {
                        jugador.PosX = nuevaPosX;
                        jugador.PosY = nuevaPosY;
                        movimientoRealizado = true;
                        AnsiConsole.MarkupLine($"[green]{jugador.Nombre} se mueve a ({nuevaPosX}, {nuevaPosY})[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]¡Movimiento inválido! Intenta de nuevo.[/]");
                    }
                }
            }

            private (int dx, int dy) ObtenerDireccionMovimiento(ConsoleKey tecla, Personaje jugador)
            {
                if (jugador == jugador1)
                {
                    return tecla switch
                    {
                        ConsoleKey.W => (0, 1),
                        ConsoleKey.S => (0, -1),
                        ConsoleKey.A => (-1, 0),
                        ConsoleKey.D => (1, 0),
                        _ => (0, 0)
                    };
                }
                else
                {
                    return tecla switch
                    {
                        ConsoleKey.UpArrow => (0, 1),
                        ConsoleKey.DownArrow => (0, -1),
                        ConsoleKey.LeftArrow => (-1, 0),
                        ConsoleKey.RightArrow => (1, 0),
                        _ => (0, 0)
                    };
                }
            }
            private void RealizarAtaque(Personaje atacante)
            {
                var defensor = atacante == jugador1 ? jugador2 : jugador1;
                
                if (EstanEnRangoDeAtaque(atacante, defensor))
                {
                    defensor.Vidas -= atacante.dañoInicial;
                    AnsiConsole.MarkupLine($"""
                        [red]{atacante.Emoji} {atacante.Nombre} ataca a {defensor.Nombre}!
                        Daño causado: {atacante.dañoInicial}
                        Vida restante de {defensor.Nombre}: {defensor.Vidas}[/]
                        """);
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]¡Objetivo fuera de rango de ataque![/]");
                }
            }

            private void UsarHabilidadEspecial(Personaje personaje)
            {
                if (personaje.TiempoEnfriamiento > 0)
                {
                    AnsiConsole.MarkupLine($"[red]Habilidad en enfriamiento. Turnos restantes: {personaje.TiempoEnfriamiento}[/]");
                    return;
                }

                var objetivo = personaje == jugador1 ? jugador2 : jugador1;
                EjecutarHabilidadEspecial(personaje, objetivo);
                personaje.TiempoEnfriamiento = personaje.TiempoEnfriamientoOriginal;
            }

            private void EjecutarHabilidadEspecial(Personaje usuario, Personaje objetivo)
            {
                switch (usuario.Nombre)
                {
                    case "Guerrero":
                        GolpePoderosoGuerrero(usuario, objetivo);
                        break;
                    case "Mago":
                        RayoMagico(usuario, objetivo);
                        break;
                    case "Arquero":
                        DisparoRapido(usuario, objetivo);
                        break;
                    case "Asesino":
                        AtaqueSorpresa(usuario, objetivo);
                        break;
                    case "Sanador":
                        CuracionRapida(usuario);
                        break;
                }
            }
            private void GolpePoderosoGuerrero(Personaje guerrero, Personaje objetivo)
            {
                int dañoBase = guerrero.dañoInicial * 2;
                objetivo.Vidas -= dañoBase;
                AnsiConsole.MarkupLine($"""
                    [red]{guerrero.Emoji} ¡{guerrero.Nombre} ejecuta Corte Poderoso!
                    Daño crítico: {dañoBase}
                    Vida restante de {objetivo.Nombre}: {objetivo.Vidas}[/]
                    """);
            }

            private void RayoMagico(Personaje mago, Personaje objetivo)
            {
                int dañoMagico = (int)(mago.dañoInicial * 1.8);
                objetivo.Vidas -= dañoMagico;
                objetivo.DañosReducidosPorTurnos = 2;
                AnsiConsole.MarkupLine($"""
                    [blue]{mago.Emoji} ¡{mago.Nombre} lanza Rayo Mágico!
                    Daño mágico: {dañoMagico}
                    {objetivo.Nombre} sufre reducción de daño por 2 turnos
                    Vida restante de {objetivo.Nombre}: {objetivo.Vidas}[/]
                    """);
            }

            private void DisparoRapido(Personaje arquero, Personaje objetivo)
            {
                int disparos = 3;
                int dañoPorDisparo = (int)(arquero.dañoInicial * 0.6);
                int dañoTotal = dañoPorDisparo * disparos;
                
                objetivo.Vidas -= dañoTotal;
                AnsiConsole.MarkupLine($"""
                    [green]{arquero.Emoji} ¡{arquero.Nombre} realiza {disparos} Disparos Rápidos!
                    Daño por disparo: {dañoPorDisparo}
                    Daño total: {dañoTotal}
                    Vida restante de {objetivo.Nombre}: {objetivo.Vidas}[/]
                    """);
            }
            private void AtaqueSorpresa(Personaje asesino, Personaje objetivo)
            {
                int dañoCritico = (int)(asesino.dañoInicial * 2.5);
                objetivo.Vidas -= dañoCritico;
                objetivo.TurnosPerdidos += 1;
                
                AnsiConsole.MarkupLine($"""
                    [purple]{asesino.Emoji} ¡{asesino.Nombre} ejecuta un Ataque Sorpresa!
                    Daño crítico: {dañoCritico}
                    {objetivo.Nombre} pierde su próximo turno
                    Vida restante de {objetivo.Nombre}: {objetivo.Vidas}[/]
                    """);
            }

            private void CuracionRapida(Personaje sanador)
            {
                int curacion = 200;
                sanador.Vidas += curacion;
                sanador.TurnosAdicionales++;
                
                AnsiConsole.MarkupLine($"""
                    [green]{sanador.Emoji} ¡{sanador.Nombre} usa Curación Rápida!
                    Curación: +{curacion} HP
                    Gana un turno adicional
                    Vida actual: {sanador.Vidas}[/]
                    """);
            }

            private void VerificarTrampas(Personaje jugador)
            {
                int tipoTrampa = ObtenerTipoTrampa(jugador.PosX, jugador.PosY);
                if (tipoTrampa > 0)
                {
                    AplicarEfectoTrampa(tipoTrampa, jugador);
                    LimpiarTrampa(jugador.PosX, jugador.PosY);
                }
            }

            private void AplicarEfectoTrampa(int tipoTrampa, Personaje jugador)
            {
                switch (tipoTrampa)
                {
                    case 1: // Trampa de daño
                        TrampaDeDaño(jugador);
                        break;
                    case 2: // Trampa de retroceso
                        TrampaDeRetroceso(jugador);
                        break;
                    case 3: // Trampa de turno perdido
                        TrampaDeTurnoPerdido(jugador);
                        break;
                    case 4: // Trampa de reducción de daño
                        TrampaDeReduccionDeDaño(jugador);
                        break;
                }
            }
            private void TrampaDeDaño(Personaje jugador)
            {
                int dañoTrampa = 30;
                jugador.Vidas -= dañoTrampa;
                AnsiConsole.MarkupLine($"""
                    [red]💀 ¡{jugador.Nombre} activó una trampa de daño!
                    Daño recibido: {dañoTrampa}
                    Vida restante: {jugador.Vidas}[/]
                    """);
            }

            private void TrampaDeRetroceso(Personaje jugador)
            {
                if (historialPosiciones.Count >= 3)
                {
                    var posicionAnterior = historialPosiciones[^3];
                    jugador.PosX = posicionAnterior.X;
                    jugador.PosY = posicionAnterior.Y;
                    AnsiConsole.MarkupLine($"""
                        [yellow]↩️ ¡{jugador.Nombre} activó una trampa de retroceso!
                        Regresa a la posición: ({posicionAnterior.X}, {posicionAnterior.Y})[/]
                        """);
                }
            }

            private void TrampaDeTurnoPerdido(Personaje jugador)
            {
                jugador.TurnosPerdidos += 2;
                AnsiConsole.MarkupLine($"""
                    [red]⏸️ ¡{jugador.Nombre} activó una trampa de tiempo!
                    Pierde los próximos 2 turnos[/]
                    """);
            }

            private void TrampaDeReduccionDeDaño(Personaje jugador)
            {
                int reduccionDaño = 20;
                jugador.dañoInicial -= reduccionDaño;
                jugador.DañosReducidosPorTurnos = 4;
                AnsiConsole.MarkupLine($"""
                    [blue]⚔️ ¡{jugador.Nombre} activó una trampa de debilidad!
                    Daño reducido en {reduccionDaño} durante 4 turnos
                    Daño actual: {jugador.dañoInicial}[/]
                    """);
            }
            private void ActualizarHistorialPosiciones(Personaje jugador)
            {
                historialPosiciones.Add((jugador.PosX, jugador.PosY));
                if (historialPosiciones.Count > MAX_HISTORIAL)
                {
                    historialPosiciones.RemoveAt(0);
                }
            }

            private bool EstanEnRangoDeAtaque(Personaje atacante, Personaje defensor)
            {
                int distanciaX = Math.Abs(atacante.PosX - defensor.PosX);
                int distanciaY = Math.Abs(atacante.PosY - defensor.PosY);
                return distanciaX <= 2 && distanciaY <= 2;
            }

            private int ObtenerTipoTrampa(int x, int y)
            {
                if (x < 0 || x >= laberinto.GetAncho() || y < 0 || y >= laberinto.GetAlto())
                    return 0;

                int valorCelda = laberinto.GetValorCelda(x, y);
                return valorCelda > 10 ? valorCelda - 10 : 0;
            }

            private void LimpiarTrampa(int x, int y)
            {
                laberinto.SetValorCelda(x, y, 1);
            }

            public void ConfigurarModoDificil()
            {
                laberinto = new Laberinto(31, 31);
                jugador1.Vidas = (int)(jugador1.Vidas * 0.8);
                jugador2.Vidas = (int)(jugador2.Vidas * 0.8);
                
                // Aumentar cantidad de trampas
                laberinto.AumentarTrampas();
                
                AnsiConsole.MarkupLine($"""
                    [red]¡Modo Difícil Activado!
                    - Laberinto más grande
                    - Vida reducida al 80%
                    - Más trampas
                    - Mayor distancia a la meta[/]
                    """);
            }
            private void MostrarResultadoFinal()
            {
                Console.Clear();
                string ganador = jugador1.Vidas <= 0 ? jugador2.Nombre : jugador1.Nombre;
                string perdedor = jugador1.Vidas <= 0 ? jugador1.Nombre : jugador2.Nombre;

                AnsiConsole.Write(
                    new FigletText("¡FIN DEL JUEGO!")
                    .Centered()
                    .Color(Color.Yellow));

                AnsiConsole.MarkupLine($"""
                    [green]¡{ganador} es el ganador![/]

                    [blue]Estadísticas finales:[/]
                    
                    {jugador1.Nombre}:
                    Vida final: {jugador1.Vidas}
                    Daño final: {jugador1.dañoInicial}
                    Posición final: ({jugador1.PosX}, {jugador1.PosY})

                    {jugador2.Nombre}:
                    Vida final: {jugador2.Vidas}
                    Daño final: {jugador2.dañoInicial}
                    Posición final: ({jugador2.PosX}, {jugador2.PosY})
                    """);

                Console.WriteLine("\nPresione cualquier tecla para volver al menú principal...");
                Console.ReadKey();
            }

            private void MostrarOpcionesJugador()
            {
                var jugadorActual = turnoJugador1 ? jugador1 : jugador2;
                AnsiConsole.MarkupLine($"""
                    [blue]Opciones para {jugadorActual.Nombre}:[/]
                    [green]1. Mover[/]
                    [red]2. Atacar[/]
                    [yellow]3. Usar Habilidad Especial ({jugadorActual.HabilidadEspecial})[/]
                    """);
            }
            public class PowerUp
            {
                public enum TipoPowerUp
                {
                    CuracionExtra = 1,
                    PotenciadorDaño = 2,
                    TurnoAdicional = 3
                }

                private readonly Dictionary<TipoPowerUp, string> simbolosPowerUp = new()
                {
                    { TipoPowerUp.CuracionExtra, "❤️" },
                    { TipoPowerUp.PotenciadorDaño, "⚡" },
                    { TipoPowerUp.TurnoAdicional, "🕒" }
                };

                public void AplicarPowerUp(Personaje personaje, TipoPowerUp tipo)
                {
                    switch (tipo)
                    {
                        case TipoPowerUp.CuracionExtra:
                            AplicarCuracionExtra(personaje);
                            break;
                        case TipoPowerUp.PotenciadorDaño:
                            AplicarPotenciadorDaño(personaje);
                            break;
                        case TipoPowerUp.TurnoAdicional:
                            AplicarTurnoAdicional(personaje);
                            break;
                    }
                }

                private void AplicarCuracionExtra(Personaje personaje)
                {
                    int curacion = 40;
                    personaje.Vidas += curacion;
                    AnsiConsole.MarkupLine($"""
                        [green]{simbolosPowerUp[TipoPowerUp.CuracionExtra]} ¡{personaje.Nombre} obtiene curación extra!
                        Curación: +{curacion}
                        Vida actual: {personaje.Vidas}[/]
                        """);
                }

                private void AplicarPotenciadorDaño(Personaje personaje)
                {
                    int aumentoDaño = 25;
                    personaje.dañoInicial += aumentoDaño;
                    personaje.DañosReducidosPorTurnos = 4;
                    AnsiConsole.MarkupLine($"""
                        [yellow]{simbolosPowerUp[TipoPowerUp.PotenciadorDaño]} ¡{personaje.Nombre} obtiene potenciador de daño!
                        Aumento: +{aumentoDaño}
                        Duración: 4 turnos
                        Daño actual: {personaje.dañoInicial}[/]
                        """);
                }
private void AplicarTurnoAdicional(Personaje personaje)
                {
                    personaje.TurnosAdicionales++;
                    AnsiConsole.MarkupLine($"""
                        [blue]{simbolosPowerUp[TipoPowerUp.TurnoAdicional]} ¡{personaje.Nombre} obtiene un turno adicional![/]
                        """);
                }
            }
        }
    }
}