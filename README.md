# Unity Design Patterns

A growing reference library of design pattern implementations written in C# for Unity.

Each pattern is built to answer a question I keep coming back to: *how does this actually
work in a Unity project, and when should I use it?* Not just the textbook definition —
the real implementation, the trade-offs, the Unity-specific gotchas, and the decision
points that matter in practice.

This repository exists because I forget things. When I haven't used a pattern for months,
I need more than a Wikipedia summary to get back up to speed. Every entry here is written
the way I'd want to find it: progressive implementations from the simplest version to a
production-ready solution, documented with the reasoning behind each step.

---

## Structure

Patterns are grouped by their classical GoF category. Each pattern folder contains
numbered example implementations that build on each other, a dedicated README explaining
why each step exists, and curated resources for going deeper.

```
Unity_Design_Patterns/
├── Creational_Design_Patterns/
│   └── Singleton/
├── Behavioral_Design_Patterns/
│   └── Observer/
└── Structural_Design_Patterns/
```

---

## Current Content

### Creational
| Pattern | Status | Description |
|---------|--------|-------------|
| [Singleton](Creational_Design_Patterns/Singleton) | ✅ Complete | 8 progressive implementations from Basic to ScriptableObject-backed |

### Behavioral
| Pattern | Status | Description |
|---------|--------|-------------|
| [Observer](Behavioral_Design_Patterns/Observer) | ✅ Complete | 7 progressive implementations from NoPattern to EventBus |

---

## Roadmap

### Behavioral
| Pattern | Status |
|---------|--------|
| Strategy | 🔜 Planned |
| Command | 🔜 Planned |

### Structural
| Pattern | Status |
|---------|--------|
| Decorator | 🔜 Planned |

---

## What Each Pattern Entry Contains

- **Progressive implementations** — each step introduces one new concept, shows what problem
  it solves, and what new problem it introduces
- **XML summaries** — every class and method documented so explanations show up in IDE
  tooltips without cluttering the code
- **Pattern README** — trade-offs, decision guide, Unity-specific notes, and the reasoning
  behind every implementation choice
- **Curated resources** — videos and articles worth reading, not just a list of links

---

## Unity Version

Implementations use Unity 6 APIs where relevant (e.g. `FindFirstObjectByType`).
Older API equivalents are straightforward substitutions if needed.
