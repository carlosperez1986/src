namespace Modular.Core.Models
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
