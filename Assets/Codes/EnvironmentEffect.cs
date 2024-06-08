using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnvironmentEffect : MonoBehaviour
{
    [SerializeField] private Color _backColor;
    [SerializeField] private Color _midColor;
    [SerializeField] private Color _frontColor;

    [SerializeField] private int _backIndex;
    [SerializeField] private int _middleIndex;
    [SerializeField] private int _frontIndex;


    private List<SpriteRenderer> _allEnvironments = new List<SpriteRenderer>();



    [Button]
    private void SetAllEnvironmetnts()
    {
        foreach (var environment in GetComponentsInChildren<SpriteRenderer>())
        {
            _allEnvironments.Add(environment);
        }
    }
    [Button]
    private void ResetAlLEnvironments()
    {
        _allEnvironments.Clear();
    }
    [Button]
    private void ChangeEnvironmentColors()
    {
        foreach (var environment in _allEnvironments)
        {
            if (environment.sortingOrder == _backIndex)
            {
                environment.color = _backColor;
            }
            else if (environment.sortingOrder == _middleIndex)
            {
                environment.color = _midColor;
            }
            else if(environment.sortingOrder == _frontIndex)
            {
                environment.color = _frontColor;
            }
        }
    }

}
