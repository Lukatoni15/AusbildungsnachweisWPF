using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using iTextSharp.text;
using PageSize = iTextSharp.text.PageSize;

namespace Ausbildungsnachweis_erstellen_WPF.Classes
{
    internal class Pdfcreator
    {
        private readonly Ausbildungsnachweis _nachweis;
        private PdfStamper _pdfStamper;
        private PdfReader _pdfReader;
        private PdfContentByte _pdfContentByte;

        public Pdfcreator(Ausbildungsnachweis ausbildungsnachweis)
        {
            _nachweis = ausbildungsnachweis;
        }

        public void CreatePdfFromScratch()
        {
            SetPdfReader_setFilestream();
            SetContenByte();
            CreateAusbildungsnachweisPdf();
        }

        private void SetPdfReader_setFilestream()
        {
            _pdfReader                                   = new PdfReader(NewDocument());
            FileStream fileStreamForAusbildungsnachweis = new FileStream(CreatePathForAsubildungsnachweis(), FileMode.Create);
            _pdfStamper                                  = new PdfStamper(_pdfReader, fileStreamForAusbildungsnachweis);
        }

        private void SetContenByte()
        {
            _pdfContentByte = _pdfStamper.GetOverContent(1);
        }

        private string NewDocument()
        {
            string newDocumentPath = Directory.GetCurrentDirectory() + "\\Vorlage.pdf";
            Document doc = new Document(PageSize.A4, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(newDocumentPath, FileMode.Create));

            doc.Open();
            doc.Add(new Paragraph(" "));
            doc.Close();
            writer.Close();

            return newDocumentPath;
        }

        private string CreatePathForAsubildungsnachweis()
        {
            string nachweisNummer = _nachweis.GetNummer();
            string[] von          = _nachweis.GetVon().Split('.');
            string bis            = _nachweis.GetBis();
            string pdfName        = $"\\Ausbildungsnachweis Nr. {nachweisNummer} {von[0]}.{von[1]}-{bis}.pdf";
            return _nachweis.GetToPath() + pdfName;
        }

        private void CreateAusbildungsnachweisPdf()
        {
            using(_pdfStamper)
            {
                WriteAllgemeineDatenIntoPdf();
                WriteBetriebsaufgabenIntoPdf();
                WriteSchulungenIntoPdf();
                WriteSchulunterrichtIntoPdf();
                WriteUnterschriftenIntoPdf();
            }
        }
       
        private void WriteAllgemeineDatenIntoPdf()
        {
            AddRectangleToDocument(68, 527, 704, 771.5f);
            
            CreateText("Name, Vorname:",                126.25f, 758f,   true,  14);
            CreateText("Ausbildungsnachweis",           144.7f,  742,    true,  14);
            CreateText("Nr.",                           228.5f,  742,    false, 10);
            CreateText("für die Woche vom",             317f,    742.0f, false, 10);
            CreateText("bis",                           425,     742,    false, 10);
            CreateText("Abteilung oder Arbeitsgebiet:", 135,     718f,   false, 10);
            CreateText("Ausbildungsjahr:",              454.5f,  718,    false, 10);

            createTextfield_withBorder_singleline(183.5f,   416.5f, 753.6f, 771.8f, _nachweis.GetName()              , 14);
            createTextfield_withBorder_singleline(240f,     270f,   738,    752.5f, _nachweis.GetNummer()            , 10);       
            createTextfield_withBorder_singleline(361.5f,   416.5f, 738,    753,    _nachweis.GetVon()               , 10);
            createTextfield_withBorder_singleline(435,      490,    738,    753,    _nachweis.GetBis()               , 10);           
            createTextfield_withBorder_singleline(202,      410,    711,    733,    _nachweis.GetAbteilung()         , 10);            
            createTextfield_withBorder_singleline(495,      520,    711,    733,    _nachweis.GetAusbildungsjahr()   , 10);
        }

        private void WriteBetriebsaufgabenIntoPdf()
        {
            AddRectangleToDocument(68, 527, 687, 698.5f);
            CreateText("Betriebliche Tätigkeit", 120.25f, 688.75f, false, 10);

            AddRectangleToDocument(68, 527, 490f, 687);
            createTextfield_withoutBorder_multiline(68, 687, 490f, 686f, _nachweis.GetBetriebsaufgaben());
        }

