using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	[SerializeField]private TimeManager timeManager;
	[SerializeField]private GameObject startButton;
	[SerializeField]private GameObject pauseButton;

	private enum TimerStatus : int {
		WAIT = 0,
		DRAFT = 1,
		FINISH = 2,
	}

	TimerStatus timerStatus = TimerStatus.WAIT;

	public void StartButton() {
		timerStatus = TimerStatus.DRAFT;
		startButton.SetActive(false);
		pauseButton.SetActive(true);
	}

	public void PauseButton(){
		timerStatus = TimerStatus.WAIT;
		startButton.SetActive(true);
		pauseButton.SetActive(false);
	}

	private void Update(){
		if(timerStatus == TimerStatus.DRAFT){
			timeManager.TimerUpdate();
			if((int)timeManager.draftStatus == 3){
				timerStatus = TimerStatus.FINISH;
			}
		}
	}
}
