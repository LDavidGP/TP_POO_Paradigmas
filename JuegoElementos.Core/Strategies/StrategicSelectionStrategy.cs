using JuegoElementos.Core.Domain;
using JuegoElementos.Core.ElementTypes;

namespace JuegoElementos.Core.Strategies
{
    public class StrategicSelectionStrategy : ISelectionStrategy
    {
        private readonly Random _random = new();

        public string Name => "Estratégica";

        // Propiedad opcional para asignar el elemento que puso el jugador humano
        public Element? OpponentElement { get; set; }

        // Constructor vacio por defecto para que no tire error en Program.cs
        public StrategicSelectionStrategy() { }

        // Constructor con parametro opcional por si se quiere instanciar directamente
        public StrategicSelectionStrategy(Element opponentElement)
        {
            OpponentElement = opponentElement;
        }

        public Element SelectElement(List<Element> availableElements)
        {
            if (availableElements == null || availableElements.Count == 0)
            {
                throw new ArgumentException("La lista de elementos disponibles no puede estar vacía.");
            }

            // Si no hay elemento asignado del oponente, elige uno al azar
            if (OpponentElement == null)
            {
                return availableElements[_random.Next(availableElements.Count)];
            }

            Element bestElement = availableElements[0];
            double maxDamage = -1;

            foreach (var element in availableElements)
            {
                double damage = CalculateDamage(element, OpponentElement);
                if (damage > maxDamage)
                {
                    maxDamage = damage;
                    bestElement = element;
                }
            }

            return bestElement;
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