        private void WriteSchulungenIntoPdf()
        {
            AddRectangleToDocument(68, 527, 471f, 484.5f);
            CreateText("Unterweisungen, betrieblicher Unterricht, sonstige Schulungen", 211.25f, 473.5f, false, 10);

            AddRectangleToDocument(68, 527, 311.9f, 471f);
            createTextfield_withoutBorder_multiline(68, 687, 311.9f, 471f, _nachweis.GetSchulungen());
        }

        private void WriteSchulunterrichtIntoPdf()
        {
            AddRectangleToDocument(68 , 527, 292.9f, 306.5f);
            CreateText("Berufsschule (Unterrichtsthemen)", 147f, 295.5f, false, 10);

            AddRectangleToDocument(68, 527, 133, 292.9f);
            createTextfield_withoutBorder_multiline(68, 687, 133, 292.9f, _nachweis.GetSchule());
        }

        private void WriteUnterschriftenIntoPdf()
        {
            AddRectangleToDocument(68,      297.5f, 58, 127.6f);
            AddRectangleToDocument(297.5f,  527,    58, 127.6f);

            CreateText("Datum:",                                    87f,    107,    false, 10);
            CreateText("_______________________________________",   180,    73,     false, 10);
            CreateText("Unterschrift Auszubildender",               133,    61.5f,  false, 10);
            CreateText("Datum:", 319.5f, 110, false, 10);
            CreateText("_______________________________________",   409.5f, 73,     false, 10);
            CreateText("Unterschrift Auszubildender",               362.5f, 61.5f,  false, 10);

            createTextfield_withoutBorder_singleline(107,       212,    100, 122, " ");
            createTextfield_withoutBorder_singleline(336.5f,    441.5f, 100, 122, "  ");            
        }

        private void AddRectangleToDocument(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben)
        {
            Rectangle rec = createRectangle(seiteLinks, seiteRechts, seiteUnten, seiteOben);
            _pdfContentByte.Rectangle(rec);
        }

        private void CreateText(string text, float x, float y, bool bold, int fontsize)
        {
            BaseFont bf = BaseFont.CreateFont(bold ? BaseFont.HELVETICA_BOLD : BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            _pdfContentByte.SetColorFill(BaseColor.DARK_GRAY);
            _pdfContentByte.SetFontAndSize(bf, fontsize);
            _pdfContentByte.BeginText();
            _pdfContentByte.ShowTextAligned(1, text, x, y, 0);
            _pdfContentByte.EndText();

        }

        private void createTextfield_withBorder_singleline(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text, int fontsize)
        {
            TextField textField     = CreateTextField(seiteLinks, seiteRechts, seiteUnten, seiteOben, text, fontsize);
            textField.BorderColor   = BaseColor.RED;              
            _pdfStamper.AddAnnotation(textField.GetTextField(), 1);
        }

        private void createTextfield_withoutBorder_multiline(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text)
        {
            TextField textField = CreateTextField(seiteLinks, seiteRechts, seiteUnten, seiteOben, text, 13);
            textField.Options   = BaseField.MULTILINE;
            _pdfStamper.AddAnnotation(textField.GetTextField(), 1);
        }

        private void createTextfield_withoutBorder_singleline(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text)
        {
            TextField textField = CreateTextField(seiteLinks, seiteRechts, seiteUnten, seiteOben, text, 13);
            _pdfStamper.AddAnnotation(textField.GetTextField(), 1);
        }

        private TextField CreateTextField(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text, int fontsize)
        {
            TextField textField = new TextField(_pdfStamper.Writer,createRectangle(seiteLinks, seiteRechts, seiteUnten, seiteOben), text)
            {
                Rotation = 0, 
                Options  = 0, 
                FontSize = fontsize, 
                Text     = text
            };
            return textField;
        }

        private Rectangle createRectangle(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben)
        {
            Rectangle rec = new Rectangle(seiteLinks, seiteUnten, seiteRechts, seiteOben)
            {
                Border      = Rectangle.BOX, 
                BorderWidth = 0.5f, 
                BorderColor = BaseColor.BLACK
            };
            return rec;
        }
    }
}
