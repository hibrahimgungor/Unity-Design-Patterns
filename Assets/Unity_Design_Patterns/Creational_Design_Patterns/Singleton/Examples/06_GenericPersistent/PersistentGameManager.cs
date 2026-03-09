using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// An example manager that inherits <see cref="GenericPersistentSingleton{T}"/> to handle game flow.
    /// Survives scene transitions and remains accessible from any scene.
    /// Always call base.Awake() when overriding Awake.
    /// </summary>
    public class PersistentGameManager : GenericPersistentSingleton<PersistentGameManager>
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
            Debug.Log($"[PersistentGameManager] Score: {Score}");
        }
    }
}
