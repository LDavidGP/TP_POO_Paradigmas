using System.Drawing;
using TP_POO_Paradigmas.Enums;
using TP_POO_Paradigmas.Interfaces;
using TP_POO_Paradigmas.Models;
using TP_POO_Paradigmas.Services;

namespace TP_POO_Paradigmas.Scenes;

/// <summary>
/// Escena principal del menú acorde a la consigna del TP:
/// Juego por turnos tipo Piedra-Papel-Tijera (Agua, Tierra y Fuego).
/// Ofrece las opciones de Jugar, Configuración y Salir.
/// </summary>
public class MenuScene : IScene
{
    private enum ViewState
    {
        MainMenu,
        InfoDialog
    }

    private record MenuItem(string Title, string Description, string Tag, Action Action);

    private record MenuTheme(
        string Name,
        Color BorderColor,
        Color TitleColor,
        Color AccentColor,
        Color SelectedFg,
        Color SelectedBg,
        Color UnselectedFg,
        Color DescriptionColor,
        Color HotkeyColor
    );

    private static readonly BoxStyle RoundedBox = new('╭', '╮', '╰', '╯', '─', '│');
    private static readonly BoxStyle DoubleBox = new('╔', '╗', '╚', '╝', '═', '║');

    // Logo ASCII representativo de "TP:POO" (48 caracteres de ancho)
    private static readonly string[] AsciiLogo =
    [
        @"████████╗██████╗      ██████╗  ██████╗  ██████╗ ",
        @"╚══██╔══╝██╔══██╗     ██╔══██╗██╔═══██╗██╔═══██╗",
        @"   ██║   ██████╔╝ ██  ██████╔╝██║   ██║██║   ██║",
        @"   ██║   ██╔═══╝      ██╔═══╝ ██║   ██║██║   ██║",
        @"   ██║   ██║      ██  ██║     ╚██████╔╝╚██████╔╝",
        @"   ╚═╝   ╚═╝          ╚═╝      ╚═════╝  ╚═════╝ "
    ];

    private readonly List<MenuTheme> _themes =
    [
        new MenuTheme(
            Name: "Tríada Elemental",
            BorderColor: Color.FromArgb(79, 70, 229),       // Índigo
            TitleColor: Color.FromArgb(248, 250, 252),        // Blanco brillante
            AccentColor: Color.FromArgb(56, 189, 248),      // Cian Agua
            SelectedFg: Color.FromArgb(255, 255, 255),       // Blanco
            SelectedBg: Color.FromArgb(2, 132, 199),         // Azul Océano
            UnselectedFg: Color.FromArgb(226, 232, 240),     // Slate claro
            DescriptionColor: Color.FromArgb(74, 222, 128),  // Verde Tierra
            HotkeyColor: Color.FromArgb(251, 146, 60)        // Naranja Fuego
        ),
        new MenuTheme(
            Name: "Llama y Fuego",
            BorderColor: Color.FromArgb(220, 38, 38),       // Carmesí
            TitleColor: Color.FromArgb(254, 243, 199),       // Ámbar claro
            AccentColor: Color.FromArgb(249, 115, 22),       // Naranja
            SelectedFg: Color.FromArgb(255, 255, 255),       // Blanco
            SelectedBg: Color.FromArgb(185, 28, 28),         // Rojo oscuro
            UnselectedFg: Color.FromArgb(254, 215, 170),     // Arena
            DescriptionColor: Color.FromArgb(251, 191, 36),  // Amarillo fuego
            HotkeyColor: Color.FromArgb(239, 68, 68)         // Rojo vivo
        ),
        new MenuTheme(
            Name: "Bosque y Tierra",
            BorderColor: Color.FromArgb(22, 163, 74),        // Verde bosque
            TitleColor: Color.FromArgb(240, 253, 244),       // Menta clara
            AccentColor: Color.FromArgb(74, 222, 128),       // Verde brillante
            SelectedFg: Color.FromArgb(20, 83, 45),          // Verde pino profundo
            SelectedBg: Color.FromArgb(134, 239, 172),       // Verde primavera
            UnselectedFg: Color.FromArgb(187, 247, 208),     // Verde suave
            DescriptionColor: Color.FromArgb(52, 211, 153),  // Esmeralda
            HotkeyColor: Color.FromArgb(250, 204, 21)        // Oro
        )
    ];

