using ApiExcel.DAL;
using OfficeOpenXml;


namespace ApiExcel
{
    public class Importer
    {
        public class ImportData
        {
            public string DepartmentName { get; set; }

            public string ParentDepartmentName { get; set; } 
            
            public string Position { get; set; }

            public string Name { get; set; }
        }
        
        public async Task ImportFromXlsAsync(
            MemoryStream stream, 
            ApiExcelDataContext context)
        {
            using (var package = new ExcelPackage(stream))
            {
                var dataList = new List<ImportData>();
                var firstSheet = package.Workbook.Worksheets[0];
                
                var counter = 2;
                
                var firstColumn = firstSheet.Cells[$"A{counter}"].Text;
                var secondColumn = firstSheet.Cells[$"B{counter}"].Text;
                var thirdColumn = firstSheet.Cells[$"C{counter}"].Text;
                var fourthColumn = firstSheet.Cells[$"D{counter}"].Text;

                while (!IsNull(firstColumn, secondColumn, thirdColumn, fourthColumn))
                {
                    dataList.Add(new ImportData 
                    {
                        DepartmentName = firstColumn,
                        ParentDepartmentName = secondColumn,
                        Position = thirdColumn,
                        Name = fourthColumn
                    });
                    
                    counter++; 
                    
                    firstColumn = firstSheet.Cells[$"A{counter}"].Text;
                    secondColumn = firstSheet.Cells[$"B{counter}"].Text;
                    thirdColumn = firstSheet.Cells[$"C{counter}"].Text; 
                    fourthColumn = firstSheet.Cells[$"D{counter}"].Text;
                }

                var departments = dataList
                    .GroupBy(x => 
                        new Department
                        {
                            Name = x.DepartmentName, 
                            ParentDepartmentName = x.ParentDepartmentName
                        })
                    .ToDictionary(x => x.Key,
                        x => x.Select(z => 
                            new User
                            {
                                Name = z.Name,
                                Position = z.Position
                            }).ToList());

                foreach (var department in departments)
                {
                    department.Key.Users = department.Value;
                }

                var dep = departments.Select(x => x.Key).ToList();
                
                context.Departments.AddRange(dep);

                await context.SaveChangesAsync();
            }
        }

        private static bool IsNull(string? firstColumn, string? secondColumn, string? thirdColumn, string? fourthColumn)
        {
            return firstColumn == null && secondColumn == null && thirdColumn == null && fourthColumn == null;
        }
    }
}
