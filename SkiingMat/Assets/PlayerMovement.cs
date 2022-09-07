using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody rb;
    public float mSpeed;
    public float constantStillSpeed;
    Vector2 moveDir = Vector2.zero;
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDir = new Vector2(moveX, moveY);
    }

    private void FixedUpdate() {
        //rb.velocity = new Vector3(moveDir.x * mSpeed, 0, constantStillSpeed);
        float actualMoveDir = moveDir.x;
        float turnPercentage;
        if(actualMoveDir > 0) {
            turnPercentage = 1 - actualMoveDir;
        } else {
            turnPercentage = actualMoveDir + 1;
        }
        turnPercentage = turnPercentage > 0.05f ? turnPercentage/2 : 0.05f;
        transform.rotation = Quaternion.Euler(12.2f, actualMoveDir * 45f, 0f);
        rb.AddForce(new Vector3(actualMoveDir * mSpeed, 0, constantStillSpeed * (turnPercentage + 1)));
    }
}