    private readonly List<MenuItem> _items;
    private int _selectedIndex = 0;
    private int _currentThemeIndex = 0;
    private ViewState _viewState = ViewState.MainMenu;

    private string _dialogTitle = "";
    private string[] _dialogContent = [];
    private string? _statusNotification;

    public MenuScene()
    {
        _items =
        [
            new MenuItem(
                Title: "Jugar",
                Description: "Inicia la partida por turnos (Humano vs IA) con 5 elementos aleatorios.",
                Tag: "JUGAR",
                Action: OpenPlayInfo
            ),
            new MenuItem(
                Title: "Configuración",
                Description: "Configura reglas de daño elemental, matriz de efectividad y modo de IA.",
                Tag: "CONFIG",
                Action: OpenConfigInfo
            ),
            new MenuItem(
                Title: "Salir",
                Description: "Cierra la aplicación y finaliza la sesión.",
                Tag: "SALIR",
                Action: ExitApp
            )
        ];
    }

    public void Draw(Engine.Canvas canvas)
    {
        canvas.Clear();

        if (canvas.Width < 45 || canvas.Height < 14)
        {
            DrawSmallWindowWarning(canvas);
            return;
        }

        var theme = _themes[_currentThemeIndex];

        if (_viewState == ViewState.InfoDialog)
        {
            DrawInfoDialog(canvas, theme);
        }
        else
        {
            DrawMainMenu(canvas, theme);
        }
    }

