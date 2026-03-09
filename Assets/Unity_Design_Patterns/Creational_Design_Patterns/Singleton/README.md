# Unity Singleton Patterns
### `Unity_Design_Patterns > Creational_Design_Patterns > Singleton`

## What is a Singleton?

A Singleton is a creational design pattern that ensures a class has only one instance
throughout the application lifetime and provides a global access point to that instance.

In plain terms: no matter how many times you ask for it, you always get the same object back.

```csharp
var a = MyManager.Instance;
var b = MyManager.Instance;
// a and b are the same object
```

**Why does this matter in Unity?**
Games typically have systems that must be unique and globally accessible — a single AudioManager
that controls all sound, a single GameManager that owns the game state, a single SaveManager
that writes to disk. Creating multiple instances of these by accident leads to conflicting state,
duplicated behaviour, and bugs that are hard to trace.

The Singleton pattern solves this by making it structurally impossible to create more than one
instance, and by giving every other system a reliable way to reach it without passing references
around manually.

**The trade-off:**
A Singleton is a global. Globals make systems easier to reach but harder to reason about in
isolation — any part of the codebase can read or modify the instance at any time. This is why
Singletons are sometimes called an anti-pattern. They are a tool, not a rule. Used where
genuinely needed, they are clean and practical. Overused, they create hidden dependencies that
make a project difficult to maintain and test.

---

## About This Folder

Eight implementations, ordered from the simplest possible version to a production-ready
solution. Each step keeps everything from the previous one and adds exactly one new capability
to solve exactly one new problem — so the reason every layer exists is always clear.

---

## Repository Structure

```
Singleton/
├── Scripts/                        # Core implementations — base classes only
│   ├── 01_Basic/
│   │   └── BasicSingleton.cs
│   ├── 02_ThreadSafe/
│   │   └── ThreadSafeSingleton.cs
│   ├── 03_MonoBehaviour/
│   │   └── MonoBehaviourSingleton.cs
│   ├── 04_Persistent/
│   │   └── PersistentSingleton.cs
│   ├── 05_Generic/
│   │   └── GenericSingleton.cs
│   ├── 06_GenericPersistent/
│   │   └── GenericPersistentSingleton.cs
│   ├── 07_LazyPersistent/
│   │   └── LazyPersistentSingleton.cs
│   └── 08_ScriptableObject/
│       └── ScriptableObjectSingleton.cs
└── Examples/                       # Concrete usage — scripts and scenes
    ├── 01_Basic/
    │   └── BasicSingletonExample.cs
    ├── 02_ThreadSafe/
    │   └── ThreadSafeSingletonExample.cs
    ├── 03_MonoBehaviour/
    │   └── MonoBehaviourSingletonExample.cs
    ├── 04_Persistent/
    │   └── PersistentSingletonExample.cs
    ├── 05_Generic/
    │   ├── GameManager.cs
    │   ├── AudioManager.cs
    │   └── GenericSingletonExample.cs
    ├── 06_GenericPersistent/
    │   ├── PersistentGameManager.cs
    │   └── GenericPersistentSingletonExample.cs
    ├── 07_LazyPersistent/
    │   ├── EventManager.cs
    │   └── LazyPersistentSingletonExample.cs
    └── 08_ScriptableObject/
        ├── GameConfig.cs
        └── ScriptableObjectSingletonExample.cs
```

---

## Why Does Each Step Exist?

### 01 — Basic
The minimum viable Singleton: a private constructor and a static field.
**Problem it introduces:** Not thread-safe. Two threads hitting the null check simultaneously
can create two separate instances. Not connected to Unity in any way.

