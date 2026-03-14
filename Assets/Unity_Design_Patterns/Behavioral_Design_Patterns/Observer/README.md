# Unity Observer Pattern
### `Unity_Design_Patterns > Behavioral_Design_Patterns > Observer`

## What is the Observer Pattern?

The Observer is a behavioral design pattern that defines a one-to-many dependency between objects.
When one object (the **subject**) changes state, all of its dependents (the **observers**) are
notified and updated automatically.

In plain terms: something happens, and everyone who cares about it gets told — without the source
needing to know who is listening.

```
┌──────────────────┐          ┌─────────────────┐
│   <<interface>>  │          │  <<interface>>  │
│    ISubject      │          │    IObserver    │
│──────────────────│          │─────────────────│
│ AddObserver()    │          │ OnNotify()      │
│ RemoveObserver() │          └─────────────────┘
│ NotifyObservers()│
└──────────────────┘
```

**Why does this matter in Unity?**
Games are full of systems that need to react to the same event without knowing about each other.
When a quest is completed, the UI needs to update, the audio system needs to play a sound, and
the reward system needs to grant items. Connecting these systems directly creates tight coupling.
Every new listener requires modifying QuestSystem. Every removed listener risks a null reference.

The Observer pattern solves this by inverting the dependency. QuestSystem knows nothing about
who is listening. Listeners register themselves and respond independently.

**The trade-off:**
Observers introduce indirection. When a notification is sent, it is not immediately obvious
from the subject's code what will happen. Debugging a chain of observers requires tracking
subscriptions across multiple classes. Unsubscribing at the wrong time — or forgetting to
unsubscribe — leads to bugs that are hard to reproduce. Observer is a tool, not a default.
Use it where decoupling genuinely matters.

---

## The Example: Quest System

All examples in this folder use the same scenario: a **QuestSystem** that broadcasts events
when a quest starts, completes, or fails, and a set of independent systems that respond to it.

**Why Quest System?**
- It has one clear publisher and multiple independent listeners — the ideal shape for Observer.
- The listeners (UI, Audio, Reward) have no reason to know about each other.
- It scales naturally: adding a new listener never requires touching QuestSystem.
- It exposes the static event problem clearly when a second player is introduced.

**Who is listening?**

| Observer | Response |
|----------|----------|
| QuestUI | Updates the quest log on screen |
| QuestAudio | Plays a quest completion sound |
| QuestReward | Grants XP to the player |

Each observer is independent. QuestSystem holds no reference to any of them.

**Event data across all examples:**

Every example uses the same three events with the same data contracts:

| Event | Data | Meaning |
|-------|------|---------|
| QuestStarted | — | A quest became active. No payload needed. |
| QuestCompleted | `QuestData` (QuestId, RewardXP, QuestName) | A quest was successfully completed. |
| QuestFailed | `int questId` | A quest was failed. ID identifies which one. |

This progression — no data, primitive data, custom struct data — is intentional.
It demonstrates how each Observer implementation handles increasingly rich payloads.

---

## Prerequisites — C# Delegates and Events

Before reading the code examples, a basic understanding of C#'s delegate and event system
is essential. Starting from Example 02, the classic IObserver/ISubject interfaces are no
longer used — C#'s own language features replace the manual observer contract. Skipping
this section will make the transition between examples harder to follow.

The sections below cover the essentials. For a deeper understanding, see the
**Delegates and Events** resources at the bottom of this file.

### What is a delegate?

A delegate is a type that holds a reference to a method. It defines the signature that any
assigned method must match — the return type and the parameter list.

```csharp
// Declare a delegate type
public delegate void QuestCompletedHandler();

// Create an instance and assign a method
QuestCompletedHandler handler = OnQuestCompleted;

// Invoke it
handler();
```

Delegates can hold multiple methods at once (multicast):

```csharp
handler += AnotherMethod;
handler(); // Both methods are called
handler -= AnotherMethod;
```

### What is an event?

An event is a delegate field wrapped with a protection layer. Without the `event` keyword,
any external class can invoke the delegate directly or replace all its subscribers by assigning
to it with `=`. The `event` keyword prevents both.

