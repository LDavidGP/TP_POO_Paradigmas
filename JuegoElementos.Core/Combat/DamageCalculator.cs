using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Combat
{
    public class DamageCalculator
    {
        //(AttackerType, DefenderType) => Damage against DefenderType
        public readonly Dictionary<(IElementType Attacker, IElementType Defender), int> DamageMatrix = new()
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
    }
}