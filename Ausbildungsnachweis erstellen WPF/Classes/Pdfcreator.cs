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
    class Pdfcreator
    {
        Ausbildungsnachweis nachweis;
        PdfStamper pdfStamper;
        PdfReader pdfReader;
        PdfContentByte pdfContentByte;

        public Pdfcreator(Ausbildungsnachweis ausbildungsnachweis)
        {
            nachweis = ausbildungsnachweis;
        }

        public void pdfFromScratch()
        {
            setPdfReader_setPdfReader_setFilestream();
            setContenByte();
            createAusbildungsnachweisPDF();
        }

        private void setPdfReader_setPdfReader_setFilestream()
        {
            pdfReader                                   = new PdfReader(newDocument());
            FileStream fileStreamForAusbildungsnachweis = new FileStream(createPathForAsubildungsnachweis(), FileMode.Create);
            pdfStamper                                  = new PdfStamper(pdfReader, fileStreamForAusbildungsnachweis);
        }

        private void setContenByte()
        {
            pdfContentByte = pdfStamper.GetOverContent(1);
        }

        private string newDocument()
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

        private string createPathForAsubildungsnachweis()
        {
            string nachweisNummer = nachweis.getNummer();
            string[] von = nachweis.getVon().Split('.');
            string bis = nachweis.getBis();
            string pdfName = $"\\Ausbildungsnachweis Nr. {nachweisNummer} {von[0]}.{von[1]}-{bis}.pdf";
            return nachweis.getToPath() + pdfName;
        }

        private void createAusbildungsnachweisPDF()
        {
            using(pdfStamper)
            {
                writeAllgemeineDatenIntoPdf();
                writeBetriebsaufgabenIntoPDF();
                writeSchulungenIntoPDF();
                writeSchulunterrichtIntoPDF();
                writeUnterschriftenIntoPDF();
            }
        }
       
        private void writeAllgemeineDatenIntoPdf()
        {
            addRectangleToDocument(68, 527, 704, 771.5f);
            
            createText("Name, Vorname:",                126.25f, 758f,   true,  14);
            createText("Ausbildungsnachweis",           144.7f,  742,    true,  14);
            createText("Nr.",                           228.5f,  742,    false, 10);
            createText("für die Woche vom",             317f,    742.0f, false, 10);
            createText("bis",                           425,     742,    false, 10);
            createText("Abteilung oder Arbeitsgebiet:", 135,     718f,   false, 10);
            createText("Ausbildungsjahr:",              454.5f,  718,    false, 10);

            createTextfield_withBorder_singleline(183.5f,   416.5f, 753.6f, 771.8f, nachweis.getName()              , 14);
            createTextfield_withBorder_singleline(240f,     270f,   738,    752.5f, nachweis.getNummer()            , 10);       
            createTextfield_withBorder_singleline(361.5f,   416.5f, 738,    753,    nachweis.getVon()               , 10);
            createTextfield_withBorder_singleline(435,      490,    738,    753,    nachweis.getBis()               , 10);           
            createTextfield_withBorder_singleline(202,      410,    711,    733,    nachweis.getAbteilung()         , 10);            
            createTextfield_withBorder_singleline(495,      520,    711,    733,    nachweis.getAusbildungsjahr()   , 10);
        }

        private void writeBetriebsaufgabenIntoPDF()
        {
            addRectangleToDocument(68, 527, 687, 698.5f);
            createText("Betriebliche Tätigkeit", 120.25f, 688.75f, false, 10);

            addRectangleToDocument(68, 527, 490f, 687);
            createTextfield_withoutBorder_multiline(68, 687, 490f, 686f, nachweis.getBetriebsaufgaben());
        }

        private void writeSchulungenIntoPDF()
        {
            addRectangleToDocument(68, 527, 471f, 484.5f);
            createText("Unterweisungen, betrieblicher Unterricht, sonstige Schulungen", 211.25f, 473.5f, false, 10);

            addRectangleToDocument(68, 527, 311.9f, 471f);
            createTextfield_withoutBorder_multiline(68, 687, 311.9f, 471f, nachweis.getSchulungen());
        }

        private void writeSchulunterrichtIntoPDF()
        {
            addRectangleToDocument(68 , 527, 292.9f, 306.5f);
            createText("Berufsschule (Unterrichtsthemen)", 147f, 295.5f, false, 10);

            addRectangleToDocument(68, 527, 133, 292.9f);
            createTextfield_withoutBorder_multiline(68, 687, 133, 292.9f, nachweis.getSchule());
        }

        private void writeUnterschriftenIntoPDF()
        {
            addRectangleToDocument(68,      297.5f, 58, 127.6f);
            addRectangleToDocument(297.5f,  527,    58, 127.6f);

            createText("Datum:",                                    87f,    107,    false, 10);
            createText("_______________________________________",   180,    73,     false, 10);
            createText("Unterschrift Auszubildender",               133,    61.5f,  false, 10);
            createText("Datum:", 319.5f, 110, false, 10);
            createText("_______________________________________",   409.5f, 73,     false, 10);
            createText("Unterschrift Auszubildender",               362.5f, 61.5f,  false, 10);

            createTextfield_withoutBorder_singleline(107,       212,    100, 122, " ");
            createTextfield_withoutBorder_singleline(336.5f,    441.5f, 100, 122, "  ");            
        }

        private void addRectangleToDocument(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben)
        {
            Rectangle rec = createRectangle(seiteLinks, seiteRechts, seiteUnten, seiteOben);
            pdfContentByte.Rectangle(rec);
        }

        private void createText(string text, float x, float y, bool bold, int fontsize)
        {
            BaseFont bf = BaseFont.CreateFont(bold ? BaseFont.HELVETICA_BOLD : BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            pdfContentByte.SetColorFill(BaseColor.DARK_GRAY);
            pdfContentByte.SetFontAndSize(bf, fontsize);
            pdfContentByte.BeginText();
            pdfContentByte.ShowTextAligned(1, text, x, y, 0);
            pdfContentByte.EndText();

        }

        private void createTextfield_withBorder_singleline(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text, int fontsize)
        {
            TextField textField     = createTextField(seiteLinks, seiteRechts, seiteUnten, seiteOben, text, fontsize);
            textField.BorderColor   = BaseColor.RED;              
            pdfStamper.AddAnnotation(textField.GetTextField(), 1);
        }

        private void createTextfield_withoutBorder_multiline(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text)
        {
            TextField textField = createTextField(seiteLinks, seiteRechts, seiteUnten, seiteOben, text, 13);
            textField.Options   = TextField.MULTILINE;
            pdfStamper.AddAnnotation(textField.GetTextField(), 1);
        }

        private void createTextfield_withoutBorder_singleline(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text)
        {
            TextField textField = createTextField(seiteLinks, seiteRechts, seiteUnten, seiteOben, text, 13);
            pdfStamper.AddAnnotation(textField.GetTextField(), 1);
        }

        private TextField createTextField(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben, string text, int fontsize)
        {
            TextField textField = new TextField(pdfStamper.Writer, createRectangle(seiteLinks, seiteRechts, seiteUnten, seiteOben), text);
            textField.Rotation  = 0;
            textField.Options   = 0;
            textField.FontSize  = fontsize;
            textField.Text      = text;
            return textField;
        }

        private Rectangle createRectangle(float seiteLinks, float seiteRechts, float seiteUnten, float seiteOben)
        {
            Rectangle rec   = new Rectangle(seiteLinks, seiteUnten, seiteRechts, seiteOben);
            rec.Border      = Rectangle.BOX;
            rec.BorderWidth = 0.5f;
            rec.BorderColor = BaseColor.BLACK;
            return rec;
        }
    }
}
