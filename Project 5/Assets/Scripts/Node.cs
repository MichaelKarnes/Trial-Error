using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {


	//Transform objectTransform = new Transform(); 
	public Vector3 nodePos = new Vector3(0,0,0);
	public List<Node> neighborNodes = new List<Node>();
	Node parentNode;
	float actualCostG = 0;
	float estimatedCostH = 0;
	float fResult = 0;

	public Node()
	{
		 nodePos = new Vector3(0,0,0);

		 actualCostG = 0;
		 estimatedCostH = 0;
		 fResult = 0;
	}

	public Node(Vector3 p)
	{
		nodePos = p;
		
		actualCostG = 0;
		estimatedCostH = 0;
		fResult = 0;
	}
	// Use this for initialization
	void Start () {

		//nodePos.x = transform.position.x;
		//nodePos.y = transform.position.y;
		//nodePos.z = transform.position.z;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	public void outputPosition() {
	
		//Debug.Log ("TEST");
		//Debug.Log (nodePos.x);
	}

	public void setNodePos(Vector3 pos)
	{
		nodePos = pos;
	}


	public void setParentNode(Node n)
	{
		parentNode = n;
	}
	public void setActualCostG(float actualCost)
	{
		actualCostG = actualCost;
	}
	public void setEstimatedCostH(float estimatedCost)
	{
		estimatedCostH = estimatedCost;
	}
	public void setF(float f)
	{
		fResult = f;
	}
	public float getActualCost()
	{
		return actualCostG;
	}
	public float getEstimatedCost()
	{
		return estimatedCostH;
	}
	public float getFvalue()
	{
		return fResult;
	}
	public Node getParent()
	{
		return parentNode;
	}
}