    public void OnKeyPressed(ConsoleKeyInfo keyInfo)
    {
        if (_viewState == ViewState.InfoDialog)
        {
            if (keyInfo.Key is ConsoleKey.Escape or ConsoleKey.Enter or ConsoleKey.Spacebar)
            {
                _viewState = ViewState.MainMenu;
            }
            return;
        }

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow or ConsoleKey.W:
                _selectedIndex = (_selectedIndex - 1 + _items.Count) % _items.Count;
                _statusNotification = null;
                break;

            case ConsoleKey.DownArrow or ConsoleKey.S:
                _selectedIndex = (_selectedIndex + 1) % _items.Count;
                _statusNotification = null;
                break;

            case ConsoleKey.Home:
                _selectedIndex = 0;
                _statusNotification = null;
                break;

            case ConsoleKey.End:
                _selectedIndex = _items.Count - 1;
                _statusNotification = null;
                break;

            case ConsoleKey.T:
                CycleTheme();
                break;

            case ConsoleKey.Enter or ConsoleKey.Spacebar:
                _items[_selectedIndex].Action();
                break;

            case ConsoleKey.Escape or ConsoleKey.Q:
                ExitApp();
                break;

            case ConsoleKey.D1 or ConsoleKey.NumPad1:
                SelectAndExecute(0);
                break;

            case ConsoleKey.D2 or ConsoleKey.NumPad2:
                SelectAndExecute(1);
                break;

            case ConsoleKey.D3 or ConsoleKey.NumPad3:
                SelectAndExecute(2);
                break;
        }
    }

    public void Dispose()
    {
    }

    private void DrawMainMenu(Engine.Canvas canvas, MenuTheme theme)
    {
        var totalW = canvas.Width;
        var totalH = canvas.Height;

        // Calcular dimensiones del contenedor principal
        var boxWidth = (uint)Math.Clamp(totalW - 6, 52, 72);
        var showAsciiLogo = totalH >= 22 && boxWidth >= 52;
        var boxHeight = (uint)(showAsciiLogo ? 19 : 14);

        var startX = (uint)Math.Max(0, (totalW - (int)boxWidth) / 2);
        var startY = (uint)Math.Max(0, (totalH - (int)boxHeight) / 2);

        // Marco exterior
        canvas.DrawBox(startX, startY, boxWidth, boxHeight, new Style(theme.BorderColor), RoundedBox);

        // Badge de tema visual en el borde superior derecho
        var themeBadge = $" [{theme.Name}] ";
        if (boxWidth > themeBadge.Length + 4)
        {
            canvas.Draw(themeBadge, startX + boxWidth - (uint)themeBadge.Length - 2, startY,
                new Style(theme.AccentColor, decoration: Decoration.Bold));
        }

        var currentY = startY + 1;

        // Cabecera: Logo ASCII o versión compacta
        if (showAsciiLogo)
        {
            for (var i = 0; i < AsciiLogo.Length; i++)
            {
                canvas.Draw(AsciiLogo[i], (uint)(totalW / 2), currentY++,
                    new Style(theme.TitleColor, decoration: Decoration.Bold), Alignment.Center);
            }

            canvas.Draw("[ AGUA • TIERRA • FUEGO ]", (uint)(totalW / 2), currentY++,
                new Style(theme.AccentColor, decoration: Decoration.Bold), Alignment.Center);
            currentY++;
        }
        else
        {
            canvas.Draw("◆  TP:POO  ◆", (uint)(totalW / 2), currentY++,
                new Style(theme.TitleColor, decoration: Decoration.Bold), Alignment.Center);
            canvas.Draw("AGUA • TIERRA • FUEGO  |  COMBATE POR TURNOS", (uint)(totalW / 2), currentY++,
                new Style(theme.AccentColor, decoration: Decoration.Bold), Alignment.Center);
            currentY++;
        }

        // Línea divisoria
        var dividerText = new string('─', (int)boxWidth - 4);
        canvas.Draw(dividerText, startX + 2, currentY++, new Style(theme.BorderColor, decoration: Decoration.Faint));

        // Lista de 3 Opciones principales
        var itemWidth = boxWidth - 6;
        var itemX = startX + 3;

        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            var isSelected = i == _selectedIndex;
            var rowY = currentY++;

            if (isSelected)
            {
                // Resaltar opción seleccionada
                canvas.SetBackground(itemX, rowY, itemWidth, 1, theme.SelectedBg);

                var prefix = "  ▶  ";
                var label = $"{prefix}[{i + 1}] {item.Title}";
                canvas.Draw(label, itemX, rowY,
                    new Style(theme.SelectedFg, theme.SelectedBg, Decoration.Bold));

                var enterHint = "[ENTER]";
                if (itemWidth > label.Length + enterHint.Length + 2)
                {
                    canvas.Draw(enterHint, itemX + itemWidth - (uint)enterHint.Length - 1, rowY,
                        new Style(theme.AccentColor, theme.SelectedBg, Decoration.Bold));
                }
            }
            else
            {
                var prefix = "     ";
                var label = $"{prefix}[{i + 1}] {item.Title}";
                canvas.Draw(label, itemX, rowY,
                    new Style(theme.UnselectedFg));

                var tag = item.Tag;
                if (itemWidth > label.Length + tag.Length + 2)
                {
                    canvas.Draw(tag, itemX + itemWidth - (uint)tag.Length - 1, rowY,
                        new Style(theme.BorderColor, decoration: Decoration.Faint));
                }
            }
        }

        currentY++;

        // Caja de descripción de la opción seleccionada
        var descBoxHeight = 3u;
        var descBoxY = currentY;
        canvas.DrawBox(startX + 2, descBoxY, boxWidth - 4, descBoxHeight, new Style(theme.BorderColor), RoundedBox);

        var activeDesc = _statusNotification ?? $"» {_items[_selectedIndex].Description}";
        if (activeDesc.Length > boxWidth - 8)
        {
            activeDesc = activeDesc[..(int)(boxWidth - 11)] + "...";
        }

        var descStyle = _statusNotification != null
            ? new Style(theme.HotkeyColor, decoration: Decoration.Bold)
            : new Style(theme.DescriptionColor, decoration: Decoration.Italic);

        canvas.Draw(activeDesc, startX + 4, descBoxY + 1, descStyle);

        // Barra inferior de atajos de navegación
        var footerY = startY + boxHeight + 1;
        if (footerY < totalH)
        {
            var navHelp = totalW >= 74
                ? " [▲/▼] Navegar  •  [ENTER] Aceptar  •  [1-3] Rápido  •  [T] Tema  •  [ESC] Salir "
                : " [▲/▼] Navegar  [ENTER] Aceptar  [1-3] Rápido  [ESC] Salir ";

            canvas.Draw(navHelp, (uint)(totalW / 2), footerY,
                new Style(theme.HotkeyColor, decoration: Decoration.Bold), Alignment.Center);
        }
    }

    private void DrawInfoDialog(Engine.Canvas canvas, MenuTheme theme)
    {
        var totalW = canvas.Width;
        var totalH = canvas.Height;

        var dialogW = (uint)Math.Clamp(totalW - 4, 44, 74);
        var dialogH = (uint)Math.Clamp(_dialogContent.Length + 7, 10, totalH - 2);

        var startX = (uint)Math.Max(0, (totalW - (int)dialogW) / 2);
        var startY = (uint)Math.Max(0, (totalH - (int)dialogH) / 2);

        // Marco del diálogo modal
        canvas.DrawBox(startX, startY, dialogW, dialogH, new Style(theme.BorderColor), DoubleBox);

        // Título del diálogo
        var titleText = $"  {_dialogTitle}  ";
        canvas.Draw(titleText, (uint)(totalW / 2), startY + 1,
            new Style(theme.TitleColor, decoration: Decoration.Bold), Alignment.Center);

        // Separador
        var divider = new string('═', (int)dialogW - 4);
        canvas.Draw(divider, startX + 2, startY + 2, new Style(theme.BorderColor));

        // Contenido del diálogo
        var currentY = startY + 4;
        foreach (var line in _dialogContent)
        {
            if (currentY < startY + dialogH - 2)
            {
                canvas.Draw(line, startX + 3, currentY++, new Style(theme.UnselectedFg));
            }
        }

        // Pie del diálogo
        var footerText = "[ Presione ESC o ENTER para volver al menú ]";
        canvas.Draw(footerText, (uint)(totalW / 2), startY + dialogH - 2,
            new Style(theme.HotkeyColor, decoration: Decoration.Bold), Alignment.Center);
    }

    private static void DrawSmallWindowWarning(Engine.Canvas canvas)
    {
        canvas.Draw("Por favor amplía la ventana de la terminal", (uint)(canvas.Width / 2), (uint)(canvas.Height / 2),
            new Style(Color.FromArgb(251, 191, 36), decoration: Decoration.Bold), Alignment.Center);
    }

    private void SelectAndExecute(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _selectedIndex = index;
            _items[index].Action();
        }
    }

    private void CycleTheme()
    {
        _currentThemeIndex = (_currentThemeIndex + 1) % _themes.Count;
        _statusNotification = $"✔ Tema aplicado: {_themes[_currentThemeIndex].Name}";
    }

    private void OpenPlayInfo()
    {
        _dialogTitle = "JUGAR - PARTIDA POR TURNOS";
        _dialogContent =
        [
            "Mecánica según consigna del TP:",
            "",
            "  • Enfrentamiento: Jugador Humano vs Inteligencia Artificial (IA).",
            "  • Elementos: Agua, Tierra y Fuego (5 unidades por jugador).",
            "  • Dinámica: En cada ronda ambos seleccionan un elemento activo.",
            "  • Combate: Se aplica daño mutuo según la regla de efectividad.",
            "  • Victoria: Eliminar todos los elementos del oponente (llevarlos a 0%).",
            "",
            "[ Nota: La lógica de combate e interacción entre elementos se ]",
            "[ conectará a esta opción en la siguiente etapa del proyecto. ]"
        ];
        _viewState = ViewState.InfoDialog;
    }

    private void OpenConfigInfo()
    {
        _dialogTitle = "CONFIGURACIÓN DEL SISTEMA";
        _dialogContent =
        [
            "Parámetros del juego (flexibilidad solicitada en la consigna):",
            "",
            "  • Matriz de Daño entre Elementos:",
            "      - Agua contra Fuego (ej: 50%) / Fuego contra Agua (ej: 20%)",
            "      - Fuego contra Tierra (ej: 40%) / Tierra contra Fuego (ej: 30%)",
            "      - Tierra contra Agua (ej: 50%) / Agua contra Tierra (ej: 20%)",
            "  • Tipo de Oponente (Inteligencia Artificial):",
            "      - IA Aleatoria    -> Selección al azar",
            "      - IA Estratégica  -> Maximiza ventaja táctica",
            "      - Super IA        -> Estrategia óptima y eficiencia de remate",
            "  • Elementos por Jugador: 5 por defecto (configurable N)",
            "  • Energía Inicial: 100% por unidad",
            "",
            "[ Nota: El gestor de configuración y persistencia se ]",
            "[ integrará respetando desacoplamiento y encapsulación. ]"
        ];
        _viewState = ViewState.InfoDialog;
    }

    private static void ExitApp()
    {
        Engine.Instance?.Stop();
    }
}
