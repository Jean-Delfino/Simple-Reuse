using System.Collections.Generic;
using Reuse.CSV;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Reuse.UI
{
    public class VersatileDropdown : MonoBehaviour, IVersatileText
    {
        [SerializeField] private TMP_Dropdown tmpDropdown;
        [SerializeField] private string[] keys;

        private int _value;
        private void OnDisable()
        {
            GameVersatileTextsController.Unsubscribe(this);
            _value = tmpDropdown.value;
        }

        private void OnEnable()
        {
            GameVersatileTextsController.Subscribe(this);
            SetText();
            tmpDropdown.value = _value;
        }
        
        public void SetText()
        {
            if (tmpDropdown.options != null && tmpDropdown.options.Count == keys.Length)
            {
                LocalizeOptionsText();
                return;
            }

            AddOptions();
        }

        private void LocalizeOptionsText()
        {
            for (int i = 0; i < keys.Length; i++)
            {
                tmpDropdown.options[i].text = GameVersatileTextsLocator.Localize(keys[i]);
            }

            tmpDropdown.captionText.text = tmpDropdown.options[GetDropdownValue()].text;
        }
        private void AddOptions()
        {
            tmpDropdown.ClearOptions();
            var dropdownOptions = new List<TMP_Dropdown.OptionData>();

            foreach (var key in keys)
            {
                var text = GameVersatileTextsLocator.Localize(key);
                dropdownOptions.Add(new TMP_Dropdown.OptionData(text));
            }
            
            tmpDropdown.AddOptions(dropdownOptions);
        }

        public void SetText(string key)
        {
            SetText();
        }

        public void AddListener(UnityAction<int> call)
        {
            tmpDropdown.onValueChanged.AddListener(call);
        }
        
        public int GetDropdownValue()
        {
            return tmpDropdown.value;
        }

        public void SetDropdownValue(int value)
        {
            _value = value;
        }
    }
}