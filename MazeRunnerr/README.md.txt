El proyecto "Maze Runners" consiste en la creación de un juego de laberinto utilizando el lenguaje de programación C# y Visual Studio Code (VSC). Este informe detalla el proceso de desarrollo, los algoritmos empleados y las características del juego.
Creación del Laberinto
Para generar el laberinto, se implementó un algoritmo recursivo de backtracking. Este método permite explorar todas las posibles rutas en el laberinto, retrocediendo cuando se encuentra un callejón sin salida. La lógica detrás del algoritmo implica:
Selección de una celda inicial: Se escoge una celda aleatoria como punto de partida.
Exploración de vecinos: Se selecciona un vecino aleatorio que no haya sido visitado.
Remoción de paredes: Se eliminan las paredes entre la celda actual y el vecino seleccionado.
Marcado como visitado: La celda se marca como visitada y se añade al stack para facilitar el backtracking.
Repetición del proceso: Se repite hasta que todas las celdas han sido visitadas.
Representación Visual
Para mejorar la experiencia visual del juego, se utilizaron emojis para representar diferentes elementos en el código, como paredes, caminos y personajes. Esto no solo hace que el código sea más atractivo, sino que también facilita la comprensión de la estructura del laberinto.
Estructura del Código
El juego fue diseñado con una estructura modular que incluye diversas clases y métodos, tales como:
PowerUps: Elementos que otorgan habilidades especiales al jugador.
Habilidades: Funciones que permiten al jugador realizar acciones únicas.
Trampas: Obstáculos que dificultan el avance del jugador.
Personajes: Clases que representan a los jugadores y sus interacciones dentro del laberinto.
Interfaz y Experiencia del Usuario
Se implementó un ciclo while para manejar la lógica del juego y permitir que los jugadores realicen movimientos hasta alcanzar la meta. Además, se utilizó la biblioteca Spectre.Console para personalizar el menú, mejorando su apariencia visual y haciéndolo más interactivo.
Instrucciones para Jugar
Una vez iniciado el juego, los jugadores cuentan con instrucciones claras sobre cómo jugarlo. El juego se puede ejecutar directamente desde la terminal de PowerShell utilizando Spectre.Console, lo que permite una experiencia fluida y accesible.