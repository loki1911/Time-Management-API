using Microsoft.AspNetCore.Mvc;
using TimeMangementSystemAPI.Services;

namespace TimeMangementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet("DownloadTimeSheet")]
        public async Task<IActionResult> DownloadTimeSheet([FromQuery] string email)
        {
            try
            {
                var excelStream = await _exportService.ExportTimeSheetToExcelAsync(email);
                return File(excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TimeSheetData.xlsx");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }
    }
}
