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
	
	//	float titleHeight;
	//	float titleWidth;

	public Texture tex;
	float buttonX;
	float buttonZ;
	float buttonY;
	Vector3 buttonMovement;
	public Sprite[] circleSprites;

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

		circleSprites = Resources.LoadAll<Sprite>("blue_circle");

		buttonX = 0;
		buttonZ = 0;
		buttonY = 0;
		buttonMovement = new Vector3 (buttonX, buttonY, buttonZ);
	}

	void OnGUI() 
	{		
		if (!tex)
			Debug.LogError ("No texture Found, please assign a texture on the inspector");
		
		//if (GUILayout.Button ("Paint!")) --- Works as well 
		Rect paintbutton = new Rect (10, 70, 50, 30);
		paintbutton.position = new Vector2(978,200);
		if(GUI.Button(paintbutton, "Paint!")) 
		{
			Debug.Log ("Clicked the button I wanted");

			GameObject circle = new GameObject ("PaintTest");
			circle.AddComponent<SpriteRenderer> ();
			circle.GetComponent<SpriteRenderer> ().sprite = circleSprites [0];

			Vector3 paint = arm3.t.position;
			paint.x = paint.x - 3.9F;
			paint.y = paint.y;
			paint.z = paint.z;
			
			Vector3 circleSpriteRotation = new Vector3 (0, 90, 0);
			circle.transform.Rotate (0, 90, 0);
			circle.transform.localScale += new Vector3 (-.9F, -.9F, -.9F);
			circle.transform.position = paint;
		}
	}


	// Update is called once per frame
	void Update () {

		/*GameObject circle = new GameObject ("PaintTest");
		circle.AddComponent<SpriteRenderer> ();
		circle.GetComponent<SpriteRenderer> ().sprite = circleSprites [0];*/

		arm1.Update ();
		arm2.Update ();
		arm3.Update ();
		dynamicbase.Update ();
		staticbase.Update ();

		//if (Input.GetMouseButtonDown (0)) {  Works fine. Detects when mouse left button is clicked, not necessarily the created button.

			/*Debug.Log ("Clicked the button I wanted");
						Vector3 paint = arm3.t.position;
						paint.x = paint.x - 3.9F;
						paint.y = paint.y;
						paint.z = paint.z;

						Vector3 circleSpriteRotation = new Vector3 (0, 90, 0);
						circle.transform.Rotate (0, 90, 0);
						circle.transform.localScale += new Vector3 (-.9F, -.9F, -.9F);
						circle.transform.position = paint;

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
