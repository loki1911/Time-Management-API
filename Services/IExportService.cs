namespace TimeMangementSystemAPI.Services
{
    public interface IExportService
    {
        Task<MemoryStream> ExportTimeSheetToExcelAsync(string email);
    }
}
