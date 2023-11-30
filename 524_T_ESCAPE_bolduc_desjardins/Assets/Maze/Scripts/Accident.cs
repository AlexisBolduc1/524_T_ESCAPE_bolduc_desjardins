using UnityEngine;
using System.Collections;

public class Accident : MonoBehaviour {

	public GameObject redPanel;
	public GameObject losePanel;
	public float redTime = .3f;

	public AudioSource audioSource;
	public AudioClip loseClip;

	public static int health = 3;
	
	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "Wall") {

			StartCoroutine(Red());

			GetComponent<AudioSource>().Play();

			health--;

            if (health <= 0)
            {
				Lose();
            }
		}	
	}

	IEnumerator Red()
	{
		redPanel.SetActive(true);
		yield return new WaitForSeconds(redTime);
		redPanel.SetActive(false);
	}

	void Lose()
    {
		losePanel.SetActive(true);
		audioSource.clip = loseClip;
		audioSource.Play();
		Destroy(gameObject, 5);
	}
}
