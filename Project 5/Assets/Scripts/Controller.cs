using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	private bool _isMoving = false;
	private List<Vector3> _path = new List<Vector3>();
	private List<Vector3> _points = new List<Vector3> ();
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
		_points = new List<Vector3> ();
		_points.Add (p);
		_points.AddRange (CellDivisionPoints (GameObject.Find ("Building 1")));
		_points.AddRange (CellDivisionPoints (GameObject.Find ("Building 2")));
		_points.AddRange (CellDivisionPoints (GameObject.Find ("Building 3")));
		_points.Add (q);
		_path = new List<Vector3> ();
		//_start = Time.time;
		//_isMoving = true;
	}

	private List<Vector3> CellDivisionPoints(GameObject obstacle) {
		List<Vector3> points = new List<Vector3> ();
		Transform t = obstacle.transform;
		Vector3 o = obstacle.transform.position;
		RaycastHit hit;
		Vector3 origin;
		Vector3 point;

		// Bottom Left Corner
		origin = o - t.right * t.localScale.x / 2 - t.forward * t.localScale.z / 2;
		point = CellDivisionPoint (obstacle, origin, -Vector3.forward);
		if (point != Vector3.zero)
			points.Add (point);

		// Top Left Corner
		origin = o - t.right * t.localScale.x / 2 + t.forward * t.localScale.z / 2;
		point = CellDivisionPoint (obstacle, origin, Vector3.forward);
		if (point != Vector3.zero)
			points.Add (point);

		// Top Right Corner
		origin = o + t.right * t.localScale.x / 2 + t.forward * t.localScale.z / 2;
		point = CellDivisionPoint (obstacle, origin, Vector3.forward);
		if (point != Vector3.zero)
			points.Add (point);

		// Bottom Right Corner
		origin = o + t.right * t.localScale.x / 2 - t.forward * t.localScale.z / 2;
		point = CellDivisionPoint (obstacle, origin, -Vector3.forward);
		if (point != Vector3.zero)
			points.Add (point);

		return points;
	}

	private Vector3 CellDivisionPoint(GameObject obstacle, Vector3 origin, Vector3 direction) {
		RaycastHit hit;
		if (Physics.Raycast (new Ray (origin, direction), out hit, direction.z > 0 ? 5f + origin.z : 5f - origin.z)) {
			if (hit.transform.gameObject != obstacle)
				return new Vector3 (origin.x, 0, (hit.point.z + origin.z) / 2);
		} else {
			if (GameObject.Find ("Building 1") != obstacle && GameObject.Find ("Building 1").GetComponent<BoxCollider> ().bounds.Contains (origin))
				return origin;
			if (GameObject.Find ("Building 2") != obstacle && GameObject.Find ("Building 2").GetComponent<BoxCollider> ().bounds.Contains (origin))
				return origin;
			if (GameObject.Find ("Building 3") != obstacle && GameObject.Find ("Building 3").GetComponent<BoxCollider> ().bounds.Contains (origin))
				return origin;
			return new Vector3 (origin.x, 0, direction.z > 0 ? (5f + origin.z) / 2 : (-5f + origin.z) / 2);
		}
		return Vector3.zero;
	}
}
