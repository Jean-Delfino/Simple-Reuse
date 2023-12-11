using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Reuse.UI
{
    public class SelectionOption : MonoBehaviour
    {
        [SerializeField] private SimpleUISelection selection;
        [SerializeField] private Transform arrowPosition;
        [SerializeField] private int line;
        [SerializeField] private int column;
        [SerializeField] private GameObject onClick;

        private Button _button;

        public Transform ArrowPosition => arrowPosition;

        private bool _locked = false;

        public bool Locked => _locked;

        void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void SendSelectionToManager(){
            selection.SetSelection(line, column);
        }

        public void Execute(){
            onClick.SetActive(true);
        }

        public void Select(){
            _button.Select();
        }

        public void MouseLock(){
            _locked = true;
        }

        public void MouseUnlock(){
            _locked = false;
        }
    }
}