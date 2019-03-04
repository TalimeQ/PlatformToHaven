using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRigidBodyRef;

    //TODO :: Replace with player Scriptable ref so i can have everything in one neat place
    [SerializeField]
    float movementSpeed = 5.0f;
    [SerializeField]
    Animator playerAnimatorRef;
    private float movement = 0;

    // Start is called before the first frame update
    void Start()
    {
         playerRigidBodyRef = this.GetComponentInParent<Rigidbody2D>();
         playerAnimatorRef = this.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical  = Input.GetAxis("Vertical");
        ProcessMovementInput(horizontal, vertical);
    }
    void ProcessMovementInput(float horizontal, float vertical)
    {
        movement = horizontal + vertical;
        playerAnimatorRef.SetFloat("movement", movement);
        Vector2 horizontalMovementVector = this.transform.right * horizontal * movementSpeed;
        Vector2 verticalMovementVector = this.transform.up *  vertical * movementSpeed;
        playerRigidBodyRef.velocity = verticalMovementVector + horizontalMovementVector;
        
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
