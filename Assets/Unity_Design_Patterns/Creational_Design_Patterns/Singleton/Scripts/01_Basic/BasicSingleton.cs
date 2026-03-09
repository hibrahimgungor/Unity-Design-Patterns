namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// The most fundamental Singleton implementation using a private constructor
    /// and a static instance field. Ensures only one instance of the class exists
    /// throughout the application lifetime.
    ///
    /// Limitations:
    /// - Not thread-safe: concurrent access can create multiple instances.
    /// - No Unity lifecycle integration (not a MonoBehaviour).
    ///
    /// Use when: The instance is accessed only from the main thread and requires
    /// no Unity lifecycle callbacks (Awake, Update, coroutines).
    /// For thread-safe access, use <see cref="ThreadSafeSingleton"/> instead.
    /// </summary>
    public class BasicSingleton
    {
        private static BasicSingleton _instance;

        private BasicSingleton() { }

        public static BasicSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BasicSingleton();

                return _instance;
            }
        }
    }
}
