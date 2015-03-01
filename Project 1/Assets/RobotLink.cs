using UnityEngine;
using System.Collections;

public class RobotLink {
	
	public RobotLink prev = null;
	public float[,] frame;
	public Transform t;

	public RobotLink()
	{
	}
	public RobotLink(Transform t)
	{
		this.t = t;
		frame = new float[4,4]{{1,0,0,0},{0,1,0,0},{0,0,1,0},{t.position.x,t.position.y,t.position.z,1}};
	}

	public void Multiply( float[,] m )
	{
		float[,] f = frame;
		float[,] nf = new float[4, 4];
		for (int i = 0; i < 4; i++)
			for(int j = 0; j < 4; j++)
				nf [i, j] = m [i, 0] * f [0, j] + m [i, 1] * f [1, j] + m [i, 2] * f [2, j] + m [i, 3] * f [3, j];
		frame = nf;
		//if (next != null)
		//	next.Multiply (m);
	}

	public float[,] T()
	{
		float[,] f = frame;
		float[,] p = new float[4, 4]{{1,0,0,0},{0,1,0,0},{0,0,1,0},{0,0,0,1}};
		if (prev != null)
			p = prev.T ();
		float[,] nf = new float[4, 4];
		for (int i = 0; i < 4; i++)
			for(int j = 0; j < 4; j++)
				nf [i, j] = f [i, 0] * p [0, j] + f [i, 1] * p [1, j] + f [i, 2] * p [2, j] + f [i, 3] * p [3, j];
		return nf;
	}
	
	public void Update () {
		//(t.name == "Arm1")
		float[,] f = T ();
		float rx = Mathf.Atan2 (f [1, 2], f [2, 2]);
		float ry = Mathf.Atan2 (-f [0, 2], Mathf.Sqrt (Mathf.Pow (f [1, 2], 2) + Mathf.Pow (f[2, 2], 2)));
		float rz = Mathf.Atan2 (f [0, 1], f [0, 0]);
		t.eulerAngles = new Vector3(rx*180/Mathf.PI, ry*180/Mathf.PI, rz*180/Mathf.PI);
		t.position = new Vector3(f[3,0], f[3,1], f[3,2]);
	}
}