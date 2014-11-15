/* 
	Unity Gyroscope control for iOS and Android adapted from a Javascript implementation by Perry Hoberman <hoberman@bway.net>.
	Justin Kovac
*/

using UnityEngine;
using System.Collections;

public class GyroCamera : MonoBehaviour {
	
	static bool gyroBool;
	private Gyroscope gyro;
	private Quaternion quatMult;
	private Quaternion quatMap;
	
	// Camera grandparent node to rotate heading
	private GameObject camGrandparent;
	
	// Mouse/touch input
	public bool touchRotatesHeading;
	private Vector2 screenSize;
	private Vector2 mouseStartPoint;
	
	// Custom Component menu
	[AddComponentMenu("Gyro Camera")]
		
	void Awake () {
	
		// Find the current parent of the camera's transform
		Transform currentParent = transform.parent;
		
		// Instantiate a new transform
		GameObject camParent = new GameObject ("camParent");
		
		// Match the transform to the camera position
		camParent.transform.position = transform.position;
		
		// Make the new transform the parent of the camera transform
		transform.parent = camParent.transform;
		
		// Instantiate a new transform
		camGrandparent = new GameObject ("camGrandparent");
		
		// Match the transform to the camera position
		camGrandparent.transform.position = transform.position;
		
		// Make the new transform the grandparent of the camera transform
		camParent.transform.parent = camGrandparent.transform;
		
		// Make the original parent the great grandparent of the camera transform
		camGrandparent.transform.parent = currentParent;

		gyroBool = Input.isGyroAvailable;
		gyroBool = SystemInfo.supportsGyroscope;

		if (gyroBool) {
		
			gyro = Input.gyro;
			gyro.enabled = true;
			
			#if UNITY_IPHONE
				
           		camParent.transform.eulerAngles = new Vector3(90, 90, 0);

            	if (Screen.orientation == ScreenOrientation.LandscapeLeft) {

                	quatMult = new Quaternion(0, 0, 0.7071f, 0.7071f);

            	} else if (Screen.orientation == ScreenOrientation.LandscapeRight) {

                	quatMult = new Quaternion(0, 0, -0.7071f, 0.7071f);

            	} else if (Screen.orientation == ScreenOrientation.Portrait) {

                	quatMult = new Quaternion(0, 0, 1, 0);

            	} else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown) {

                	quatMult = new Quaternion(0, 0, 0, 1);

            	}
				
			#endif

	        #if UNITY_ANDROID
	
	            camParent.transform.eulerAngles = Vector3(-90, 0, 0);
	
	            if (Screen.orientation == ScreenOrientation.LandscapeLeft) {
	
	                quatMult = Quaternion(0, 0, 0.7071, -0.7071);
	
	            } else if (Screen.orientation == ScreenOrientation.LandscapeRight) {
	
	                quatMult = Quaternion(0, 0, -0.7071, -0.7071);
	
	            } else if (Screen.orientation == ScreenOrientation.Portrait) {
	
	                quatMult = Quaternion(0, 0, 0, 1);
	
	            } else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown) {
	
	                quatMult = Quaternion(0, 0, 1, 0);
	
	            }
	
	        #endif
	
	        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	
		    } else {
		
		        #if UNITY_EDITOR
		
		            		
		        #endif
		
		    }
		
	}

	void Start () {
		screenSize.x = Screen.width;
		screenSize.y = Screen.height;
	
	}
	
	void Update () {
	    if (gyroBool) {

	        #if UNITY_IPHONE

	            quatMap = gyro.attitude;

	        #endif

	        #if UNITY_ANDROID

	            quatMap = Quaternion(gyro.attitude.w,gyro.attitude.x,gyro.attitude.y,gyro.attitude.z);

	        #endif

	        transform.localRotation = quatMap * quatMult;

	    }

	    #if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR

	        if (touchRotatesHeading) {

	           // GetTouchMouseInput();

	        }

	    #endif
	
	}
}
