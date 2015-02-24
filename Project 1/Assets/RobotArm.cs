using UnityEngine;
using System.Collections;

public class RobotArm : MonoBehaviour {
	float time;
	/*RobotEndEffector arm;
	RobotLink dynamicbase;
	RobotBase staticbase;*/
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		time = time % 4;
		if (time < 2) {
						foreach (Transform t in transform)
								if (t.name == "Arm")
										t.Rotate (-100 * Time.deltaTime, 0, 0);
				} else {
						foreach (Transform t in transform)
								if (t.name == "Arm")
										t.Rotate (100 * Time.deltaTime, 0, 0);
				}
	}
}
