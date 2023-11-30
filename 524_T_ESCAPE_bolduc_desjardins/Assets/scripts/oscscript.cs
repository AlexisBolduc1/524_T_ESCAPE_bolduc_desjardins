using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class oscscript : MonoBehaviour
{
    public extOSC.OSCReceiver oscReceiver;
    public extOSC.OSCTransmitter oscTransmitter;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/enc", mouvementPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void mouvementPlayer(OSCMessage oscMessage) {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
