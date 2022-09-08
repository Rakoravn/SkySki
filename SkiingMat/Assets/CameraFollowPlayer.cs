using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    public Transform player;
    Vector3 initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + initialOffset;
    }
}
