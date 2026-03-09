using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// An example manager that inherits <see cref="GenericSingleton{T}"/> to handle game flow.
    /// Demonstrates how a single line of inheritance is sufficient to gain full Singleton behaviour.
    /// Always call base.Awake() when overriding Awake.
    /// </summary>
    public class GameManager : GenericSingleton<GameManager>
    {
        public int Score { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Score = 0;
        }

        public void AddScore(int amount)
        {
            Score += amount;
            Debug.Log($"[GameManager] Score: {Score}");
        }
    }
}
