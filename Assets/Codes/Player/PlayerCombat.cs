using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;


    void Update()
    {
        
    }

    private void Attack()
    {
        if (_inputHandler.AttackInput)
        {
            
        }
    }

}
