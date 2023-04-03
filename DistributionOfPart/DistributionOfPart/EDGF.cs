using System;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Authentication;

namespace DistributionOfPart
{
    public partial class EDGF : Form
    {
        public EDGF()
        {
            InitializeComponent();
        }
        PathOfFiles path = new PathOfFiles();
        Defect defect = new Defect();
        private void EDGF_Load(object sender, EventArgs e)
        {
            try
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

                tb_FacadesSize.Text = Properties.Settings.Default.FacadesSizes_Value; // Загрузка пути из файла DistributionOfPart.exe.config в папке debag\bin
                path.NamefileFS = tb_FacadesSize.Text;

                tb_VendorsCodes.Text = Properties.Settings.Default.VendorsCodes_Value;  // Почитать об этом можно будет на сайтах: https://www.cyberforum.ru/windows-forms/thread928071.html и https://translated.turbopages.org/proxy_u/en-ru.ru.b8bf7ae1-63a07ba8-2e9b3e8e-74722d776562/https/codehill.com/2009/01/saving-user-and-application-settings-in-winforms/
                path.NamefileVC = tb_VendorsCodes.Text;

                tb_FacadesSizesAcron.Text = Properties.Settings.Default.FacadesSizesAcron_Value;
                path.NamefileFCA = tb_FacadesSizesAcron.Text;

                tb_uptoTen.Text = Properties.Settings.Default.uptoTen_Value; //Загрузка значения барака в DistributionOfPart.exe.config 
                defect.UptoTen = double.Parse(tb_uptoTen.Text, formatter);

                tb_uptoFifty.Text = Properties.Settings.Default.uptoFifty_Value;
                defect.UptoFifty = double.Parse(tb_uptoFifty.Text, formatter);

                tb_uptoEighty.Text = Properties.Settings.Default.uptoEighty_Value;
                defect.UptoEighty = double.Parse(tb_uptoEighty.Text, formatter);

                tb_uptoThreeHundred.Text = Properties.Settings.Default.uptoThreeHundred_Value;
                defect.UptoThreeHundred = double.Parse(tb_uptoThreeHundred.Text, formatter);

                tb_uptoOneThousand.Text = Properties.Settings.Default.uptoOneThousand_Value;
                defect.UptoOneThousand = double.Parse(tb_uptoOneThousand.Text, formatter);

                tb_uptoTwoThousand.Text = Properties.Settings.Default.uptoTwoThousand_Value;
                defect.UptoTwoThousand = double.Parse(tb_uptoTwoThousand.Text, formatter);

                tb_fromTwoThousand.Text = Properties.Settings.Default.fromTwoThousand_Value;
                defect.FromTwoThousand = double.Parse(tb_fromTwoThousand.Text, formatter);
            }
            catch
            {
                MessageBox.Show("Укажие пути к файлам настроек");
            }
        }


        private void but_PuthByFacadesSizes_Click(object sender, EventArgs e) //PuthByVendorsCodes(for dist)
        {
            OpenFileDialog ofdFacadesSize = new OpenFileDialog();
            if (ofdFacadesSize.ShowDialog() == DialogResult.OK)
            {

                tb_FacadesSize.Text = ofdFacadesSize.FileName;
                path.NamefileFS = tb_FacadesSize.Text;
            }
            Properties.Settings.Default.FacadesSizes_Value = tb_FacadesSize.Text; // Сохраняем выбранный путь файла. При выборе другого пути, значение в файле обновится 
            Properties.Settings.Default.Save();
        }
        private void but_VendorsCodes_Click(object sender, EventArgs e) //PuthByVendorsCodes(for dist)
        {
            OpenFileDialog ofdVendorsCodes = new OpenFileDialog();
            if (ofdVendorsCodes.ShowDialog() == DialogResult.OK)
            {
                tb_VendorsCodes.Text = ofdVendorsCodes.FileName;
                path.NamefileVC = tb_VendorsCodes.Text;
            }
            Properties.Settings.Default.VendorsCodes_Value = tb_VendorsCodes.Text;
            Properties.Settings.Default.Save();
        }
        private void but_Acron_Click(object sender, EventArgs e) //FacadesCodesAcron
        {
            OpenFileDialog ofdFacadesCodeAcron = new OpenFileDialog();
            if (ofdFacadesCodeAcron.ShowDialog() == DialogResult.OK)
            {
                tb_FacadesSizesAcron.Text = ofdFacadesCodeAcron.FileName;
                path.NamefileFCA = tb_FacadesSizesAcron.Text;
            }
            Properties.Settings.Default.FacadesSizesAcron_Value = tb_FacadesSizesAcron.Text;
            Properties.Settings.Default.Save();
        }


