using JuegoElementos.ConsoleApp.Services;
using JuegoElementos.Core.ElementTypes;
using JuegoElementos.Core.Strategies;

namespace JuegoElementos.ConsoleApp.Models;

public class GameSettings(string playerName)
{
    public string PlayerName { get; set; } = playerName;
    public string AiConfigName { get; private set; } = "Sorpresa (Al azar)";
    public string MatrixPresetName { get; private set; } = "Estándar (Reglas del TP)";

    public Dictionary<(IElementType Attacker, IElementType Defender), int> DamageMatrix { get; private set; } =
        DamageMatrixPresets.GetDefault();

    private Func<ISelectionStrategy> _strategyProvider = () =>
    {
        var strategies = new ISelectionStrategy[]
        {
            new RandomSelectionStrategy(),
            new StrategicSelectionStrategy(),
            new SuperSelectionStrategy()
        };
        return strategies[Random.Shared.Next(strategies.Length)];
    };

    public ISelectionStrategy CreateOpponentStrategy() => _strategyProvider();

    public void SetAiStrategy(string configName, Func<ISelectionStrategy> strategyFactory)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(configName);
        ArgumentNullException.ThrowIfNull(strategyFactory);

        AiConfigName = configName;
        _strategyProvider = strategyFactory;
    }

    public void SetDamageMatrix(string presetName, Dictionary<(IElementType Attacker, IElementType Defender), int> matrix)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(presetName);
        ArgumentNullException.ThrowIfNull(matrix);

        MatrixPresetName = presetName;
        DamageMatrix = new Dictionary<(IElementType Attacker, IElementType Defender), int>(matrix);
    }
}
