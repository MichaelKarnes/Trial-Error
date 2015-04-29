using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	private Vector3 offset;
	private bool _isDragging = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (_isDragging) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
			float distance = 0;
			if (xzPlane.Raycast(ray, out distance))
				transform.position = ray.GetPoint(distance) - offset;
		}
	}

	void OnMouseDown() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
		float distance = 0;
		if (xzPlane.Raycast(ray, out distance))
			offset = ray.GetPoint(distance) - transform.position;
		_isDragging = true;
	}

	void OnMouseUp() {
		_isDragging = false;
	}
}
