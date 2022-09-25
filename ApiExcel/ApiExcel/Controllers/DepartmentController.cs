using ApiExcel.DAL;
using IronXL;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiExcel.Controllers;

[ApiController]
[Route("department")]
public class DepartmentController : ControllerBase
{
    private readonly ApiExcelDataContext _context;
    private readonly Importer _importer;

    public DepartmentController(
        ApiExcelDataContext context,
        Importer importer)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _importer = importer ?? throw new ArgumentNullException(nameof(importer));
    }

    /// <summary>
    /// Import department from xls file
    /// </summary>
    /// <returns></returns>
    [HttpPost("import")]
    public async Task ImportFromXls(
        [FromForm][Required] IFormFile upload)
    {
        await using (var stream = new MemoryStream())
        { 
            await upload.CopyToAsync(stream);
            WorkBook workbook = WorkBook.Load(stream);
            await _importer.ImportFromXlsAsync(workbook, _context);
        }
    }
}