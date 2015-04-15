using UnityEngine;
using System.Collections;

public class Buggy : MonoBehaviour {
	public WheelCollider LeftWheel;
	public WheelCollider RightWheel;
	public Transform LeftSensor;
	public Transform RightSensor;
	public float[,] K;
	private Light[] lights;

	// Use this for initialization
	void Start () {
		K = new float[2, 2]{{-.9f,1},{1,-.9f}};
		lights = FindObjectsOfType (typeof(Light)) as Light[];
	}

	float DistanceToLight(Transform sensor, Light light) {
		return Vector3.Magnitude (light.transform.position - sensor.position);
	}
	
	// Update is called once per frame
	void Update () {
		//LeftWheel.motorTorque = 1f;
		//RightWheel.motorTorque = .5f;
		LeftWheel.motorTorque = 0f;
		RightWheel.motorTorque = 0f;
		foreach (Light light in lights) {
			LeftWheel.motorTorque += K[0,0]*100/DistanceToLight (LeftSensor, light) + K[1,0]*100/DistanceToLight (RightSensor, light);
			RightWheel.motorTorque += K[0,1]*100/DistanceToLight (LeftSensor, light) + K[1,1]*100/DistanceToLight (RightSensor, light);
		}
		LeftWheel.transform.Find ("Tire_L").Rotate (LeftWheel.rpm / 60 * 360 * Time.deltaTime,0,0);
		RightWheel.transform.Find ("Tire_R").Rotate (RightWheel.rpm / 60 * 360 * Time.deltaTime,0,0);
	}
}
