using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public CharacterController characterController;
	public float speed = 12f;

	public float gravity = -9.8f;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundmask;
	Vector3 velocity;
	bool isgrounded;

	public float jumpHeight = 2f;


	public AudioSource leftStep;
	public AudioSource rightStep;
	bool isWalk = false;
	bool isStepping = false;
	int steps;
	public float timeForPlay;

	void Update() {
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		Vector3 move = transform.right * x + transform.forward * z;
		characterController.Move(move * speed * Time.deltaTime);


		isgrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);
		if (isgrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}
		velocity.y += gravity * Time.deltaTime;
		characterController.Move(velocity * Time.deltaTime);

		if (Input.GetKey(KeyCode.Space) && isgrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}


		if (x != 0 || z != 0)
		{
			isWalk = true;
			if (!isStepping)
			{
				StartCoroutine(playFootStepAudio());
			}
			
		}
		else
		{
			isWalk = false;
			leftStep.Stop();
			rightStep.Stop();
		}


	}

	IEnumerator playFootStepAudio()
	{
		if (isStepping == false && isWalk == true)
		{
			steps = Random.Range(1, 3);
			isStepping = true;
			if (steps == 1)
			{
				leftStep.Play();
				
			}
			else
			{
				rightStep.Play();
			}

		}

		yield return new WaitForSeconds(timeForPlay);
		isStepping = false;

	}
}
