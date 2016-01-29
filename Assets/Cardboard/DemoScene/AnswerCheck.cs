using UnityEngine;
using System.Collections;

public class AnswerCheck : MonoBehaviour 
{

	public int playerAnswer = 0;
	public int correctAnswer;
	public int score;
	public bool answerIsCorrect = false;
	public bool answerChecked = false;

	void Start()
	{

	}

	void Update () 
	{
		GameObject selection1 = GameObject.Find ("Selection 1");
		GameObject selection2 = GameObject.Find ("Selection 2");
		GameObject selection3 = GameObject.Find ("Selection 3");

		SelectionScript selectionScript = selection1.GetComponent<SelectionScript>();
		SelectionScript2 selectionScript2 = selection2.GetComponent<SelectionScript2>();
		SelectionScript3 selectionScript3 = selection3.GetComponent<SelectionScript3>();

		if ((selectionScript.triggered))
		{
			playerAnswer = 1;
			answerCheck();
		}
		if (selectionScript2.triggered2)
		{
			playerAnswer = 2;
			answerCheck();
		}
		if (selectionScript3.triggered3)
		{
			playerAnswer = 3;
			answerCheck();
		}

		Debug.Log (answerIsCorrect); //print whether answer is correct or not

	}
	
	void answerCheck()
	{

		if (playerAnswer!= 0)
		{
			if (playerAnswer == correctAnswer)
			{
				answerIsCorrect = true;
				//increment score
				score++;
				Debug.Log(score);
				//update bool for question switcher to read and stop script
				answerChecked = true;


			}
			if (playerAnswer != correctAnswer)
			{
				answerIsCorrect = false;
				//update bool for question switcher to read and stop script
				answerChecked = true;
			}
		}
	}


}
