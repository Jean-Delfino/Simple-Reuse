using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reuse.CSV
{
    public class GameVersatileTextsController : MonoBehaviour
    {
        [SerializeField] private VersatileTextsFiles files;
        
        private static VersatileTextsFiles _files;

        private static readonly List<VersatileText> VersatileTexts = new();
        public void Awake()
        {
            _files = files;
            GameVersatileTextsLocator.InitializeTexts(_files);
            ChangeActualLanguage(files.actualLanguage);
        }

        public static void ChangeActualLanguage(int newLanguage)
        {
            _files.actualLanguage = newLanguage; //This saves the file in memory
            GameVersatileTextsLocator.ChangeActualLanguage(_files.actualLanguage);
            SetAllTexts();
        }
        
        private static void SetAllTexts()
        {
            foreach (var text in VersatileTexts)
            {
                text.SetText();
            }
        }

        public static void Subscribe(VersatileText text)
        {
            VersatileTexts.Add(text);
        }
        
        public static void Unsubscribe(VersatileText text)
        {
            VersatileTexts.Remove(text);
        }
    }
}