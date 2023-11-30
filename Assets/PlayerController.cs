using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//DONT SHARE
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
        //try normal movement
        if(movementInput != Vector2.zero){
           bool success = TryMove(movementInput);
//if that fails try x direction
           if(!success) {
            success = TryMove(new Vector2(movementInput.x, 0));
            //if that fails try y direction
           
                if(!success) {
            success = TryMove(new Vector2(0, movementInput.y));
           }
           }
           
            
        }
    }

    private bool TryMove(Vector2 direction) {
        //check for potential collisions
        int count = rb.Cast(
                direction, //X and Y vals between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter,// THe settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the cast equal to the movement plus an offset
                moveSpeed * Time.fixedDeltaTime + collisionOffset); //The amount to cast equal to movement plus an offset

            if(count==0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }  
            else{
                return false;
            }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
}
