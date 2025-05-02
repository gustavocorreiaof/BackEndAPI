namespace Core.Domain.Entities.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
