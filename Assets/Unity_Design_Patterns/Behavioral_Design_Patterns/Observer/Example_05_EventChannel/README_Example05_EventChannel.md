# Example 05 — Event Channel
### `Observer > Example_05_EventChannel`

## What is an Event Channel?

An Event Channel is a decoupled communication mechanism where publishers and subscribers
never reference each other directly. Both sides communicate through a shared channel —
the channel is the only shared contract.

**EventChannel** — each event type has its own isolated channel:
```
Publisher ──▶ EventChannel<QuestStartedEvent>   ──▶ Subscribers
Publisher ──▶ EventChannel<QuestCompletedEvent> ──▶ Subscribers
Publisher ──▶ EventChannel<QuestFailedEvent>    ──▶ Subscribers
```

In earlier examples, observers always needed to know **whose event to subscribe to**.
QuestUI had to find QuestSystem, hold a reference to it, and subscribe to its specific event.
With Event Channel, neither side knows about the other — any class can raise or listen to
a channel without knowing who is on the other end.

```
Example 02 (C# Event):   QuestUI ──▶ QuestSystem.OnQuestCompleted  (must know QuestSystem)
Example 04 (Static):     QuestUI ──▶ QuestSystem.OnQuestCompleted  (no reference, but still tied to QuestSystem)
Event Channel:           QuestUI ──▶ EventChannel<QuestCompletedEvent>  (knows nothing about QuestSystem)
```

---

## Three Implementations

This example shows the same Event Channel concept implemented three different ways.

### 01 — C# Event

The channel is a static generic class. Each event type gets its own isolated channel
through C#'s generic type system — `EventChannel<QuestCompletedEvent>` and
`EventChannel<QuestStartedEvent>` are entirely separate static instances.

**IEvent — why it exists:**
All event types implement `IEvent`. This interface is intentionally empty. It serves two purposes:
- Constrains `EventChannel<T>` to only accept intentional event types — `where T : struct, IEvent`
- Provides a future extensibility point — if all events ever need a common property (timestamp,
  priority, source ID), adding it to `IEvent` propagates to every event struct automatically
  without touching each one individually.

**How channels are identified:**
The channel is identified by the event **type** — `EventChannel<QuestCompletedEvent>` and
`EventChannel<QuestStartedEvent>` are separate channels because they are separate types.
The struct carries both the data and the identity of the event.

**When to use Clear():**
Call `Clear()` on scene unload or application quit to prevent stale subscribers from
accumulating across scene transitions. Static events persist for the application lifetime —
`Clear()` is the safety valve.

---

### 02 — ScriptableObject

Ryan Hipple's approach from Unite Austin 2017. Each channel is a ScriptableObject asset.
Publishers and subscribers both hold a reference to the asset — not to each other.
Works seamlessly across scenes because assets persist independently of scene lifecycle.

This approach is further divided into two sub-implementations:

#### 02 / 01 — Direct Subscription

Subscribers hold a reference to the channel asset and subscribe directly in `OnEnable` /
`OnDisable` — the same lifecycle pattern used in earlier examples, but targeting an asset
instead of a concrete class.

#### 02 / 02 — Listener Component

An `EventListener` MonoBehaviour acts as the bridge between a channel asset and a UnityEvent.
Subscribers no longer write any subscription code. Instead, they expose public methods and
designers wire them up to the listener's UnityEvent in the Inspector via drag-and-drop.
This makes the system fully designer-friendly — reactions to events can be configured
without touching any script.

```
QuestStartedEvent (asset) ──▶ EmptyEventListener ──▶ UnityEvent ──▶ QuestUI.OnQuestStarted()
                                                                  ──▶ QuestAudio.OnQuestStarted()
```

**Static vs Dynamic — a critical distinction:**

When wiring a method to a listener's UnityEvent in the Inspector, Unity presents two options:

```
Dynamic int
  └── QuestReward.OnQuestCompleted   ← runtime data from the channel flows here ✅

Static Parameters
  └── QuestReward.OnQuestCompleted   ← fixed value entered in the Inspector ❌
```

**Always use Dynamic.** The Dynamic section passes the runtime value from the channel
directly to the method — this is the intended behaviour.

The Static Parameters section lets you enter a fixed value in the Inspector. If selected,
the channel's runtime data is completely ignored and the hardcoded Inspector value is used
instead. This bypasses the entire channel system silently — no error, no warning, wrong
behaviour. It is the most common mistake when setting up Listener Components.

**When to use which:**

| | 01 — Direct Subscription | 02 — Listener Component |
|---|---|---|
| Wiring | Code (`OnEnable` / `OnDisable`) | Inspector (drag-and-drop) |
| Designer-friendly | No | Yes |
| Refactor visibility | IDE tracks all usages | Inspector connections invisible to IDE |
| Debugging | Stack trace shows subscriber | Harder to trace through UnityEvent |
| Best for | Programmer-owned systems | Designer-configured reactions |

---

## ScriptableObject — Design Decisions

### How channels are identified

In `01_CSharpEvent`, the channel is identified by the event **type** — the struct itself
carries the identity of the event. `EventChannel<QuestCompletedEvent>` and
`EventChannel<QuestStartedEvent>` are separate because they are separate types.

