using UnityEngine;

public class thirdPersonCam : MonoBehaviour
{
    public Transform orient;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float camSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orient.forward = viewDir.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orient.forward * verticalInput + orient.right * horizontalInput;

        if(inputDir != Vector3.zero){
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir, Time.deltaTime * camSpeed);
        }
    }
}
