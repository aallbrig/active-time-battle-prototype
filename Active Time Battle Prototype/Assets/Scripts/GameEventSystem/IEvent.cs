namespace GameEventSystem
{
    public interface IEvent<in T>
    {
        void Raise();
        void RegisterListener(T listener);
        void UnregisterListener(T listener);
    }
}