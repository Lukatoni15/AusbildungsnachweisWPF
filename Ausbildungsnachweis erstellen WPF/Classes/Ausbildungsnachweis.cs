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
        private readonly string _allgemeineDatenFile = Directory.GetCurrentDirectory() + "\\allgemeine Daten\\Text.txt";
        private readonly string _betriebsaufgabenFile = Directory.GetCurrentDirectory() + "\\Betrieb.txt";

        private string _name;
        private string _nummer;
        private string _von;
        private string _bis;
        private string _abteilung;
        private string _ausbildungsjahr;
        private string _toPath;

        private string _betriebsaufgaben;
        private string _schulungen;
        private string _schule;

        public Ausbildungsnachweis()
        {
            GetAllgemeineDatenFromFile();
            //getBetriebsaufgabenFromFile();
        }

        private void GetAllgemeineDatenFromFile()
        {
            Boolean doesFileExists = File.Exists(_allgemeineDatenFile);
            if(doesFileExists)
            {
                string[] allgemeineDaten     = File.ReadAllLines(_allgemeineDatenFile);
                _name                        = allgemeineDaten[0];
                _nummer                      = allgemeineDaten[1];
                _von                         = allgemeineDaten[2];
                _bis                         = allgemeineDaten[3];
                _ausbildungsjahr             = allgemeineDaten[4];
                _abteilung                   = allgemeineDaten[5];
                _toPath                      = allgemeineDaten[6];
            }
        }

        private void GetBetriebsaufgabenFromFile()
        {
            Boolean doesFileExists = File.Exists(_betriebsaufgabenFile);
            if (doesFileExists)
            {
                //_betriebsaufgaben = File.ReadAllLines(_betriebsaufgabenFile);
            }
        }

        public void WriteAllgemineDatenToFile()
        {
            
        }

        public void WriteBetriebsaufgabenToFile()
        {
            
        }        

        public string GetName()
        {
            return _name;
        }

        public string GetNummer()
        {
            return _nummer;
        }

        public string GetVon()
        {
            return _von;
        }

        public string GetBis()
        {
            return _bis;
        }

        public string GetAbteilung()
        {
            return _abteilung;
        }

        public string GetAusbildungsjahr()
        {
            return _ausbildungsjahr;
        }

        public string GetBetriebsaufgaben()
        {
            return _betriebsaufgaben;
        }

        public string GetSchulungen()
        {
            return _schulungen;
        }

        public string GetSchule()
        {
            return _schule;
        }

        public string GetToPath()
        {
            return _toPath;
        }
    }
}