namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Fired when a quest is successfully completed.
    /// Carries all data subscribers need to react without querying any external system.
    /// </summary>
    public struct QuestCompletedEvent : IEvent
    {
        public int QuestId;
        public int RewardXP;
        public string QuestName;

        public QuestCompletedEvent(int questId, int rewardXP, string questName)
        {
            QuestId = questId;
            RewardXP = rewardXP;
            QuestName = questName;
        }
    }
}
