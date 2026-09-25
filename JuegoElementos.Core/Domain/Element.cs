using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Domain
{
    public class Element
    {
        public IElementType Type { get; }
        public int MaxHealth { get;}
        public int Health { get; private set; }
        public bool IsAlive => Health > 0;

        public Element(IElementType type, int maxHealth = 100)
        {
            Type = type;
            MaxHealth = Math.Max(1, maxHealth);
            Health = MaxHealth;
        }
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