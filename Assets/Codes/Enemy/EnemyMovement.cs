using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Grounded _grounded;
    public float MoveDirection;
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    public void MoveTo()
    {
        MoveDirection = Mathf.Sign(_player.position.x - transform.position.x);
        //Moving
        Debug.Log("Moving");
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(_player.position.x, transform.position.y), 
            _enemy.EnemyT.Speed * Time.deltaTime);
        if (_player.position.x > transform.position.x)
        {
            _enemy.SP.flipX = false;
        }
        else
        {
            _enemy.SP.flipX = true;
        }
    }
    public bool CanFollow()
    {
        return _grounded.IsGrounded() &&
               PlayerFarFromUs() &&
               _grounded.MyGround() == _player.GetComponent<Grounded>().MyGround();
    }
    public bool CanFollowArcher()
    {
        return 
               PlayerFarFromUs();
    }
    public bool PlayerFarFromUs()
    {
        return Vector2.Distance(transform.position, _player.transform.position) > _enemy.EnemyT.DistanceToPlayer 
               && Vector2.Distance(transform.position, _player.transform.position) <= _enemy.EnemyT.MaxDistanceToPlayer;
    }
    public bool PlayerCloseFromUs()
    {
        return Vector2.Distance(transform.position, _player.transform.position) <= _enemy.EnemyT.DistanceToPlayer;
    }
}