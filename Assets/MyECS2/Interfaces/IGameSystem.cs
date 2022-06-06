namespace MyECS2
{
    public interface IGameSystem
    {
        void Initialize();
        void Update();
        void OnDestroy();
    }
}
