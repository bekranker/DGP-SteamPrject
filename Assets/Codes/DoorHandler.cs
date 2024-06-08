using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] SwitchHandler _switchHandler;
    [SerializeField] private Transform _door;
    [SerializeField] private Transform _to;
    [SerializeField] private float _speed;


    private Vector3 _startPos;

    void Start()
    {
        _startPos = _door.position;
    }

    void OnEnable()
    {
        _switchHandler.OnSwitch += OpenDoor;
    }

    void OnDisable()
    {
        _switchHandler.OnSwitch -= OpenDoor;
    }


    void OpenDoor()
    {
        _door.DOMove(_to.position, _speed);
    }
}