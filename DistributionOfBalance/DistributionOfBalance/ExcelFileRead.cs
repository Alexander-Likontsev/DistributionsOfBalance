using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Aspose.Cells;
using Aspose.Cells.Drawing;

namespace DistributionOfBalance
{
    internal class ExcelFileRead
    {
        public List<InPut> InPutFile(string name)
        {
            List<InPut> dataList = new List<InPut>();
            List<string> DataRead = new List<string>();
            try
            {
                Workbook wb = new Workbook(name);
                Worksheet ws = wb.Worksheets[0];

                int rows = ws.Cells.MaxDataRow;
                int cols = ws.Cells.MaxDataColumn;

                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 0; j <= cols; j++)
                    {
                        DataRead.Add(ws.Cells[i, j].Value.ToString());
                    }
                }
            }
            catch { MessageBox.Show("Не удалось прочитать файл LAK. Возможно, неправильно заполнены поля."); }

            for (int i = 0; i < DataRead.Count; i += 3)
            {
                InPut input = new InPut()
                {
                    NumberOfDetails = DataRead[i],
                    Color = DataRead[i + 1],
                    CountOfBalance = int.Parse(DataRead[i + 2])
                };
                dataList.Add(input);
            }
            return dataList;
        }

        public List<VendorsCodes> VendorsRead(string name)
        {
            List<VendorsCodes> dataList = new List<VendorsCodes>();
            List<string> DataRead = new List<string>();
            try
            {
                Workbook wb = new Workbook(name);
                Worksheet ws = wb.Worksheets[0];

                int rows = ws.Cells.MaxDataRow;
                int cols = ws.Cells.MaxDataColumn;

                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 0; j <= cols; j++)
                    {
                        DataRead.Add(ws.Cells[i, j].Value.ToString());
                    }
                }
            }
            catch { MessageBox.Show("Ошибка чтения VendorsCodes, попробуйте другой файл"); }

            for (int i = 0; i < DataRead.Count; i += 5)
            {
                VendorsCodes vendorsCodes = new VendorsCodes()
                {
                    Code = DataRead[i],
                    NameOfDetails = DataRead[i + 1],
                    NumberOfDetails = DataRead[i + 2],
                    Amount = int.Parse(DataRead[i + 3]),
                    Color = DataRead[i + 4]
                };
                dataList.Add(vendorsCodes);
            }
            return dataList;
        }
    }
    class InPut
    {
        public string NumberOfDetails { get; set; }
        public string Color { get; set; }
        public int CountOfBalance { get; set; }
    }
    class VendorsCodes
    {
        public string Code { get; set; }
        public string NameOfDetails { get; set; }
        public string NumberOfDetails { get; set; }
        public int Amount { get; set; }
        public string Color { get; set; }
        public int CountOfBox { get; set; }
        public int CountOfBalanceAfterDistributions { get; set; }
    }
}
