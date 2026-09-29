using JuegoElementos.Core.ElementsTypes;

namespace JuegoElementos.ConsoleApp.Services;

public static class DamageMatrixPresets
{
    public static Dictionary<(IElementType Attacker, IElementType Defender), int> GetDefault() => new()
    {
        { (new FireType(), new WaterType()), 5 },
        { (new FireType(), new FireType()), 10 },
        { (new FireType(), new EarthType()), 15 },
        { (new WaterType(), new FireType()), 20 },
        { (new WaterType(), new WaterType()), 10 },
        { (new WaterType(), new EarthType()), 5 },
        { (new EarthType(), new FireType()), 15 },
        { (new EarthType(), new WaterType()), 10 },
        { (new EarthType(), new EarthType()), 10 }
    };

    public static Dictionary<(IElementType Attacker, IElementType Defender), int> GetSuddenDeath() => new()
    {
        { (new FireType(), new WaterType()), 15 },
        { (new FireType(), new FireType()), 25 },
        { (new FireType(), new EarthType()), 40 },
        { (new WaterType(), new FireType()), 50 },
        { (new WaterType(), new WaterType()), 25 },
        { (new WaterType(), new EarthType()), 15 },
        { (new EarthType(), new FireType()), 40 },
        { (new EarthType(), new WaterType()), 25 },
        { (new EarthType(), new EarthType()), 25 }
    };

    public static Dictionary<(IElementType Attacker, IElementType Defender), int> GetTactical() => new()
    {
        { (new FireType(), new WaterType()), 5 },
        { (new FireType(), new FireType()), 10 },
        { (new FireType(), new EarthType()), 30 },
        { (new WaterType(), new FireType()), 30 },
        { (new WaterType(), new WaterType()), 10 },
        { (new WaterType(), new EarthType()), 5 },
        { (new EarthType(), new FireType()), 5 },
        { (new EarthType(), new WaterType()), 30 },
        { (new EarthType(), new EarthType()), 10 }
    };
}
