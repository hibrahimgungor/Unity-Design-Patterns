# Unity Design Patterns

A growing collection of design pattern implementations written in C# for Unity.
Each pattern is self-contained, progressively structured from the simplest possible
version to a production-ready solution, and documented with real use cases in mind.

This repository is built to be a practical reference — not just a textbook exercise.
Every implementation answers the question: *why would I actually use this in a Unity project?*

---

## Structure

Patterns are grouped by their classical GoF category. Each pattern folder is split into
two parts: **Scripts** for core implementations and **Examples** for concrete usage scripts
and scenes. This keeps base classes clean and separable from demonstration code.

```
Unity_Design_Patterns/
├── Creational_Design_Patterns/
│   └── Singleton/
│       ├── Scripts/        # Base classes only
│       └── Examples/       # Example scripts and scenes
├── Behavioral_Design_Patterns/
│   ├── Observer/
│   ├── Strategy/
│   └── Command/
└── Structural_Design_Patterns/
    └── Decorator/
```

---

## Current Content

### Creational
| Pattern | Status | Description |
|---------|--------|-------------|
| Singleton | ✅ Complete | 8 progressive implementations from Basic to ScriptableObject-backed |

---

## Roadmap

### Behavioral
| Pattern | Status |
|---------|--------|
| Observer | 🔜 Planned |
| Strategy | 🔜 Planned |
| Command | 🔜 Planned |

### Structural
| Pattern | Status |
|---------|--------|
| Decorator | 🔜 Planned |

More patterns will be added over time. The list will grow as the project evolves.

---

## How This Repo Is Organized

Each pattern folder contains:
- **Scripts/** — base class implementations, documented via XML summaries
- **Examples/** — concrete subclasses, MonoBehaviour runners, and Unity scenes

No inline comments. Everything that needs explanation lives in the `<summary>` block
so it shows up in IDE tooltips and stays out of the way while reading code.

Each pattern also has its own `README.md` that explains:
- Why each implementation step exists
- What problem it solves and what new problem it introduces
- A feature matrix and a decision guide for picking the right variant
- Curated external resources (videos and articles)

---

## Unity Version

Implementations use Unity 6 APIs where relevant (e.g. `FindFirstObjectByType`).
Older API equivalents are straightforward substitutions if needed.
