# Example 02 — C# Event
### `Observer > Example_02_CSharpEvent`

## What Changed From Classic Observer Pattern?

The IObserver/ISubject interfaces are no longer used — C#'s event system takes their place.
The single `OnNotify()` entry point is replaced by three separate channels: `OnQuestStarted`,
`OnQuestCompleted`, and `OnQuestFailed`. Each is a typed `Action`. Observers subscribe to
exactly the events they care about.

`QuestAudio` subscribes only to `OnQuestStarted` and `OnQuestCompleted` — it has no reason
to react to a failed quest. `QuestReward` subscribes only to `OnQuestCompleted`. This
selectivity was impossible in the classic observer approach.

---

## Data — Action\<T\>

`OnQuestCompleted` carries `QuestData` directly via `Action<QuestData>`. No cast, no pull —
the data arrives with the notification. `OnQuestFailed` carries `int questId` via `Action<int>`.
`OnQuestStarted` carries no data — `Action` with no type parameter.

```
Classic Observer pull:  Observer ──(cast)──▶ QuestSystem.LastQuestData
C# Event push:          QuestSystem ──(QuestData)──▶ Observer
```

Each event type carries exactly the data its subscribers need — no more, no less.

---

## What It Shows

How C# events solve the single entry point and multiple event type problems natively,
and why the manual IObserver/ISubject contract is rarely necessary in C#.

---

## What It Introduces

The reference problem remains. Observers still need a direct reference to `QuestSystem`
to subscribe — Inspector assignment is still required.
