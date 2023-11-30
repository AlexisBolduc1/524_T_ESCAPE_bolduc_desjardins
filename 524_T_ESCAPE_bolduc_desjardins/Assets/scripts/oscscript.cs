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

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    Vector2 movement;
    Vector2 mousePosition;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/enc", mouvementPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        //deplacement code
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //bullet code
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    void mouvementPlayer(OSCMessage oscMessage) {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y , lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
