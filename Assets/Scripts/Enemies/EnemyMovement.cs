using UnityEngine;
using UnityEngine.Animations;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private Zawardo zawardo;
    private Vector3 characterDirection;
    
    [Header("Movement")] 
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float sideSpeed;
    [Header("Rotation"), Range(15, 360)]
    [SerializeField] private float rotationSpeed;
    [Header("Vertical Stuff")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity =9.81f;
    [SerializeField] private float stickToGroundVelocity;

    [Header("Hover")]
    [SerializeField] private float desiredHeight;
    [SerializeField] private float springStrength;
    [SerializeField] private float damping;
    [SerializeField] private float raycastLength;
    
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
        player = GameObject.FindGameObjectWithTag("Player");
        zawardo = player.GetComponentInChildren<Zawardo>();
        
    }

    private void Update()
    {
        UpdateMoveVelocity();
        UpdateVerticalVelocity();
        ApplyTotalVelocity();
        UpdateRotation();
        
    }
    private void UpdateMoveVelocity()
    { 
        characterDirection = player.transform.position - transform.position;
    }

    private void ApplyTotalVelocity()
    {
        var totalVelocity = _playerVelocity +  _verticalVelocity * Vector3.up + _slideVelocity * _slideVelocityFactor;
        _characterController.Move(new Vector3(characterDirection.x, _verticalVelocity ,characterDirection.z).normalized * (forwardSpeed * Time.deltaTime * (zawardo.isZawarding == false ? 1 : 0)));
    }

    private void UpdateRotation()
    {
        
        transform.LookAt(player.transform.position);
        var mouseInput = Input.GetAxis("Mouse X");
        //transform.Rotate(0, mouseInput * rotationSpeed * Time.deltaTime, 0);
    }

    private void UpdateVerticalVelocity()
    {
        //Slato de david pero sin lo del grounded
        if (Input.GetAxisRaw("Jump") > 0.5f && !_isJumping)
        {
            _isJumping = true;
            _verticalVelocity = jumpForce;
        }

        RaycastHit hit;
        //mi muelle de salto
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastLength))
        {
            //guardamos la distancia en una variable
            float distance = hit.distance;
            if (_isJumping && _verticalVelocity <= 0 && distance <= desiredHeight + 0.1f) //si salta y esta callendo le quitamos la habilidad de saltar
            {
                _isJumping = false;
            }
            
            
            if (!_isJumping) // si no esta saltando se usa el muelle
            {
                float displacement = desiredHeight - distance;
                float springForce = displacement * springStrength;
                float dampingForce = -_verticalVelocity * damping;
                float totalForce = springForce + dampingForce;
                _verticalVelocity += totalForce * Time.deltaTime;
            }
            else //pero si si esta saltando entonces usa la gravedad
            {
                _verticalVelocity -= gravity * Time.deltaTime;
            }
            Debug.DrawRay(transform.position, Vector3.down * distance, Color.maroon);
        }
        else //si ni siquiera llega a ver el rayo nada pues cae
        {
            _verticalVelocity -= gravity * Time.deltaTime;
        }
    }
}