In `02_ScriptableObject`, the channel is identified by the **asset** — the asset name in
the Project window (`QuestCompletedEvent`, `QuestStartedEvent`) carries the identity.
The class only describes what data the channel carries. This shifts naming responsibility
from the type system to the editor.

```
01_CSharpEvent:      EventChannel<QuestCompletedEvent>   — type carries the identity
02_ScriptableObject: IntEventChannelSO asset named "QuestCompletedEvent" — asset carries the identity
```

### Why struct and IEvent constraints were removed

`01_CSharpEvent` constrains `T` with `where T : struct, IEvent`. Both constraints work
together: `struct` enforces value type semantics, `IEvent` marks the type as an intentional
event and prevents accidental misuse.

In `02_ScriptableObject` both constraints are intentionally removed. The `CreateAssetMenu`
attribute does not work on generic classes — Unity's serialization system cannot instantiate
generic types directly. This means concrete channel classes are required:

```csharp
// This is what makes EmptyEventChannelSO and IntEventChannelSO necessary —
// not a design preference, but a Unity constraint.
[CreateAssetMenu(menuName = "Events/IntEventChannel")]
public class IntEventChannelSO : BaseEventChannelSO<int> { }
```

Because `int` does not implement `IEvent`, the constraint must be dropped.
The trade-off: less compile-time protection, simpler and more flexible API.
`int`, `float`, `string`, `Vector3` can all be passed directly without wrapper structs.

### The Empty struct

Removing the `struct` constraint does not eliminate the need for a type argument on
`BaseEventChannelSO<T>`. C# does not allow `Action<void>` — `void` is a keyword, not a
type, and cannot be used as a generic parameter. This means channels that carry no data
still require a concrete type argument.

The alternative would be a separate non-generic `BaseEventChannelSO` base class — but that
means maintaining two parallel class hierarchies. `Empty` is the deliberate trade-off:
one base class, one extra struct.

```csharp
public struct Empty { }

// In handler signatures, use the discard parameter to signal the value is intentionally ignored:
public void OnQuestStarted(Empty _) => ...
```

---

### The Script Proliferation Problem

As the project grows, the number of concrete channel classes grows with it. Every new data
type requires a new channel class:

```
EmptyEventChannelSO.cs
IntEventChannelSO.cs
FloatEventChannelSO.cs
StringEventChannelSO.cs
Vector3EventChannelSO.cs
QuestDataEventChannelSO.cs
EnemyDataEventChannelSO.cs
PlayerDataEventChannelSO.cs
...
```

In a large project with dozens of event types, this results in a long list of nearly identical
one-line classes. Each class exists solely to fix the generic type parameter and provide a
`CreateAssetMenu` attribute — the actual logic lives entirely in `BaseEventChannelSO<T>`.

This is a Unity constraint, not a design preference. `CreateAssetMenu` does not work on
generic classes, so concrete subclasses are unavoidable. The trade-off is explicit and
worth acknowledging: the ScriptableObject approach trades script count for editor visibility
and scene-persistent channels.

---

## Event Design Rules

- **Keep events small.** An event is a notification, not a data store. If you need to pass
  large or complex data, pass an ID and let the subscriber fetch the data from the appropriate system.
- **Prefer value types.** Structs and primitives carry no heap allocation and cannot be null.
  If a struct contains a reference type field (e.g. `string`), that field still lives on the heap —
  the struct only holds the reference. The GC will collect it, but the timing is non-deterministic.
- **Pass IDs, not strings.** For high-frequency events, avoid string fields in event data.
  Pass an integer ID instead and look up the associated data in a dedicated registry:
  ```csharp
  // Instead of: struct QuestCompletedEvent { public string QuestName; }
  // Prefer:     struct QuestCompletedEvent { public int QuestId; }
  // Then:       string name = QuestDatabase.GetName(e.QuestId);
  ```
- **One event per meaningful occurrence.** `QuestCompletedEvent` and `QuestStartedEvent`
  are separate assets (or types) — do not reuse the same channel for different situations.
- **Always unsubscribe.** In `OnDisable` if you subscribed in `OnEnable`. Static channels
  persist for the application lifetime — a forgotten unsubscribe leaks forever.
- **Call Clear() on scene transitions** if your architecture does not guarantee
  all subscribers unsubscribe themselves.

---

## Pro Tips

### Prevent duplicate subscriptions with HashSet

The default delegate chain allows the same handler to be added more than once.
If `Subscribe` is called twice with the same method, that method will be invoked twice
per `Raise` — a silent bug that is hard to reproduce.

Using a `HashSet` instead of a delegate chain prevents this at the cost of a small
additional memory overhead per channel. For typical subscriber counts (2–10), this
overhead is negligible.

```csharp
private readonly HashSet<Action<T>> _observers = new();

public void Subscribe(Action<T> handler) => _observers.Add(handler);
public void Unsubscribe(Action<T> handler) => _observers.Remove(handler);
public void Raise(T eventData) { foreach (var observer in _observers) observer(eventData); }
public void Clear() => _observers.Clear();
```

Note: `HashSet` equality for delegates works correctly for instance methods but can behave
unexpectedly with lambdas — two identical lambda expressions are not considered equal.
Stick to named methods when using this pattern.


