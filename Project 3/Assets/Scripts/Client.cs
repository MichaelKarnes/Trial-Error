using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour {
	private string IP;
	private int Port;
	private NetworkPlayer player;
	private TimeSpan delay;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.peerType != NetworkPeerType.Disconnected) {
			if(Network.connections.Length > 0) {
				transform.Find ("Connection").gameObject.SetActive (true);
				transform.Find ("IP Address Box").gameObject.SetActive (false);
				transform.Find ("IP Address").gameObject.SetActive (true);
				transform.Find ("Port Box").gameObject.SetActive (false);
				transform.Find ("Port").gameObject.SetActive (true);
				transform.Find ("Connect").gameObject.SetActive (false);
			}
			else {
				transform.Find ("Connection").gameObject.SetActive (false);
				transform.Find ("IP Address Box").gameObject.SetActive (true);
				transform.Find ("IP Address").gameObject.SetActive (false);
				transform.Find ("Port Box").gameObject.SetActive (true);
				transform.Find ("Port").gameObject.SetActive (false);
				transform.Find ("Connect").gameObject.SetActive (true);
			}
		}
	}
	
	void Connect() {
		if (Network.peerType != NetworkPeerType.Disconnected)
			return;
		IP = transform.Find ("IP Address Box").Find ("Text").GetComponent<Text> ().text;
		Port = int.Parse (transform.Find ("Port Box").Find ("Text").GetComponent<Text> ().text);
		transform.Find ("IP Address").GetComponent<Text> ().text = "IP Address: "+IP;
		transform.Find ("Port").GetComponent<Text> ().text = "Port: "+Port;
		Network.Connect (IP, Port);
	}

	void OnConnectedToServer() {
		SendMessageToServer("CalculateDelay", DateTime.UtcNow.ToString());
	}

	[RPC]
	void SetNetworkPlayer(NetworkPlayer player) {
		this.player = player;
	}

	[RPC]
	void SetDelay(string delaystr) {
		delay = TimeSpan.Parse (delaystr);
	}

	[RPC]
	public void SendMessageToServer(string func, string msg){
		GetComponent<NetworkView>().RPC(func, RPCMode.Server, msg);
		print ("Sent message: " + msg);
	}
	
	[RPC]
	void ReceiveMessageFromServer(string msg) {
		print ("Received message: "+msg);
		//messagelog += someInfo + "\n";
	}
	
	// fix RPC errors
	[RPC]
	void UpdateRobot(string message) { }
	[RPC]
	void CalculateDelay(string delay) { }
	[RPC]
	void SendMessageToClient(string func, string msg) { }
	[RPC]
	void ReceiveMessageFromClient(string msg) { }
}
