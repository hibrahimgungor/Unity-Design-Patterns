using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// A persistent generic Singleton that creates its own GameObject if none exists
    /// in the scene. The instance is never null as long as the application is running.
    ///
    /// <para><b>Lazy creation:</b> On first access, the getter searches the scene.
    /// If no instance is found, a new GameObject named "[Singleton] TypeName" is
    /// instantiated and registered with DontDestroyOnLoad automatically.</para>
    ///
    /// <para><b>AutoUnparentOnAwake:</b> Ensures DontDestroyOnLoad works correctly
    /// when the component is part of a nested prefab hierarchy.</para>
    ///
    /// <para><b>OnInitialize:</b> Called once inside Awake after the instance is confirmed.
    /// Override this instead of Awake to perform subclass setup without repeating
    /// the Singleton guard logic.</para>
    ///
    /// <para><b>OnShutdown:</b> Called from OnApplicationQuit. Override to flush data,
    /// unsubscribe events, or release resources before the process exits.</para>
    ///
    /// <para><b>Quit guard:</b> Once OnApplicationQuit fires, Instance returns null
    /// and HasInstance returns false. This prevents Unity from creating a new
    /// GameObject during the teardown phase, which would produce a leaked object
    /// and a console warning.</para>
    ///
    /// <para><b>OnDestroy:</b> Clears the static reference and sets the quit flag so
    /// that any lingering references do not cause a re-creation after destruction.</para>
    ///
    /// Use when: Systems that may be needed at any point during runtime and should
    /// not require manual scene setup (e.g. EventManager, PoolManager, LogService).
    /// Always call base.Awake() when overriding Awake in a subclass.
    /// </summary>
    public abstract class LazyPersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool AutoUnparentOnAwake = true;

        private static T _instance;
        private static bool _isQuitting;

        public static bool HasInstance => _instance != null && !_isQuitting;
        public static T TryGetInstance() => HasInstance ? _instance : null;

        public static T Instance
        {
            get
            {
                if (_isQuitting)
                {
                    Debug.LogWarning($"[{typeof(T).Name}] Instance requested during application quit. Returning null.");
                    return null;
                }

                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();

                    if (_instance == null)
                    {
                        var go = new GameObject($"[Singleton] {typeof(T).Name}");
                        _instance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (AutoUnparentOnAwake)
                transform.SetParent(null);

            if (_instance != null && _instance != this as T)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            OnInitialize();
        }

        protected virtual void OnInitialize() { }

        private void OnApplicationQuit()
        {
            _isQuitting = true;
            OnShutdown();
        }

        protected virtual void OnShutdown() { }

        protected virtual void OnDestroy()
        {
            if (_instance == this as T)
            {
                _instance = null;
                _isQuitting = true;
            }
        }
    }
}
