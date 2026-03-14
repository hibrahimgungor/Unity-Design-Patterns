namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Defines the contract for any object that maintains a list of observers
    /// and notifies them when a change occurs.
    ///
    /// This is the core publisher interface in the GoF Observer pattern.
    /// The subject owns the observer list and is solely responsible for
    /// managing subscriptions and broadcasting notifications.
    ///
    /// Key design decisions:
    /// - Observers are registered and removed by reference via <see cref="AddObserver"/>
    ///   and <see cref="RemoveObserver"/>. The subject holds no knowledge of
    ///   concrete observer types — only the <see cref="IObserver"/> contract.
    /// - <see cref="NotifyObservers"/> is kept separate from business logic.
    ///   Concrete subjects decide when to call it, not the interface itself.
    ///
    /// Open/Closed Principle:
    /// New observer types can be introduced without modifying the subject.
    /// The subject broadcasts to whoever is registered — it does not need
    /// to know what those observers do with the notification.
    /// </summary>
    public interface ISubject
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }
}
