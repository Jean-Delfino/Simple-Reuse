using System;
using UnityEngine;

public class TurnActiveByTime : MonoBehaviour
{
    [SerializeField] private float timeInactive;
    [SerializeField] private float timeActive;
    [SerializeField] private GameObject toSet;

    [SerializeField] private bool setTimeAtStart;
    private float _actualTime;

    private void Start()
    {
        if(setTimeAtStart) _actualTime = toSet.activeInHierarchy ? timeActive : timeInactive;
    }

    private void Update()
    {
        if (_actualTime > 0)
        {
            _actualTime -= Time.deltaTime;
            return;
        }
        
        toSet.SetActive(!toSet.activeInHierarchy);

        _actualTime = toSet.activeInHierarchy ? timeActive : timeInactive;
    }
}