namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName) where T : class;
    }
}
