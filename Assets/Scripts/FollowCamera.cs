using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;


public class FollowCamera : MonoBehaviour
{
    [Header("Propoeties")]
    [SerializeField, Range(0.05f, 10f)] private float _speed = 0.3f;
    [SerializeField, Range(0.05f, 10f)] private float _zoomSpeed = 0.2f;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _lookDown;
    [SerializeField] private float _lookUp;



    [Header("Components")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _rotationParent;
    [SerializeField] private Transform _zoomParent;
    [SerializeField] private InputHandler _inputHandler;
    private Vector3 _zoomStartPosition;
    private float _startOffsetY;


    void Start()
    {
        _zoomStartPosition = _zoomParent.position;
        _startOffsetY = _offset.y;
    } 
    void Update()
    {
        LookSomeWhere();
    }
    void LateUpdate()
    {
        FollowSmoothly();
    }
    private void FollowSmoothly()
    {
        transform.position = Vector3.Lerp(transform.position,
                                new Vector3(_target.position.x, _target.position.y, transform.position.z) + _offset, 
                                Time.deltaTime * _speed);
    }
    public void TakeDamage()
    {
        DOTween.Kill(_zoomParent);
        var sequence = DOTween.Sequence();
        sequence.SetUpdate(true);
        sequence.Join(_zoomParent.DOMoveZ(_zoomParent.position.z - 2f, _zoomSpeed)).SetEase(Ease.OutFlash);
        sequence.Join(_rotationParent.DORotate(new Vector3(0, 0, 1) * Mathf.Sign(Random.Range(-1, 1)), _zoomSpeed)).SetEase(Ease.InOutSine);
        sequence.Append(_zoomParent.DOMoveZ(_zoomStartPosition.z, _zoomSpeed)).SetEase(Ease.InFlash);
        sequence.Join(_zoomParent.DORotate(new Vector3(0, 0, 0), _zoomSpeed)).SetEase(Ease.InOutSine);
    }
    private void LookSomeWhere()
    {
        print(_inputHandler.MoveInput.y);
        if (_inputHandler.MoveInput.y < 0)
        {
            _offset.y = _lookDown;
        }
        if (_inputHandler.MoveInput.y > 0)
        {
            _offset.y = _lookUp;
        }
        if (_inputHandler.MoveInput.y == 0)
        {
            _offset.y = _startOffsetY;
        }
        
    }
}