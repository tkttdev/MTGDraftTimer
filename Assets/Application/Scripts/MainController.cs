using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour {

	[SerializeField]private TimeManager timeManager;
	[SerializeField]private GameObject startButton;
	[SerializeField]private GameObject pauseButton;
	[SerializeField]private GameObject resumeButton;
	[SerializeField]private GameObject exitButton;

	private enum TimerStatus : int {
		WAIT = 0,
		DRAFT = 1,
		FINISH = 2,
	}

	TimerStatus timerStatus = TimerStatus.WAIT;

	public void StartButton() {
		timeManager.Initialize();
		timerStatus = TimerStatus.DRAFT;
		startButton.SetActive(false);
		pauseButton.SetActive(true);
	}

	public void PauseButton(){
		timerStatus = TimerStatus.WAIT;
		resumeButton.SetActive(true);
		exitButton.SetActive(true);
		pauseButton.SetActive(false);
	}

	public void ResumeButton() {
		timerStatus = TimerStatus.DRAFT;
		resumeButton.SetActive(false);
		exitButton.SetActive(false);
		pauseButton.SetActive(true);
	}

	public void ExitButton() {
		SceneManager.LoadScene("Title");
	}

	private void Update(){
		if(timerStatus == TimerStatus.DRAFT){
			timeManager.TimerUpdate();
			if(timeManager.draftStatus == TimeManager.DraftStatus.FINISH){
				timerStatus = TimerStatus.FINISH;
			}
		}
	}
}
