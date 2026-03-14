namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_03_UnityEvent
{
    /// <summary>
    /// Data payload for a completed quest.
    ///
    /// Carries all information subscribers need to react to quest completion
    /// without querying any external system.
    /// </summary>
    public struct QuestData
    {
        public int QuestId;
        public int RewardXP;
        public string QuestName;
    }
}
