using System.Collections;
using System.Reflection;
using ClosedXML.Excel;
using cdr_group.Contracts.Interfaces.Services;

namespace cdr_group.Application.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] ExportToExcel<T>(IEnumerable<T> data, string sheetName) where T : class
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(sheetName);

            var properties = GetExportableProperties(typeof(T));

            // Headers
            for (int i = 0; i < properties.Count; i++)
            {
                var headerCell = worksheet.Cell(1, i + 1);
                headerCell.Value = FormatHeaderName(properties[i].Name);
                headerCell.Style.Font.Bold = true;
                headerCell.Style.Fill.BackgroundColor = XLColor.LightGray;
            }

            // Data rows
            var items = data.ToList();
            for (int row = 0; row < items.Count; row++)
            {
                for (int col = 0; col < properties.Count; col++)
                {
                    var value = properties[col].GetValue(items[row]);
                    var cell = worksheet.Cell(row + 2, col + 1);
                    SetCellValue(cell, value);
                }
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        private static List<PropertyInfo> GetExportableProperties(Type type)
        {
            return type.GetProperties()
                .Where(p =>
                {
                    var propType = p.PropertyType;

                    // Skip collections of complex objects (e.g. List<CompanyDto>, List<PermissionDto>)
                    if (propType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propType))
                    {
                        // Allow List<string> (e.g. Roles, Permissions)
                        if (propType.IsGenericType)
                        {
                            var genericArg = propType.GetGenericArguments().FirstOrDefault();
                            return genericArg == typeof(string);
                        }
                        return false;
                    }

                    // Skip complex navigation objects (e.g. Manager of type EmployeeBasicDto)
                    if (!IsSimpleType(propType))
                        return false;

                    return true;
                })
                .ToList();
        }

        private static bool IsSimpleType(Type type)
        {
            var underlying = Nullable.GetUnderlyingType(type) ?? type;
            return underlying.IsPrimitive
                || underlying == typeof(string)
                || underlying == typeof(decimal)
                || underlying == typeof(DateTime)
                || underlying == typeof(DateTimeOffset)
                || underlying == typeof(TimeSpan)
                || underlying == typeof(Guid);
        }

        private static void SetCellValue(IXLCell cell, object? value)
        {
            if (value == null)
            {
                cell.Value = string.Empty;
                return;
            }

            if (value is IEnumerable<string> stringList)
            {
                cell.Value = string.Join(", ", stringList);
                return;
            }

            switch (value)
            {
                case DateTime dt:
                    cell.Value = dt;
                    cell.Style.DateFormat.Format = "yyyy-MM-dd HH:mm:ss";
                    break;
                case TimeSpan ts:
                    cell.Value = ts.ToString(@"hh\:mm");
                    break;
                case bool b:
                    cell.Value = b ? "Yes" : "No";
                    break;
                case int or long or decimal or double or float:
                    cell.Value = Convert.ToDouble(value);
                    break;
                case Guid guid:
                    cell.Value = guid.ToString();
                    break;
                default:
                    cell.Value = value.ToString();
                    break;
            }
        }

        private static string FormatHeaderName(string propertyName)
        {
            // Convert PascalCase to spaced words: "CompanyNameEn" -> "Company Name En"
            var result = new System.Text.StringBuilder();
            for (int i = 0; i < propertyName.Length; i++)
            {
                if (i > 0 && char.IsUpper(propertyName[i]) && !char.IsUpper(propertyName[i - 1]))
                    result.Append(' ');
                result.Append(propertyName[i]);
            }
            return result.ToString();
        }
    }
}
