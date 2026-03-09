using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// A Singleton backed by a ScriptableObject asset instead of a MonoBehaviour.
    /// The instance is loaded from Resources on first access and cached for the
    /// duration of the session.
    ///
    /// <para><b>No scene dependency:</b> The asset lives in the project, not in a scene.
    /// It is available from the very first frame without any Awake or Start ordering concerns.</para>
    ///
    /// <para><b>Inspector editability:</b> All fields are editable in the Unity Inspector
    /// without entering Play mode, making it ideal for tuning game data.</para>
    ///
    /// <para><b>Asset placement:</b> The concrete asset must be saved at
    /// <c>Assets/Resources/Singletons/TypeName.asset</c> for the loader to find it.</para>
    ///
    /// <para><b>Limitation:</b> ScriptableObjects are not MonoBehaviours. They cannot
    /// use Update, Coroutines, or scene-lifecycle callbacks. Use this only for
    /// configuration data and stateless services.</para>
    ///
    /// Use when: Game configuration, balance data, global settings, or any data that
    /// designers need to edit and that does not require scene-level lifecycle events.
    /// </summary>
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>($"Singletons/{typeof(T).Name}");

                    if (_instance == null)
                        Debug.LogError(
                            $"[{typeof(T).Name}] Asset not found at Resources/Singletons/{typeof(T).Name}. " +
                            "Create the asset via the CreateAssetMenu and place it in that path.");
                }
                return _instance;
            }
        }
    }
}
