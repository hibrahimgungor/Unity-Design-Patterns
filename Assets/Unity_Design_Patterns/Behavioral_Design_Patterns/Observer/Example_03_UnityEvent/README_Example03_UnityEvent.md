# Example 03 — UnityEvent
### `Observer > Example_03_UnityEvent`

## What Changed From C# Event?

The same Quest System rebuilt with `UnityEvent` instead of C# events. UnityEvent is Unity's
own serializable event type — built on top of C# delegates, not a separate mechanism.

The defining feature of UnityEvent is Inspector visibility. Listeners can be assigned
directly in the Inspector without writing a single line of subscription code. This makes
it a natural fit for designer-driven workflows and rapid prototyping.

---

## Two Subscription Modes

**Persistent listener — QuestUI:**
Assigned directly in the Inspector, no subscription code required. The binding is serialized
with the scene and survives play mode changes. Renaming the handler method silently breaks
the Inspector binding with no compile error.

**Runtime listener — QuestAudio, QuestReward:**
Added via `AddListener()` in code — functionally identical to the C# event `+=` syntax.
Not serialized. Must be removed manually via `RemoveListener()`.

---

## The Mixed Subscription Problem

UnityEvent supports both persistent (Inspector) and runtime (code) listeners on the same event.
This creates a hidden complexity that is easy to overlook.

When a bug appears — a handler firing twice, a handler not firing at all, or a handler
firing after the object is destroyed — the cause may be in the Inspector, in the code,
or in both. There is no single place to look.

```
OnQuestCompleted
 ├── [Inspector] QuestUI.HandleQuestCompleted     ← persistent, serialized with scene
 ├── [Code]      QuestAudio.HandleQuestCompleted  ← runtime, added in OnEnable
 └── [Code]      QuestReward.HandleQuestCompleted ← runtime, added in OnEnable
```

Persistent listeners are invisible to the IDE — they do not appear in Find References,
they are not caught by rename refactoring, and they leave no trace in the script files.
A developer reading the code sees only the runtime listeners. The persistent listeners
exist only in the serialized scene data.

This dual-source nature means:
- A method rename breaks persistent listeners silently with no compile error.
- Removing a component from a GameObject does not remove its persistent listener bindings.
- Debugging requires checking both the Inspector and the code simultaneously.
- Code reviews cannot capture the full subscription picture.

**Recommendation:** pick one subscription mode per event and stick to it. Mixing persistent
and runtime listeners on the same event is a frequent source of hard-to-reproduce bugs.

---

## UnityEvent\<T\> and Serialization

As of Unity 2020, `UnityEvent<T>` can be declared and serialized directly without subclassing:

```csharp
public UnityEvent<int> OnQuestFailed;
public UnityEvent<QuestData> OnQuestCompleted;
```

Both appear in the Inspector and accept persistent listeners without a `[System.Serializable]`
wrapper class. Older Unity versions required subclassing — if you encounter that pattern in
legacy codebases, that is why.

Note that complex types like `QuestData` cannot be entered as static values in the Inspector.
Persistent listeners work best with primitives (`int`, `float`, `string`). For custom structs
or classes, use runtime listeners via `AddListener()` — they handle all data types without
restriction.

---

## UnityAction

`UnityAction` is Unity's own delegate type — the Unity equivalent of `Action`. Always
returns void. Always store the reference before passing to `AddListener` — passing a
lambda directly makes `RemoveListener` ineffective:

```csharp
// WRONG — lambda creates a new reference every time, RemoveListener won't work
_questSystem.OnQuestCompleted.AddListener(data => HandleQuestCompleted(data));

// CORRECT — store the method reference
_questSystem.OnQuestCompleted.AddListener(HandleQuestCompleted);
_questSystem.OnQuestCompleted.RemoveListener(HandleQuestCompleted);
```

---

## UnityEvent Parameter Limits

UnityEvent supports up to four type parameters: `UnityEvent<T0, T1, T2, T3>`.
Beyond four parameters, wrap the data in a class or struct instead.

---

## Trade-offs vs C# Event

- Inspector visibility is a major advantage for designer-driven workflows.
- UnityEvent carries more overhead than a plain C# event — avoid in performance-critical paths.
- Persistent listeners create implicit dependencies — a method rename silently breaks
  the Inspector binding with no compile error.
- Runtime listeners must be removed manually via `RemoveListener`. Forgetting carries
  the same memory leak risk as forgetting to unsubscribe from a C# event.
- Mixing persistent and runtime listeners on the same event creates a dual-source debugging
  problem — see The Mixed Subscription Problem above.

---

## What It Introduces

The reference problem remains for runtime listeners. Persistent listeners solve the
reference problem for the Inspector — but introduce a different coupling: the scene now
depends on specific method names staying stable.
