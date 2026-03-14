# Example 04 — Static Event
### `Observer > Example_04_StaticEvent`

## What Changed From C# Event?

Events are marked `static`. Observers subscribe directly via the class name — no Inspector
reference needed. For a single QuestSystem in the scene this works without issue.

---

## Where It Breaks

Add a second QuestSystem, QuestUI, QuestAudio, and QuestReward to the scene.
Press C to complete Player 1's quest. The console shows:

```
QuestSystem [QuestSystem1]: Quest completed.
QuestUI [QuestUI1]: Quest completed. Earned 100 XP.
QuestUI [QuestUI2]: Quest completed. Earned 100 XP.
QuestAudio [QuestAudio1]: Playing quest complete sound.
QuestAudio [QuestAudio2]: Playing quest complete sound.
QuestReward [QuestReward1]: Granting 100 XP.
QuestReward [QuestReward2]: Granting 100 XP.
```

Only QuestSystem1 fired — but every observer in the scene responded. The static event
carries no identity. There is no way to know which QuestSystem triggered it.

---

## The Attempted Fix and Why It Fails

A natural response is to pass an identifier through the event and filter inside each observer:

```csharp
public static event Action<int, int> OnQuestCompleted; // playerID, rewardXP

private void HandleQuestCompleted(int playerID, int rewardXP)
{
    if (playerID != _myPlayerID) return;
    Debug.Log($"QuestReward: Granting {rewardXP} XP.");
}
```

This violates two SOLID principles:
- **Open/Closed Principle** — adding a third player requires touching every observer to
  ensure the filter still holds. The observers are not closed for modification.
- **Single Responsibility Principle** — QuestUI is now responsible for both updating the
  UI and deciding whether the event belongs to it. These are two separate concerns.

The real fix is to return to instance events — as in Example 02. Each QuestSystem owns
its events. QuestUI1 subscribes to QuestSystem1, QuestUI2 subscribes to QuestSystem2.
No filtering. No SOLID violations.

---

## When Static Events Are Acceptable

A single instance that is guaranteed never to be duplicated — a global GameManager,
an ApplicationLifecycle system, a SceneLoader. If multiplicity is architecturally
impossible, the static event problem never surfaces.

---

## What It Introduces

Unsubscribe hygiene becomes more critical. Static events are never garbage collected
as long as subscribers exist. A forgotten unsubscribe leaks for the entire application
lifetime, not just the scene.
