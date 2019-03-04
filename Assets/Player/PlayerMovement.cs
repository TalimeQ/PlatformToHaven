using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO :: Change class name and refactor it as a playercontroller.
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRigidBodyRef;

    //TODO :: Replace with player Scriptable ref so i can have everything in one neat place
    [SerializeField]
    float movementSpeed = 5.0f;
    [SerializeField]
    float crosshairSpeed = 26.0f;

    [SerializeField]
    Animator playerAnimatorRef;
    private float movement = 0;
    private ParticleSystem playerCrosshair;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBodyRef = this.GetComponentInParent<Rigidbody2D>();
        playerAnimatorRef = this.GetComponentInParent<Animator>();
        playerCrosshair = this.GetComponentInChildren<ParticleSystem>();
        playerSprite = this.GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical  = Input.GetAxis("Vertical");
       
        float aimVertical = Input.GetAxis("VerticalAim");
        ProcessMovementInput(horizontal, vertical);
        ProcessCrossHairInput(aimVertical);
        if (Input.GetButtonDown("HorizontalAim"))
        {
            FlipPlayer();
        }
       
    }
    void ProcessMovementInput(float horizontal, float vertical)
    {
        movement = horizontal + vertical;
        playerAnimatorRef.SetFloat("movement", movement);
        Vector2 horizontalMovementVector = this.transform.right * horizontal * movementSpeed;
        Vector2 verticalMovementVector = this.transform.up *  vertical * movementSpeed;
        playerRigidBodyRef.velocity = verticalMovementVector + horizontalMovementVector;
        
    }
    void ProcessCrossHairInput(float verticalAim)
    {
        Vector3 newCrosshairPosition = playerCrosshair.transform.rotation.eulerAngles;
        newCrosshairPosition.x += verticalAim * Time.deltaTime * crosshairSpeed;
        playerCrosshair.transform.rotation = Quaternion.Euler(newCrosshairPosition);
    }
    void FlipPlayer()
    {
        
        if(Input.GetAxis("HorizontalAim") < 0)
        {
            playerSprite.flipX = true;
            FlipCrosshair();
        }
        else if (Input.GetAxis("HorizontalAim") > 0)
        {
            playerSprite.flipX = false;
            FlipCrosshair();
        }
    }

    private void FlipCrosshair()
    {
        Vector3 newRot = this.playerCrosshair.transform.rotation.eulerAngles;
        newRot.x = newRot.x + 180;
        playerCrosshair.transform.rotation = Quaternion.Euler(newRot);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Object collided");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Object Triggered");
    }

}
