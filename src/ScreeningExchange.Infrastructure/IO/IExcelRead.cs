namespace ScreeningExchange.Infrastructure.IO;

public record ExcelParams(
    Stream stream
);

public interface IExcelRead
{
    public List<T> Read<T>(
        ExcelParams excelParams
    );
}