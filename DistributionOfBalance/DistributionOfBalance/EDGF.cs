using System;
using System.Windows.Forms;

namespace DistributionOfBalance
{
    public partial class EDGF : Form
    {
        public EDGF()
        {
            InitializeComponent();
        }
        WriterOutPutFile writer = new WriterOutPutFile();
        Logic logic= new Logic();
        PathOfFile Path = new PathOfFile();

        private void EDGF_Load(object sender, EventArgs e)
        {
            try
            {
                tb_VendorsCodesOriginal.Text = Properties.Settings.Default.VendorsCodesOrigValue;
                Path.PathOfVendorsCodesOriginal = tb_VendorsCodesOriginal.Text;
            }
            catch { label2.Text = "Укажите путь к файлу!"; };
        }

        private void but_PathOfFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFDVendorsCodesOrig = new OpenFileDialog();
            if (OFDVendorsCodesOrig.ShowDialog() == DialogResult.OK)
            {
                tb_VendorsCodesOriginal.Text = OFDVendorsCodesOrig.FileName;
                Path.PathOfVendorsCodesOriginal = tb_VendorsCodesOriginal.Text;
            }
            Properties.Settings.Default.VendorsCodesOrigValue = tb_VendorsCodesOriginal.Text;
            Properties.Settings.Default.Save();
        }

        private void but_LoadAndOutPut_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFDInPut = new OpenFileDialog();
            if(OFDInPut.ShowDialog() == DialogResult.OK) { Path.PathInputFile = OFDInPut.FileName; }
            try
            {
                FolderBrowserDialog FBDSaveFile = new FolderBrowserDialog();
                if(FBDSaveFile.ShowDialog()==DialogResult.OK)
                {
                    Path.PathOutPutFile = FBDSaveFile.SelectedPath;
                    writer.WriteExel(Path);
                }
                label2.Text = "Файл с распределением остатков сформирован";
            }
            catch { MessageBox.Show("Закройте файл с распределением остатков для продолжения работы"); }
        }
    }
    class PathOfFile
    {
        public string PathOfVendorsCodesOriginal { get; set; }
        public string PathInputFile { get; set; }
        public string PathOutPutFile { get; set; }
    }
}
