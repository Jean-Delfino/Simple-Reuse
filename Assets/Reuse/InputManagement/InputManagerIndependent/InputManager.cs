using System;
using System.Collections.Generic;
using Reuse.Patterns;
using UnityEngine;
using KeyMappingValues = Reuse.InputManagement.InputManagerIndependent.InputMapper.KeyMappingValues;

namespace Reuse.InputManagement.InputManagerIndependent
{
    public class InputManager : Singleton<InputManager>
    {
        public InputMapper inputMapping;
        
        private readonly Dictionary<string, KeyMappingValues> _keyValues = new();

        private new void Awake()
        {
            base.Awake();
            SetKeyValues();
        }

        public static void SetKeyValues()
        {
            var inputMappingDictionary = Instance._keyValues;
            
            foreach (var key in Instance.inputMapping.visibleMapping)
            {
                if (!inputMappingDictionary.ContainsKey(key.id))
                {
                    inputMappingDictionary.Add(key.id, key.value);
                }
                else
                {
                    inputMappingDictionary[key.id] = key.value;
                }
            }
        }

        public static int GetKey(string key)
        {
            return Convert.ToInt32(Input.GetKey(Instance._keyValues[key].positiveKey.value));
        }

        public static int GetAllKey(string key)
        {
            return Convert.ToInt32(Input.GetKey(Instance._keyValues[key].positiveKey.value) || Input.GetKey(Instance._keyValues[key].negativeKey.value));
        }

        public static int GetAxis(string key)
        {
            return Convert.ToInt32(Input.GetKey(Instance._keyValues[key].positiveKey.value)) - 
                   Convert.ToInt32(Input.GetKey(Instance._keyValues[key].negativeKey.value));
        }

        public static int GetAxisLimit(string key, int limit = 1)
        {
            return Mathf.Min(Convert.ToInt32(Input.GetKey(Instance._keyValues[key].positiveKey.value)) +
                             Convert.ToInt32(Input.GetKey(Instance._keyValues[key].negativeKey.value)), limit);
        }

        public static int GetSummedAxis(string key)
        {
            return Convert.ToInt32(Input.GetKey(Instance._keyValues[key].positiveKey.value)) +
                   Convert.ToInt32(Input.GetKey(Instance._keyValues[key].negativeKey.value));
        }
        
        public static string GetKeyValue(string key, bool isNegative){
            return isNegative ? (Instance._keyValues[key].negativeKey.value).ToString() : (Instance._keyValues[key].positiveKey.value).ToString();
        }
    }
}