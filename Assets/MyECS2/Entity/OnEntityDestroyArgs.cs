namespace MyECS2
{
    public class OnEntityDestroyArgs
    {
        public OnEntityDestroyArgs(ref EntityObject entity)
        {
            entityObject = entity;
        }
        public EntityObject entityObject;
    }
}
