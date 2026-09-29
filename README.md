# ⚔️ Batalla de Elementos: Agua, Tierra y Fuego

**Trabajo Práctico Integrador — Programación Orientada a Objetos (POO)**  
Juego de combate estratégico por turnos inspirado en la mecánica de Piedra–Papel–Tijera, desarrollado en C# y .NET 10 aplicando principios de diseño orientado a objetos (SOLID, separación de responsabilidades, patrones de diseño y encapsulación estricta).

---

## 📌 Descripción del Proyecto

El sistema modela una partida de combate por turnos entre dos participantes: un **Jugador Humano** y un oponente controlado por **Inteligencia Artificial (IA)**.

### Características Principales:

- **Elementos y Tipos**: Cada jugador dispone de un mazo de elementos (`Agua 💧`, `Tierra 🌱`, `Fuego 🔥`), cada uno con 100% de energía inicial y efectividades particulares frente a los demás.
- **Mecánica de Combate**: Los jugadores envían una unidad al campo de batalla. En cada ronda, los elementos intercambian ataques reduciendo la vida del rival según una matriz de efectividad. Si una unidad llega a 0% de vida, queda fuera de combate y debe ser reemplazada.
- **Condición de Victoria**: Gana el jugador que logre dejar al contrincante sin unidades activas.
- **Tipos de Oponentes (IA)**:
  - 🎲 **IA Aleatoria**: Selecciona elementos al azar sin evaluar ventajas.
  - 🧠 **IA Estratégica**: Evalúa las ventajas elementales para maximizar el daño frente al elemento oponente.
  - ⚡ **Super IA**: Maximiza la eficiencia táctica buscando el remate (K.O.) con el mínimo daño necesario o infligiendo el máximo daño disponible.
  - ❓ **Modo Sorpresa**: Selecciona aleatoriamente la estrategia de IA en cada partida.
- **Reglas de Daño Configurables**: Matriz de efectividades totalmente flexible y desacoplada del dominio. Permite elegir presets (_Estándar_, _Muerte Súbita x2_, _Táctica Extrema_) o personalizar valores de daño específicos desde la consola.
- **Interfaz de Consola Rica**: Renderizado en consola con formato de cajas, barras de salud proporcionales, paletas de colores ANSI y registro de combate en tiempo real.
- **Diagrama de Clases**: Arquitectura documentada en PlantUML.

---

## 🛠️ Tecnología Utilizada

