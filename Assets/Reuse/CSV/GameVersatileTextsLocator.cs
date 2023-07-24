using System.Collections.Generic;
using UnityEngine;

namespace Reuse.CSV
{
    public static class GameVersatileTextsLocator
    {
        private static VersatileTextsFiles _versatileTextsFilesAsset = new();
        
        private static readonly Dictionary<string, string[]> Texts = new();

        private static int _actualLanguage = 0; //The values in the array of texts are accessed by language 
        
        private static bool _hasBeenInitialized;
        
        public static void InitializeTexts(VersatileTextsFiles files)
        {
            if(_hasBeenInitialized) return;
            
            _versatileTextsFilesAsset = files;

            if (Texts.Count == 0)
            {
                ReadAllVersatileFiles();
            }
        }
        
        public static void ChangeActualLanguage(int actualLanguage)
        {
            _actualLanguage = actualLanguage;
        }

        private static void ReadAllVersatileFiles()
        {
            foreach (var csv in _versatileTextsFilesAsset.files)
            {
                foreach (var csvValues in CommaSeparatedValuesReader.ReadCommaSeparatedFile(csv))
                {
                    Texts.Add(csvValues.Key, csvValues.Value);
                } 
            }
        }

        public static string Localize(string key)
        {
            return !Texts.ContainsKey(key) ? null : Texts[key][_actualLanguage];
        }
    }
}
