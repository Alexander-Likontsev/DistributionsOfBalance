using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Cells;
using DistributionOfPart;

namespace DistributionOfPart
{
    internal class ExcelO
    {
        Logic logic = new Logic();
        public void ShowClear(PathOfFiles path, Defect defects, TextBox text) //PuthOfFiles puth  
        {
            logic.Run(path);
            List<OutPut> FinalData = logic.Distribution(defects, text);
            Workbook wb = new Workbook();
            Worksheet worksheet = wb.Worksheets[0];

            var Homag = from p in FinalData
                        where (p.Belonging == "Homag")
                        orderby p.Color, p.Multiplicity descending
                        select new { p.DetailNo, p.Color, p.Multiplicity, p.Count2, p.CountDefect };

            var Akron = from p in FinalData
                        where (p.Belonging == "Akron")
                        orderby p.Color, p.Multiplicity descending
                        select new { p.DetailNo, p.Color, p.Multiplicity, p.Count2, p.CountDefect };



            worksheet.Cells[0, 0].Value = "Homag";
            worksheet.Cells[1, 0].Value = "№ дет";
            worksheet.Cells[1, 1].Value = "толщ";
            worksheet.Cells[1, 2].Value = "вид дет.";
            worksheet.Cells[1, 3].Value = "цвет";
            worksheet.Cells[1, 4].Value = "кратность";
            worksheet.Cells[1, 5].Value = "кол-во";
            worksheet.Cells[1, 6].Value = "кол-во с учетом брака";

            int i = 2;
            foreach (var item in Homag)
            {
                worksheet.Cells[i, 0].PutValue(item.DetailNo);
                worksheet.Cells[i, 1].PutValue("16 мм");
                worksheet.Cells[i, 2].PutValue("фасад");
                worksheet.Cells[i, 3].PutValue(item.Color);
                worksheet.Cells[i, 4].PutValue(item.Multiplicity);
                worksheet.Cells[i, 5].PutValue(item.Count2);
                worksheet.Cells[i, 6].PutValue(item.CountDefect);
                i++;
            }
            worksheet.Cells[0, 12].Value = "Akron";
            worksheet.Cells[1, 12].Value = "№ дет";
            worksheet.Cells[1, 13].Value = "толщ";
            worksheet.Cells[1, 14].Value = "вид дет.";
            worksheet.Cells[1, 15].Value = "цвет";
            worksheet.Cells[1, 16].Value = "кол-во";
            worksheet.Cells[1, 17].Value = "кратность";
            worksheet.Cells[1, 18].Value = "кол-во с учетом брака";
            int j = 2;
            foreach (var item in Akron)
            {
                worksheet.Cells[j, 12].PutValue(item.DetailNo);
                worksheet.Cells[j, 13].PutValue("16 мм");
                worksheet.Cells[j, 14].PutValue("фасад");
                worksheet.Cells[j, 15].PutValue(item.Color);
                worksheet.Cells[j, 16].PutValue(item.Multiplicity);
                worksheet.Cells[j, 17].PutValue(item.Count2);
                worksheet.Cells[j, 18].PutValue(item.CountDefect);
                j++;
            }
            wb.Save(path.NamefileOutput + "\\plan.xlsx", SaveFormat.Xlsx);
        }
    }
}