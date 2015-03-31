using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

public class Server : MonoBehaviour {
	private string IP = "127.0.0.1";
	private int Port = 25001;

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
}
