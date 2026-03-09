using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates basic usage of <see cref="BasicSingleton"/>.
    /// Attach this component to any GameObject in the scene to run the example.
    /// </summary>
    public class BasicSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            var instanceA = BasicSingleton.Instance;
            var instanceB = BasicSingleton.Instance;

            Debug.Log($"Same instance: {ReferenceEquals(instanceA, instanceB)}");
        }
    }
}
