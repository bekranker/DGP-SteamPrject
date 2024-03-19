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
        
        //animator.Play("Carry");
        Debug.Log("Previous Target Point: " + _targetPosition);
        _targetPosition = TargetPoint(enemy);
        Debug.Log("New Target Point: " + _targetPosition);

        _canMove = true;
        _carryDelayCounter = enemy.EnemyT.PatrollDelay;
    }
    private Vector3 TargetPoint(Enemy enemy)
    {
        Vector3 targetPoint;
        Grounded grounded = enemy.Ground;
        GameObject theGround = grounded.MyGround();
        BoxCollider2D groundCollider = theGround.GetComponent<BoxCollider2D>();
        float boundX = groundCollider.bounds.extents.x;
        int boundLeft = (int)(boundX - theGround.transform.position.x);
        int boundRight = (int)(boundX + theGround.transform.position.x);

        int randPointLeft = Random.Range(boundRight - 1, boundLeft + 1);
        float farPoint = FarPoint(enemy.transform.position, boundLeft, boundRight) - .5f;
        //float randPointRight = 

        targetPoint = new Vector3(
            randPointLeft,
            enemy.transform.position.y,
            enemy.transform.position.z);

        return targetPoint;
    }
    private float FarPoint(Vector2 enemyPosition, int boundLeft, int boundRight)
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
            return boundRight;
        }
        else
            return boundLeft;
    }
}