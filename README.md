
# Eternal Realms CLI ⚔️

Motor RPG por consola desarrollado con C# y .NET, enfocado en buenas prácticas de arquitectura, principios SOLID, testing y diseño de software escalable.

El objetivo del proyecto es simular cómo se desarrolla software profesional mientras se construye un sistema de juego modular y mantenible.

---

# Características

- Creación de personajes
- Sistema de combate por turnos
- Inventario
- Sistema de equipamiento
- Enemigos con comportamiento propio
- Sistema de loot
- Sistema de quests/misiones
- Guardado y carga mediante JSON
- Arquitectura desacoplada
- Sistema de eventos
- Testing automatizado
- Diseño extensible y mantenible

---

# Tecnologías utilizadas

- C#
- .NET
- NUnit
- FluentAssertions
- System.Text.Json
- Spectre.Console
- Git & GitHub

---

# Objetivos del proyecto

Este proyecto fue diseñado para practicar:

- Programación Orientada a Objetos
- Principios SOLID
- Arquitectura por capas
- Clean Architecture
- Testing automatizado
- Persistencia de datos
- Diseño desacoplado
- Buenas prácticas de Git
- Desarrollo de software mantenible

---

# Arquitectura

El proyecto utiliza una arquitectura inspirada en Clean Architecture.

```txt
/UI.Console
    ↓
/Application
    ↓
/Core
    ↓
/Infrastructure
```

Cada capa tiene una responsabilidad específica y está desacoplada del resto.

---

# Estructura del proyecto

```txt
EternalRealmsCLI/
│
├── src/
│   │
│   ├── EternalRealms.Core/
│   │   ├── Entities/
│   │   ├── ValueObjects/
│   │   ├── Interfaces/
│   │   ├── Enums/
│   │   ├── Events/
│   │   ├── Exceptions/
│   │   └── Services/
│   │
│   ├── EternalRealms.Application/
│   │   ├── Services/
│   │   ├── DTOs/
│   │   ├── Commands/
│   │   ├── Handlers/
│   │   ├── Validators/
│   │   └── Interfaces/
│   │
│   ├── EternalRealms.Infrastructure/
│   │   ├── Persistence/
│   │   ├── Serialization/
│   │   ├── Logging/
│   │   ├── Repositories/
│   │   └── Configuration/
│   │
│   └── EternalRealms.UI.Console/
│       ├── Menus/
│       ├── Screens/
│       ├── Prompts/
│       ├── Rendering/
│       └── Program.cs
│
├── tests/
│   │
│   ├── EternalRealms.Core.Tests/
│   ├── EternalRealms.Application.Tests/
│   └── EternalRealms.IntegrationTests/
│
├── docs/
│   ├── architecture.md
│   ├── domain-model.md
│   └── roadmap.md
│
├── saves/
├── assets/
├── README.md
└── .gitignore
```

---

# Conceptos principales

## Entidades

Representan los objetos principales del dominio.

Ejemplos:
- Character
- Enemy
- Item
- Weapon
- Armor
- Quest

---

## Value Objects

Objetos inmutables que representan valores del dominio.

Ejemplos:
- Stats
- Position
- Damage
- Health

---

## Servicios

Contienen lógica de negocio compleja.

Ejemplos:
- CombatService
- LootService
- QuestService
- SaveGameService

---

# Testing

El proyecto cuenta con testing automatizado usando NUnit y FluentAssertions.

## Ejemplos de pruebas

- Un personaje muerto no puede atacar
- Equipar un arma aumenta el daño
- El inventario no permite superar el límite
- El sistema de combate calcula correctamente el daño
- El guardado JSON mantiene la integridad de los datos

---

# Persistencia

El juego utiliza serialización JSON mediante:

```csharp
System.Text.Json
```

Los datos del jugador se almacenan en:

```txt
/saves/
```

---

# Principios SOLID aplicados

## S — Single Responsibility
Cada clase tiene una única responsabilidad.

## O — Open/Closed
El sistema permite agregar nuevas clases sin modificar código existente.

## L — Liskov Substitution
Las clases derivadas pueden reemplazar correctamente a sus clases base.

## I — Interface Segregation
Las interfaces son específicas y pequeñas.

## D — Dependency Inversion
Las capas superiores dependen de abstracciones.

---

# Ejemplo de flujo

```txt
Crear personaje
    ↓
Explorar zonas
    ↓
Combatir enemigos
    ↓
Obtener loot
    ↓
Completar quests
    ↓
Guardar progreso
```

---

# Posibles mejoras futuras

- Multiplayer
- IA para enemigos
- Árbol de habilidades
- Sistema de crafting
- Comercio entre NPCs
- Mapa procedural
- Sistema de diálogos
- Plugins/mods
- Sockets TCP
- Base de datos real

---

# Cómo ejecutar el proyecto

## Clonar repositorio

```bash
git clone <repo-url>
```

---

## Entrar al proyecto

```bash
cd EternalRealmsCLI
```

---

## Ejecutar aplicación

```bash
dotnet run --project src/EternalRealms.UI.Console
```

---

# Ejecutar tests

```bash
dotnet test
```

---

# Roadmap

- [ ] Sistema base
- [ ] Combate
- [ ] Inventario
- [ ] Quests
- [ ] Eventos
- [ ] Crafting
- [ ] NPCs
- [ ] Sistema de habilidades
- [ ] Multiplayer

---

# Autor

Desarrollado por Bautista Caudana como proyecto de práctica avanzada de arquitectura y desarrollo backend en C#.
