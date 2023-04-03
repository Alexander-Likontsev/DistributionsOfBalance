using Aspose.Cells;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace DistributionOfPart
{
    internal class ExcelL
    {
        public List<Detail> FacadesSizes(string name)
        {
            List<string> testData = new List<string>();
            List<Detail> detaiList = new List<Detail>();
            try
            {
                Workbook wb = new Workbook(name);
                Worksheet ws = wb.Worksheets[0];

                int rows = ws.Cells.MaxDataRow;
                int cols = ws.Cells.MaxDataColumn;

                for (int i = 1; i <= rows; i++)
                    for (int j = 0; j <= cols; j++)
                        testData.Add(ws.Cells[i, j].Value.ToString());
            }
            catch
            {
                MessageBox.Show("Не удалось прочитать файл FacadesSizes(for dist) или FacadesSizesAkron. Замените их на новые из архива.");
            }

            for (int i = 0; i < testData.Count; i += 4)
            {
                detaiList.Add(new Detail()
                {
                    DetailNo = testData[i],
                    Length = double.Parse(testData[i + 1]),
                    Width = double.Parse(testData[i + 2]),
                    Multiplicity = int.Parse(testData[i + 3]),
                });
            }
            return detaiList;
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
            catch { MessageBox.Show("Не удалось прочитать файл VendorsCodes. Замените его на новый из архива.");}

            for (int i = 0; i < DataRead.Count; i += 5)
            {
                VendorsCodes vendors = new VendorsCodes()
                {
                    Code = int.Parse(DataRead[i]),
                    NameV = DataRead[i + 1],
                    DetailNo = DataRead[i + 2],
                    Amount = int.Parse(DataRead[i + 3]),
                    Color = DataRead[i + 4]
                };
                dataList.Add(vendors);
            }
            return dataList;
        }


        public List<InputData> InputRead(string name)
        {
            List<InputData> dataList = new List<InputData>();
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

            for (int i = 0; i < DataRead.Count; i += 2)
            {

                InputData input = new InputData()
                {
                    Code = int.Parse(DataRead[i]),
                    Count = int.Parse(DataRead[i + 1])
                };
                dataList.Add(input);
            }
            return dataList;
        }
    }

    class Detail
    {
        public string DetailNo { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public int Multiplicity { get; set; }
    }

    class VendorsCodes
    {
        public int Code { get; set; }
        public string NameV { get; set; }
        public string DetailNo { get; set; }
        public int Amount { get; set; }
        public string Color { get; set; }
    }

    class InputData
    {
        public int Code { get; set; }
        public int Count { get; set; }
    }

}
    
 



