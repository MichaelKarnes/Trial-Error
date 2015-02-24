using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RobotJointType
{
	Revolute,
	Prismatic
};

public class RobotJoint : MonoBehaviour {

	RobotJointType type;

	public RobotJoint(RobotJointType type)
	{
		this.type = type;
	}
		// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
