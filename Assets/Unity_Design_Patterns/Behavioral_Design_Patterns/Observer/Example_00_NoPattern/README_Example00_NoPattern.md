# Example 00 — No Pattern
### `Observer > Example_00_NoPattern`

## Why This Example Exists

Before introducing any pattern, it helps to see the problem it solves. This example
implements the same Quest System — QuestUI, QuestAudio, QuestReward — without Observer.
Two approaches are shown, each with its own failure mode.

---

## 01 — Direct Call

QuestSystem holds a reference to every system that reacts to it and calls their methods
directly on each event.

```
QuestSystem
 ├── _questUI.OnQuestCompleted(data)
 ├── _questAudio.OnQuestCompleted(data)
 └── _questReward.OnQuestCompleted(data)
```

**What breaks when the project grows:**
With three listeners this looks perfectly fine. The problem surfaces as the project scales.
Every system that needs to react to a quest event — analytics, achievements, save, particles,
daily challenges — requires opening QuestSystem and adding another line.

**This violates two principles:**
- **Open/Closed Principle** — QuestSystem must be modified every time a new dependent is added.
  It is never closed for modification.
- **Single Responsibility Principle** — QuestSystem is responsible for quest logic and for
  knowing which systems react to it. These are two separate concerns.

```
QuestSystem
 ├── _questUI.OnQuestCompleted()
 ├── _questAudio.OnQuestCompleted()
 ├── _questReward.OnQuestCompleted()
 ├── _analyticsSystem.OnQuestCompleted()
 ├── _achievementSystem.OnQuestCompleted()
 ├── _saveSystem.OnQuestCompleted()
 ├── _particleSystem.OnQuestCompleted()
 └── _dailyChallengeSystem.OnQuestCompleted()
```

QuestSystem stops being a quest system and becomes an orchestrator — a class that knows
every other system in the game that cares about quests.

There is also a collaboration problem. If `AchievementSystem` is written by a different
team member, they must open and modify QuestSystem to wire up their reaction. This creates
merge conflicts and means QuestSystem is changed by people who have no reason to own it.

**With Observer**, `AchievementSystem` registers itself:
```csharp
_questSystem.OnQuestCompleted += HandleQuestCompleted;
```
QuestSystem is never touched. New behaviour is added without modifying existing code.

---

## 02 — Polling

QuestSystem exposes its state as public boolean flags. Each observer checks those flags
every frame in its own `Update()` loop and reacts when a change is detected.

```csharp
// QuestUI — runs every frame
void Update()
{
    if (_questSystem.QuestCompleted)
        Debug.Log($"QuestUI: Quest '{_questSystem.LastQuestData.QuestName}' completed.");
}
```

**What breaks:**

Polling runs every frame regardless of whether anything changed. With many observers
this becomes a measurable overhead — each one waking up, checking a flag, finding nothing,
going back to sleep.

The reset problem is more subtle. After reacting to a flag, it must be cleared — otherwise
every observer fires again on the next frame. But who clears it, and when? In this example
`QuestSystemTester` owns the reset, calling `ResetFlags()` in `LateUpdate` — not `Update`.
This ordering matters: if reset happened inside the same `Update` call that set the flag,
observers whose `Update` runs later in the same frame would miss the change entirely.
`LateUpdate` guarantees all `Update` calls across all components complete first.

But this only defers the problem — it does not solve it. In a real project the reset
responsibility has no natural owner. If `QuestSystemTester` is removed or replaced,
the reset must be moved somewhere else. The ownership question never goes away.

**Observer replaces both problems at once:**
- No per-frame polling — the subject pushes a notification exactly once when a change occurs.
- No reset logic — each notification is a discrete call, not a persistent flag.
