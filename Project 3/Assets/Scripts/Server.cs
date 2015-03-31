using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Server : MonoBehaviour {
	private string IP = "127.0.0.1";
	private int Port = 25001;
	private long start;
	private long delay;
	public RobotArm robot;

	// Use this for initialization
	void Start () {
		IP = Network.player.ipAddress;
		transform.Find ("IP Address").GetComponent<Text> ().text = "IP Address: "+IP;
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.connections.Length > 0)
			transform.Find ("Connection").gameObject.SetActive (true);
		else
			transform.Find ("Connection").gameObject.SetActive (false);
	}

	void Create() {
		if (Network.peerType != NetworkPeerType.Disconnected)
			return;
		Port = int.Parse (transform.Find ("PortBox").Find ("Text").GetComponent<Text> ().text);
		transform.Find ("PortBox").gameObject.SetActive (false);
		transform.Find ("Port").GetComponent<Text> ().text = "Port: " + Port;
		transform.Find ("Port").gameObject.SetActive (true);
		Destroy (transform.Find ("Create").gameObject);
		Network.InitializeServer (10, Port);
	}
	IEnumerator HoldUp(int timedelay, Action func) {
		yield return new WaitForSeconds (timedelay / 1000f);
		func ();
	}

	[RPC]
	void RotateArm1L(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.RotateArm1L));
	}
	[RPC]
	void RotateArm1R(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.RotateArm1R));
	}
	[RPC]
	void RotateArm2L(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.RotateArm2L));
	}
	[RPC]
	void RotateArm2R(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.RotateArm2R));
	}
	[RPC]
	void RotatePointerL(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.RotatePointerL));
	}
	[RPC]
	void RotatePointerR(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.RotatePointerR));
	}
	[RPC]
	void StopRotate(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.StopRotate));
	}
	[RPC]
	void MoveUp(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.MoveUp));
	}
	[RPC]
	void MoveDown(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.MoveDown));
	}
	[RPC]
	void MoveLeft(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.MoveLeft));
	}
	[RPC]
	void MoveRight(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.MoveRight));
	}
	[RPC]
	void StopMove(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.StopMove));
	}
	[RPC]
	void Paint(byte[] data) {
		int delay = BitConverter.ToInt32 (data, 0);
		StartCoroutine(HoldUp (delay, robot.Paint));
	}

	void OnPlayerConnected(NetworkPlayer player) {
		GetComponent<NetworkView>().RPC("SetNetworkPlayer", RPCMode.Others, player);
	}

	[RPC]
	void SendMessageToClient(string func, string msg) {
		GetComponent<NetworkView>().RPC(func, RPCMode.Others, msg);
		print ("Sent message: "+msg);
	}
	[RPC]
	void PrintMessageFromClient(string msg) {
		print ("Client: "+msg);
	}
	
	// fix RPC errors
	[RPC]
	void SetNetworkPlayer(NetworkPlayer player) { }
	[RPC]
	void SendMessageToServer(string func, string msg) { }
}
