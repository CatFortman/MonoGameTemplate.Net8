using MonoGameTemplate.Components;
using MonoGameTemplate.Entities;

namespace MonoGameTemplate.Systems
{
    public class RestorationSystem : ISystem
    {
        public void Update(EntityManager entityManager)
        {
            foreach (var entity in entityManager.GetAll())
            {
                if (entity.HasComponent<RestorableComponent>())
                {
                    var restorable = entity.GetComponent<RestorableComponent>();

                    if (restorable.State == RestorationState.Corrupted)
                    {
                        // Simulate "restoration" — you can tie this to player input or events later
                        restorable.State = RestorationState.Purified;
                        System.Console.WriteLine($"Entity {entity.Id} has been purified.");
                    }
                }
            }
        }
    }
}
