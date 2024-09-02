namespace Domain.Entities
{
    public abstract class BaseEntitty<T>
    {
        public T Id { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
    }
}
