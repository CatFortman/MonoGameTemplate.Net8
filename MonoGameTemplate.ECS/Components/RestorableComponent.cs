using MonoGameTemplate.ECS.Entities;

namespace MonoGameTemplate.ECS.Components
{
    public enum RestorationState
    {
        Corrupted,
        Purified
    }

    public class RestorableComponent : IComponent
    {
        public RestorationState State;

        public RestorableComponent(RestorationState initialState)
        {
            State = initialState;
        }
    }
}
