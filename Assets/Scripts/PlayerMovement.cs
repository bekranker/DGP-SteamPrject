using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement Props")]
	public PlayerData Data;
	//COMPONENTS
    public Rigidbody2D RB { get; private set; }
	
	//parameters
	public bool IsFacingRight { get; private set; }//karakterin ne tarafa baktıgını kontrol etmek için
	//get;private set; sadece sınıfın içindeki metodlar tarafından değiştirilebilir.
	public bool IsJumping { get; private set; } // zıplama
	public bool IsWallJumping { get; private set; }//duvardan zıplama
	public bool IsDashing { get; private set; }//dash
	public bool IsSliding { get; private set; }//kayma

    [SerializeField] InputHandler _inputHandler;
	//[SerializeField] private Animator _animator;
	public bool CanMove;

	//timers
	public float LastOnGroundTime { get; private set; }
	public float LastOnWallTime { get; private set; }
	public float LastOnWallRightTime { get; private set; }
	public float LastOnWallLeftTime { get; private set; }
	

	//Jump
	private bool _isJumpCut;// ne kadar uzun tutarsak o kadar fazla zıplıyor
	private bool _isJumpFalling; //zıplama hareketinde en tepeye ulaştıktan sonra yerçekimi etkisiyle düşmeye başladığı kısım

	//Wall Jump
	private float _wallJumpStartTime;//hareketinin başlama zamanını saklamak için
	private int _lastWallJumpDir;//en son yapılan duvara zıplama wall jump hareketinin yönünü saklamak için

	//Dash
	private int _dashesLeft;
	private bool _dashRefilling;//dash bekleme süresi
	private Vector2 _lastDashDir; //dash yönü
	private bool _isDashAttacking;

	

	// INPUT PARAMETERS
	public Vector2 _moveInput; //(vector2 2 değer alır genel) karakterin hareket girişini yon saklamak için kullandık.

	public float LastPressedJumpTime { get; private set; }
	//en son ne zaman zıplama veya dash tuşuna bastığını takip etmek içn
	public float LastPressedDashTime { get; private set; }
	

	//CHECK PARAMETERS
	//karakterimizin zeminde olup olmadığını sağında solunda bir şeyler olup olmadığını checkliyoruz.
	[Header("Checks")] 
	[SerializeField] private Transform _groundCheckPoint; //serializefield inspectorda görünmesini sağlıyo.
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	[Space(5)] //inspectorde beş birimlik boşluklar bırakır yazıların arasında
	[SerializeField] private Transform _frontWallCheckPoint;
	[SerializeField] private Transform _backWallCheckPoint;
	[SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    

   	// LAYERS & TAGS
    [Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;

    private void Awake()//starttan bile önce başlar 1 kere çalışır script aktif değilse bile çalıştırır
	{
		RB = GetComponent<Rigidbody2D>();
	}

	public void AllStartFunctionsForMovement() // private void Start()
	{
		SetGravityScale(Data.gravityScale);//scriptable object oldugu için ordan alısn gravity
		IsFacingRight = true; // karakterin yüzünün ne tarafa döneceği 
		CanMove = true;
	}
	public void AllUpdateFunctionsForMovement() //private void Update()
	{
		// TIMERS
        LastOnGroundTime -= Time.deltaTime; // oyunun farklı sistemlerinde sabit bir hızda çalışmasını sağlar.
		LastOnWallTime -= Time.deltaTime;
		LastOnWallRightTime -= Time.deltaTime;
		LastOnWallLeftTime -= Time.deltaTime;

		LastPressedJumpTime -= Time.deltaTime;
		LastPressedDashTime -= Time.deltaTime;               	
		

		//INPUT HANDLER
		_moveInput.x = Input.GetAxisRaw("Horizontal"); //karakter kontrol getaxisraw -1 1 arasında deger alıyor getaxis virgüllü
		_moveInput.y = Input.GetAxisRaw("Vertical");

		if (_moveInput.x != 0)
			CheckDirectionToFace();// CheckDirectionToFace(_moveInput.x > 0);

		if (CanMove) //bunu yeni eklemis
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				OnJumpInput();
			}
			if (Input.GetKeyUp(KeyCode.Space))//ziplama birakildigindaki iptali 
			{
				OnJumpUpInput();
			}
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				OnDashInput();
			}
			
		}



		
		//COLLISION CHECKS
		//karakterin zeminde olup olmadigini ve duvara temas edip etmedigini kontrol eder. 
		if (!IsDashing && !IsJumping)
		{
			//Ground Check
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer)) 
			{
				if(LastOnGroundTime < -0.1f) //zemine temas ettiği anda animasyon yapsın
                {
					//AnimHandler.justLanded = true;
                }

				LastOnGroundTime = Data.coyoteTime; //"coyoteTime"  zeminin havada kalma süresi gibi bişe dışında zıplama yapmaya devam etme süresini belirtir.  
            }		

			
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
				LastOnWallRightTime = Data.coyoteTime;

			
			if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
				LastOnWallLeftTime = Data.coyoteTime;

			//kontrol noktaları taraf değiştirdiğinden hem sol hem de sağ duvarlar için iki kontrol gerekiyor
			LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
		}
		

		//JUMP CHECKS
		if (IsJumping && RB.velocity.y < 0)//yukarı yönlü hızının negatif olduğu durumu kontrol ediyor
		{
			IsJumping = false;

			_isJumpFalling = true;//düşüş
		}

		if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
		{
			IsWallJumping = false;
		}

		if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)//zemine geldiğinde ne zıplama ne de duvardan zıplayıp zıplamama durumunu kontrol eder
        {
			_isJumpCut = false;

			_isJumpFalling = false;
		}

		if (!IsDashing) // dash atmadığı durumda zıplama ve duvardan zoplama hareketi
		{
			//Jump
			if (CanJump() && LastPressedJumpTime > 0)
			{
				IsJumping = true;
				IsWallJumping = false;
				_isJumpCut = false;
				_isJumpFalling = false;
				Jump();

				//AnimHandler.startedJumping = true;
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
		

		//DASH CHECKS
		if (CanDash() && LastPressedDashTime > 0)
		{
			//dash atarken biraz duraksatiyor
			Sleep(Data.dashSleepTime); 

			//eger yöne basilmazsa ileri atliyo
			if (_moveInput != Vector2.zero)
				_lastDashDir = _moveInput;
			else
				_lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;



			IsDashing = true;
			IsJumping = false;
			IsWallJumping = false;
			_isJumpCut = false;

			StartCoroutine(nameof(StartDash), _lastDashDir);
		}
		

		// SLIDE CHECKS
		if (CanSlide() && ((LastOnWallLeftTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
			IsSliding = true;
		else
			IsSliding = false;
		

		//GRAVITY
		if (!_isDashAttacking)//dash atarkenki halinde degilse
		{
			
			if (IsSliding)
			{
				SetGravityScale(0);
			}
			else if (RB.velocity.y < 0 && _moveInput.y < 0)
			{
				
				SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);//fastfallgravitiy space bascek yapinca daha hizli dusuyor
				
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
	public void AllFixedUpdateFunctionsForMovement() //private void FixedUpdate()
	{
		if (!CanMove) return; //bu yok

		//Handle Run
		if (!IsDashing)
		{
			if (IsWallJumping)
				Run(Data.wallJumpRunLerp);
			else
			{
				Run(1);
			}
		}
		else if (_isDashAttacking)
		{
			Run(Data.dashEndRunLerp);
		}

		//Handle Slide
		if (IsSliding)
			Slide();
	}
    private void OnJumpInput()
	{
		LastPressedJumpTime = Data.jumpInputBufferTime;
	}

	private void OnJumpUpInput()
	{
		if (CanJumpCut() || CanWallJumpCut())
			_isJumpCut = true;
	}

	private void OnDashInput() //oyuncunun belirli bir zaman aralığında birden fazla kez dash yapma girişini algılar. 
	{
		LastPressedDashTime = Data.dashInputBufferTime;
	}
    

    // GENERAL METHODS
    private void SetGravityScale(float scale)
	{
		RB.gravityScale = scale;
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
    

	//MOVEMENT METHODS
    //RUN METHODS
    private void Run(float lerpAmount)
	{
		
		float targetSpeed = _moveInput.x * Data.runMaxSpeed;
		
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

		
		float accelRate;

		
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
		if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
		{
			accelRate *= Data.jumpHangAccelerationMult;
			targetSpeed *= Data.jumpHangMaxSpeedMult;
		}
		if(Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			
			accelRate = 0; 
		}
		float speedDif = targetSpeed - RB.velocity.x;
		float movement = speedDif * accelRate;
		RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}

	private void Turn(float direction)
	{
		
		Vector3 scale = transform.localScale; 
		scale.x = direction;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
    

    // JUMP METHODS
    private void Jump()
	{
		
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;

		// Perform Jump
		
		float force = Data.jumpForce;
		if (RB.velocity.y < 0)
			force -= RB.velocity.y;

		RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		
	}

	private void WallJump(int dir)
	{
		//Ensures we can't call Wall Jump multiple times from one press
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;
		LastOnWallRightTime = 0;
		LastOnWallLeftTime = 0;

		// Perform Wall Jump
		Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
		force.x *= dir; //apply force in opposite direction of wall

		if (Mathf.Sign(RB.velocity.x) != Mathf.Sign(force.x))
			force.x -= RB.velocity.x;

		if (RB.velocity.y < 0) //checks whether player is falling, if so we subtract the velocity.y (counteracting force of gravity). This ensures the player always reaches our desired jump force or greater
			force.y -= RB.velocity.y;

		//Unlike in the run we want to use the Impulse mode.
		//The default mode will apply are force instantly ignoring masss
		RB.AddForce(force, ForceMode2D.Impulse);
		
	}
	

	//DASH METHODS
	
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
	

	//OTHER MOVEMENT METHODS
	private void Slide()
	{
		if(RB.velocity.y > 0)
		{
		    RB.AddForce(-RB.velocity.y * Vector2.up,ForceMode2D.Impulse);
		}
	
		
		float speedDif = Data.slideSpeed - RB.velocity.y;	
		float movement = speedDif * Data.slideAccel;
		movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif)  * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

		RB.AddForce(movement * Vector2.up);
	}
   


    //CHECK METHODS
	//public void CheckDirectionToFace(bool isMovingRight)
	//{
	//	if (isMovingRight != IsFacingRight)
	//		Turn();
	//}
    private void CheckDirectionToFace() 

	{
		Turn(_moveInput.x);
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
	private bool CanSlide()
    {
		if (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0)
			return true;
		else
			return false;
	}
}