```csharp
// Without event — dangerous
public QuestCompletedHandler OnQuestCompleted;
// Any external class can do this:
questSystem.OnQuestCompleted = null; // wipes all subscribers
questSystem.OnQuestCompleted();      // invokes from outside

// With event — protected
public event QuestCompletedHandler OnQuestCompleted;
// External classes can only do this:
questSystem.OnQuestCompleted += MyMethod;
questSystem.OnQuestCompleted -= MyMethod;
// Invocation is restricted to the declaring class only
```

**Rule of thumb:** always use `event` when exposing a delegate that others subscribe to.

### Action and Func

C# provides built-in generic delegate types so you rarely need to declare your own.

`Action` — a delegate that returns void:

```csharp
Action onQuestCompleted;            // no parameters
Action<int> onQuestCompleted;       // one parameter
Action<int, string> onEvent;        // two parameters
```

`Func` — a delegate that returns a value. The last type parameter is always the return type:

```csharp
Func<int> getScore;          // returns int, no parameters
Func<int, bool> isValid;     // takes int, returns bool
```

For game events, `Action` covers almost every case. `Func` is rarely needed in Observer
scenarios because observers react to notifications — they do not return values to the subject.

### EventHandler

`EventHandler` is .NET's standard delegate type for events, defined as:

```csharp
public delegate void EventHandler(object sender, EventArgs e);
public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);
```

It follows the convention of passing the sender and event arguments separately. You will
encounter it in .NET libraries and WinForms/WPF code. In Unity, it is rarely used because
`Action<T>` is more concise and does not require subclassing `EventArgs`. It is worth
knowing it exists — and knowing why Unity developers tend to reach for `Action` instead.

---

## Repository Structure

Each example lives in its own folder with a dedicated README and a Scripts directory.
Example_05_EventChannel is further divided into numbered sub-implementations.

```
Observer/
├── README.md
├── Example_00_NoPattern/
│   ├── README_Example00_NoPattern.md
│   ├── 01_DirectCall/Scripts/
│   └── 02_Polling/Scripts/
├── Example_01_ClassicObserverPattern/
│   ├── README_Example01_ClassicObserverPattern.md
│   └── Scripts/
├── Example_02_CSharpEvent/
│   ├── README_Example02_CSharpEvent.md
│   └── Scripts/
├── Example_03_UnityEvent/
│   ├── README_Example03_UnityEvent.md
│   └── Scripts/
├── Example_04_StaticEvent/
│   ├── README_Example04_StaticEvent.md
│   └── Scripts/
├── Example_05_EventChannel/
│   ├── README_Example05_EventChannel.md
│   ├── 01_CSharpEvent/
│   └── 02_ScriptableObject/
│       ├── 01_DirectSubscription/
│       └── 02_ListenerComponent/
└── Example_06_EventBus/
    ├── README_Example06_EventBus.md
    └── Scripts/
```

---

## Why Does Each Step Exist?

