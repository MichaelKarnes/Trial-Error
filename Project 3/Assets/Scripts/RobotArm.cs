using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class RobotArm : MonoBehaviour {
	float time;
	public float[,] frame;
	RobotLink arm1;
	RobotLink arm2;
	RobotLink pointer;
	RobotLink staticbase;
	RobotLink dynamicbase;
	RobotLink part;
	int deg;
	float t1, t2, t3 = 0;
	Vector2 moveDir = Vector2.zero;
	public Client client;
	
	public Sprite[] circleSprites;

	void Start () {
		staticbase = new RobotLink (transform.Find ("StaticBase"));
		dynamicbase = new RobotLink (transform.Find ("DynamicBase"));
		arm1 = new RobotLink (transform.Find ("Arm1"));
		arm2 = new RobotLink (transform.Find ("Arm2"));
		pointer = new RobotLink (transform.Find ("Pointer"));
		pointer.Link (arm2);
		arm2.Link (arm1);
		arm1.Link (dynamicbase);
		dynamicbase.Link (staticbase);
		// SIMPLE ROTATION ABOUT THE X AXIS BY X DEGREES
		//float x = 30;
		//float c = Mathf.Cos (x * Mathf.PI / 180);
		//float s = Mathf.Sin (x * Mathf.PI / 180);
		//arm1.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});

		circleSprites = Resources.LoadAll<Sprite>("blue_circle");
	}

	public void RotateArm1L() {
		if (client != null)
			client.RotateArm1L(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		part = arm1;
		deg = -1;
	}

	public void RotateArm1R() {
		if (client != null)
			client.RotateArm1R(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		part = arm1;
		deg = 1;
	}

	public void RotateArm2L() {
		if (client != null)
			client.RotateArm2L(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		part = arm2;
		deg = 1;
	}
	
	public void RotateArm2R() {
		part = arm2;
		deg = -1;
		if (client != null)
			client.RotateArm2R(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
	}
	public void RotatePointerL() {
		if (client != null)
			client.RotatePointerL(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		part = pointer;
		deg = -1;
	}
	
	public void RotatePointerR() {
		if (client != null)
			client.RotatePointerR(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		part = pointer;
		deg = 1;
	}

	public void StopRotate() {
		if (client != null)
			client.StopRotate(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		part = null;
	}

	public void MoveUp() {
		if (client != null)
			client.MoveUp(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		moveDir = Vector2.up;
	}

	public void MoveDown() {
		if (client != null)
			client.MoveDown(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		moveDir = -Vector2.up;
	}

	public void MoveLeft() {
		if (client != null)
			client.MoveLeft(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		moveDir = -Vector2.right;
	}

	public void MoveRight() {
		if (client != null)
			client.MoveRight(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		moveDir = Vector2.right;
	}

	public void StopMove() {
		if (client != null)
			client.StopMove(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		moveDir = Vector2.zero;
	}

	private void Move() {
		float c, s;
		c = Mathf.Cos (-t1 * Mathf.PI / 180);
		s = Mathf.Sin (-t1 * Mathf.PI / 180);
		arm1.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});
		c = Mathf.Cos (-t2 * Mathf.PI / 180);
		s = Mathf.Sin (-t2 * Mathf.PI / 180);
		arm2.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});
		c = Mathf.Cos (-t3 * Mathf.PI / 180);
		s = Mathf.Sin (-t3 * Mathf.PI / 180);
		pointer.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});

		float l1 = 16.65f;
		float l2 = 15.25f;
		float l3 = 8.33f;
		Vector2 p = l1 * new Vector2 (Mathf.Sin (t1 * Mathf.PI/180), Mathf.Cos (t1 * Mathf.PI/180));
		Vector2 q = p + l2 * new Vector2 (Mathf.Sin ((t1+t2) * Mathf.PI/180), Mathf.Cos ((t1+t2) * Mathf.PI/180));
		Vector2 r = q + l3 * new Vector2 (Mathf.Sin ((t1+t2+t3) * Mathf.PI/180), Mathf.Cos ((t1+t2+t3) * Mathf.PI/180));
		Vector2 rp = r + 0.1f*moveDir;
		Vector2 qp = q + 0.1f*moveDir;
		if (rp.sqrMagnitude > Mathf.Pow (l1 + l2 + l3, 2)) {
			rp = rp.normalized * (l1 + l2 + l3);
		}
		//rp = new Vector2 (l2 + l3, l1);
		//qp = rp - new Vector2 (l3, 0);

		if (qp.sqrMagnitude <= Mathf.Pow (l1 + l2, 2) && qp.sqrMagnitude >= Mathf.Pow (l1 - l2, 2)) {
			float gamma = Mathf.Acos ((qp.sqrMagnitude + l1 * l1 - l2 * l2) / (2 * l1 * qp.magnitude)) * 180 / Mathf.PI;
			float beta = Mathf.Atan2 (qp.y, qp.x) * 180 / Mathf.PI;
			float t1p1, t1p2, t2p1, t2p2;
			t1p1 = 90 - (beta + gamma);
			t2p1 = Mathf.Acos ((Mathf.Pow (qp.x, 2) + Mathf.Pow (qp.y, 2) - l1 * l1 - l2 * l2) / (2 * l1 * l2)) * 180 / Mathf.PI;
			t1p2 = 90 - (beta - gamma);
			t2p2 = -Mathf.Acos ((Mathf.Pow (qp.x, 2) + Mathf.Pow (qp.y, 2) - l1 * l1 - l2 * l2) / (2 * l1 * l2)) * 180 / Mathf.PI;
			float t1p, t2p;
			if(float.IsNaN (t1p1))
				t1p = t1p2;
			else
				t1p = 180 - Mathf.Abs(Mathf.Abs(t1p1-t1) - 180) < 180 - Mathf.Abs(Mathf.Abs(t1p2-t1) - 180) ? t1p1 : t1p2;
			if(float.IsNaN (t2p1))
				t2p = t2p2;
			else
				t2p = 180 - Mathf.Abs(Mathf.Abs(t2p1-t2) - 180) < 180 - Mathf.Abs(Mathf.Abs(t2p2-t2) - 180) ? t2p1 : t2p2;
			if (!float.IsNaN (t1p) && !float.IsNaN (t2p)) {
				t3 -= (t1p + t2p) - (t1 + t2);
				t1 = t1p;
				t2 = t2p;
			}
		} else {
			//Debug.Break ();
			rp -= p;
			float gamma = Mathf.Acos ((rp.sqrMagnitude + l2 * l2 - l3 * l3) / (2 * l2 * rp.magnitude)) * 180 / Mathf.PI;
			float beta = Mathf.Atan2 (rp.y, rp.x) * 180 / Mathf.PI + t1;
			float t2p1, t2p2, t3p1, t3p2;
			t2p1 = 90 - (beta - gamma);
			t3p1 = -Mathf.Acos ((Mathf.Pow (rp.x, 2) + Mathf.Pow (rp.y, 2) - l2 * l2 - l3 * l3) / (2 * l2 * l3)) * 180 / Mathf.PI;
			t2p2 = 90 - (beta + gamma);
			t3p2 = Mathf.Acos ((Mathf.Pow (rp.x, 2) + Mathf.Pow (rp.y, 2) - l2 * l2 - l3 * l3) / (2 * l2 * l3)) * 180 / Mathf.PI;
			float t2p, t3p;
			if(float.IsNaN (t2p1))
				t2p = t2p2;
			else
				t2p = 180 - Mathf.Abs(Mathf.Abs(t2p1-t2) - 180) < 180 - Mathf.Abs(Mathf.Abs(t2p2-t2) - 180) ? t2p1 : t2p2;
			if(float.IsNaN (t3p1))
				t3p = t3p2;
			else
				t3p = 180 - Mathf.Abs(Mathf.Abs(t3p1-t3) - 180) < 180 - Mathf.Abs(Mathf.Abs(t3p2-t3) - 180) ? t3p1 : t3p2;
			if(!float.IsNaN (t2p) && !float.IsNaN (t3p)) {
				t2 = t2p;
				t3 = t3p;
			}
		}

		c = Mathf.Cos (t1 * Mathf.PI / 180);
		s = Mathf.Sin (t1 * Mathf.PI / 180);
		arm1.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});
		c = Mathf.Cos (t2 * Mathf.PI / 180);
		s = Mathf.Sin (t2 * Mathf.PI / 180);
		arm2.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});
		c = Mathf.Cos (t3 * Mathf.PI / 180);
		s = Mathf.Sin (t3 * Mathf.PI / 180);
		pointer.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});

		UpdateText ();
	}

	private void Rotate(RobotLink part, int deg) {
		float c = Mathf.Cos (deg * Mathf.PI / 180);
		float s = Mathf.Sin (deg * Mathf.PI / 180);
		part.Multiply (new float[4,4] {{1,0,0,0},{0,c,s,0},{0,-s,c,0},{0,0,0,1}});
		if (part == arm1) {
			t1 += deg;
			Text text = (Text)GameObject.Find ("TextArm1").GetComponent (typeof(Text));
			int d = int.Parse (text.text.Substring (0, text.text.Length - 1));
			d = Mathf.RoundToInt ((t1+360)%360);
			d = d > 180 ? d-360 : d;
			text.text = d + "°";
		}
		else if (part == arm2) {
			t2 += deg;
			Text text = (Text)GameObject.Find ("TextArm2").GetComponent (typeof(Text));
			int d = int.Parse (text.text.Substring (0, text.text.Length - 1));
			d = Mathf.RoundToInt ((t2+360)%360);
			d = d > 180 ? d-360 : d;
			text.text = d + "°";
		}
		else if (part == pointer) {
			t3 += deg;
		}
		UpdateText ();
	}

	private void UpdateText() {
		Text text;
		int d;
		text = (Text)GameObject.Find ("TextArm1").GetComponent (typeof(Text));
		d = Mathf.RoundToInt ((t1+360)%360);
		d = d > 180 ? d-360 : d;
		text.text = d + "°";
		text = (Text)GameObject.Find ("TextArm2").GetComponent (typeof(Text));
		d = Mathf.RoundToInt ((t2+360)%360);
		d = d > 180 ? d-360 : d;
		text.text = d + "°";
		text = (Text)GameObject.Find ("TextArm3").GetComponent (typeof(Text));
		d = Mathf.RoundToInt ((t3+360)%360);
		d = d > 180 ? d-360 : d;
		text.text = d + "°";

		float l1 = 16.65f;
		float l2 = 15.25f;
		float l3 = 8.33f;
		Vector2 p = l1 * new Vector2 (Mathf.Sin (t1 * Mathf.PI/180), Mathf.Cos (t1 * Mathf.PI/180));
		Vector2 q = p + l2 * new Vector2 (Mathf.Sin ((t1+t2) * Mathf.PI/180), Mathf.Cos ((t1+t2) * Mathf.PI/180));
		Vector2 r = q + l3 * new Vector2 (Mathf.Sin ((t1+t2+t3) * Mathf.PI/180), Mathf.Cos ((t1+t2+t3) * Mathf.PI/180));

		text = (Text)GameObject.Find ("TextPosition").GetComponent (typeof(Text));
		text.text = "(" + string.Format ("{0:0.0}", Mathf.Round (r.x*10f)/10f) + "," + string.Format ("{0:0.0}", Mathf.Round (r.y*10f)/10f) + ")";
	}

	public void Paint() {
		if (client != null)
			client.Paint(BitConverter.GetBytes (DateTime.UtcNow.Ticks));
		GameObject circle = new GameObject ("PaintTest");
		circle.AddComponent<SpriteRenderer> ();
		circle.GetComponent<SpriteRenderer> ().sprite = circleSprites [0];

		float[,] f = new float[4, 4]{{1,0,0,0},{0,1,0,0},{0,0,1,0},{0,.8575f,0,1}};
		float[,] p = pointer.T ();
		float[,] nf = new float[4, 4];
		for (int i = 0; i < 4; i++)
			for(int j = 0; j < 4; j++)
				nf [i, j] = f [i, 0] * p [0, j] + f [i, 1] * p [1, j] + f [i, 2] * p [2, j] + f [i, 3] * p [3, j];

		circle.transform.Rotate (0, 90, 0);
		circle.transform.localScale = new Vector3 (.2f, .2f, .2f);
		circle.transform.position = pointer.t.position + 8.33f*pointer.t.up - 10*pointer.t.right;
	}
	
	void FixedUpdate () {
		if(part != null)
			Rotate (part, deg);
		if (moveDir != Vector2.zero)
			Move ();
		arm1.Update ();
		arm2.Update ();
		pointer.Update ();
		dynamicbase.Update ();
		staticbase.Update ();
	}



}
