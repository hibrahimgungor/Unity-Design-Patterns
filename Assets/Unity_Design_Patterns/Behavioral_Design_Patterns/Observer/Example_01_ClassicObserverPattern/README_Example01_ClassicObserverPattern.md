# Example 01 — Classic Observer Pattern
### `Observer > Example_01_ClassicObserverPattern`

## What is the Classic Observer Pattern?

The purest form of the pattern, as described by the Gang of Four (GoF) in *Design Patterns:
Elements of Reusable Object-Oriented Software* (1994). Two interfaces define the contract:
`ISubject` manages the observer list, `IObserver` defines the notification method. No C#
language features beyond interfaces and lists. Any object-oriented language could implement
this identically.

```
┌──────────────────────────┐          ┌─────────────────────┐
│      <<interface>>       │          │    <<interface>>    │
│         ISubject         │          │      IObserver      │
│──────────────────────────│          │─────────────────────│
│ AddObserver()            │          │ OnNotify(ISubject)  │
│ RemoveObserver()         │          └─────────────────────┘
│ NotifyObservers()        │                    ▲
└──────────────────────────┘                    │ implements
              ▲                                 │
              │ implements          ┌────────────┴──────────┐
              │                     │       QuestUI         │
┌─────────────┴────────────┐        │       QuestAudio      │
│   QuestStartedSubject    │        │       QuestReward     │
│   QuestCompletedSubject  │        └───────────────────────┘
│   QuestFailedSubject     │
└──────────────────────────┘
```

**What it shows:** the shape of the pattern — publisher, subscriber, and the contract between them.

**What it introduces:** each event type gets its own dedicated subject. Observers register
only with the subjects they care about — `QuestReward` registers only with
`QuestCompletedSubject` and never receives start or fail notifications.

---

## One Subject Per Event Type

The GoF pattern was originally designed for a single subject with a single state change.
When a scenario has multiple distinct events — quest started, completed, failed — the
natural GoF solution is one subject per event type.

```
QuestStartedSubject   → QuestUI, QuestAudio register
QuestCompletedSubject → QuestUI, QuestAudio, QuestReward register
QuestFailedSubject    → QuestUI registers
```

This keeps each subject focused on a single responsibility. `QuestReward` subscribes only
to `QuestCompletedSubject` — there is no filtering logic inside `OnNotify`, no flags to
check, no state to manage.

---

## Pull Model — How Data is Passed

Rather than pushing data through the interface, the subject passes itself as an `ISubject`
reference. The observer casts to the concrete type and pulls whatever data it needs.
This keeps the interface generic — `IObserver` is not tied to any specific data type —
but introduces a cast at the observer side.

```
Push model:  Subject ──(data)──▶ Observer
Pull model:  Subject ──(self)──▶ Observer ──(cast + fetch)──▶ Subject
```

`QuestReward` casts the incoming `ISubject` to `QuestCompletedSubject` and reads
`LastQuestData` directly. `QuestAudio` only checks the type — it needs no data, just
confirmation that the notification came from the right subject.

**The trade-off:** the observer must know the concrete subject type to cast to. This
reintroduces a dependency — just indirectly, at runtime rather than compile time.

---

## The Cost of Multiple Subjects

Splitting events across dedicated subjects solves the single-entry-point problem — but
introduces its own costs.

**Boilerplate:** every new event type requires a new subject class. `QuestStartedSubject`,
`QuestCompletedSubject`, and `QuestFailedSubject` are nearly identical — the only
differences are the data they expose and the name. As events grow, so does the class count.

**if/else chains in observers:** `QuestUI` subscribes to all three subjects and must
identify the source in `OnNotify`:

```csharp
if (subject is QuestStartedSubject) { ... }
else if (subject is QuestCompletedSubject completed) { ... }
else if (subject is QuestFailedSubject failed) { ... }
```

This is state management leaking into the observer. The more subjects an observer listens
to, the longer this chain grows. The pattern has shifted responsibility — instead of the
subject knowing its dependents, the observer now manages its own branching logic.

**The root tension:** `IObserver` has a single `OnNotify` entry point. Whether you use
one subject with a state flag or multiple subjects with if/else chains, something somewhere
must distinguish between event types. The classic interface cannot eliminate this — it can
only move it around.

This is precisely what C# events solve: each event is its own subscription point.
Observers subscribe per event, not per subject. No if/else, no cast, no state management.

---

## On NotifyObservers Being Public

Because `NotifyObservers` is part of the `ISubject` interface, it must be public. This means
any external class can trigger a broadcast manually, bypassing the subject's own business logic.
This is a structural limitation of the classic interface approach — not a bug in the implementation.

---

## Classic Observer Limitations — Why WithData and Generic Are Not Separate Examples

Two natural extensions of the pure classic approach were considered but not implemented as
standalone examples. They reveal exactly why C# events are a better fit.

**Classic Observer with data** would add a parameter to `OnNotify`:
```csharp
void OnNotify(QuestData data);
```
This solves the missing context problem but binds the interface to a specific data type.
`IObserver` is no longer reusable across different event scenarios.

**Classic Observer with generics** would make the interface type-safe:
```csharp
interface IObserver<T> { void OnNotify(T data); }
```
This restores reusability — but introduces a new problem. A class that wants to observe both
`QuestSystem` and `EnemySystem` would need to implement `IObserver<QuestData>` and
`IObserver<EnemyData>` simultaneously. C# does not allow a class to implement the same
generic interface twice with different type parameters:

```csharp
// COMPILE ERROR
public class QuestUI : IObserver<QuestData>, IObserver<EnemyData> { }
```

Both approaches expose the same underlying tension: a single `OnNotify` method cannot cleanly
serve multiple event types or multiple subjects. C# events solve this not by extending the
interface — but by replacing the manual contract entirely. Each event is its own channel.
Observers subscribe per channel, not per subject.
