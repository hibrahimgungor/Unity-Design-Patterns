using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// A concrete game configuration asset that extends <see cref="ScriptableObjectSingleton{T}"/>.
    /// Create the asset via Assets > Create > Singletons > GameConfig and save it to
    /// Assets/Resources/Singletons/GameConfig.asset.
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Singletons/GameConfig")]
    public class GameConfig : ScriptableObjectSingleton<GameConfig>
    {
        [Header("Player")]
        public float playerSpeed = 5f;
        public int   maxHealth   = 100;
        public float jumpForce   = 8f;

        [Header("Enemy")]
        public float enemySpeed  = 2f;
        public int   enemyDamage = 10;

        [Header("Game")]
        public int   maxLives             = 3;
        public float difficultyMultiplier = 1f;
    }
}
