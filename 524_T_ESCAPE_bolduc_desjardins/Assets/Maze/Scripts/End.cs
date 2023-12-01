﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {

	public GameObject winPanel;
	public AudioSource audioSource;
	public AudioClip winClip;

	public static bool endState;

	void OnTriggerEnter2D (Collider2D col){

		if (col.gameObject.tag == "Player") {

			audioSource.clip = winClip;
			audioSource.Play();
			
			endState = true;

			SceneManager.LoadScene(1);

			
		}
	}

}
