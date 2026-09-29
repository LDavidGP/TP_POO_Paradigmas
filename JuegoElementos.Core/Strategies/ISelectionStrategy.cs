using JuegoElementos.Core.Domain;

namespace JuegoElementos.Core.Strategies;
    /// <summary>
    /// Define la estrategia de selección de elementos (Patrón Strategy),
    /// permitiendo intercambiar el algoritmo entre jugador humano y los distintos tipos de IA.
    /// </summary>
    public interface ISelectionStrategy
    {
        string Name { get; }
        Element SelectElement(CombatContext context);
    }
