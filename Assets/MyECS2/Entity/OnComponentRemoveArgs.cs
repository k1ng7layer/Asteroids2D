namespace MyECS2
{
    public class OnComponentRemoveArgs
    {
        public OnComponentRemoveArgs(ref EntityObject entity, int hash)
        {
            entityObject = entity;
            removedComponentHash = hash;
        }

        public EntityObject entityObject;
        public int removedComponentHash;

    }
}
