using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerJumppp : MonoBehaviour
{
    public PlayerData Data;
    public Rigidbody2D RB { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWallJumping { get; private set; }
    public float LastOnGroundTime { get; private set; }
	public float LastOnWallTime { get; private set; }
	public float LastOnWallRightTime { get; private set; }
	public float LastOnWallLeftTime { get; private set; }
    private bool _isJumpCut;
	private bool _isJumpFalling;
    private float _wallJumpStartTime;
	private int _lastWallJumpDir;
    private Vector2 _moveInput;
    public float LastPressedJumpTime { get; private set; }
	public float LastPressedDashTime { get; private set; }
	public bool IsDashing { get; private set; }
	private int _dashesLeft;
	private bool _dashRefilling;
	private Vector2 _lastDashDir;
	private bool _isDashAttacking;

	[SerializeField] private PlayerMoovee _playerMove;
    [Header("Checks")] 
	[SerializeField] private Transform _groundCheckPoint;
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	[Space(5)]
	[SerializeField] private Transform _frontWallCheckPoint;
	[SerializeField] private Transform _backWallCheckPoint;
	[SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    [Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;
    void Start()
    {
        SetGravityScale(Data.gravityScale);
    }
    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        LastOnGroundTime -= Time.deltaTime;
		LastOnWallTime -= Time.deltaTime;
		LastOnWallRightTime -= Time.deltaTime;
		LastOnWallLeftTime -= Time.deltaTime;
		LastPressedDashTime -= Time.deltaTime;

		LastPressedJumpTime -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J))
        {
			OnJumpInput();
        }

		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J))
		{
			OnJumpUpInput();
		}
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.K))
		{
			OnDashInput();
		}

        if (!IsDashing && !IsJumping)
		{
			//Ground Check
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
			{
				if(LastOnGroundTime < -0.1f)
                {
					//AnimHandler.justLanded = true;
                }

				LastOnGroundTime = Data.coyoteTime; 
            }		

			
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer)&& _playerMove.MoveDirection>0) //facing yazamadığımız için
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer)&& _playerMove.MoveDirection<0 )) && !IsWallJumping)
				LastOnWallRightTime = Data.coyoteTime;

			
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer)&& _playerMove.MoveDirection<0 )
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer)&& _playerMove.MoveDirection>0  )) && !IsWallJumping)
				LastOnWallLeftTime = Data.coyoteTime;

			
			LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
		}
    
        if (IsJumping && RB.velocity.y < 0)
		{
			IsJumping = false;

			_isJumpFalling = true;
		}

		if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
		{
			IsWallJumping = false;
		}

		if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
			_isJumpCut = false;

			_isJumpFalling = false;
		}
        if (!IsDashing)
		{
			if (CanJump() && LastPressedJumpTime > 0)
			{
				IsJumping = true;
				IsWallJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;
				Jump();

			
			}
			
			else if (CanWallJump() && LastPressedJumpTime > 0)
			{
				IsWallJumping = true;
				IsJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;
				_wallJumpStartTime = Time.time;
				_lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;
				WallJump(_lastWallJumpDir);
			}
		}
		//dash
		if (CanDash() && LastPressedDashTime > 0)
		{
			
			Sleep(Data.dashSleepTime); 

			


			IsDashing = true;
			IsJumping = false;
			IsWallJumping = false;
			_isJumpCut = false;

			StartCoroutine(nameof(StartDash), new Vector2(_playerMove.MoveDirection,0));
		}
		if (!_isDashAttacking)
		{
         	if (RB.velocity.y < 0 && _moveInput.y < 0)
			{
			
				SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
			}
			else if (_isJumpCut)
			{
			
				SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
			{
				SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
			}
			else if (RB.velocity.y < 0)
			{
			
				SetGravityScale(Data.gravityScale * Data.fallGravityMult);
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else
			{
				SetGravityScale(Data.gravityScale);
			}
		} 
		else
		{
			SetGravityScale(0);
		}
    }
	/*private void FixedUpdate()
	{
		//Handle Run
		if (!IsDashing)
		{
			if (IsWallJumping)
				Run(Data.wallJumpRunLerp);
			else
				Run(1);
		}
		else if (_isDashAttacking)
		{
			Run(Data.dashEndRunLerp);
		}

		//Handle Slide
		
    }
	*/
	public void OnDashInput()
	{
		LastPressedDashTime = Data.dashInputBufferTime;
	}
	
    public void OnJumpInput()
	{
		LastPressedJumpTime = Data.jumpInputBufferTime;
	}
    public void OnJumpUpInput()
	{
		if (CanJumpCut() || CanWallJumpCut())
			_isJumpCut = true;
	}
	private IEnumerator StartDash(Vector2 dir)
	{
		

		LastOnGroundTime = 0;
		LastPressedDashTime = 0;

		float startTime = Time.time;

		_dashesLeft--;
		_isDashAttacking = true;

		SetGravityScale(0);

		
		while (Time.time - startTime <= Data.dashAttackTime)
		{
			print("calsti");
			RB.velocity = dir.normalized * Data.dashSpeed;
			
			yield return null;
		}

		startTime = Time.time;

		_isDashAttacking = false;

		
		SetGravityScale(Data.gravityScale);
		RB.velocity = Data.dashEndSpeed * dir.normalized;

		while (Time.time - startTime <= Data.dashEndTime)
		{
			yield return null;
		}

		
		IsDashing = false;
	}

	
	private IEnumerator RefillDash(int amount)
	{
		
		_dashRefilling = true;
		yield return new WaitForSeconds(Data.dashRefillTime);
		_dashRefilling = false;
		_dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
	}
    public void SetGravityScale(float scale)
	{
		RB.gravityScale = scale;
	}
    private void Jump()
	{
		
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;

		#region Perform Jump
		float force = Data.jumpForce;
		if (RB.velocity.y < 0)
			force -= RB.velocity.y;

		RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		#endregion
	}
    private void WallJump(int dir)
	{
		
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;
		LastOnWallRightTime = 0;
		LastOnWallLeftTime = 0;

		#region Perform Wall Jump
		Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
		force.x *= dir;

		if (Mathf.Sign(RB.velocity.x) != Mathf.Sign(force.x))
			force.x -= RB.velocity.x;

		if (RB.velocity.y < 0) 
			force.y -= RB.velocity.y;

		
		RB.AddForce(force, ForceMode2D.Impulse);
		#endregion
	}
	private void Sleep(float duration)
    {
		
		StartCoroutine(nameof(PerformSleep), duration);
    }

	private IEnumerator PerformSleep(float duration)
    {
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(duration); 
		Time.timeScale = 1;
	}
    private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }
    private bool CanWallJump()
    {
		return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
			 (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
	}
    private bool CanJumpCut()
    {
		return IsJumping && RB.velocity.y > 0;
    }
    private bool CanWallJumpCut()
	{
		return IsWallJumping && RB.velocity.y > 0;
	}
	private bool CanDash()
	{
		if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
		{
			StartCoroutine(nameof(RefillDash), 1);
		}

		return _dashesLeft > 0;
	}
}
