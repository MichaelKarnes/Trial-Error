using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour  {


	float xPosition;
	float yPosition;
	float r;
	Rect testBug;

	public Bug(float x, float y, float r)
	{
		xPosition = x;
		yPosition = y;
		r = r;
  
		testBug = new Rect (0, 0, 10, 10);
//		testBug = gameObject.AddComponent<Rect>();

		testBug.x = x;
		testBug.y = y;

	}

	public void Place() {
		print ("Place");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame(
	void Update () {
	
	}
}
