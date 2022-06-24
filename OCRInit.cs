using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using SolidFramework;

namespace SolidConverter
{
    class OCRInit
    {
        public static bool IrisInstalled()
        {
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string irisFolder = Path.Combine(dataPath, @"SolidDocuments\Iris");
            
            if (!Directory.Exists(irisFolder))
            {
                return false;
            }

            string licFile = Path.Combine(irisFolder, "idrs_software_keys_15.inf");

            if (!File.Exists(licFile))
            {
                return false;
            }

            // Check that the inf has unlock info.
            List<string> Temp = new List<string>();

            using (StreamReader sr = new StreamReader(licFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string clean = line.Trim('\0');
                    clean = clean.Remove(0, 1);
                    if (!string.IsNullOrEmpty(clean))
                    {
                        Temp.Add(line);
                    }
                }
                sr.Close();
            }

            if (Temp.Count < 1)
                return false;

            return true;
        }

        public static StringsArray GetOCRLanguages()
        {
            StringsArray langs = SolidFramework.Imaging.Ocr.Languages;
            if (IrisInstalled() != false)
            {
                langs.Add("ko");
                langs.Add("ja");
                langs.Add("zh");
                langs.Add("zt");
            }
            return langs;
        }
    }
}
