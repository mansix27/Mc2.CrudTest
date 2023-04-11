namespace Mc2.CrudTest.Domain.Common
{
    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }
    public interface IEntity
    {
    }
}