- **Plataforma**: [.NET 10 SDK](https://dotnet.microsoft.com/) (`net10.0`)
- **Lenguaje**: C# 13/14
  - Características del lenguaje aplicadas: _Primary Constructors_, _Records_ con igualdad por valor, _Collection Expressions_, _File-Scoped Namespaces_, _Pattern Matching_ y tipado estricto.
- **Testing**: xUnit 2.9.3 con suite de 60 pruebas unitarias automatizadas.
- **Diseño & Modelado**: UML 2.0 (PlantUML).

---

## 📦 Dependencias y Librerías

El proyecto está diseñado bajo una arquitectura limpia y modular de 3 proyectos:

### 1. `JuegoElementos.Core` (Librería de Clases de Dominio)

- **Dependencias externas**: **Ninguna (0 dependencias externas)**.
- Diseñado exclusivamente sobre la BCL (_Base Class Library_) estándar de .NET.
- Contiene las entidades (`Element`, `Deck`, `Player`), tipos de elementos (`IElementType`), cálculo de daño (`DamageCalculator`, `IDamageCalculator`), estrategias de IA (`ISelectionStrategy`, `RandomSelectionStrategy`, `StrategicSelectionStrategy`, `SuperSelectionStrategy`, `HumanSelectionStrategy`), fábrica (`DeckFactory`) y abstracciones de eventos (`ICombatEventsListener`, `IElementSelector`).

### 2. `JuegoElementos.ConsoleApp` (Aplicación de Interfaz de Usuario)

- **Dependencias de proyecto**: Referencia a `JuegoElementos.Core`.
- **Dependencias externas**: **Ninguna (0 dependencias externas)**.
- Implementa renderizado ANSI nativo (`System.Console`, `System.Drawing.Color`, expresiones regulares) para visualización sin necesidad de librerías de terceros.

### 3. `JuegoElementos.Tests` (Suite de Pruebas Unitarias)

- **Dependencias de proyecto**: Referencia a `JuegoElementos.Core`.
- **Paquetes NuGet**:
  - `xunit` (v2.9.3) — Framework de pruebas unitarias.
  - `xunit.runner.visualstudio` (v3.1.4) — Adaptador para ejecución de tests.
  - `Microsoft.NET.Test.Sdk` (v17.14.1) — SDK para ejecución de pruebas en .NET.
  - `coverlet.collector` (v6.0.4) — Recolector de cobertura de código.

---

## 🚀 Instrucciones de Ejecución y Pruebas

Desde la raíz del proyecto:

### Compilar la solución:

```bash
dotnet build
```

### Ejecutar el juego por consola:

```bash
dotnet run --project JuegoElementos.ConsoleApp
```

### Ejecutar las pruebas unitarias:

```bash
dotnet test
```

---

## 📐 Patrones y Principios de POO Aplicados

- **Strategy**: Intercambio dinámico de algoritmos de decisión para las IAs y el usuario humano (`ISelectionStrategy`).
- **Factory Method**: Creación y distribución aleatoria o personalizada de mazos desacoplada del juego (`DeckFactory`).
- **Observer / Listener**: Desacoplamiento total entre el motor de combate del Core y la interfaz de usuario (`ICombatEventsListener`).
- **Open/Closed Principle (OCP)**: Nuevos elementos o reglas de daño pueden incorporarse sin modificar la lógica interna del combate (`IDamageCalculator`).
- **Encapsulación y Estado Inmutable**: Colecciones protegidas con `IReadOnlyList`, salud validada con encapsulamiento estricto y copias defensivas.

---

## 📋 Consigna Original del Trabajo Práctico

<details>
<summary>Hacer clic para desplegar la consigna oficial</summary>

### 1. Introducción

El presente Trabajo Práctico tiene como objetivo aplicar los principios de Programación Orientada a Objetos en el diseño e implementación de un juego por turnos basado en una mecánica tipo Piedra–Papel–Tijera, utilizando elementos:

- 💧 Agua
- 🌱 Tierra
- 🔥 Fuego

Cada elemento posee ventajas y desventajas frente a los demás, generando interacciones estratégicas durante el combate.
A diferencia del juego clásico, cada jugador dispondrá de un conjunto de elementos (unidades), los cuales tendrán un nivel de energía (vida). Durante cada turno, los elementos se enfrentan y se aplican daños configurables según la combinación de tipos.
El objetivo del juego es reducir la energía del oponente hasta dejarlo sin unidades activas.
El sistema deberá ser flexible, permitiendo modificar reglas de daño sin cambiar la lógica principal del juego.

### 2. Descripción General del Sistema

El sistema a diseñar y desarrollar deberá:

- Permitir definir una partida entre jugador y computadora.
- Generar un conjunto de N elementos (por ejemplo 6) de manera aleatoria.
- Asignar a cada elemento:
  - Tipo (Agua, Tierra o Fuego)
  - Energía inicial (100%)
- Implementar un sistema de combate por turnos.
- Aplicar daño según una matriz configurable de efectividad entre tipos.
- Determinar el ganador en función de la energía restante.

### Criterios de Evaluación y Consideraciones

- Correcta aplicación de los conceptos de POO.
- Funcionalidad del software de acuerdo con las reglas y requerimientos establecidos.
- No se permite lógica centralizada en una sola clase.
- No se permite resolver interacciones con múltiples if o switch.
- No se permite el uso de estructuras sin encapsulación.
- No se permite resolver el sistema como funciones globales.

</details>
