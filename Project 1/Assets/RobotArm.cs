using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;

public class RobotArm : MonoBehaviour {
	float time;
	public float[,] frame;
	RobotLink arm1;
	RobotLink arm2;
	RobotLink arm3;
	RobotLink staticbase;
	RobotLink dynamicbase;
	RobotLink part;
	int deg;

	public Texture tex;
	public Sprite[] circleSprites;
	
	void Start () {
		Transform t = transform.Find ("Static Base");
		staticbase = new RobotLink (t, t.position.x, t.position.y, t.position.z);
		t = transform.Find ("Dynamic Base");
		dynamicbase = new RobotLink (t, t.position.x-staticbase.t.position.x, t.position.y-staticbase.t.position.y, t.position.z-staticbase.t.position.z);
		t = transform.Find ("Arm1");
		arm1 = new RobotLink (t, t.position.x-dynamicbase.t.position.x, t.position.y-dynamicbase.t.position.y, t.position.z-dynamicbase.t.position.z);
		t = transform.Find ("Arm2");
		arm2 = new RobotLink (t, t.position.x-arm1.t.position.x, t.position.y-arm1.t.position.y, t.position.z-arm1.t.position.z);
		t = transform.Find ("Arm3");
		arm3 = new RobotLink (t, t.position.x-arm2.t.position.x, t.position.y-arm2.t.position.y, t.position.z-arm2.t.position.z);
		arm3.prev = arm2;
		arm2.prev = arm1;
		arm1.prev = dynamicbase;
		dynamicbase.prev = staticbase;
		// SIMPLE ROTATION ABOUT THE X AXIS BY X DEGREES
		//float x = 30;
		//float c = Mathf.Cos (x * Mathf.PI / 180);
		//float s = Mathf.Sin (x * Mathf.PI / 180);
		//arm1.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});

		circleSprites = Resources.LoadAll<Sprite>("blue_circle");
	}

	void RotateArm1L() {
		part = arm1;
		deg = -1;
	}

	void RotateArm1R() {
		part = arm1;
		deg = 1;
	}

	void RotateArm2L() {
		part = arm2;
		deg = 1;
	}
	
	void RotateArm2R() {
		part = arm2;
		deg = -1;
	}

	void RotateArm3L() {
		part = arm3;
		deg = -1;
	}
	
	void RotateArm3R() {
		part = arm3;
		deg = 1;
	}

	void StopRotate() {
		part = null;
	}

	private void Rotate(RobotLink part, int deg) {
		float c = Mathf.Cos (deg * Mathf.PI / 180);
		float s = Mathf.Sin (deg * Mathf.PI / 180);
		part.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});
		if (part == arm1) {
			Text text = (Text)GameObject.Find ("TextArm1").GetComponent (typeof(Text));
			int d = int.Parse (text.text.Substring (0, text.text.Length - 1));
			d = (d+deg+360)%360;
			d = d > 180 ? d-360 : d;
			text.text = d + "°";
		}
		else if (part == arm2) {
			Text text = (Text)GameObject.Find ("TextArm2").GetComponent (typeof(Text));
			int d = int.Parse (text.text.Substring (0, text.text.Length - 1));
			d = (d+deg+360)%360;
			d = d > 180 ? d-360 : d;
			text.text = d + "°";
		}
		else if (part == arm3) {
			Text text = (Text)GameObject.Find ("TextArm3").GetComponent (typeof(Text));
			int d = int.Parse (text.text.Substring (0, text.text.Length - 1));
			d = (d+deg+360)%360;
			d = d > 180 ? d-360 : d;
			text.text = d + "°";
		}
	}

	void Paint() {
		GameObject circle = new GameObject ("PaintTest");
		circle.AddComponent<SpriteRenderer> ();
		circle.GetComponent<SpriteRenderer> ().sprite = circleSprites [0];

		float[,] f = new float[4, 4]{{1,0,0,0},{0,1,0,0},{0,0,1,0},{0,.8575f,0,1}};
		float[,] p = arm3.T ();
		float[,] nf = new float[4, 4];
		for (int i = 0; i < 4; i++)
			for(int j = 0; j < 4; j++)
				nf [i, j] = f [i, 0] * p [0, j] + f [i, 1] * p [1, j] + f [i, 2] * p [2, j] + f [i, 3] * p [3, j];

		circle.transform.Rotate (0, 90, 0);
		circle.transform.localScale = new Vector3 (.0227f, .0227f, .0227f);
		circle.transform.position = new Vector3(nf[3,0] - 3.9f, nf[3,1], nf[3,2]);
	}
	
	void Update () {
		if(part != null)
			Rotate (part, deg);
		arm1.Update ();
		arm2.Update ();
		arm3.Update ();
		dynamicbase.Update ();
		staticbase.Update ();
	}



}
