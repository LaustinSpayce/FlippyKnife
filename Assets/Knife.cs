using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Knife : MonoBehaviour {

	public Rigidbody rb;
	public float force = 5.0f;
	public float torque = 20f;
	public Text scoreText;

	private Vector2 startSwipe;
	private Vector2 endSwipe;
	private float timeWhenWeStartedFlying;
	private int score;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore();
	}
	
	// Update is called once per frame
	void Update () {

		if (!rb.isKinematic)
			return;

		if (Input.GetMouseButtonDown(0))
		{
			startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		if (Input.GetMouseButtonUp(0))
		{
			endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			Swipe();
		}
	}

	void Swipe()
	{
		rb.isKinematic = false;
		timeWhenWeStartedFlying = Time.time;
		Vector2 swipe = endSwipe - startSwipe;
		Debug.Log(swipe);
		rb.AddForce(swipe * force, ForceMode.Impulse); 
		rb.AddTorque(0f, 0f, -torque, ForceMode.Impulse);
		score++;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Block")
		{
			rb.isKinematic = true;
			UpdateScore();
		}
		else
		{
			Restart();
		}
		
	}

	void OnCollisionEnter(Collision other)
	{
		float timeInAir = Time.time - timeWhenWeStartedFlying;

		if (!rb.isKinematic && timeInAir >= .05f)
		{
			Restart();
		}
	}

	void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void UpdateScore()
	{
		scoreText.text = score.ToString();
	}
}
