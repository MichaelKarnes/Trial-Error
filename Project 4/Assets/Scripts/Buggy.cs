﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buggy : MonoBehaviour {
	public Transform transform;
	public WheelCollider LeftWheel;
	public WheelCollider RightWheel;
	public Transform LeftSensor;
	public Transform RightSensor;
	public float[,] K;
	private List<Light> lights;
	private bool on = false;

	public void Init() {
		K = new float[2, 2]{{-.5f,1},{1,-.5f}};
		lights = new List<Light> ();
	}

	// Use this for initialization
	void Start () {
		Init ();
	}

	float DistanceToLight(Transform sensor, Light light) {
		return Vector3.Magnitude (light.transform.position - sensor.position);
	}

	public void SetLights(List<Light> lights) {
		this.lights = lights;
	}

	public void Activate() {
		LeftWheel.enabled = false;
		RightWheel.enabled = false;
		LeftWheel.enabled = true;
		RightWheel.enabled = true;
		on = true;
	}
	
	// Update is called once per frame
	void Update () {
		LeftWheel.motorTorque = 0f;
		RightWheel.motorTorque = 0f;
		if (!on)
			return;
		foreach (Light light in lights) {
			LeftWheel.motorTorque += K[0,0]*100/DistanceToLight (LeftSensor, light) + K[1,0]*100/DistanceToLight (RightSensor, light);
			RightWheel.motorTorque += K[0,1]*100/DistanceToLight (LeftSensor, light) + K[1,1]*100/DistanceToLight (RightSensor, light);
		}
		LeftWheel.transform.Find ("Tire_L").Rotate (LeftWheel.rpm / 60 * 360 * Time.deltaTime,0,0);
		RightWheel.transform.Find ("Tire_R").Rotate (RightWheel.rpm / 60 * 360 * Time.deltaTime,0,0);
	}
}
