using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate() {
        if(movementInput != Vector2.zero){
            int count = rb.Cast(
                movementInput, //X and Y vals between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter,// THe settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the cast equal to the movement plus an offset
                moveSpeed * Time.fixedDeltaTime + collisionOffset); //The amount to cast equal to movement plus an offset

            if(count==0){
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            }  
            
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
}