        private void but_LoadFile_Click(object sender, EventArgs e) //Inputfile
        {
            OpenFileDialog ofdLoadFile = new OpenFileDialog();
            if (ofdLoadFile.ShowDialog() == DialogResult.OK)
            {
                string tmp = ofdLoadFile.FileName;
                path.NamefileInput = ofdLoadFile.FileName;
            }

            label12.Text = "Файл с планом загружен";
        }
        ExcelO excelO = new ExcelO();
        private void but_Save_Click(object sender, EventArgs e) // SaveFile
        {
            try
            {
                FolderBrowserDialog fbdOutPutFile = new FolderBrowserDialog();
                if (fbdOutPutFile.ShowDialog() == DialogResult.OK)
                {
                    path.NamefileOutput = fbdOutPutFile.SelectedPath;
                    excelO.ShowClear(path, defect, textBox1);
                }
                label13.Text = "Файл c распределением сформирован";
            }
            catch
            {
                textBox1.Clear();
                MessageBox.Show("Закройте файл plan.xlsx");
            }
        }


        private void but_Defect_Click(object sender, EventArgs e)
        {
            try
            {
                IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "," };
                defect.UptoTen = double.Parse(tb_uptoTen.Text, formatter);
                defect.UptoFifty = double.Parse(tb_uptoFifty.Text, formatter);
                defect.UptoEighty = double.Parse(tb_uptoEighty.Text, formatter);
                defect.UptoThreeHundred = double.Parse(tb_uptoThreeHundred.Text, formatter);
                defect.UptoOneThousand = double.Parse(tb_uptoOneThousand.Text, formatter);
                defect.UptoTwoThousand = double.Parse(tb_uptoTwoThousand.Text, formatter);
                defect.FromTwoThousand = double.Parse(tb_fromTwoThousand.Text, formatter);

                //defect.UptoTen = Convert.ToDouble(tb_uptoTen.Text, formatter);
                //defect.UptoFifty = Convert.ToDouble(tb_uptoFifty.Text, formatter);
                //defect.UptoEighty = Convert.ToDouble(tb_uptoEighty.Text, formatter);
                //defect.UptoThreeHundred = Convert.ToDouble(tb_uptoThreeHundred.Text, formatter);
                //defect.UptoOneThousand = Convert.ToDouble(tb_uptoOneThousand.Text, formatter);
                //defect.UptoTwoThousand = Convert.ToDouble(tb_uptoTwoThousand.Text, formatter);
                //defect.FromTwoThousand = Convert.ToDouble(tb_fromTwoThousand.Text, formatter);


                //сохранение данных в xml
                Properties.Settings.Default.uptoTen_Value = tb_uptoTen.Text;
                Properties.Settings.Default.uptoFifty_Value = tb_uptoFifty.Text;
                Properties.Settings.Default.uptoEighty_Value = tb_uptoEighty.Text;
                Properties.Settings.Default.uptoThreeHundred_Value = tb_uptoThreeHundred.Text;
                Properties.Settings.Default.uptoOneThousand_Value = tb_uptoOneThousand.Text;
                Properties.Settings.Default.uptoTwoThousand_Value = tb_uptoTwoThousand.Text;
                Properties.Settings.Default.fromTwoThousand_Value = tb_fromTwoThousand.Text;
                Properties.Settings.Default.Save();

                label11.Text = "Коэффициенты брака успешно сохранены";
            }
            catch
            {
                MessageBox.Show("Ошибка ввода или заполните все поля по браку. Если брака на какой-то интервал нет, то укажите 0.0");
            }
        }


    }
    class PathOfFiles 
    {
        public string NamefileFS { get; set; }
        public string NamefileVC { get; set; }
        public string NamefileFCA { get; set; }
        public string NamefileInput { get; set; }
        public string NamefileOutput { get; set; }
    }
    class Defect
    {
        public double UptoTen { get; set; } //<10
        public double UptoFifty { get; set; } //<50
        public double UptoEighty { get; set; } //<80
        public double UptoThreeHundred { get; set; } //<300
        public double UptoOneThousand { get; set; } //<1000
        public double UptoTwoThousand { get; set; } //<2000
        public double FromTwoThousand { get; set; } //>2000
    }
}
