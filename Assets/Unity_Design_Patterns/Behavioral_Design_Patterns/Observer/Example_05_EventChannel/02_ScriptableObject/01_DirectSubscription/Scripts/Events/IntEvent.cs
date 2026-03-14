namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._01_DirectSubscription
{
    /// <summary>
    /// Event struct for notifications that carry a single integer value.
    ///
    /// The QuestCompletedEvent asset uses this type as its channel —
    /// the int Value field carries the RewardXP amount to all subscribers.
    ///
    /// The field is named Value deliberately — it is generic by design.
    /// The asset name (QuestCompletedEvent) provides the semantic context.
    /// The same IntEvent struct can be reused for any channel that needs
    /// to deliver a single integer: score changes, damage amounts, level numbers.
    /// </summary>
    public struct IntEvent : IEvent
    {
        public int Value;
    }
}