### 02 — Thread-Safe
Wraps instance creation in a double-checked lock.
**Why:** Makes the pattern safe for background threads and async code.
**Why it is not carried forward:** From 03 onwards, all implementations inherit from
`MonoBehaviour`. Unity's entire API — `GameObject`, `Transform`, `MonoBehaviour` lifecycle
callbacks — is restricted to the main thread by design. Accessing these from a background
thread throws a runtime exception regardless of locking. A lock on a MonoBehaviour Singleton
therefore provides no real safety and adds unnecessary overhead. Thread-safe access remains
relevant only for pure C# classes like 01 and 02.
**Problem it introduces:** Still not a MonoBehaviour — has no transform, no lifecycle callbacks,
and cannot appear in the Inspector.

### 03 — MonoBehaviour
Inherits from `MonoBehaviour` so the Singleton can live on a scene GameObject.
**Why:** Most Unity systems (physics, rendering, animation) expect components, not plain C# objects.
**Problem it introduces:** The instance is tied to a scene. When the scene unloads, the Singleton
is destroyed and any reference to it becomes invalid.

### 04 — Persistent
Calls `DontDestroyOnLoad` to keep the instance alive across scene transitions.
Adds `HasInstance` and `TryGetInstance` for safer access patterns.
**Why:** Global managers (audio, input, saves) must survive scene loads. `HasInstance` and
`TryGetInstance` allow callers to query existence without risking a null exception.
**Problem it introduces:** Every manager class must repeat the same Singleton boilerplate.
Any inconsistency between managers creates subtle bugs.

### 05 — Generic
Turns the Singleton into a generic base class `GenericSingleton<T>`. Any class becomes a
scene-scoped Singleton by inheriting from it — no repeated boilerplate.
**Why:** Removes duplication. All managers share the exact same, well-tested Singleton logic.
A bug fix or improvement in the base class propagates to every subclass automatically.
**Problem it introduces:** Persistence is a design decision, not a configuration option.
Having one class handle both persistent and non-persistent behaviour through a bool flag
is error-prone and blurs intent.

### 06 — Generic Persistent
Splits persistence into a dedicated base class `GenericPersistentSingleton<T>`.
Adds `AutoUnparentOnAwake` to handle nested prefab hierarchies correctly.
**Why:** A manager is either global for the entire application lifetime or scoped to a single
scene. This distinction belongs at the type level, not behind a toggle. Inheriting from the
right base class makes the intent impossible to misread.
**Problem it introduces:** Both `GenericSingleton<T>` and `GenericPersistentSingleton<T>`
require the instance to exist in the scene. If a manager is accessed before its GameObject
is placed, the getter logs an error and returns null.

### 07 — Lazy Persistent
Extends the persistent pattern with auto-creation. If no instance is found in the scene,
the getter creates a new `GameObject` and adds the component automatically.
Also adds `OnInitialize` and `OnShutdown` hooks for structured lifecycle management.
**Why:** Some systems are needed on demand and it is inconvenient or error-prone to require
manual placement in every scene. The Singleton manages its own lifecycle entirely.
`OnInitialize` replaces Awake overrides in subclasses, keeping the guard logic in one place.
`OnShutdown` centralises cleanup (event unsubscription, data flushing) at application exit.
A quit guard prevents Unity from recreating the object during teardown.

### 08 — ScriptableObject
Backs the Singleton with a `ScriptableObject` asset rather than a scene component.
**Why:** Configuration data (balance values, settings, feature flags) does not belong in a
scene or in code. ScriptableObject assets are editable in the Inspector, can be version
controlled independently, and are available before any scene loads. This step cleanly
separates *data* from *behaviour*.
ScriptableObjects have no `Update`, no coroutines, and no scene-lifecycle callbacks.
They are data containers, not logic hosts.

---

## Feature Matrix

