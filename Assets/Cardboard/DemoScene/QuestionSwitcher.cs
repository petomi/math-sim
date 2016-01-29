using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class QuestionSwitcher : MonoBehaviour 
{

	public int playerAnswer = 0;
	public int answer;
	public int score;
	public bool answerIsCorrect = false;
	public int questionNumber;
	public int maxQuestionNumber = 10;
	public float maxTimeLimit = 30.0f;
	public bool readyForReset = false;
	public bool runTimer = true;
	public float startVolume = 0.1f;
	public float audioIncrements = 0.1f;
	private int questionNumAudio = 0;
	public int audioFadeIn = 1;

	public GameObject[] QuestionList;
	public GameObject[] OptionAList;
	public GameObject[] OptionBList;
	public GameObject[] OptionCList;
	public int[] AnswerList;

	//make GUI text for score
	public Text scoreText;
	public Text timerText;

	// Use this for initialization
	void Start () 
	{

		//start off questions at 0
		questionNumber = 0;

		//start audio quiet
		AudioListener.volume = startVolume;

		//make arrays based on all game objects
		QuestionList = GameObject.FindGameObjectsWithTag("Question"); //create arrays based on game object tags in editor and sort based on name
		Array.Sort(QuestionList, AlphaSort);

		OptionAList = GameObject.FindGameObjectsWithTag("OptionA");
		Array.Sort (OptionAList, AlphaSort);

		OptionBList = GameObject.FindGameObjectsWithTag("OptionB");
		Array.Sort (OptionBList, AlphaSort);

		OptionCList = GameObject.FindGameObjectsWithTag("OptionC");
		Array.Sort (OptionCList, AlphaSort);

		//test # questions
		Debug.Log("There are " + (QuestionList.Length) + " questions.");

//		//debugging arrays
//		for(int i=0; i<QuestionList.Length; i++){
//			Debug.Log ("Question " + (i+1) + " is called " + QuestionList[i].name); //check array works
		
//		}
//		for(int i=0; i<OptionAList.Length; i++){
//		Debug.Log ("Option A " + (i+1) + " is called " + OptionAList[i].name); //checks array works

//		}
//		for(int i=0; i<OptionBList.Length; i++){
//		//	Debug.Log ("Option B " + (i+1) + " is called " + OptionBList[i].name); //checks array works
//
//		}
//		for(int i=0; i<OptionCList.Length; i++){
//		//	Debug.Log ("Option C " + (i+1) + " is called " + OptionCList[i].name); //checks array works
//
//		}
//		for (int i=0; i<AnswerList.Length; i++){
//		//	Debug.Log ("Answer " + i + " is " + AnswerList[i]); //answer array test
//
//		}


	}

	// Update is called once per frame
	void Update () 
	{


		GameObject selection1 = GameObject.Find ("Selection 1");
		GameObject selection2 = GameObject.Find ("Selection 2");
		GameObject selection3 = GameObject.Find ("Selection 3");


		SelectionScript selectionScript = selection1.GetComponent<SelectionScript> ();
		SelectionScript2 selectionScript2 = selection2.GetComponent<SelectionScript2> ();
		SelectionScript3 selectionScript3 = selection3.GetComponent<SelectionScript3> ();


		if (selectionScript.triggered && questionNumber <= (QuestionList.Length -1)) 
		{
				//check answer using first option
				playerAnswer = 1;
				answerCheck (questionNumber); //passing questionNumber through to answer check
				selectionScript.triggered = false;
				resetQuestion();
				readyForReset = true;
		}

			
		if (selectionScript2.triggered2 && questionNumber <= (QuestionList.Length -1)) 
		{
				//check answer using second option
				playerAnswer = 2;
				answerCheck (questionNumber);
				selectionScript2.triggered2 = false;
				resetQuestion();
				readyForReset = true;
		}

			
		if (selectionScript3.triggered3 && questionNumber <= (QuestionList.Length -1)) 
		{
				//check answer using third option
				playerAnswer = 3;
				answerCheck (questionNumber); 
				selectionScript3.triggered3 = false;
				resetQuestion();
				readyForReset = true;
		}

		//if question number goes past maximum, end game. OR if timer runs out, end game.
		if (Time.timeSinceLevelLoad > maxTimeLimit && questionNumber < QuestionList.Length-1) 
		{				
			endGame ();
		}

		//HAVE NEXT QUESTION COME ON CARDBOARD BUTTON CLICK/SPACE BAR
		if((Input.GetKeyDown (KeyCode.Space) || Cardboard.SDK.CardboardTriggered) && readyForReset)
		{
			setUpNewQuestion();
			readyForReset = false;
			questionNumAudio++;
			if (questionNumAudio >= audioFadeIn)
			{
				AudioListener.volume = AudioListener.volume + audioIncrements;
			}
		}

		//update timer GUI
		if (runTimer)
		{
			timerText.text = (("Time Left: ") +(maxTimeLimit - Time.timeSinceLevelLoad).ToString());
		}
	}
	



	void answerCheck(int questionNumber)
	{

		//use question number to find proper answer in array and compare them
		answer = AnswerList[questionNumber];

		if (playerAnswer!= 0)
		{

			if (playerAnswer == answer)
			{
				answerIsCorrect = true;
				//increment score if answer was correct
				score++;
				updateScore();
			}

			if (playerAnswer != answer)
			{
				answerIsCorrect = false;
			}
			//test script is working as planned. SUCCESS.
			Debug.Log ("Player answer is " + answerIsCorrect); 
			playerAnswer = 0;
			answerIsCorrect = false;
		}
	}

	void updateScore()
	{
		scoreText.text = "Score: " + score;
	}



	void resetQuestion()
	{

		GameObject selection1 = GameObject.Find ("Selection 1");
		GameObject selection2 = GameObject.Find ("Selection 2");
		GameObject selection3 = GameObject.Find ("Selection 3");
		GameObject resetText = GameObject.Find ("ResetText");

		SelectionScript selectionScript = selection1.GetComponent<SelectionScript> ();
		SelectionScript2 selectionScript2 = selection2.GetComponent<SelectionScript2> ();
		SelectionScript3 selectionScript3 = selection3.GetComponent<SelectionScript3> ();

		//disable selection so doesn't loop unless new selection made
		selectionScript.triggered = false; 
		selectionScript.enabled = false;
		selectionScript2.triggered2 = false;
		selectionScript2.enabled = false;
		selectionScript3.triggered3 = false;
		selectionScript3.enabled = false;

		
		//disable previous questions and options 
		QuestionList [questionNumber].GetComponent<MeshRenderer> ().enabled = false;
		OptionAList [questionNumber].GetComponent<MeshRenderer> ().enabled = false; 
		OptionBList [questionNumber].GetComponent<MeshRenderer> ().enabled = false;
		OptionCList [questionNumber].GetComponent<MeshRenderer> ().enabled = false;

		//enable mid text instructions
		resetText.GetComponent<MeshRenderer>().enabled = true;
		
		Debug.Log ("Player score is " + score);
		//signal end of test case
		Debug.Log ("test case completed"); 
	}

	void setUpNewQuestion()
	{
		GameObject selection1 = GameObject.Find ("Selection 1");
		GameObject selection2 = GameObject.Find ("Selection 2");
		GameObject selection3 = GameObject.Find ("Selection 3");
		GameObject resetText = GameObject.Find ("ResetText");

		SelectionScript selectionScript = selection1.GetComponent<SelectionScript> ();
		SelectionScript2 selectionScript2 = selection2.GetComponent<SelectionScript2> ();
		SelectionScript3 selectionScript3 = selection3.GetComponent<SelectionScript3> ();

		//take down mid text instructions
		resetText.GetComponent<MeshRenderer>().enabled = false;

		//enable next set of questions and options
		int nextQuestion = questionNumber + 1;
		QuestionList [nextQuestion].GetComponent<MeshRenderer> ().enabled = true;
		OptionAList [nextQuestion].GetComponent<MeshRenderer> ().enabled = true; 
		OptionBList [nextQuestion].GetComponent<MeshRenderer> ().enabled = true;
		OptionCList [nextQuestion].GetComponent<MeshRenderer> ().enabled = true;

		//update array index
		questionNumber++; 
		
		//re-enable selectionscripts
		selectionScript.enabled = true;
		selectionScript2.enabled = true;
		selectionScript3.enabled = true;
	}

	void endGame()
	{

		GameObject selection1 = GameObject.Find ("Selection 1");
		GameObject selection2 = GameObject.Find ("Selection 2");
		GameObject selection3 = GameObject.Find ("Selection 3");
		//GameObject endText = GameObject.Find ("endText");
		
		SelectionScript selectionScript = selection1.GetComponent<SelectionScript> ();
		SelectionScript2 selectionScript2 = selection2.GetComponent<SelectionScript2> ();
		SelectionScript3 selectionScript3 = selection3.GetComponent<SelectionScript3> ();
		
		//stop timer
		runTimer = false;
		timerText.text = ("Time Left: 0");

		//disable selection so doesn't loop unless new selection made
		selectionScript.triggered = false; 
		selectionScript.enabled = false;
		selectionScript2.triggered2 = false;
		selectionScript2.enabled = false;
		selectionScript3.triggered3 = false;
		selectionScript3.enabled = false;
		
		
		//disable previous questions and options 
		QuestionList [questionNumber].GetComponent<MeshRenderer> ().enabled = false;
		OptionAList [questionNumber].GetComponent<MeshRenderer> ().enabled = false; 
		OptionBList [questionNumber].GetComponent<MeshRenderer> ().enabled = false;
		OptionCList [questionNumber].GetComponent<MeshRenderer> ().enabled = false;
		
		//MAKE THIS DISPLAY SCORE /////LEGACY: JUST USE EXTRA SLIDE AT THE END WITH SAME NOMENCLATURE AS QUESTIONS
		//enable mid text instructions
		//endText.GetComponent<Text>().enabled = true;
		
		Debug.Log ("Player score is " + score);
		Debug.Log ("test case completed"); //signal end of test case


	}


//	IEnumerator MyDelay(float seconds)
//	{
//		yield return new WaitForSeconds(seconds);
//	}



	int AlphaSort(GameObject x, GameObject y)
	{
		return x.name.CompareTo(y.name);
	}


	}

