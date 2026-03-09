using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates reading configuration data from <see cref="GameConfig"/>.
    /// Requires GameConfig.asset to exist at Assets/Resources/Singletons/GameConfig.asset.
    /// Attach to any GameObject in the scene to run the example.
    /// </summary>
    public class ScriptableObjectSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            var config = GameConfig.Instance;

            Debug.Log($"[GameConfig] Player speed:  {config.playerSpeed}");
            Debug.Log($"[GameConfig] Max health:    {config.maxHealth}");
            Debug.Log($"[GameConfig] Difficulty:    {config.difficultyMultiplier}");
        }
    }
}
