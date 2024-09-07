namespace Domain.Entities
{
    public abstract class BaseEntitty<T>
    {
        public T Id { get; set; }
    }
}