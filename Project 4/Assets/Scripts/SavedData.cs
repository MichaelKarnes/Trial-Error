using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SavedData : MonoBehaviour {
	public List<Buggy> bugs;
	public List<Light> lights;
	public Transform prefab;

	private float Round(float f) {
		return Mathf.Round (f * 100f) / 100f;
	}

	public void FindBugData() {
		Slider bugSelector = (Slider)GameObject.Find ("BugSelector").GetComponent (typeof(Slider));
		int num = (int)bugSelector.value;
		Buggy bug = bugs [num];

		Text bugNum = (Text)GameObject.Find ("BugNumText").GetComponent (typeof(Text));
		InputField k11 = (InputField)GameObject.Find ("BugK11").GetComponent (typeof(InputField));
		InputField k12 = (InputField)GameObject.Find ("BugK12").GetComponent (typeof(InputField));
		InputField k21 = (InputField)GameObject.Find ("BugK21").GetComponent (typeof(InputField));
		InputField k22 = (InputField)GameObject.Find ("BugK22").GetComponent (typeof(InputField));
		InputField bugX = (InputField)GameObject.Find ("BugX").GetComponent (typeof(InputField));
		InputField bugY = (InputField)GameObject.Find ("BugY").GetComponent (typeof(InputField));
		InputField bugR = (InputField)GameObject.Find ("BugR").GetComponent (typeof(InputField));
		InputField bugSize = (InputField)GameObject.Find ("BugSize").GetComponent (typeof(InputField));

		bugNum.text = (num+1).ToString ();

		k11.text = Round(bug.K [0, 0]).ToString ();
		k12.text = Round(bug.K [1, 0]).ToString ();
		k21.text = Round(bug.K [0, 1]).ToString ();
		k22.text = Round(bug.K [1, 1]).ToString ();

		bugX.text = Mathf.Round(bug.transform.position.x).ToString ();
		bugY.text = Mathf.Round(bug.transform.position.z).ToString ();
		bugR.text = Mathf.Round(bug.transform.rotation.eulerAngles.y).ToString ();
		bugSize.text = Mathf.Round(bug.transform.localScale.x).ToString ();
	}

	public void UpdateNumberOfBugs()
	{
		InputField numbugs = (InputField)GameObject.Find ("NumBugs").GetComponent (typeof(InputField));
		int num;
		if (!int.TryParse (numbugs.text, out num))
			return;
		while (bugs.Count > num) {
			GameObject go = GameObject.Find ("Buggy"+bugs.Count);
			Buggy b = bugs[bugs.Count-1];
			bugs.RemoveAt (bugs.Count - 1);
			Destroy (go);
		}
		while (bugs.Count < num) {
			GameObject go = (Instantiate (prefab, prefab.transform.position, prefab.transform.rotation) as Transform).gameObject;
			go.SetActive (true);
			Buggy b = go.GetComponent<Buggy>();
			b.Init ();
			bugs.Add (b);
			go.name = "Buggy"+bugs.Count;
		}

		Slider bugSelector = (Slider)GameObject.Find ("BugSelector").GetComponent (typeof(Slider));
		bugSelector.maxValue = bugs.Count - 1;
		bugSelector.value = bugs.Count - 1;
		FindBugData ();
	}

	public void UpdateBugData()
	{
		Slider bugSelector = (Slider)GameObject.Find ("BugSelector").GetComponent (typeof(Slider));
		int num = (int)bugSelector.value;
		Buggy bug = bugs [num];
		
		InputField k11 = (InputField)GameObject.Find ("BugK11").GetComponent (typeof(InputField));
		InputField k12 = (InputField)GameObject.Find ("BugK12").GetComponent (typeof(InputField));
		InputField k21 = (InputField)GameObject.Find ("BugK21").GetComponent (typeof(InputField));
		InputField k22 = (InputField)GameObject.Find ("BugK22").GetComponent (typeof(InputField));
		InputField bugX = (InputField)GameObject.Find ("BugX").GetComponent (typeof(InputField));
		InputField bugY = (InputField)GameObject.Find ("BugY").GetComponent (typeof(InputField));
		InputField bugR = (InputField)GameObject.Find ("BugR").GetComponent (typeof(InputField));
		InputField bugSize = (InputField)GameObject.Find ("BugSize").GetComponent (typeof(InputField));

		float k11F;
		if (float.TryParse (k11.text, out k11F))
			bug.K [0, 0] = Round (k11F);
		float k12F;
		if (float.TryParse (k12.text, out k12F))
			bug.K [1, 0] = Round (k12F);
		float k21F;
		if (float.TryParse (k21.text, out k21F))
			bug.K [0, 1] = Round (k21F);
		float k22F;
		if (float.TryParse (k22.text, out k22F))
			bug.K [1, 1] = Round (k22F);

		float bugXF;
		if (float.TryParse (bugX.text, out bugXF))
			bug.transform.position = new Vector3 (Mathf.Round (bugXF), bug.transform.position.y, Mathf.Round(bug.transform.position.z));
		float bugYF;
		if (float.TryParse (bugY.text, out bugYF))
			bug.transform.position = new Vector3 (Mathf.Round(bug.transform.position.x), bug.transform.position.y, Mathf.Round (bugYF));

		float bugRF;
		if (float.TryParse (bugR.text, out bugRF))
			bug.transform.rotation = Quaternion.AngleAxis(Mathf.Round (bugRF), Vector3.up);

		float bugSizeF;
		if (float.TryParse (bugSize.text, out bugSizeF))
			bug.transform.localScale = new Vector3 (Mathf.Round (bugSizeF), Mathf.Round (bugSizeF), Mathf.Round (bugSizeF));
	}

	public void FindLightData() {
		Slider lightSelector = (Slider)GameObject.Find ("LightSelector").GetComponent (typeof(Slider));
		int num = (int)lightSelector.value;
		Light light = lights [num];
		
		Text lightNum = (Text)GameObject.Find ("LightNumText").GetComponent (typeof(Text));
		InputField lightX = (InputField)GameObject.Find ("LightX").GetComponent (typeof(InputField));
		InputField lightY = (InputField)GameObject.Find ("LightY").GetComponent (typeof(InputField));
		
		lightNum.text = (num+1).ToString ();
		
		lightX.text = Mathf.Round (light.transform.position.x).ToString ();
		lightY.text = Mathf.Round (light.transform.position.z).ToString ();
	}
	
	public void UpdateNumberOfLights()
	{
		InputField numlights = (InputField)GameObject.Find ("NumLights").GetComponent (typeof(InputField));
		int num;
		if (!int.TryParse (numlights.text, out num))
			return;
		while (lights.Count > num) {
			GameObject go = GameObject.Find ("Light"+lights.Count);
			Light l = lights[lights.Count-1];
			lights.RemoveAt (lights.Count - 1);
			Destroy (go);
		}
		while (lights.Count < num) {
			GameObject go = new GameObject("Light"+(lights.Count+1));
			Light l = go.AddComponent<Light>();
			l.transform.position = new Vector3(0,1,0);
			lights.Add (l);
		}
		
		Slider lightSelector = (Slider)GameObject.Find ("LightSelector").GetComponent (typeof(Slider));
		lightSelector.maxValue = lights.Count - 1;
		lightSelector.value = lights.Count - 1;
		FindLightData ();
	}
	
	public void UpdateLightData()
	{
		Slider lightSelector = (Slider)GameObject.Find ("LightSelector").GetComponent (typeof(Slider));
		int num = (int)lightSelector.value;
		Light light = lights [num];

		InputField lightX = (InputField)GameObject.Find ("LightX").GetComponent (typeof(InputField));
		InputField lightY = (InputField)GameObject.Find ("LightY").GetComponent (typeof(InputField));
		
		float lightXF;
		if (float.TryParse (lightX.text, out lightXF))
			light.transform.position = new Vector3 (Mathf.Round (lightXF), light.transform.position.y, Mathf.Round (light.transform.position.z));
		float lightYF;
		if (float.TryParse (lightY.text, out lightYF))
			light.transform.position = new Vector3 (Mathf.Round (light.transform.position.x), light.transform.position.y, Mathf.Round (lightYF));
	}

	public void Play()
	{
		foreach (Buggy bug in bugs) {
			bug.SetLights(lights);
			bug.Activate ();
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
