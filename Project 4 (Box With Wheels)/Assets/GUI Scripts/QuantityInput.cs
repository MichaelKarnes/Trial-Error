using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuantityInput : MonoBehaviour {

	bool buttonPressed = false;
	//public string stringToEdit = "Hello World";
	void OnGUI() {


		InputField numberOfBugsField = (InputField)GameObject.Find ("NumberofBugs").GetComponent (typeof(InputField));
		string value = numberOfBugsField.text;
		int numberOfBugs;
		try
		{
			numberOfBugs = int.Parse(value);
		}
		catch(System.FormatException e)
		{
			return;
		}

		Slider bugNumber = (Slider)GameObject.Find ("bugNumber").GetComponent (typeof(Slider));
		bugNumber.maxValue = numberOfBugs;

		//float sliderValue = bugNumber.value;
		//bugNumber.onValueChanged.AddListener (delegate{updateLabel(sliderValue);});

		Text bugNumberLabel = (Text)GameObject.Find ("BugNumberText").GetComponent (typeof(Text));
		bugNumberLabel.text = bugNumber.value.ToString();

		//stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25);
		//if (Event.current.Equals (Event.KeyboardEvent ("[enter]"))) {


			// Add validation
			//NumberOfBugs.characterValidation = InputField.CharacterValidation.Alphanumeric;
		//	Debug.Log("Validation Complete");
		//}
		//Debug.Log (numberOfBugs);
		//Debug.Log ("Nothing Happened");

		//---------------------------------------------------------------PLACE BUG
		/*if (buttonPressed == true) {
			placeBug();
			buttonPressed = false;
		}*/

		//if(GUI.Button(new Rect(110, 155, 300, 100),"Submit")){  
		//	placeBug();
		//}			
				//nameInputField.onSubmit.AddListener((value) => SubmitName(value));
	}

	public void updateLabel(float sliderValue){
		//myString = GUI.TextField (Rect (155, 25, 100, 30), textVal);
		Text bugNumberLabel = (Text)GameObject.Find ("BugNumberText").GetComponent (typeof(Text));
	}

	public void boolToogle()
	{
		buttonPressed = true;
	}
	
	public void getBug()
	{
		InputField bugToFind = (InputField)GameObject.Find ("findBug").GetComponent (typeof(InputField));
		Slider bugNumber = (Slider)GameObject.Find ("bugNumber").GetComponent (typeof(Slider));
		string value = bugToFind.text;
		try
		{
			int numberOfBugs = int.Parse(value);
			bugNumber.value = numberOfBugs;
		}
		catch(System.FormatException e)
		{

			return;
		}
	}

	public void placeBug()
	{
		InputField bugXPos = (InputField)GameObject.Find ("xPosition").GetComponent (typeof(InputField));
		InputField bugYPos = (InputField)GameObject.Find ("yPosition").GetComponent (typeof(InputField));

		string xValue = bugXPos.text;
		string yValue = bugYPos.text;
		float xFValue = float.Parse (xValue);
		float yFValue = float.Parse (yValue);
		float rValue = 0;

		//Rect testBug = new Rect (0, 0, 10, 10);
		//Bug testBug = gameObject.AddComponent<Bug>();

//		testBug.x = xFValue;
//		testBug.y = yFValue;
		//GameObject bug;
		//Bug newBug = gameObject.AddComponent<Bug>();
		//Bug newBug = new Bug(xFValue, yFValue, rValue);
		//newBug = new Bug(xFValue, yFValue, rValue);
		//bug = gameObject.AddComponent<Bug>();

		//Bug firstBug = (Bug)GameObject.Find ("firstBug").GetComponent (typeof(Bug));
		
		//Instantiate(firstBug, new Vector3(xFValue, yFValue, 0), Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
