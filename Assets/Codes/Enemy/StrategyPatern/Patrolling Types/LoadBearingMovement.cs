using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LoadBearingMovement : Patrolling
{

    private Vector3 _targetPosition;
    private bool _canMove, _canInvoke;
    private float _carryDelayCounter;




    public override void PatrollingFunction(Enemy enemy)
    {
        if (_canMove)
        {
            enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position,
            _targetPosition,
            enemy.EnemyT.PatrollMovementSpeed * Time.deltaTime
            );
        }
        if (Mathf.RoundToInt(enemy.transform.position.x) == Mathf.RoundToInt(_targetPosition.x))
        {
            if (_carryDelayCounter >= 0)
            {
                _canMove = false;
                _carryDelayCounter -= Time.deltaTime;
                if (_canInvoke)
                {
                    print("idle");
                    enemy.GetComponent<Animator>().Play("Idle");
                    _canInvoke = false;
                }
            }
            else
            {
                LoadBearing(enemy, enemy.GetComponent<Animator>());
            }
        }
    }

    public override void PatrollEnterFunction(Enemy enemy, Animator animator)
    {
        _canInvoke = true;
        LoadBearing(enemy, enemy.GetComponent<Animator>());
    }
    
    
    
    
    private void LoadBearing(Enemy enemy, Animator animator)
    {
        Debug.Log("Previous Target Point: " + _targetPosition);
        _targetPosition = TargetPoint(enemy);
        if (_targetPosition.x > enemy.transform.position.x)
        {
            enemy.SP.flipX = false;
        }
        else
        {
            enemy.SP.flipX = true;
        }
        enemy.GetComponent<Animator>().Play("Walk");
        Debug.Log("New Target Point: " + _targetPosition);
        _canMove = true;
        _carryDelayCounter = enemy.EnemyT.PatrollDelay;
        _canInvoke = true;
    }
    private Vector3 TargetPoint(Enemy enemy)
    {
        Vector3 targetPoint;
        Grounded grounded = enemy.Ground;
        GameObject theGround = grounded.MyGround();
        BoxCollider2D groundCollider = theGround.GetComponent<BoxCollider2D>();
        float boundX = groundCollider.bounds.extents.x;
        float boundLeft = (theGround.transform.position.x - boundX);
        float boundRight = (theGround.transform.position.x + boundX);


        //random point veriyor ama ilk nokta ile aynı olabiliyor ve degerler arasi ufak farklar oluyor
        //int randPointLeft = Random.Range(boundRight - 1, boundLeft + 1);
        float farPoint = FarPoint(enemy.transform.position, boundLeft, boundRight);

        targetPoint = new Vector3(
            farPoint,
            enemy.transform.position.y,
            enemy.transform.position.z);

        return targetPoint;
    }
    private float FarPoint(Vector2 enemyPosition, float boundLeft, float boundRight)
    {
        if (Vector2.Distance(
                enemyPosition,
                new Vector2(boundLeft, enemyPosition.y)) 
            >
            Vector2.Distance(
                enemyPosition,
                new Vector2(boundRight, enemyPosition.y))
            )
        {
            return boundLeft;
        }
        else
            return boundRight;
    }
}