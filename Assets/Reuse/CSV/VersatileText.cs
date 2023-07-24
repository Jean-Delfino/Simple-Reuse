using System;
using TMPro;
using UnityEngine;

namespace Reuse.CSV
{
    public class VersatileText : MonoBehaviour
    {
        [SerializeField] private string textKey;
        
        private TextMeshProUGUI _text;
        public void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            GameVersatileTextsController.Subscribe(this);
        }

        private void OnDestroy()
        {
            GameVersatileTextsController.Unsubscribe(this);
        }

        private void OnEnable()
        {
            SetText();
        }

        public void SetText()
        {
            var newText = GameVersatileTextsLocator.Localize(textKey);
            if(newText == null) return;
            
            _text.text = newText;
        }
    }
}