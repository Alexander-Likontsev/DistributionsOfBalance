using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DistributionOfPart
{
    internal class Logic
    {
        private PathOfFiles pathOfFiles;
        private ExcelL ex;
        private List<Detail> details;
        private List<Detail> details_akron;
        private List<VendorsCodes> vendors_codes;
        private List<InputData> inputs;
        
        public void Run(PathOfFiles path)
        {
            pathOfFiles = path;
            ex = new ExcelL();
            details = ex.FacadesSizes(pathOfFiles.NamefileFS);
            details_akron = ex.FacadesSizes(pathOfFiles.NamefileFCA);
            vendors_codes = ex.VendorsRead(pathOfFiles.NamefileVC);
            inputs = ex.InputRead(pathOfFiles.NamefileInput);
        }


        public List<OutPut> Result1(TextBox textB)
        {
            var outPuts = from p in inputs
                          join c in vendors_codes on p.Code equals c.Code
                          select new { c.DetailNo, c.Color, p.Count, c.Amount, p.Code }; //достаем из файла настроек VendorsCodes(for dist) все данные по Артикулам из входного файла

            var List2 = inputs.Where(item => !vendors_codes.Any(item2 => item2.Code == item.Code)); //LinQ запрос, который находит, еще не вписанные в файл VendorsCodes(for dist), артикулы.

            foreach (var item in List2)
                textB.AppendText("Не найден Артикул: " + item.Code + "\r\n");
            
            List<OutPut> result = new List<OutPut>();
            foreach (var item in outPuts)
            {
                OutPut outPut = new OutPut()
                {
                    DetailNo = item.DetailNo,
                    Color = item.Color,
                    Count = item.Count,
                    Amount = item.Amount,
                    Code = item.Code
                };
                result.Add(outPut);
            }              
            return result;
        }


        public List<OutPut> Result2(TextBox textB)
        {
            List<OutPut> result = Result1(textB);
            var outPuts = from p in result
                          join c in details_akron on p.DetailNo equals c.DetailNo
                          select new { c.Multiplicity, c.Length, Widht = c.Width, Belonging = "Akron", p.Color, p.Count, p.Amount, p.Code, p.DetailNo }; //данный запрос находит те детали из входного файла, которые идут только на Akron

            List<OutPut> resultA = new List<OutPut>();
            foreach (var item in outPuts)
            {
                OutPut outPut = new OutPut()
                {
                    DetailNo = item.DetailNo,
                    Color = item.Color,
                    Count = item.Count,
                    Amount = item.Amount,
                    Code = item.Code,
                    Length = item.Length,
                    Width = item.Widht,
                    Belonging = item.Belonging,
                    Multiplicity = item.Multiplicity
                };
                resultA.Add(outPut);
            }

            var outPuts2 = from p in result
                           join c in details on p.DetailNo equals c.DetailNo
                           select new { c.Multiplicity, c.Length, Widht = c.Width, Belonging = "Unknown", p.Color, p.Count, p.Amount, p.Code, p.DetailNo }; //детали, которые могут пойти как на Акрон, так и на Хомаг2, в зависимости от размера и количества

            foreach (var item in outPuts2)
            {
                OutPut outPut = new OutPut()
                {
                    DetailNo = item.DetailNo,
                    Color = item.Color,
                    Count = item.Count,
                    Amount = item.Amount,
                    Code = item.Code,
                    Length = item.Length,
                    Width = item.Widht,
                    Belonging = item.Belonging,
                    Multiplicity = item.Multiplicity
                };
                resultA.Add(outPut);
            }
            foreach (var item in resultA)
            {
                item.Count2 = item.Count * item.Amount / item.Multiplicity; //находим точное количество деталей. Обозначения переменных в конце класса.
                if (item.Count2 < 1)
                    item.Count2 = 1;
            }
            
            var groupCount = from x in resultA
                          group x by (x.DetailNo, x.Color) into g
                          select (g.Key.DetailNo, g.Key.Color, g.Sum(x => x.Count2)); //суммируем все похожие детали из разных распределений

            var selectGroupCount = from x in resultA
                           join c in groupCount on x.DetailNo equals c.DetailNo
                           select new { c.DetailNo, c.Color, c.Item3, x.Multiplicity, x.Width, x.Length, x.Belonging };

            var deleteDuplicate = selectGroupCount.Distinct(); //убираем получившиеся дубликаты. Это костыль, к сожалению... Как сделать без него мы не нашли время подумать

            List<OutPut> resultB = new List<OutPut>();

            foreach (var item in deleteDuplicate)
            {
                OutPut outPut = new OutPut()
                {
                    DetailNo = item.DetailNo,
                    Color = item.Color,
                    Count2 = item.Item3,
                    Multiplicity = item.Multiplicity,
                    Width = item.Width,
                    Length = item.Length,
                    Belonging = item.Belonging
                };
                resultB.Add(outPut);
            }
            return resultB;
        }


        public List<OutPut> Defected(Defect defects, TextBox textB) //метод, отвечающий за надбавку брака
        {

            List<OutPut> outPuts = Result2(textB);

            foreach (var item in outPuts)
            {
                //n - это коэфициент соответсвующий значениям >10 { item.CountDefect=item.Count2*n}
                if (item.Count2 <= 10) { item.CountDefect = Convert.ToInt32(item.Count2 * (1 + defects.UptoTen)); }
                if (item.Count2 <= 50 && item.Count2 > 10) { item.CountDefect = Convert.ToInt32(item.Count2 * (1 + defects.UptoFifty)); }
                if (item.Count2 <= 80 && item.Count2 > 50) { item.CountDefect = Convert.ToInt32(item.Count2 * (1 + defects.UptoEighty)); }
                if (item.Count2 <= 300 && item.Count2 > 80) { item.CountDefect = Convert.ToInt32(item.Count2 * (1 + defects.UptoThreeHundred)); }
                if (item.Count2 <= 1000 && item.Count2 > 300) { item.CountDefect = Convert.ToInt32(item.Count2 * (1 + defects.UptoOneThousand)); }
                if (item.Count2 <= 2000 && item.Count2 > 300) { item.CountDefect = Convert.ToInt32(item.Count2 * (1 + defects.UptoTwoThousand)); }
                if (item.Count2 > 2000) { item.CountDefect = Convert.ToInt32(item.Count2 * (1 + defects.FromTwoThousand)); }



                if (item.CountDefect - item.Count2 < 10)
                {
                    item.CountDefect = item.Count2 + 10;
                }
            }
            return outPuts;
        }


        public List<OutPut> Distribution(Defect defects, TextBox textB)//метод, отвечающий за распределение деталей по размерам. 
        {

            List<OutPut> data = Defected(defects, textB);

            foreach (var item in data)
                if (((item.Length >= 290 && item.Width >= 290) && item.CountDefect >= 100) && (item.Belonging != "Akron"))
                    item.Belonging = "Homag";
                else
                    item.Belonging = "Akron";
            return data;
        }
    }


    public class OutPut
    {
        public string DetailNo { get; set; } //номер детали
        public string Color { get; set; } //цвет детали
        public double Length { get; set; }//длина детали
        public double Width { get; set; }//ширина детали
        public int Amount { get; set; } //колиичество деталей в упаковке(артикуле)
        public string Belonging { get; set; } 
        public int Count { get; set; } //количество артикулов из входного файла.
        public int Code { get; set; }//артикул
        public int Multiplicity { get; set; }//кратность детали при производстве.
        public int CountDefect { get; set; }//надбавка брака
        public int Count2 { get; set; }//искомое количество деталей, которое нужно произвести

    }
}