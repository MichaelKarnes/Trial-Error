using UnityEngine;
using System.Collections;
using UnityEditor;

public class RobotArm : MonoBehaviour {
	float time;
	public float[,] frame;
	RobotLink arm1;
	RobotLink arm2;
	RobotLink arm3;
	RobotLink staticbase;
	RobotLink dynamicbase;
	/*RobotEndEffector arm;
	RobotLink dynamicbase;
	RobotBase staticbase;*/
	// Use this for initialization
	void Start () {
		foreach (Transform t in transform) {
			if (t.name == "Arm1")
				arm1 = new RobotLink (t);
			else if (t.name == "Arm2")
				arm2 = new RobotLink (t);
			else if (t.name == "Arm3")
				arm3 = new RobotLink (t);
			else if (t.name == "Static Base")
				staticbase = new RobotLink (t);
			else if (t.name == "Dynamic Base")
				dynamicbase = new RobotLink (t);
		}
		arm3.prev = arm2;
		arm2.prev = arm1;
		arm1.prev = dynamicbase;
		dynamicbase.prev = staticbase;
		// SIMPLE ROTATION ABOUT THE X AXIS BY X DEGREES
		// double x = 30;
		//float c = Mathf.Cos (x * Mathf.PI / 180);
		//float s = Mathf.Sin (x * Mathf.PI / 180);
		//arm1.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});
	}
	

	/*void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (paintPos, 100);
	}*/

	// Update is called once per frame
	void Update () {

		arm1.Update ();
		arm2.Update ();
		arm3.Update ();
		dynamicbase.Update ();
		staticbase.Update ();

		Vector3 paint = arm3.t.position;
		paint.x = paint.x-5;
		paint.y = paint.y;
		paint.z = paint.z;
		//Vector3 paintPos = new Vector3 (arm3.t.position [3, 0], arm3.t.position [3, 1], arm3.t.position [3, 2]);
		GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		//mySphere.transform.localScale = Vector3(...);
		sphere.transform.localScale += new Vector3 (0F, -.7F, -.7F);
		sphere.transform.position = paint;
		//OnDrawGizmosSelected ();
		/*time += Time.deltaTime;
		time = time % 4;
		if (time < 2) {
						foreach (Transform t in transform)
								if (t.name == "Arm1")
										t.Rotate (-10 * Time.deltaTime, 0, 0);
				} else {
						foreach (Transform t in transform)
								if (t.name == "Arm1")
										t.Rotate (10 * Time.deltaTime, 0, 0);
				}*/
		/*float rx = Mathf.Atan2 (frame [2, 1], frame [2, 2]);
		float ry = Mathf.Atan2 (-frame [2, 0], Mathf.Sqrt (Mathf.Pow (frame [2, 1], 2) + Mathf.Pow (frame[2, 2], 2)));
		float rz = Mathf.Atan2 (frame [1, 0], frame [0, 0]);
		foreach (Transform t in transform)
			if (t.name == "Arm1") {
				t.eulerAngles = new Vector3(rx*180/Mathf.PI, ry*180/Mathf.PI, rz*180/Mathf.PI);
				t.position = new Vector3(frame[3,0], frame[3,1], frame[3,2]);
			}*/
	}



}