| Feature | 01 | 02 | 03 | 04 | 05 | 06 | 07 | 08 |
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| Thread-safe | ❌ | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ | — |
| MonoBehaviour | ❌ | ❌ | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ |
| DontDestroyOnLoad | ❌ | ❌ | ❌ | ✅ | ❌ | ✅ | ✅ | — |
| Generic base class | ❌ | ❌ | ❌ | ❌ | ✅ | ✅ | ✅ | ✅ |
| Lazy auto-creation | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ✅ | ❌ |
| HasInstance / TryGetInstance | ❌ | ❌ | ❌ | ✅ | ✅ | ✅ | ✅ | ❌ |
| AutoUnparentOnAwake | ❌ | ❌ | ❌ | ✅ | ❌ | ✅ | ✅ | — |
| OnInitialize / OnShutdown | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ✅ | — |
| Quit guard | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ✅ | — |
| OnDestroy cleanup | ❌ | ❌ | ✅ | ✅ | ✅ | ✅ | ✅ | — |
| Inspector editable data | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ✅ |

---

## Decision Guide

```
Does the class need Unity lifecycle (Awake, Update, coroutines)?
├── No  → 01 Basic or 02 Thread-Safe
└── Yes
    ├── Configuration / balance data only?
    │   └── Yes → 08 ScriptableObject
    ├── Scoped to a single scene?
    │   ├── One-off, no shared base needed → 03 MonoBehaviour
    │   └── Multiple managers, shared base → 05 Generic
    └── Must survive scene transitions?
        ├── Manual scene placement is acceptable?
        │   ├── One-off, no shared base needed → 04 Persistent
        │   └── Multiple managers, shared base → 06 Generic Persistent
        └── Should create itself on demand → 07 Lazy Persistent
```

---

## General Rules

- Always call `base.Awake()` when overriding `Awake` in a subclass of any generic Singleton.
- Prefer `OnInitialize` over `Awake` overrides in subclasses of `LazyPersistentSingleton`.
- Always unsubscribe from events in `OnDisable` or `OnDestroy` to prevent memory leaks.
- Use `HasInstance` or `TryGetInstance()` instead of `Instance` when the caller can tolerate
  a missing manager, especially during shutdown or early initialization.
- Do not make every class a Singleton. A Singleton is a global — overuse creates hidden
  dependencies and makes systems hard to reason about in isolation.
- `AutoUnparentOnAwake` must be true (the default) when the Singleton prefab is nested inside
  another hierarchy, or `DontDestroyOnLoad` will not work as expected.

---

## Related Patterns

**Service Locator** is a complementary pattern often mentioned alongside Singletons.
Rather than accessing a concrete class directly, callers request an interface from a central
registry which returns whatever implementation is currently registered. This enables mock
injection for testing and runtime service swapping without changing call sites.
It is not included here because it is an architectural pattern, not a Singleton variant.
Consider exploring it separately under `Architectural_Patterns`.

---

## Resources

### Video

**Theory** — [Singleton Pattern – Design Patterns (ep 6)](https://youtu.be/hUE_j6q0LTQ)
No code. Focuses on the reasoning behind the pattern and why it is sometimes considered an anti-pattern. Covers private constructors and global access point theory from an architectural perspective.

**Beginner / Unity Specific** — [Everything You Need to Know About Singletons in Unity](https://youtu.be/mpM0C6quQjs)
How the standard C# Singleton adapts to MonoBehaviour. Covers Awake-based instance checks, DontDestroyOnLoad, and common mistakes. Best starting point for Unity developers.

**Intermediate / Best Practices** — [Singletons in Unity (done right)](https://youtu.be/yhlyoQ2F-NM)
Examines where Singletons help and where they cause problems through real scenarios. Highlights tight coupling issues that emerge as projects grow.

**Advanced / Alternatives** — [Better Singletons in Unity C#](https://youtu.be/LFOXge7Ak3E)
Covers Generic Singletons, Regulator Singletons, and the Null Object Pattern. Shows how to combine Singletons with C# interfaces to reduce error surface.

### Articles
- [Refactoring Guru — Singleton](https://refactoring.guru/design-patterns/singleton) — Written reference covering intent, structure, pros/cons, and code examples.
