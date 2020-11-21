using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ausbildungsnachweis_erstellen_WPF.Classes;


namespace Ausbildungsnachweis_erstellen_WPF.Classes
{
    class Ausbildungsnachweis
    {
        private string allgemeineDatenFile = Directory.GetCurrentDirectory() + "\\allgemeine Daten\\Text.txt";
        private string betriebsaufgabenFile = Directory.GetCurrentDirectory() + "\\Betrieb.txt";

        private string name;
        private string nummer;
        private string von;
        private string bis;
        private string abteilung;
        private string ausbildungsjahr;
        private string toPath;

        private string betriebsaufgaben;
        private string schulungen;
        private string schule;

        public Ausbildungsnachweis()
        {
            getAllgemeineDatenFromFile();
            //getBetriebsaufgabenFromFile();
        }

        public void getAllgemeineDatenFromFile()
        {
            Boolean doesFileExists = File.Exists(allgemeineDatenFile);
            if (doesFileExists)
            {
                string[] allgemeineDaten    = File.ReadAllLines(allgemeineDatenFile);
                name                        = allgemeineDaten[0];
                nummer                      = allgemeineDaten[1];
                von                         = allgemeineDaten[2];
                bis                         = allgemeineDaten[3];
                ausbildungsjahr             = allgemeineDaten[4];
                abteilung                   = allgemeineDaten[5];
                toPath                      = allgemeineDaten[6];
            }
        }

        //public void getBetriebsaufgabenFromFile()
        //{
        //    Boolean doesFileExists = File.Exists(betriebsaufgabenFile);
        //    if (doesFileExists)
        //    {
        //        betriebsaufgaben = File.ReadAllLines(betriebsaufgabenFile);
        //    }
        //}

        public void writeAllgemineDatenToFile()
        {

        }

        public void writeBetriebsaufgabenToFile()
        {

        }        

        public string getName()
        {
            return name;
        }

        public string getNummer()
        {
            return nummer;
        }

        public string getVon()
        {
            return von;
        }

        public string getBis()
        {
            return bis;
        }

        public string getAbteilung()
        {
            return abteilung;
        }

        public string getAusbildungsjahr()
        {
            return ausbildungsjahr;
        }

        public string getBetriebsaufgaben()
        {
            return betriebsaufgaben;
        }

        public string getSchulungen()
        {
            return schulungen;
        }

        public string getSchule()
        {
            return schule;
        }

        public string getToPath()
        {
            return toPath;
        }

        public void setName(string pName)
        {
            this.name = pName;
        }

        public void setNummer(string pNummer)
        {
            nummer = pNummer;
        }

        public void setVon(string pVon)
        {
            von = pVon;
        }

        public void setBis(string pBis)
        {
            bis = pBis;
        }

        public void setAbteilung(string pAbteilung)
        {
            abteilung = pAbteilung;
        }

        public void setAusbildungsjahr(string pAusbildungsjahr)
        {
            ausbildungsjahr = pAusbildungsjahr;
        }

        public void setBetriebsaufgaben(string pBetriebsaufgaben)
        {
            betriebsaufgaben = pBetriebsaufgaben;
        }

        public void setSchulungen(string pSchulungen)
        {
            schulungen = pSchulungen;
        }

        public void setSchule(string pSchule)
        {
            schule = pSchule;
        }

        public void setToPath(string pToPath)
        {
            toPath = pToPath;
        }
    }
}