using FalseSpirits.Entities;

namespace FalseSpirits.Components
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
