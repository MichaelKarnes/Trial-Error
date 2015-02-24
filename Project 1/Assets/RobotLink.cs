using UnityEngine;
using System.Collections;

public class RobotLink : MonoBehaviour {

	public RobotJoint prevJoint;
	public RobotJoint nextJoint;

	public RobotLink()
	{
	}

	public RobotLink(RobotJoint prevJoint, RobotJoint nextJoint)
	{
		this.prevJoint = prevJoint;
		this.nextJoint = nextJoint;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}

public class RobotEndEffector : RobotLink {

	public RobotEndEffector(RobotJoint prevJoint)
	{
		base.prevJoint = prevJoint;
		base.nextJoint = null;
	}
}

public class RobotBase : RobotLink {

	public RobotBase(RobotJoint nextJoint)
	{
		base.nextJoint = nextJoint;
	}
}