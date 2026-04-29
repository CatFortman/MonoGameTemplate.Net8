namespace MonoGameTemplate.ECS.Entities
{
    public interface ISystem
    {
        void Update(EntityManager entityManager);
    }
}
