using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Rigidbody2D rb;
	private bool isPressed = false;

	public Rigidbody2D hook;
	public float maxDragDistance = 2f;

	void Update(){
		if (isPressed) {
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			if (Vector3.Distance(mousePos,hook.position) > maxDragDistance){
				rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
			}
			else{ 
				rb.position = mousePos;
			}
		}
	}

	void OnMouseDown (){
		isPressed = true;
		rb.isKinematic = true;
	}

	void OnMouseUp(){
		isPressed = false;
		rb.isKinematic = false;
		StartCoroutine (Release());
	}

	public float releaseTime = .15f;

	public GameObject nextBall;

	IEnumerator Release(){
		yield return new WaitForSeconds (releaseTime);
		GetComponent<SpringJoint2D> ().enabled = false;
		this.enabled = false;

		yield return new WaitForSeconds (2f);
		if (nextBall != null){
			nextBall.SetActive (true);
		} else{
			Enemy.EnemiesAlive = 0;
			UnityEngine.SceneManagement.SceneManager.LoadScene(
				UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
	}
}
