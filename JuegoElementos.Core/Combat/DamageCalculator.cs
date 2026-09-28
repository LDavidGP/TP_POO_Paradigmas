using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Combat
{
    public class DamageCalculator(IDictionary<(IElementType Attacker, IElementType Defender), int>? customMatrix = null) :  IDamageCalculator
    {
        //(AttackerType, DefenderType) => Damage against DefenderType
        
        private static readonly Dictionary<(IElementType Attacker, IElementType Defender), int> DefaultMatrix = new()
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
        
        private readonly Dictionary<(IElementType Attacker, IElementType Defender), int> _damageMatrix = 
            customMatrix != null 
                ? new Dictionary<(IElementType Attacker, IElementType Defender), int>(customMatrix) 
                : new Dictionary<(IElementType Attacker, IElementType Defender), int>(DefaultMatrix);
        public int CalculateDamage(IElementType attacker, IElementType defender)
        {
            return _damageMatrix.GetValueOrDefault((attacker, defender), 10); //10 of default damage
        }
    }
}