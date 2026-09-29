using System.Drawing;
using JuegoElementos.ConsoleApp.Enums;
using JuegoElementos.ConsoleApp.Input;
using JuegoElementos.ConsoleApp.Models;
using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.ElementsTypes;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.ConsoleApp.Screens;

public static class MenuScreens
{
    private static int GetTerminalWidth() =>
        Console.IsOutputRedirected ? 80 : Math.Max(Console.WindowWidth, 80);

    public static MainMenuOption ShowMainMenu(GameSettings settings)
    {
        Console.Clear();
        var width = Math.Min(GetTerminalWidth(), 74);
        var box = BoxStyle.Default;

        Console.WriteLine();

        // 1. Main Title
        var titleStyle = new Style(Color.Goldenrod, decoration: Decoration.Bold);
        var titleText = $"{Ansi.GetStyleSequence(titleStyle)}⚔️  JUEGO DE ELEMENTOS (TP POO)  ⚔️{Ansi.Reset}";
        Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
        Console.WriteLine(BoxHelper.CreateLine(titleText, width, box, center: true));
        Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

        Console.WriteLine();

        // 2. Current game configuration card
        Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
        Console.WriteLine(BoxHelper.CreateLine("  📊 ESTADO Y CONFIGURACIÓN ACTUAL:", width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateLine($"   • Duelista Activo : {settings.PlayerName}", width, box));
        Console.WriteLine(BoxHelper.CreateLine($"   • Oponente (IA)   : {settings.AiConfigName}", width, box));
        Console.WriteLine(BoxHelper.CreateLine($"   • Reglas de Daño  : {settings.MatrixPresetName}", width, box));
        Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

        Console.WriteLine();

        // 3. Options Menu
        Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
        Console.WriteLine(BoxHelper.CreateLine("  🎯 MENÚ PRINCIPAL", width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [1] ⚔️  Iniciar Partida", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [2] 🤖  Elegir Oponente (Inteligencia Artificial)", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [3] ⚙️  Configurar Matriz de Daño y Efectividades", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [4] 📜  Ver Tabla de Efectividades Actual", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [5] 👤  Cambiar Nombre de Jugador", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [6] 🚪  Salir del Juego", width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

        Console.WriteLine();
        Console.Write("  ➤ Selecciona una opción (1-6): ");
        var selection = ConsoleInputReader.ReadOption(1, 6);

        return (MainMenuOption)selection;
    }

    public static void ConfigureAiStrategy(GameSettings settings)
    {
        Console.Clear();
        var width = Math.Min(GetTerminalWidth(), 74);
        var box = BoxStyle.Default;

        Console.WriteLine();
        Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
        Console.WriteLine(BoxHelper.CreateLine("  🤖 CONFIGURACIÓN DEL OPONENTE (IA)", width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [1] 🎲 IA Aleatoria (Selección al azar sin táctica)", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [2] 🧠 IA Estratégica (Evalúa ventajas elementales)", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [3] ⚡ Super IA (Maximiza efectividad y busca K.O.)", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [4] ❓ Sorpresa (Elige una IA al azar en cada partida)", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   [5] ↩️  Volver al Menú Principal", width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

        Console.WriteLine();
        Console.Write("  ➤ Elige el tipo de oponente (1-5): ");
        var option = ConsoleInputReader.ReadOption(1, 5);

        switch (option)
        {
            case 1:
                settings.SetAiStrategy("IA Aleatoria", () => new RandomSelectionStrategy());
                ShowToast("Oponente configurado: IA Aleatoria");
                break;
            case 2:
                settings.SetAiStrategy("IA Estratégica", () => new StrategicSelectionStrategy());
                ShowToast("Oponente configurado: IA Estratégica");
                break;
            case 3:
                settings.SetAiStrategy("Super IA", () => new SuperSelectionStrategy());
                ShowToast("Oponente configurado: Super IA");
                break;
            case 4:
                settings.SetAiStrategy("Sorpresa (Al azar)", () =>
                {
                    var strategies = new ISelectionStrategy[]
                    {
                        new RandomSelectionStrategy(),
                        new StrategicSelectionStrategy(),
                        new SuperSelectionStrategy()
                    };
                    return strategies[Random.Shared.Next(strategies.Length)];
                });
                ShowToast("Oponente configurado: Sorpresa (Al azar)");
                break;
            case 5:
                break;
        }
    }

    public static void ConfigureDamageMatrix(GameSettings settings)
    {
        var stayingInMatrixMenu = true;
        while (stayingInMatrixMenu)
        {
            Console.Clear();
            var width = Math.Min(GetTerminalWidth(), 74);
            var box = BoxStyle.Default;

            Console.WriteLine();
            Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
            Console.WriteLine(BoxHelper.CreateLine("  ⚙️  CONFIGURACIÓN DE REGLAS DE DAÑO", width, box));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
            Console.WriteLine(BoxHelper.CreateLine($"   Configuración activa: {settings.MatrixPresetName}", width, box));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
            Console.WriteLine(BoxHelper.CreateLine("   [1] 📋 Estándar (Reglas oficiales del TP)", width, box));
            Console.WriteLine(BoxHelper.CreateLine("   [2] 💀 Muerte Súbita (Daño duplicado: partidas rápidas y letales)", width, box));
            Console.WriteLine(BoxHelper.CreateLine("   [3] ⚖️ Táctica Extrema (Ventajas muy marcadas de 30 pts)", width, box));
            Console.WriteLine(BoxHelper.CreateLine("   [4] ✏️ Personalizar un valor de daño específico", width, box));
            Console.WriteLine(BoxHelper.CreateLine("   [5] ↩️ Volver al Menú Principal", width, box));
            Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
            Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

            Console.WriteLine();
            Console.Write("  ➤ Selecciona una opción (1-5): ");
            var option = ConsoleInputReader.ReadOption(1, 5);

            switch (option)
            {
                case 1:
                    settings.SetDamageMatrix("Estándar (Reglas del TP)", DamageMatrixPresets.GetDefault());
                    ShowToast("Matriz restablecida a valores Estándar.");
                    break;
                case 2:
                    settings.SetDamageMatrix("Muerte Súbita (x2)", DamageMatrixPresets.GetSuddenDeath());
                    ShowToast("Matriz configurada en modo Muerte Súbita.");
                    break;
                case 3:
                    settings.SetDamageMatrix("Táctica Extrema", DamageMatrixPresets.GetTactical());
                    ShowToast("Matriz configurada en modo Táctica Extrema.");
                    break;
                case 4:
                    CustomizeSingleInteraction(settings);
                    break;
                case 5:
                    stayingInMatrixMenu = false;
                    break;
            }
        }
    }

    private static void CustomizeSingleInteraction(GameSettings settings)
    {
        Console.Clear();
        var width = Math.Min(GetTerminalWidth(), 74);
        var box = BoxStyle.Default;

        Console.WriteLine();
        Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
        Console.WriteLine(BoxHelper.CreateLine("  ✏️  PERSONALIZAR INTERACCIÓN DE DAÑO", width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateLine("   Elementos disponibles: [1] 💧 Agua   [2] 🌱 Tierra   [3] 🔥 Fuego", width, box));
        Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

        Console.WriteLine();
        Console.Write("  ➤ Elige el elemento ATACANTE (1-3): ");
        var atkChoice = ConsoleInputReader.ReadOption(1, 3);
        IElementType attacker = atkChoice switch
        {
            1 => new WaterType(),
            2 => new EarthType(),
            _ => new FireType()
        };

        Console.Write("  ➤ Elige el elemento DEFENSOR (1-3): ");
        var defChoice = ConsoleInputReader.ReadOption(1, 3);
        IElementType defender = defChoice switch
        {
            1 => new WaterType(),
            2 => new EarthType(),
            _ => new FireType()
        };

        var currentDamage = settings.DamageMatrix.GetValueOrDefault((attacker, defender), 10);
        Console.WriteLine($"\n   Daño actual de {attacker.Name} contra {defender.Name}: {currentDamage} pts.");
        Console.Write("  ➤ Ingresa el NUEVO valor de daño (1-100 pts): ");
        var newDamage = ConsoleInputReader.ReadOption(1, 100);

        settings.DamageMatrix[(attacker, defender)] = newDamage;
        settings.SetDamageMatrix("Personalizada", settings.DamageMatrix);

        ShowToast($"¡Actualizado! {attacker.Name} ahora causa {newDamage} pts a {defender.Name}.");
    }

    public static void ShowRulesAndMatrix(GameSettings settings)
    {
        Console.Clear();
        var width = Math.Min(GetTerminalWidth(), 74);
        var box = BoxStyle.Default;

        Console.WriteLine();
        Console.WriteLine(BoxHelper.CreateTopBorder(width, box));
        Console.WriteLine(BoxHelper.CreateLine("  📜 REGLAS Y TABLA DE EFECTIVIDADES", width, box));
        Console.WriteLine(BoxHelper.CreateLine(string.Empty, width, box));
        Console.WriteLine(BoxHelper.CreateLine("   • Cada jugador inicia con 5 elementos al 100% de vida.", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   • En cada ronda, ambos seleccionan un elemento para combatir.", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   • Si un elemento cae abatido, debe ser reemplazado por la reserva.", width, box));
        Console.WriteLine(BoxHelper.CreateLine("   • Gana quien logre dejar al rival sin unidades activas.", width, box));
        Console.WriteLine(BoxHelper.CreateBottomBorder(width, box));

        Console.WriteLine();

        // Tabla visual de daño
        var elements = new IElementType[] { new WaterType(), new EarthType(), new FireType() };

        Console.WriteLine($"  {Ansi.GetStyleSequence(new Style(Color.Goldenrod, decoration: Decoration.Bold))}TABLA DE DAÑO CONFIGURADA ({settings.MatrixPresetName}):{Ansi.Reset}\n");
        Console.WriteLine("  ┌───────────────┬─────────────┬─────────────┬─────────────┐");
        Console.WriteLine("  │ Atac. \\ Def.  │   💧 AGUA   │  🌱 TIERRA  │   🔥 FUEGO   │");
        Console.WriteLine("  ├───────────────┼─────────────┼─────────────┼─────────────┤");

        foreach (var atk in elements)
        {
            var (icon, style) = ElementVisuals.GetVisuals(atk);
            var atkBadge = $"{Ansi.GetStyleSequence(style)}{icon} {atk.Name,-8}{Ansi.Reset}";

            var dWater = settings.DamageMatrix.GetValueOrDefault((atk, new WaterType()), 10);
            var dEarth = settings.DamageMatrix.GetValueOrDefault((atk, new EarthType()), 10);
            var dFire  = settings.DamageMatrix.GetValueOrDefault((atk, new FireType()), 10);

            Console.WriteLine($"  │ {atkBadge} │   {dWater,2} pts   │   {dEarth,2} pts   │   {dFire,2} pts   │");
        }

        Console.WriteLine("  └───────────────┴─────────────┴─────────────┴─────────────┘");

        Console.WriteLine("\n  Presiona cualquier tecla para regresar al menú...");
        if (!Console.IsInputRedirected)
        {
            Console.ReadKey(intercept: true);
        }
    }

    private static void ShowToast(string message)
    {
        Console.WriteLine();
        var style = new Style(Color.MediumSeaGreen, decoration: Decoration.Bold);
        Console.WriteLine($"  {Ansi.GetStyleSequence(style)}✔ {message}{Ansi.Reset}");
        Thread.Sleep(700);
    }
}
