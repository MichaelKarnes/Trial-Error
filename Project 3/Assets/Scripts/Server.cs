using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Server : MonoBehaviour {
	public string IP = "127.0.0.1";
	public int Port = 25001;

	// Use this for initialization
	void Start () {
		Network.InitializeServer (10, Port);
	}
	
	// Update is called once per frame
	void OnGUI () {
		transform.Find ("IP Address").GetComponent<Text> ().text = "IP Address: "+IP;
		transform.Find ("Port").GetComponent<Text> ().text = "Port: "+Port;
		if(Network.connections.Length > 0)
			transform.Find ("Connection").gameObject.SetActive (true);
		else
			transform.Find ("Connection").gameObject.SetActive (false);
	}
}
