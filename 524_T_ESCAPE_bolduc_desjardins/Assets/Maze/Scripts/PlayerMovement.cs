using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	float x,y;

	// Update is called once per frame
	void Update () {

			x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
			y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

			transform.Translate(x, y, 0);
		

	}
}
