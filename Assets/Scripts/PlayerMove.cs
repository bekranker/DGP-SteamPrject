using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{   
    public PlayerData Data;
    public float LastOnGroundTime { get; private set; }
    [SerializeField] Rigidbody2D RB;
	[SerializeField] InputHandler _inputHandler;
    private Vector2 _moveInput;
    public bool IsFacingRight { get; private set; }
	public float MoveDirection;

    void Start()
    {
        IsFacingRight = true;
    }
    void Update()
    {
        #region INPUT HANDLER
		_moveInput.x = _inputHandler.MoveInput.x;
		_moveInput.y = _inputHandler.MoveInput.y;
    
		#endregion

		MoveDirection = RB.velocity.x;
        
    }
    void FixedUpdate()
    {
        Run(1);
    }
     //RUN METHODS
    private void Run(float lerpAmount)
	{
        if (_moveInput.x != 0)
        {
			CheckDirectionToFace(_moveInput.x > 0);
        }
		//Calculate the direction we want to move in and our desired velocity
		float targetSpeed = _moveInput.x * Data.runMaxSpeed;
		//We can reduce are control using Lerp() this smooths changes to are direction and speed
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

		#region Calculate AccelRate
		float accelRate;

		//Gets an acceleration value based on if we are accelerating (includes turning) 
		//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
		#endregion

		// #region Add Bonus Jump Apex Acceleration
		// //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
		// if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
		// {
		// 	accelRate *= Data.jumpHangAccelerationMult;
		// 	targetSpeed *= Data.jumpHangMaxSpeedMult;
		// }
		// #endregion

		#region Conserve Momentum
		//We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
		if(Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			//Prevent any deceleration from happening, or in other words conserve are current momentum
			//You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
			accelRate = 0; 
		}
		#endregion

		//Calculate difference between current velocity and desired velocity
		float speedDif = targetSpeed - RB.velocity.x;
		//Calculate force along x-axis to apply to thr player

		float movement = speedDif * accelRate;

		//Convert this to a vector and apply to rigidbody
		RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

		/*
		 * For those interested here is what AddForce() will do
		 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
	}
        
    private void AirRun(ref float accelRate, ref float targetSpeed)// if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
    {
        accelRate*= Data.jumpHangAccelerationMult;
		targetSpeed *= Data.jumpHangMaxSpeedMult;
    }
    private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
    public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != IsFacingRight)
			Turn();
	}

    
}