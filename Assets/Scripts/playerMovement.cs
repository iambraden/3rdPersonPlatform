using UnityEngine;

public class playerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float speed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;


    [Header("Ground Check")]
    public Transform groundCheck;
    public  float groundDistance;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orient;
    float horizontalInput;
    float verticalInput;
    Vector3 inputDir;
    Rigidbody rb;
    public KeyCode jumpKey = KeyCode.Space;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");    

        if(Input.GetKey(jumpKey) && grounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
        Debug.Log(grounded);
        MyInput();
        SpeedControl();

        if(grounded){
            rb.linearDamping = groundDrag;
        } else {
            rb.linearDamping = 0;
        }
    }

    private void MovePlayer()
    {
        inputDir = orient.forward * verticalInput + orient.right * horizontalInput;
        
        if(grounded){
            rb.AddForce(inputDir.normalized * speed * 10f, ForceMode.Force);
        }else if(!grounded){
            rb.AddForce(inputDir.normalized * speed * 10f * airMultiplier, ForceMode.Force);
        }
    
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        if (flatVel.magnitude > speed)
        {
            rb.linearVelocity = new Vector3(flatVel.normalized.x * speed, rb.linearVelocity.y, flatVel.normalized.z * speed);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;
    }
}
