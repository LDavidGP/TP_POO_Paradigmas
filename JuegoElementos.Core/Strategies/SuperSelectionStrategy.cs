using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Strategies
{
    public class SuperSelectionStrategy : ISelectionStrategy
    {
        private readonly Random _random = new();

        public string Name => "Super IA";

        // Propiedad opcional para asignar el elemento que puso el jugador humano
        public Element? OpponentElement { get; set; }

        // Constructor vacio por defecto para que no tire error en Program.cs
        public SuperSelectionStrategy() { }

        // Constructor con parametro opcional
        public SuperSelectionStrategy(Element opponentElement)
        {
            OpponentElement = opponentElement;
        }

        public Element SelectElement(List<Element> availableElements)
        {
            if (availableElements == null || availableElements.Count == 0)
            {
                throw new ArgumentException("La lista de elementos disponibles no puede estar vacía.");
            }

            if (OpponentElement == null)
            {
                return availableElements[_random.Next(availableElements.Count)];
            }

            double targetHealth = OpponentElement.Health;

            var lethalElements = availableElements
                .Select(e => new { Element = e, Damage = CalculateDamage(e, OpponentElement) })
                .Where(x => x.Damage >= targetHealth)
                .OrderBy(x => x.Damage)
                .ToList();

            if (lethalElements.Any())
            {
                return lethalElements.First().Element;
            }

            return availableElements
                .OrderByDescending(e => CalculateDamage(e, OpponentElement))
                .First();
        }

        private double CalculateDamage(Element attacker, Element defender)
        {
            if (attacker == null || defender == null) return 0;

            if (attacker.Type is WaterType)
            {
                if (defender.Type is FireType) return 50;
                if (defender.Type is EarthType) return 20;
            }
            else if (attacker.Type is FireType)
            {
                if (defender.Type is EarthType) return 40;
                if (defender.Type is WaterType) return 20;
            }
            else if (attacker.Type is EarthType)
            {
                if (defender.Type is WaterType) return 30;
                if (defender.Type is FireType) return 20;
            }

            return 20;
        }
    }
}