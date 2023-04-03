using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionOfBalance
{
    internal class WriterOutPutFile
    {
        Logic logic = new Logic();
        public void WriteExel(PathOfFile Path)
        {
            logic.Run(Path);
            List<OutPut> FinalList = logic.JoinVendorsCount();
            Workbook wb = new Workbook();
            Worksheet worksheet = wb.Worksheets[0];

            worksheet.Cells[0, 0].Value = "Артикул";
            worksheet.Cells[0, 1].Value = "Количество коробок";
            worksheet.Cells[0, 2].Value = "Номер детали";
            worksheet.Cells[0, 3].Value = "Остатки";

            int i = 1;
            foreach(var item in FinalList)
            {
                worksheet.Cells[i, 0].PutValue(item.Code);
                worksheet.Cells[i, 1].PutValue(item.CountOfBox);
                worksheet.Cells[i, 2].PutValue(item.NumberOfDetails);
                worksheet.Cells[i, 3].PutValue(item.CountOfBalance);
                i++;
            }
            wb.Save(Path.PathOutPutFile + "\\OstDitributions.xlsx", SaveFormat.Xlsx);
        }
    }
}
