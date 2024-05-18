using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsHandler : MonoBehaviour
{
    [SerializeField] private SwitchHandler _switchHandler;
    [SerializeField] private List<BreakableGround> _breakableGrounds;
    [SerializeField] private float _spawnDelay;
    

    void OnEnable()
    {
        _switchHandler.OnSwitch += SpawnPlatforms;
    }

    void OnDisable()
    {
        _switchHandler.OnSwitch -= SpawnPlatforms;
    }
    void SpawnPlatforms()
    {
        StartCoroutine(spawnThePlatforms());
    }
    IEnumerator spawnThePlatforms()
    {
        for (int i = 0; i < _breakableGrounds.Count; i++)
        {
            _breakableGrounds[i].GetMe();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}