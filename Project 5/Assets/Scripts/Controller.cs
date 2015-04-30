using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
	private bool _isMoving = false;
	private List<Vector3> _path = new List<Vector3>();
	private List<Node> _nodes = new List<Node> ();
	private float _start;
	private float speed = 5.0f;
	private TrailRenderer trail;

	// Use this for initialization
	void Start () {
		trail = GetComponent<TrailRenderer> ();
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
			Plane xzPlane = new Plane(Vector3.up, new Vector3(0, 0.25f, 0));
			float distance = 0;
			if (xzPlane.Raycast(ray, out distance))
				MoveTo(new Vector3(ray.GetPoint(distance).x, transform.position.y, ray.GetPoint (distance).z));
		}
	}

	static IEnumerator ResetTrail(TrailRenderer trail) {
		trail.time = 0;
		yield return 0;
		trail.time = float.PositiveInfinity;
	}

	private void MoveTo(Vector3 q) {
		Vector3 p = transform.position;
		List<Vector3> points = new List<Vector3> ();
		points.AddRange (CellDivisionPoints (GameObject.Find ("Building 1")));
		points.AddRange (CellDivisionPoints (GameObject.Find ("Building 2")));
		points.AddRange (CellDivisionPoints (GameObject.Find ("Building 3")));
		_nodes = new List<Node> ();
		foreach (Vector3 point in points) {
			Node node = new Node (point);
			foreach(Node n in _nodes) {
				n.neighborNodes.Add (node);
				node.neighborNodes.Add (n);
			}
			_nodes.Add (node);
		}
		Node start = new Node (p);
		foreach(Node n in _nodes) {
			n.neighborNodes.Add (start);
			start.neighborNodes.Add (n);
		}
		_nodes.Add (start);
		Node goal = new Node (q);
		foreach(Node n in _nodes) {
			n.neighborNodes.Add (goal);
			goal.neighborNodes.Add (n);
		}
		_nodes.Add (goal);
		foreach(Node n in _nodes) {
			print (n.nodePos);
		}
		_nodes = AStarSearch (start, goal);
		_path = new List<Vector3> ();
		foreach (Node n in _nodes)
			_path.Add (n.nodePos);
		//_path.AddRange (_points);
		StartCoroutine(ResetTrail(trail));
		_start = Time.time;
		_isMoving = true;
	}

	public List<Node> AStarSearch( Node p, Node q )
	{
		List<Node> closedList = new List<Node>();
		List<Node> openList = new List<Node>();
		openList.Add (p);
		
		p.setActualCostG (0);
		p.setF(p.getActualCost() + Vector3.Distance(p.nodePos, q.nodePos));
		while (openList.Count !=0) 
		{
			Node current = openList[0];
			
			for(int i = 1; i<openList.Count; i++)
			{
				if(openList[i].getFvalue() < current.getFvalue())
				{
					current = openList[i];
				}
			}
			if(current == q)
			{
				return reconstructPath(q);
			}
			
			
			openList.Remove(current);
			closedList.Add(current); // Att the begginning we have Start and we move it to the closed list.
			
			foreach(Node n in current.neighborNodes)
			{
				if (closedList.Contains(n))
					continue;
				float estimatedGScore = current.getActualCost() + Vector3.Distance(current.nodePos, n.nodePos);
				if(hitSomething(current.nodePos, n.nodePos))
				{
					estimatedGScore = float.PositiveInfinity;
				}
				
				if(!openList.Contains(n) || (estimatedGScore < n.getActualCost()))
				{
					n.setParentNode(current);
					n.setActualCostG(estimatedGScore);
					n.setF(n.getActualCost() + Vector3.Distance(n.nodePos, q.nodePos));
					if(!openList.Contains(n))
					{
						openList.Add(n);
					}
				}
			}
			
			
		}
		
		return new List<Node>();
	}
	
	public List<Node> reconstructPath( Node q)
	{
		List<Node> totalPath = new List<Node>();
		totalPath.Add (q);
		Node current = q;
		
		while (current.getParent() != null) 
		{
			totalPath.Add(current.getParent());
			current = current.getParent();
		}
		
		totalPath.Reverse ();
		return totalPath;
	}

	private bool hitSomething(Vector3 origin, Vector3 destination)
	{
		RaycastHit hit;
		if(Physics.Raycast(origin, (destination-origin), Vector3.Distance(destination, origin)))
			return true;
		else
			return false;
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
		if (origin.x < -5f || origin.x > 5f || origin.z < -5f || origin.z > 5f)
			return Vector3.zero;
		RaycastHit hit;
		if (Physics.Raycast (new Ray (origin, direction), out hit, direction.z > 0 ? 5f + origin.z : 5f - origin.z)) {
			if (hit.transform.gameObject != obstacle)
				return new Vector3 (origin.x, .25f, (hit.point.z + origin.z) / 2);
		} else {
			if (GameObject.Find ("Building 1") != obstacle && GameObject.Find ("Building 1").GetComponent<BoxCollider> ().bounds.Contains (origin))
				return origin;
			if (GameObject.Find ("Building 2") != obstacle && GameObject.Find ("Building 2").GetComponent<BoxCollider> ().bounds.Contains (origin))
				return origin;
			if (GameObject.Find ("Building 3") != obstacle && GameObject.Find ("Building 3").GetComponent<BoxCollider> ().bounds.Contains (origin))
				return origin;
			return new Vector3 (origin.x, .25f, direction.z > 0 ? (5f + origin.z) / 2 : (-5f + origin.z) / 2);
		}
		return Vector3.zero;
	}
}
