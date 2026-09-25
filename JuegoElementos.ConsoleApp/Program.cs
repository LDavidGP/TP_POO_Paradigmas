using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Renderers;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Clear();

var cardRenderer = new CardRenderer(BoxStyle.Default, totalWidth: 22, healthBarUnits: 10);

// 1. Instancias de prueba con distintos tipos y porcentajes de vida
var waterCard = new Element(new WaterType());
waterCard.TakeDamage(20);
var fireCard = new Element(new FireType());
fireCard.TakeDamage(55);
var earthCard = new Element(new EarthType());
earthCard.TakeDamage(85);

Console.WriteLine("================================================================================");
Console.WriteLine("                   PRUEBA DE COMPONENTES VISUALES (ARENA)                       ");
Console.WriteLine("================================================================================\n");

// 2. Renderizado de dos cartas enfrentadas (Simulación de ShowBattlefield)
Console.WriteLine("   [ JUGADOR HUMANO ]                             [ IA OPONENTE ]");

var humanLines = cardRenderer.Render(waterCard);
var aiLines = cardRenderer.Render(fireCard);

for (var i = 0; i < humanLines.Count; i++)
{
    // En la fila central colocamos el 'VS'
    var separator = (i == 2) ? "       VS       " : "                ";
    Console.WriteLine($"   {humanLines[i]}{separator}{aiLines[i]}");
}

Console.WriteLine("   Mazo vivo: 5/5                                 Mazo vivo: 4/5\n");

// 3. Prueba de tarjeta con estado crítico (Barra roja y atenuación)
Console.WriteLine("--------------------------------------------------------------------------------");
Console.WriteLine("   Prueba de estado crítico (< 20% de vida):");
var earthLines = cardRenderer.Render(earthCard);
foreach (var line in earthLines)
{
    Console.WriteLine($"   {line}");
}
Console.WriteLine("--------------------------------------------------------------------------------\n");

Console.WriteLine("Presiona cualquier tecla para salir...");
Console.ReadKey(true);