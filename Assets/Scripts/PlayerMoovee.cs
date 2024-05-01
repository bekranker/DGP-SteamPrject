using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoovee : MonoBehaviour
{
    public PlayerData Data;
    public Rigidbody2D RB { get; private set; }
    public bool IsFacingRight { get; private set; }
    private Vector2 _moveInput;
    [SerializeField] InputHandler _inputHandler;
	[SerializeField] private Animator _animator;
    public float MoveDirection{ get; private set; }
    public float LastOnGroundTime { get; private set; }
	public bool CanMove { get; set;}

    private void Awake()
	{
		CanMove = true;
		RB = GetComponent<Rigidbody2D>();
	}
    private void Start()
	{
		IsFacingRight = true;
	}

    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");
        if (_moveInput.x != 0)
		{
			CheckDirectionToFace(_moveInput.x > 0, _moveInput.x);
        	_animator.SetFloat($"{Data._runAnimation}", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
		}
		else
		{
        	_animator.SetFloat($"{Data._runAnimation}", -1);
		}

        MoveDirection = IsFacingRight ? 1 : -1;
    }
    void FixedUpdate()
    {
        Run(1);
    }
    private void Run(float lerpAmount)
	{
		print(CanMove);
		if(CanMove)
		{
			float targetSpeed = _moveInput.x * Data.runMaxSpeed;
		
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

		float accelRate;

		
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
		

		
		if(Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			
			accelRate = 0; 
		}
		
		
		float speedDif = targetSpeed - RB.velocity.x;

		float movement = speedDif * accelRate;

		RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
		}
		else
		{
			RB.velocity = Vector2.zero;
		}
		
	}
    private void AirRun(ref float accelRate, ref float targetSpeed)// if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
    {
        accelRate*= Data.jumpHangAccelerationMult;
		targetSpeed *= Data.jumpHangMaxSpeedMult;
    }
    private void Turn(float direction)
	{
		Vector3 scale = transform.localScale; 
		scale.x = Mathf.Sign(direction);
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
    public void CheckDirectionToFace(bool isMovingRight, float direction)
	{
		if (isMovingRight != IsFacingRight)
			Turn(direction);
	}
}
