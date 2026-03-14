# Example 06 — Event Bus
### `Observer > Example_06_EventBus`

## What is an Event Bus?

An Event Bus is a centralized communication mechanism where all events flow through a single
entry point. Publishers and subscribers never reference each other — both interact only with
the bus itself.

**EventChannel** — each event type has its own isolated channel:
```
Publisher ──▶ EventChannel<QuestStartedEvent>   ──▶ Subscribers
Publisher ──▶ EventChannel<QuestCompletedEvent> ──▶ Subscribers
Publisher ──▶ EventChannel<QuestFailedEvent>    ──▶ Subscribers
```

**EventBus** — all event types flow through a single entry point:
```
                    ┌──────────────────────────────────┐
                    │             EventBus             │
                    │  QuestStartedEvent   → [...]     │
Publisher ────────▶ │  QuestCompletedEvent → [...]     │──▶ Subscribers
                    │  QuestFailedEvent    → [...]     │
                    └──────────────────────────────────┘
```

This is the same decoupling goal as Event Channel — but the architecture is different.

---

## Event Bus vs Event Channel

A common source of confusion is the difference between EventBus and EventChannel. Both decouple
publishers from subscribers, but they do so differently.

```
// EventChannel — each type is its own isolated static class
EventChannel<QuestCompletedEvent>.Raise(data);
EventChannel<QuestStartedEvent>.Raise(data);

// EventBus — single entry point, all types managed in one place
EventBus.Publish<QuestCompletedEvent>(data);
EventBus.Publish<QuestStartedEvent>(data);
```

**EventChannel** uses C#'s generic type system for isolation. `EventChannel<QuestCompletedEvent>`
and `EventChannel<QuestStartedEvent>` are entirely separate static classes — they share no state
and have no awareness of each other. There is no central registry.

**EventBus** is a single static class with a `Dictionary<Type, Delegate>` inside. All event
types are managed in one place. This enables centralized control: logging, clearing, and
inspecting all active subscriptions from a single location.

| | EventChannel&lt;T&gt; | EventBus |
|---|---|---|
| Entry point | Per type | Single |
| Registry | None (generic type system) | Dictionary&lt;Type, Delegate&gt; |
| Lookup cost | Compile-time (zero) | Runtime (dictionary lookup) |
| Centralized management | No | Yes |
| Clearing all events | Must clear each type manually | `EventBus.Clear()` |

---

## How the Dictionary Works

Each event type maps to a delegate chain in the dictionary:

```
Dictionary<Type, Delegate>
{
    typeof(QuestStartedEvent)   → Action<QuestStartedEvent>  (handler1, handler2)
    typeof(QuestCompletedEvent) → Action<QuestCompletedEvent> (handler3)
    typeof(QuestFailedEvent)    → Action<QuestFailedEvent>   (handler4, handler5)
}
```

When `Subscribe<T>` is called, `Delegate.Combine` appends the handler to the existing chain.
When `Unsubscribe<T>` is called, `Delegate.Remove` removes it. If the last subscriber
unsubscribes, the key is removed from the dictionary entirely — no empty entries remain.

### Why Delegate and not Action&lt;T&gt;?

`Dictionary<Type, Action<T>>` is not valid C# — `T` is unknown at the dictionary declaration
site. `Delegate` is the common base type for all delegate types including `Action<T>`,
so it allows storing handlers of different signatures in the same collection.

The cast in `Publish<T>`:
```csharp
((Action<T>)existing)?.Invoke(eventData);
```
is safe because `Subscribe<T>` always stores an `Action<T>` under `typeof(T)`. The same `T`
is used to retrieve and cast — the types are guaranteed to match.

### Cast vs Boxing

The `Delegate` → `Action<T>` cast is a reference cast — no new object is created, no heap
allocation occurs. Only a type check is performed.

Boxing would occur if a value type were stored as `object`. Since `Action<T>` is a reference
type, storing it in a `Dictionary<Type, Delegate>` involves no boxing. The `where T : struct`
constraint ensures event data itself is never boxed during `Publish`.

---

## Publish vs Raise

EventChannel uses `Raise`, EventBus uses `Publish`. This is intentional — they reflect
different mental models:

**Raise** suggests the event originates from a specific channel. The channel owns the event
and raises it on behalf of its publisher. The terminology mirrors C# event convention
(`OnQuestCompleted?.Invoke()`).

**Publish** suggests broadcasting to an anonymous audience through a central medium — like
publishing to a message queue or a radio frequency. No single owner raises the event;
the bus routes it. This is the standard terminology in pub/sub architectures.

Both words describe the same mechanical action. The difference is conceptual framing — and
consistent naming helps communicate which system you are working with at a glance.

---

## Exception Isolation — A Deliberate Omission

A natural extension of this implementation is per-subscriber exception handling — if one
subscriber throws, the exception is caught and remaining subscribers continue to be notified.

The naive approach uses `GetInvocationList()`:

```csharp
foreach (var handler in existing.GetInvocationList())
{
    try { ((Action<T>)handler).Invoke(eventData); }
    catch (Exception e) { Debug.LogException(e); }
}
```

**The problem:** `GetInvocationList()` allocates a new `Delegate[]` array on the heap on
every `Publish` call. Unlike high-frequency per-frame checks, this cost applies to every
single event publish regardless of type — the EventBus is the single entry point for all
events in the project.

**The practical solution** is to isolate exception handling to development builds only:

```csharp
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    foreach (var handler in existing.GetInvocationList())
    {
        try { ((Action<T>)handler).Invoke(eventData); }
        catch (Exception e) { Debug.LogException(e); }
    }
#else
    ((Action<T>)existing).Invoke(eventData);
#endif
```

This gives full isolation and visibility during development with zero allocation overhead
in production builds. If this trade-off is acceptable for your project, add it directly
to the `Publish` method — it is not included here to keep the implementation focused.

---

## Event Design Rules

- **Always unsubscribe in `OnDisable`** if you subscribed in `OnEnable`. The EventBus has
  static lifetime — a forgotten unsubscribe leaks for the application lifetime.
- **Call `Clear()` on scene transitions** if your architecture does not guarantee all
  subscribers unsubscribe themselves.
- **Do not rely on invocation order.** Handlers are called in registration order, but this
  is an implementation detail — never design systems that depend on it.
- **Keep events small.** Pass IDs for complex data and let subscribers fetch what they need.