| Example | Key Concept | What It Shows | What It Introduces |
|---------|-------------|---------------|-------------------|
| [00 — No Pattern](Example_00_NoPattern/README_Example00_NoPattern.md) | Direct call, Polling | The problems Observer solves | Tight coupling, per-frame overhead, reset timing |
| [01 — Classic Observer Pattern](Example_01_ClassicObserverPattern/README_Example01_ClassicObserverPattern.md) | ISubject / IObserver interfaces | Pattern shape, language-agnostic | No data, single entry point, public NotifyObservers |
| [02 — C# Event](Example_02_CSharpEvent/README_Example02_CSharpEvent.md) | `Action<T>`, `event` keyword | Multiple event types, typed data | Inspector reference still required |
| [03 — UnityEvent](Example_03_UnityEvent/README_Example03_UnityEvent.md) | `UnityEvent<T>`, persistent listeners | Inspector-driven subscriptions | Reference still needed for runtime listeners |
| [04 — Static Event](Example_04_StaticEvent/README_Example04_StaticEvent.md) | `static event` | No Inspector reference needed | All instances share one event, SOLID violations |
| [05 — EventChannel / 01 — C# Event](Example_05_EventChannel/README_Example05_EventChannel.md) | Static generic channel, `IEvent` constraint | Class reference eliminated entirely | Static lifetime, no exception isolation |
| [05 — EventChannel / 02 — ScriptableObject / 01 — Direct Subscription](Example_05_EventChannel/README_Example05_EventChannel.md) | ScriptableObject asset as channel | Asset-based decoupling, scene-persistent channels | Custom data types via typed channel classes |
| [05 — EventChannel / 02 — ScriptableObject / 02 — Listener Component](Example_05_EventChannel/README_Example05_EventChannel.md) | EventListener + UnityEvent bridge | Designer-friendly, no subscription code | Inspector wiring, harder to trace in code |
| [06 — EventBus](Example_06_EventBus/README_Example06_EventBus.md) | Centralized `Dictionary<Type, Delegate>` | Single entry point for all events | Runtime dictionary lookup, centralized management |

---

## The Real Problem Observer Solves

A common point of confusion when learning Observer is this:

> "You keep saying loose coupling — but I still need a reference to the subject to subscribe. How is that decoupled?"

This is a legitimate observation. Observer does not eliminate references. It changes the
**direction and nature** of the dependency.

### What Observer actually removes

Without Observer, a subject knows every concrete system that reacts to it:

```
PlayerHealth
 ├── UIHealthBar.UpdateHealth()
 ├── SoundSystem.PlayDamage()
 ├── AchievementSystem.CheckDamage()
 └── Analytics.TrackDamage()
```

With Observer, the subject knows only the interface:

```
PlayerHealth → IObserver (notify all)
```

The coupling goes from `Subject → N concrete systems` to `Subject → one interface`.
That is the coupling Observer removes — the subject's knowledge of its dependents.

### What Observer does not remove

The observer still needs to know the subject to subscribe:

```
playerHealth.AddObserver(this);  // Observer → Subject
```

This reference is real. Observer does not pretend otherwise. What it eliminates is the
**reverse dependency** — `Subject → Observer`. After registration, the subject broadcasts
to whoever is listening without knowing who that is.

### The original problem Observer was designed for

The GoF pattern was conceived as a solution to **polling**:

```csharp
// Polling — checking every frame for a change
void Update()
{
    if (playerHealth.HasChanged)
        UpdateUI();
}
```

Observer replaces this with a **push model** — the subject notifies observers when a change
occurs, rather than observers checking continuously. This is the event-driven model.

### The fan-out problem

Observer's real strength is one-to-many notification:

```
Enemy died
 → ScoreSystem
 → QuestSystem
 → SoundSystem
 → ParticleSystem
 → UISystem
```

Adding a new reaction requires no changes to the subject. Each new observer registers itself.
This is the fan-out problem Observer is designed for — not reference management.

### What actually solves the reference problem

Removing the observer's reference to the subject is a different concern, addressed by
different patterns. This is exactly what Example 05 onwards demonstrates:

| Pattern | How it removes the reference |
|---------|------------------------------|
| Event Channel | Both sides reference a shared channel, not each other |
| ScriptableObject Channel | Both sides reference a shared asset |
| EventBus | Both sides reference a central registry |

### The key distinction

Observer is **notification decoupling** — the subject does not know who reacts to its events.
It is not **reference decoupling** — observers still need a way to find the channel they subscribe to.

Recognising this distinction is what separates a surface-level understanding of the pattern
from one that informs architectural decisions.

---

## General Rules

- **Subscription lifecycle depends on intent, not convention.** `OnEnable` / `OnDisable` is
  the right choice when a disabled GameObject should stop receiving notifications. `Awake` /
  `OnDestroy` is appropriate when the object should remain subscribed regardless of its active
  state. Choose based on the actual lifecycle requirement — not habit.
- Do not subscribe to the same event more than once. Adding the same method twice results
  in it being called twice per notification.
- Iterate the observer list in reverse when notifying. If an observer unsubscribes during
  notification, iterating forward causes an index shift and skips the next observer.
- `NotifyObservers` being public is a classic observer interface constraint, not a design choice.
  In C# event-based implementations this problem disappears — only the declaring class
  can invoke its own events.
- Always unsubscribe when the observer is done. Forgetting to unsubscribe keeps the observer
  in the subject's list after the GameObject is disabled or destroyed, causing notifications
  to reach dead objects.

---

# Closures and Events

A closure is a function that captures variables from its enclosing scope.
In C#, lambdas create closures. When used with events, closures introduce
two major problems: a hidden heap allocation and an unsubscribe bug.

---

## 1. What is a Closure?

```csharp
int x = 10;

Action print = () => Debug.Log(x);

x = 20;

print(); // Output: 20
```

The lambda captures the **variable** `x` — not its value at the time of creation.
When `x` changes, the lambda sees the updated value.

The compiler transforms this into a hidden class on the heap:

```csharp
// Compiler-generated
class <>c__DisplayClass
{
    public int x;
    public void Print() => Debug.Log(x);
}

var closure = new <>c__DisplayClass();
closure.x = 10;
Action print = closure.Print;
closure.x = 20;
print(); // 20
```

The captured variable is moved from the stack into a **heap-allocated object**.
This happens silently — no allocation is visible at the call site.

---

## 2. The Unsubscribe Problem

```csharp
// Subscribe
_questSystem.OnQuestCompleted += () => HandleQuestCompleted();

// Unsubscribe attempt — does nothing
_questSystem.OnQuestCompleted -= () => HandleQuestCompleted();
```

Each lambda expression creates a **new delegate instance**.
Two lambdas that look identical are different objects:

```csharp
var a = () => Debug.Log("done");
var b = () => Debug.Log("done");

bool same = a == b; // false
```

The `-=` operator compares by reference. Since the references are different,
the subscription is never removed. This causes:

- Memory leaks
- The handler firing multiple times if subscribed repeatedly
- Hard-to-reproduce bugs

**Fix — use named methods:**

```csharp
// Subscribe
_questSystem.OnQuestCompleted += HandleQuestCompleted;

// Unsubscribe — works correctly, same reference
_questSystem.OnQuestCompleted -= HandleQuestCompleted;
```

**Fix — if a lambda is unavoidable, store the reference:**

```csharp
Action<QuestData> _handler;

void OnEnable()
{
    _handler = data => HandleQuestCompleted(data);
    _questSystem.OnQuestCompleted += _handler;
}

void OnDisable()
{
    _questSystem.OnQuestCompleted -= _handler;
}
```

---

## 3. The Hidden Allocation Problem

When a lambda captures a variable, the compiler generates a hidden heap object
to hold it — even if the original variable was a value type on the stack.

```csharp
int rewardXP = 100; // lives on the stack

_questSystem.OnQuestCompleted += () =>
{
    Debug.Log($"Reward: {rewardXP}"); // rewardXP captured
};
```

The compiler generates:

```csharp
class <>c__DisplayClass
{
    public int rewardXP; // int now lives on the heap
}

var closure = new <>c__DisplayClass();
closure.rewardXP = rewardXP;
_questSystem.OnQuestCompleted += () => Debug.Log($"Reward: {closure.rewardXP}");
```

As long as the event holds a reference to the delegate, the hidden object
**cannot be garbage collected** — even after the original variable goes out of scope.

If multiple closures capture variables from the same scope, the compiler may merge
them into a single hidden class — keeping all captured variables alive as long as
any one of the delegates remains subscribed.

---

## 4. When Both Problems Combine

The two problems compound when a lambda both captures a variable and loses its reference:

```csharp
void Start()
{
    int questId = GetCurrentQuestId();

    // Closure created, heap allocated, reference lost — impossible to unsubscribe
    EventBus.Subscribe<QuestCompletedEvent>(e =>
    {
        if (e.QuestId == questId) HandleCompletion(e);
    });
}
```

The heap object holding `questId` lives for the application lifetime.
There is no way to unsubscribe because the lambda reference was never stored.

**Fix — named method with a field:**

```csharp
private int _questId;

void OnEnable()
{
    _questId = GetCurrentQuestId();
    EventBus.Subscribe<QuestCompletedEvent>(HandleQuestCompleted);
}

void OnDisable()
{
    EventBus.Unsubscribe<QuestCompletedEvent>(HandleQuestCompleted);
}

private void HandleQuestCompleted(QuestCompletedEvent e)
{
    if (e.QuestId == _questId) HandleCompletion(e);
}
```

No closure, no hidden allocation, unsubscribe works correctly.

---

## 5. A Common Unity UI Mistake

The same problem appears frequently with UI buttons:

```csharp
// Subscribe
button.onClick.AddListener(() => OpenPanel(panelId));

// Unsubscribe attempt — does nothing
button.onClick.RemoveListener(() => OpenPanel(panelId));
```

**Fix:**

```csharp
void OnEnable() => button.onClick.AddListener(OpenPanel);
void OnDisable() => button.onClick.RemoveListener(OpenPanel);
void OpenPanel() => OpenPanel(_panelId);
```

---

## Summary

| | Named Method | Stored Lambda | Inline Lambda |
|---|---|---|---|
| Unsubscribe works | ✅ | ✅ | ❌ |
| Heap allocation | ❌ | ⚠️ if captures | ⚠️ if captures |
| Recommended | ✅ | Only when necessary | Never |

This applies to all event systems in these examples: C# events, UnityEvents,
EventChannel, and EventBus.

---

## Resources

### Delegates and Events

**Articles**
- [C# Delegates — tutorialsteacher.com](https://www.tutorialsteacher.com/csharp/csharp-delegates) — Clear, practical introduction to delegates, multicast delegates, and how they relate to events.

**Video**
- [[C#] Delegates and Events (E01: delegates)](https://youtu.be/G5R4C8BLEOc)
- [[C#] Delegates and Events (E02: events)](https://youtu.be/TdiN18PR4zk)
- [Delegates, Events, and Closures in Unity — Finally Explained](https://youtu.be/za91AjX-V7M)

---

### Observer Pattern

**Video**
- [Observer Pattern – Design Patterns (ep 2)](https://youtu.be/vgpizRaJ0gs) — Theory only. Covers the intent of the pattern, the publish-subscribe relationship, and why decoupling listeners from publishers matters at an architectural level.
- [How to use the OBSERVER pattern in Unity](https://youtu.be/Yy7Dt2usGy0) — Practical walkthrough of implementing Observer in Unity with C# events. Covers subscription management, unsubscribe hygiene, and common mistakes.
- [Game Architecture with Scriptable Objects — Ryan Hipple, Unite Austin 2017](https://youtu.be/raQ3iHhE_Kk) — Introduces ScriptableObject-based event channels as an alternative to direct references and static events. Directly relevant to Example 05.
- [Event Bus & Scriptable Object Event Channels | Unity Game Architecture Tutorial](https://youtu.be/95eFgUENnTc?si=Ij3_KIV9izbZRRzb) — Covers both EventBus and ScriptableObject EventChannel patterns. Directly relevant to Examples 05 and 06.
- [Unleashing the Power of Event Channels in Unity](https://youtu.be/h8ZAOWY_5LA?si=NMGAOwOGbCPbgwng) — Deep dive into EventChannel architecture in Unity. Directly relevant to Example 05.
- [Learn to Build an Advanced Event Bus | Unity Architecture](https://youtu.be/4_DTAnigmaQ?si=OR-maXfZQ9CFT_8F) — Builds a production-grade EventBus step by step. Directly relevant to Example 06.

**Articles**
- [Refactoring Guru — Observer](https://refactoring.guru/design-patterns/observer) — Intent, UML, pros and cons, and code examples across multiple languages.
- [Game Programming Patterns — Observer](https://gameprogrammingpatterns.com/observer.html) — Robert Nystrom's take on the Observer pattern, covering the push/pull model, decoupling, and common pitfalls.
- [Create modular game architecture with ScriptableObjects — Unity](https://unity.com/resources/create-modular-game-architecture-scriptableobjects-unity-6) — Unity's official guide. Directly relevant to Example 05.

**Further Reading — Advanced EventBus**
- [Unity-Event-Bus — adammyhre](https://github.com/adammyhre/Unity-Event-Bus) — A production-grade EventBus implementation with additional patterns and techniques beyond what is covered in Example 06. The repository README contains links to further advanced EventBus examples and references.
