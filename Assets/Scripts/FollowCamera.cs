using UnityEngine;


public class FollowCamera : MonoBehaviour
{
    [Header("Propoeties")]
    [SerializeField, Range(0.05f, 10f)] private float _speed = 0.3f;
    [SerializeField] private Vector2 _rotationMinMax;

    [Header("Components")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _rotationParent;

    void LateUpdate()
    {
        FollowSmoothly();
    }
    private void FollowSmoothly()
    {
        transform.position = Vector3.Lerp(transform.position,
                                new Vector3(_target.position.x, _target.position.y, transform.position.z), 
                                Time.deltaTime * _speed);
    }
    private void RotatinHandler()
    {
        // _rotationParent.transform
    }
}