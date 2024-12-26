using ScreeningExchange.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System.Reflection;

namespace ScreeningExchange.Infrastructure.IO.Blobs;

public sealed class ExcelOfficeOpenXml(IOptions<ApiConfig> config) : IExcelRead
{
    private readonly ApiConfig Config = config.Value;

    public List<T> Read<T>(ExcelParams excelParams)
    {
        // Configurar o contexto de licença (necessário para uso não comercial do EPPlus)
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using var package = new ExcelPackage(excelParams.stream);
        var worksheet = package.Workbook.Worksheets[0]; // Obtém a primeira aba
        var resultList = new List<T>();

        // Obter os cabeçalhos do Excel (primeira linha)
        var headers = new Dictionary<int, string>();
        for (int col = 1; col <= worksheet.Dimension.Columns; col++)
        {
            var header = worksheet.Cells[1, col].Text;
            if (!string.IsNullOrEmpty(header))
            {
                headers[col] = header;
            }
        }

        // Mapear os dados do Excel para a classe T
        for (int row = 2; row <= worksheet.Dimension.Rows; row++)
        {
            var obj = (T)Activator.CreateInstance(typeof(T))!;

            foreach (var header in headers)
            {
                int colIndex = header.Key;
                string propertyName = header.Value;

                // Encontrar a propriedade correspondente na classe T (ignorar case)
                var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property != null && property.CanWrite)
                {
                    var cellValue = worksheet.Cells[row, colIndex].Text;

                    // Converter o valor do Excel para o tipo da propriedade
                    object? convertedValue = Convert.ChangeType(cellValue, property.PropertyType);
                    property.SetValue(obj, convertedValue);
                }
            }

            resultList.Add(obj!);
        }

        return resultList;
    }
}