namespace  Mc2.CrudTest.Domain.Common
{
    public abstract class AuditableWithBaseEntity<T> : BaseEntity<T>, IAuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? Modified { get; set; }
        public string? Editor { get; set; }
    }
}
