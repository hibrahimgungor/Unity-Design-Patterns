using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// An example manager that inherits <see cref="GenericSingleton{T}"/> to handle audio.
    /// Demonstrates how a single line of inheritance is sufficient to gain full Singleton behaviour.
    /// </summary>
    public class AudioManager : GenericSingleton<AudioManager>
    {
        public float MasterVolume { get; private set; } = 1f;

        public void SetVolume(float volume)
        {
            MasterVolume = Mathf.Clamp01(volume);
            Debug.Log($"[AudioManager] Volume: {MasterVolume}");
        }
    }
}
