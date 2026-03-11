namespace cdr_group.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AuditDisplayNameAttribute : Attribute
    {
        public Type EntityType { get; }
        public string DisplayPropertyEn { get; }
        public string DisplayPropertyAr { get; }

        public AuditDisplayNameAttribute(Type entityType, string displayPropertyEn, string displayPropertyAr)
        {
            EntityType = entityType;
            DisplayPropertyEn = displayPropertyEn;
            DisplayPropertyAr = displayPropertyAr;
        }

        public AuditDisplayNameAttribute(Type entityType, string displayProperty)
            : this(entityType, displayProperty, displayProperty)
        {
        }
    }
}
