using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private PlayerMoovee _move;

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (_inputHandler.AttackInput)
        {
            
        }
    }



}
