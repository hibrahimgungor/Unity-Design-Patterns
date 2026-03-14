namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Fired when a quest becomes active.
    /// Carries no data — the notification itself is the meaningful signal.
    /// </summary>
    public struct QuestStartedEvent : IEvent { }
}
