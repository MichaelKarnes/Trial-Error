using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Client : MonoBehaviour {
	private string IP;
	private int Port;
	private NetworkPlayer player;
	private int delay;

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

	void ToggleDelay() {
		delay = transform.Find ("Toggle").GetComponent<Toggle> ().isOn ? 2000 : 0;
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
		//SendMessageToServer("CalculateDelay", DateTime.UtcNow.ToString ());
	}

	[RPC]
	void SetNetworkPlayer(NetworkPlayer player) {
		this.player = player;
	}
	
	[RPC]
	public void SendMessageToServer(string func, string msg){
		GetComponent<NetworkView>().RPC(func, RPCMode.Server, msg);
		print ("Sent message: " + msg);
	}

	// fix RPC errors
	[RPC]
	public void PrintMessageFromClient(string msg) { }
	[RPC]
	public void RotateArm1L(byte[] datetime) {
		GetComponent<NetworkView>().RPC("RotateArm1L", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void RotateArm1R(byte[] datetime) {
		GetComponent<NetworkView>().RPC("RotateArm1R", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void RotateArm2L(byte[] datetime) {
		GetComponent<NetworkView>().RPC("RotateArm2L", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void RotateArm2R(byte[] datetime) {
		GetComponent<NetworkView>().RPC("RotateArm2R", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void RotatePointerL(byte[] datetime) {
		GetComponent<NetworkView>().RPC("RotatePointerL", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void RotatePointerR(byte[] datetime) {
		GetComponent<NetworkView>().RPC("RotatePointerR", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void StopRotate(byte[] datetime) {
			GetComponent<NetworkView>().RPC("StopRotate", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void MoveUp(byte[] datetime) {
		GetComponent<NetworkView>().RPC("MoveUp", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void MoveDown(byte[] datetime) {
		GetComponent<NetworkView>().RPC("MoveDown", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void MoveLeft(byte[] datetime) {
		GetComponent<NetworkView>().RPC("MoveLeft", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void MoveRight(byte[] datetime) {
		GetComponent<NetworkView>().RPC("MoveRight", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void StopMove(byte[] datetime) {
		GetComponent<NetworkView>().RPC("StopMove", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void Paint(byte[] datetime) {
		GetComponent<NetworkView>().RPC("Paint", RPCMode.Server, BitConverter.GetBytes(delay));
	}
	[RPC]
	public void SendMessageToClient(string func, string msg) { }
}
