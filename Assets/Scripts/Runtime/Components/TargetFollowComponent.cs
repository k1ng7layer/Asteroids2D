using MyECS2;

namespace AsteroidsECS
{
    public struct TargetFollowComponent
    {
        public TransformComponent targetTransform;
        public EntityObject targetEntity;
    }
}
