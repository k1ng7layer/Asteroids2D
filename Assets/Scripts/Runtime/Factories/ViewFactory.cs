namespace AsteroidsECS
{
    public abstract class ViewFactory
    {
        public abstract PlayerView SpawnedPlayerObject { get; protected set; }
        public abstract ITransformableView GetTransformableView();
    }
}
