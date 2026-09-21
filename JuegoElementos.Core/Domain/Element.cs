using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Domain
{
    public class Element(IElementType type)
    {
        public IElementType Type { get; } = type;
        public int Health { get; private set; } = 100;
        public bool IsAlive => Health > 0;
        public void TakeDamage(int damage)
        {
            if (!IsAlive)
            {
                return;
            }
            if (damage < 0)
            {
                throw new ArgumentException("Damage cannot be negative", nameof(damage));
            }
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
            }
        }
    }
}