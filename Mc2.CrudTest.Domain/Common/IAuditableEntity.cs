namespace Mc2.CrudTest.Domain.Common
{
    public interface IAuditableEntity
    {
        bool IsDeleted { get; set; }
        DateTime? Modified { get; set; }
        string? Editor { get; set; }
    }
}
