using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public PlayerData Data;
    public Vector2 _moveInput;
    public bool IsJumping { get; private set; }
    private bool _isJumpCut; // çift zıplama
	private bool _isJumpFalling; // düşme
    private float _wallJumpStartTime;
	public bool IsWallJumping;
	private int _lastWallJumpDir;
    public float LastOnGroundTime { get; private set; }
    public float LastOnWallTime { get; private set; } // Karakterin en son herhangi bir duvara temas ettiği zamanı saklar
	public float LastOnWallRightTime { get; private set; } // Karakterin en son sağ duvara temas ettiği zamanı saklar. // bunlar 
	public float LastOnWallLeftTime { get; private set; } //Karakterin en son sol duvara temas ettiği zamanı saklar. 
    public float LastPressedJumpTime { get; private set; }

    #region CHECK PARAMETERS
	//Set all of these up in the inspector
	[Header("Checks")] 
	[SerializeField] private Transform _groundCheckPoint;
	//Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	[Space(5)]
	[SerializeField] private Transform _frontWallCheckPoint;
	[SerializeField] private Transform _backWallCheckPoint;
	[SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion
    public PlayerAnimator AnimHandler { get; private set; }
    #region LAYERS & TAGS
    [Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;
	#endregion
    [SerializeField] Rigidbody2D RB;
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
		// tüm pclerde aynı hızda çalışsın diye delta time'dan çıkartıyoruz
		LastPressedJumpTime -= Time.deltaTime;
		
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUpInput();
        }
        
        
        
        #region COLLISION CHECKS
		
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer)) //checks if set box overlaps with ground
			{
				if(LastOnGroundTime < -0.1f)
                {
					//AnimHandler.justLanded = true;
                }

				LastOnGroundTime = Data.coyoteTime; //if so sets the lastGrounded to coyoteTime
            }		

			//Right Wall Check
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) )
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer))) && !IsWallJumping)
				LastOnWallRightTime = Data.coyoteTime;

			//Right Wall Check
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) )
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer))) && !IsWallJumping)
				LastOnWallLeftTime = Data.coyoteTime;

			//Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
			LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
		
		#endregion
        //jump
    {
        //#region JUMP CHECKS
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
        //Jump
		if (CanJump() && LastPressedJumpTime > 0)
		{
			IsJumping = true;
			IsWallJumping = false;
			_isJumpCut = false;
			_isJumpFalling = false;
			Jump();

		//	AnimHandler.startedJumping = true;
		}
		//WALL JUMP
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


	
        //gravity

        
            if (RB.velocity.y < 0 && _moveInput.y < 0)//moveinput skntı
			{
				//Much higher gravity if holding down
				SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
			}
			else if (_isJumpCut)
			{
				//Higher gravity if jump button released
				SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
			{
				SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
			}
			else if (RB.velocity.y < 0)
			{
				//Higher gravity if falling
				SetGravityScale(Data.gravityScale * Data.fallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
			}
			else
			{
				//Default gravity if standing on a platform or moving upwards
				SetGravityScale(Data.gravityScale);
			}
		    
		    
    }
    public void SetGravityScale(float scale)
	{
		RB.gravityScale = scale;
	}

    //   private void JumpCheck ()
    // {
    //     //#region JUMP CHECKS
	// 	if (IsJumping && RB.velocity.y < 0)
	// 	{
	// 		IsJumping = false;

	// 		_isJumpFalling = true;
	// 	}

	// 	if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
	// 	{
	// 		IsWallJumping = false;
	// 	}

	// 	if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
    //     {
	// 		_isJumpCut = false;

	// 		_isJumpFalling = false;
	// 	}
    //     //Jump
	// 	if (CanJump() && LastPressedJumpTime > 0)
	// 	{
	// 		IsJumping = true;
	// 		IsWallJumping = false;
	// 		_isJumpCut = false;
	// 		_isJumpFalling = false;
	// 		Jump();

	// 		AnimHandler.startedJumping = true;
	// 	}
	// 	//WALL JUMP
	// 	else if (CanWallJump() && LastPressedJumpTime > 0)
	// 	{
	// 		IsWallJumping = true;
	// 		IsJumping = false;
	// 		_isJumpCut = false;
	// 		_isJumpFalling = false;
	// 		_wallJumpStartTime = Time.time;
	// 		_lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;
	// 		WallJump(_lastWallJumpDir);
	// 	}
    // }
    private void Jump()
	{
		//Ensures we can't call Jump multiple times from one press
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;

		#region Perform Jump
		//We increase the force applied if we are falling
		//This means we'll always feel like we jump the same amount 
		//(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
		float force = Data.jumpForce;
		if (RB.velocity.y < 0)
			force -= RB.velocity.y;

		RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		#endregion  
	}
    private void WallJump(int dir)
	{
		//Ensures we can't call Wall Jump multiple times from one press
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;
		LastOnWallRightTime = 0;
		LastOnWallLeftTime = 0;

		#region Perform Wall Jump
		Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
		force.x *= dir; //apply force in opposite direction of wall

		if (Mathf.Sign(RB.velocity.x) != Mathf.Sign(force.x))
			force.x -= RB.velocity.x;

		if (RB.velocity.y < 0) //checks whether player is falling, if so we subtract the velocity.y (counteracting force of gravity). This ensures the player always reaches our desired jump force or greater
			force.y -= RB.velocity.y;

		//Unlike in the run we want to use the Impulse mode.
		//The default mode will apply are force instantly ignoring masss
		RB.AddForce(force, ForceMode2D.Impulse);
		#endregion
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

    

}
