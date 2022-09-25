using ApiExcel.DAL;
using IronXL;

namespace ApiExcel
{
    public class Importer
    {
        public async Task ImportFromXlsAsync(
            WorkBook workbook, 
            ApiExcelDataContext context)
        {
            WorkSheet sheet = workbook.WorkSheets.First();
            string cellValue = sheet["A1"].StringValue;

        }
    }
}
