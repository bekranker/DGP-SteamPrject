using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    
    #region Input Actions
    [Space(10)]
    [Header("Input Action Asset")]
    [Space(10)]
    [SerializeField] private InputActionAsset _playerControls;
    [Header("Input Map Name Referances")]
    [SerializeField] private string _actionMapName = "Player";
    [Header("Input Name Referances")]
    [SerializeField] private string _move = "Movement";
    [SerializeField] private string _combat = "Combat";
    [SerializeField] private string _jump = "Jump";
    [SerializeField] private string _interactive = "Interactive";


    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _combatAction;
    private InputAction _interactiveAction;



    public Vector2 MoveInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool InteractiveInput { get; private set; }


    #endregion



    void Awake()
    {
        _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_move);
        _jumpAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jump);
        _combatAction = _playerControls.FindActionMap(_actionMapName).FindAction(_combat);
        _interactiveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_interactive);

        InputRegistration();
    }
    void OnEnable()
    {
        _moveAction.Enable();
        _jumpAction.Enable();
        _combatAction.Enable();
        _interactiveAction.Enable();
    }
    void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
        _combatAction.Disable();
        _interactiveAction.Disable();
    }
    void Update()
    {
        JumpInput = _jumpAction.WasPerformedThisFrame();
        AttackInput = _combatAction.WasPerformedThisFrame();
        InteractiveInput = _interactiveAction.WasPerformedThisFrame();
    }
    void InputRegistration()
    {
        _moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        _moveAction.canceled += ctx => MoveInput = Vector2.zero;
    }
}