using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ausbildungsnachweis_erstellen_WPF.Classes;
using System.IO;

namespace Ausbildungsnachweis_erstellen_WPF
{
    public partial class MainWindow : Window
    {
        private readonly Ausbildungsnachweis _nachweis = new Ausbildungsnachweis();
        public MainWindow()
        {
            InitializeComponent();
            SetAllgemeineDaten();
            Pdfcreator pdf = new Pdfcreator(_nachweis);
            //pdf.pdfFromScratch();
        }

        private void SetAllgemeineDaten()
        {
            PathForNewPdf.Text = _nachweis.GetToPath();
        }

        private void SelectDirectory(object sender, RoutedEventArgs e)
        {
            using (FolderBrowserDialog file = new FolderBrowserDialog())
            {
                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PathForNewPdf.Text = file.SelectedPath;
                }
            }
        }

    }
}
