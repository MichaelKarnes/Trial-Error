using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			transform.Translate(Vector3.right * -Input.GetAxis("Mouse X") * 1f);
			transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * 1f, Space.World);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			transform.Translate (transform.up * Input.GetAxis ("Mouse ScrollWheel") * 10f);
		}
	}
}
