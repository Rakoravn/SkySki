using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody rb;
    public float mSpeed;
    public float constantStillSpeed;
    Vector2 moveDir = Vector2.zero;
    void Start() {
        rb.AddForce(new Vector3(0, 0, 3f), ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDir = new Vector2(moveX, moveY);
    }

    private void FixedUpdate() {
        //rb.velocity = new Vector3(moveDir.x * mSpeed, 0, constantStillSpeed);
        float turnPercentage = 0;
        if(moveDir.x > 0) {
            turnPercentage = 1 - moveDir.x;
        } else {
            turnPercentage = moveDir.x + 1;
        }
        turnPercentage = turnPercentage > 0.75f ? 0.75f : turnPercentage;
        transform.rotation = Quaternion.Euler(12.2f, moveDir.x * 45f, 0f);
        rb.AddForce(new Vector3(moveDir.x * mSpeed, 0, constantStillSpeed * turnPercentage));
        Debug.Log(turnPercentage);
    }
}
