using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	private bool _isMoving = false;
	private List<Vector3> _path = new List<Vector3>();
	private float _start;
	private float speed = 5.0f;

	// Use this for initialization
	void Start () {
		/*_path.Add (new Vector3 (-5, 0.25f, 5));
		_path.Add (new Vector3 (5, 0.25f, 5));
		_path.Add (new Vector3 (-5, 0.25f, -5));
		_path.Add (new Vector3 (5, 0.25f, -5));*/
	}
	
	// Update is called once per frame
	void Update () {
		if (_path.Count < 2)
			_isMoving = false;
		if (_isMoving) {
			float dTime = Time.time - _start;
			float segmentTime = Vector3.Distance (_path[0], _path[1]) / speed;
			while(dTime > segmentTime) {
				_path.RemoveAt (0);
				if (_path.Count < 2)
					return;
				dTime -= segmentTime;
				_start += segmentTime;
				segmentTime = Vector3.Distance (_path[0], _path[1]) / speed;
			}
			transform.position = _path[0] + dTime / segmentTime * (_path[1]-_path[0]);
		}
		if (Input.GetMouseButtonDown (1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
			float distance = 0;
			if (xzPlane.Raycast(ray, out distance))
				MoveTo(new Vector3(ray.GetPoint(distance).x, transform.position.y, ray.GetPoint (distance).z));
		}
	}

	private void MoveTo(Vector3 q) {
		Vector3 p = transform.position;
		_path = new List<Vector3> ();
		_path.Add (p);
		_path.Add (q);
		_start = Time.time;
		_isMoving = true;
	}
}
