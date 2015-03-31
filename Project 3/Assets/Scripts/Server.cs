using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Server : MonoBehaviour {
	private string IP = "127.0.0.1";
	private int Port = 25001;
	private TimeSpan delay;
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

	[RPC]
	void UpdateRobot(string message) {
		print ("Received message: " + message);
		robot.BroadcastMessage (message);
	}

	void OnPlayerConnected(NetworkPlayer player) {
		print ("Player Connected");
		print ("Sending NetworkPlayer...");
		GetComponent<NetworkView>().RPC("SetNetworkPlayer", RPCMode.Others, player);
	}

	[RPC]
	void CalculateDelay(string datetime) {
		DateTime t2 = DateTime.UtcNow;
		DateTime t1 = DateTime.Parse (datetime);
		delay = t2 - t1;
		SendMessageToClient("SetDelay", delay.ToString ());
		print (delay);
	}
	
	[RPC]
	void ReceiveMessageFromClient(string msg) {
		print ("Received message: "+msg);
	}

	[RPC]
	void SendMessageToClient(string func, string msg) {
		GetComponent<NetworkView>().RPC(func, RPCMode.Others, msg);
		print ("Sent message: "+msg);
	}
	
	// fix RPC errors
	[RPC]
	void SetDelay(string delay) { }
	[RPC]
	void SetNetworkPlayer(NetworkPlayer player) { }
	[RPC]
	void SendMessageToServer(string func, string msg) { }
	[RPC]
	void ReceiveMessageFromServer(string msg) { }
}
