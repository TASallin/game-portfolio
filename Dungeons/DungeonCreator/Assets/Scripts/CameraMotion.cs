// This sample code demonstrates how to create geometry "on demand" based on camera motion.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {


	int max_plane = -1;       // the number of planes that we've made
	float plane_size = 3.0f;  // size of the planes

	int setX;
	int setY;
	int setZ;
	bool unitysux = false;

	void Start () {
		
	}
	
	// Move the camera, and maybe create a new plane
	void FixedUpdate () {

		if (unitysux && Camera.main != null) {
			Camera.main.transform.position = new Vector3(setX, setY, setZ);
			unitysux = false;
		}

		// get the horizontal and verticle controls (arrows, or WASD keys)
		float dx = Input.GetAxis ("Horizontal");
		float dz = Input.GetAxis ("Vertical");
		bool up = Input.GetKey(KeyCode.Q);
		bool down = Input.GetKey(KeyCode.E);
		int dy = 0;
		if (up) {
			dy = 2;
		} else if (down && Camera.main != null && Camera.main.transform.position.y > 2) {
			dy = -2;
		}

		// sensitivity factors for translate and rotate
		float translate_factor = 10;
		float rotate_factor = 5.0f;

		// move the camera based on the keyboard input
		if (Camera.main != null) {
			// translate forward or backwards
			Camera.main.transform.Translate (dx*translate_factor*Time.deltaTime, dz * translate_factor*Time.deltaTime, -dy * translate_factor*Time.deltaTime);

			// translate left or right
			//Camera.main.transform.Rotate (0, dx * rotate_factor, 0);

		}

		// grab the main camera position
		Vector3 cam_pos = Camera.main.transform.position;
		//Debug.LogFormat ("x z: {0} {1}", cam_pos.x, cam_pos.z);

		// if the camera has moved far enough, create another plane
		//if (cam_pos.z > (max_plane + 0.5) * plane_size * 2) {
			//create_new_plane ();
		//}

	}

	public void Set(int x, int y, int z) {
		setX = x;
		setY = y;
		setZ = z;
		unitysux = true;
	}
}
