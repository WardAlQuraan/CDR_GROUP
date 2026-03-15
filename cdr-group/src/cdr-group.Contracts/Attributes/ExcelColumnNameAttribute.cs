namespace cdr_group.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ExcelColumnNameAttribute : Attribute
    {
        public string Name { get; }

        public ExcelColumnNameAttribute(string name)
        {
            Name = name;
        }
    }
}
