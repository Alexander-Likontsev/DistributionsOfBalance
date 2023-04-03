using System;
using System.Collections.Generic;
using System.Linq;


namespace DistributionOfBalance
{
    internal class Logic
    {
        private PathOfFile PathOfFiles;
        private ExcelFileRead EFR;
        private List<InPut> InPut;
        private List<VendorsCodes> VendorsCodes;

        public void Run(PathOfFile Path)
        {
            PathOfFiles = Path;
            EFR = new ExcelFileRead();
            InPut = EFR.InPutFile(PathOfFiles.PathInputFile);
            VendorsCodes = EFR.VendorsRead(PathOfFiles.PathOfVendorsCodesOriginal);
        }

        public List<OutPut> Result1() //Создаем список vendors codes по данным из входного файла
        {
            var inPut = from p in InPut
                        join c in VendorsCodes on new { p.NumberOfDetails, p.Color } equals new { c.NumberOfDetails, c.Color }
                        select new { c.Code, c.NameOfDetails, c.NumberOfDetails, c.Amount, c.Color, p.CountOfBalance, c.CountOfBalanceAfterDistributions };
            List<OutPut> result = new List<OutPut>();
            foreach (var item in inPut)
            {
                OutPut outPut = new OutPut()
                {
                    Code = item.Code,
                    NameOfDetails = item.NameOfDetails,
                    NumberOfDetails = item.NumberOfDetails,
                    Amount = item.Amount,
                    Color = item.Color,
                    CountOfBalance = item.CountOfBalance
                };
                result.Add(outPut);
            }
            return result;
        }
        
        public List<OutPut> ResultMulti()
        {
            List<OutPut> result = Result1();
            foreach (var item in result)
            {
                item.CountOfBox = item.CountOfBalance / item.Amount;
                item.CountOfBox = Math.Floor(item.CountOfBox); //Количество коробок, которые возможно запаковать

                item.CountOfBalanceAfterDistributions = item.CountOfBalance - ((int)item.CountOfBox * item.Amount); //Остатки после распределения(можно развить эту ветку программы для вывода остатков после распределения)
            }
            return result;
        }

        public List<VendorsCodesCount> ResultGroupCountVendorsCodes() //количество детелей в каждом наборе из файла настроек.
        {
            var groupCount = VendorsCodes.GroupBy(x => x.Code).ToDictionary(g => g.Key, g => g.Count());
            
            List<VendorsCodesCount> resultGroupCountVendorsCodes = new List<VendorsCodesCount>();
            foreach(var item in groupCount)
            {
                VendorsCodesCount vendorsCodesCount = new VendorsCodesCount() 
                { 
                    CodeCountVendorsCodes = item.Key,
                    CountDetailOfCodeInVendorsCode = item.Value
                };
                resultGroupCountVendorsCodes.Add(vendorsCodesCount);
            }
            return resultGroupCountVendorsCodes;
        }

        public List<OutPut> ResultGroupCodeOnBox() 
        { 
            List<OutPut> resultGroup = ResultMulti();
            var groupMinBoxInCollectionCode = from x in resultGroup
                                              group x by x.Code into g
                                              select (g.Key, g.Min(x => x.CountOfBox), g.Count());//группировка по минимально возможному количеству виду детали в коробке.

            var selectMinBoxInColletion = from x in resultGroup
                                          join p in groupMinBoxInCollectionCode on x.Code equals p.Key
                                          orderby x.Code descending //упорядочивание списка
                                          select (p.Key, p.Item2, p.Item3, x.NumberOfDetails, x.CountOfBalance);//запись в CountOfBox(item2) количество возможных собранных коробок по предыдущему запросу.
            //сгруппировали по минимимально возможному количеству детали в наборе
            List<OutPut> resultGroupCodeOnBox = new List<OutPut>();
            foreach (var item in selectMinBoxInColletion)
            {
                OutPut outPut = new OutPut()
                {
                    Code = item.Key,
                    CountOfBox = item.Item2,
                    CountOfDetailInBox = item.Item3,
                    NumberOfDetails = item.NumberOfDetails,
                    CountOfBalance = item.CountOfBalance
                };
                resultGroupCodeOnBox.Add(outPut);
            }
            return resultGroupCodeOnBox;
        }
        
        public List<OutPut> JoinVendorsCount() //Вывод возможных распределений
        {
            List<OutPut> resultGroupCodeInVox = ResultGroupCodeOnBox();
            List<VendorsCodesCount> resultVendorsCodesCount = ResultGroupCountVendorsCodes();
            var resultJoin = from x in resultGroupCodeInVox
                             join c in resultVendorsCodesCount on ( x.Code, x.CountOfDetailInBox ) 
                             equals (c.CodeCountVendorsCodes, c.CountDetailOfCodeInVendorsCode)
                             select new { x.Code, x.CountOfBox, x.CountOfDetailInBox, x.NumberOfDetails, x.CountOfBalance }; //сравниваем количество деталей из файла настроек VendorsCodes и распределенного списка. Если сходится, то записываем в выходной лист.
            var resultDistinct = resultJoin.Distinct().ToList();//удаление дубликатов
                                
            List<OutPut> outPuts = new List<OutPut>();
            foreach(var item in resultDistinct)
            {
                OutPut outPut = new OutPut()
                {
                    Code = item.Code,
                    CountOfBox = item.CountOfBox,
                    CountOfDetailInBox = item.CountOfDetailInBox,
                    NumberOfDetails = item.NumberOfDetails,
                    CountOfBalance= item.CountOfBalance
                };
                outPuts.Add(outPut);
            }
            return outPuts;
        }
    }

    public class OutPut
    {
        public string Code { get; set; } //артикул
        public int Amount { get; set; }//количество деталей в коробке(артикуле)
        public string NameOfDetails { get; set; }//Название детали
        public string Color { get; set; }//цвет детали
        public int CountOfDetailInBox { get; set; }//количество деталей в одной коробке(артикуле)
        public string NumberOfDetails { get; set; }//номер детали
        public int CountOfBalance { get; set; }//количество остатков 
        public double CountOfBox { get; set; }//количество коробок
        public int CountOfBalanceAfterDistributions { get; set; }//количество остатков после распределения 
    }
    public class VendorsCodesCount
    {
        public string CodeCountVendorsCodes { get; set; }
        public int CountDetailOfCodeInVendorsCode { get; set; }
    }
   
}
