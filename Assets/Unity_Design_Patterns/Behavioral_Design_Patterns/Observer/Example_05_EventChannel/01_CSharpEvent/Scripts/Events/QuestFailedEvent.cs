namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Fired when a quest is failed.
    /// Carries the quest ID so subscribers can identify which quest failed.
    /// </summary>
    public struct QuestFailedEvent : IEvent
    {
        public int QuestId;

        public QuestFailedEvent(int questId)
        {
            QuestId = questId;
        }
    }
}
