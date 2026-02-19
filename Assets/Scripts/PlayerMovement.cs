using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sideSpeed;
    [Header("Rotation"), Range(15, 360)]
    [SerializeField] private float rotationSpeed;
    [Header("Vertical Stuff")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity =9.81f;
    [SerializeField] private float stickToGroundVelocity;
    [Header("Slide Stuff")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideSlope= 45f;
    [SerializeField] private float slideSlowdownTime = 2f;
    [SerializeField] private float slideRampUpFactor = 3f;
    [SerializeField] private float slideRampDownFactor = 3f;
    [SerializeField] private float slideFactorRecovery = 10f;
    
    [SerializeField] private AnimationCurve slideSlowDownCurve = AnimationCurve.EaseInOut(0f,1f,1f,0f);
    
    private CharacterController _characterController;
    
    private Vector3 _playerVelocity;
    private float _verticalVelocity;
    private Vector3 _slideVelocity;
    private float _slideVelocityFactor = 1;
    private float _slidingTime;
    private float _slidingSlowdownTimeInverse;
    private bool _isJumping= false;
    private  bool _isSliding = false;
    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _characterController = GetComponent<CharacterController>();
        slideSlope = _characterController.slopeLimit;
        _slidingSlowdownTimeInverse = 1 / slideSlowdownTime;
    }

    private void Update()
    {
        UpdateMoveVelocity();
        UpdateVerticalVelocity();
        UpdateSlideVelocity();
        
        ApplyTotalVelocity();
        
        //Crouch
        UpdateRotation();
    }

    private void UpdateSlideVelocity()
    {
        var maxSlideVelocity = Vector3.zero;
        RaycastHit hit;
        if (_characterController.isGrounded && Physics.SphereCast(transform.position + _characterController.center,
                _characterController.radius, Vector3.down, out hit))
        {
            var angle = Vector3.Angle(hit.normal, Vector3.up);
            print(angle); // pa debugear

            if (angle > slideSlope)
            {
                _isSliding = true;

                var slideDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;
                maxSlideVelocity = slideDirection * slideSpeed;
                
                Debug.DrawRay(hit.point, hit.normal, Color.red, 3f);
                Debug.DrawRay(hit.point, slideDirection, Color.green, 3f);
            }
            else
            {
              _isSliding = false;
              _slidingTime = 0;
            }

            if (_isSliding)
            {
                _slidingTime += Time.deltaTime;
            }

            _slideVelocity = _isSliding
                ?
                Vector3.Lerp(_slideVelocity, maxSlideVelocity, Time.deltaTime * slideRampUpFactor)
                :
                Vector3.Lerp(_slideVelocity, Vector3.zero, Time.deltaTime * slideRampDownFactor);
            
            _slideVelocityFactor = _isSliding 
                ? 
                slideSlowDownCurve.Evaluate(Mathf.Clamp01(_slidingTime * _slidingSlowdownTimeInverse)) 
                :
                Mathf.Lerp(_slideVelocityFactor,1, Time.deltaTime * slideFactorRecovery);
        }
        
    }

    private void UpdateMoveVelocity()
    {
        
        var xInput = Input.GetAxis("Horizontal");
        var yInput = !_isSliding ? Input.GetAxis("Vertical") : 0;
        
        var input = xInput * transform.right + yInput * transform.forward;
        
        if(input.sqrMagnitude > 1) input.Normalize();
        
        input= new Vector3(input.x * sideSpeed, 0, input.z * forwardSpeed);
        
        _playerVelocity = input;
    }

    private void ApplyTotalVelocity()
    {
        var totalVelocity = _playerVelocity +  _verticalVelocity * Vector3.up + _slideVelocity * _slideVelocityFactor;
        _characterController.Move(totalVelocity * Time.deltaTime);
    }

    private void UpdateRotation()
    {
        var mouseInput = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseInput * rotationSpeed * Time.deltaTime, 0);
    }

    private void UpdateVerticalVelocity()
    {
        if (Input.GetAxisRaw("Jump") > 0.5 && _characterController.isGrounded && !_isJumping && !_isSliding)
        {
            _isJumping = true;
            _verticalVelocity = jumpForce;
        }

        if (_isJumping && _characterController.isGrounded && _characterController.velocity.y < 0)
        {
            _isJumping = false;
        }

        if (!_isJumping && _characterController.isGrounded && _characterController.velocity.y < 0)
        {
           _verticalVelocity = stickToGroundVelocity;
        }
        _verticalVelocity -= gravity * Time.deltaTime;
    }
}
