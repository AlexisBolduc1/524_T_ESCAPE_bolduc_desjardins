using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	float x,y;

	// Update is called once per frame
	void Update () {

		if (Accident.health > 0 && !End.endState)
		{
			x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
			y = Input.GetAxis("Vertical") * speed * Time.deltaTime;

			transform.Translate(x, y, 0);
		}
		else if (Accident.health > 0)
        {
			transform.Translate(-0.5f * Time.deltaTime, 0, 0);
		}

	}
}
