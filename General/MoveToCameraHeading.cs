/* 
	A simple method for using the existing FirstPersonCharacter controller to move the character in the direction of the camera heading.
	Great for GyroCamera or VR movement.
	Justin Kovac
*/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class MoveToCameraHeading : MonoBehaviour {
	
	public float moveSpeed = 1.0f;
	public bool isWalking = false;
	
	void Awake () {
		CharacterController controller = GetComponent<CharacterController>();

	}
	
	void Update () {
		
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

		if (isWalking) {
		
			controller.SimpleMove(forward * moveSpeed);
		}
		
		// Example check for toggling walking
		/*
		if (pedometer.isWalking) {
			isWalking = true;

		}
		else {
			isWalking = false;
			
		}
		*/
		// OR
		/*
		if (Input.GetMouseButton(0)) {
			isWalking = true;

		}
		else {
			isWalking = false;
			
		}		
		*/
		
	}
	
}
