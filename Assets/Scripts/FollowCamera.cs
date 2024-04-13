using DG.Tweening;
using UnityEngine;


public class FollowCamera : MonoBehaviour
{
    [Header("Propoeties")]
    [SerializeField, Range(0.05f, 10f)] private float _speed = 0.3f;
    [SerializeField] private float _rotationMultiplier;

    [Header("Components")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _rotationParent;

    void LateUpdate()
    {
        FollowSmoothly();
        RotatinHandler();
    }
    private void FollowSmoothly()
    {
        transform.position = Vector3.Lerp(transform.position,
                                new Vector3(_target.position.x, _target.position.y, transform.position.z), 
                                Time.deltaTime * _speed);
    }
    private void RotatinHandler()
    {
        var direction = Mathf.Sign(_target.position.x - transform.position.x);
        _rotationParent.transform.DORotate(Vector3.up * direction * _rotationMultiplier, 0.2f);
    }
}