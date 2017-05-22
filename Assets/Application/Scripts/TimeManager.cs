using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	[SerializeField]private Text timerText;
	[SerializeField]private Text StatusText;
	private readonly float[] specifiedDraftTime = new float[15]{40f, 40f, 35f, 30f, 25f, 25f, 20f, 20f, 15f, 10f, 10f, 5f, 5f, 5f, 1f};
	private readonly float[] specifiedPackIntervalTime = new float[3]{60f, 90f, 120f};
	private const float exchangeInterval = 3.0f;
	private float time = 0.0f;
	private int countsOfPack = 0;
	private int countsOfPick = 0;

	public enum DraftStatus : int {
		PICK = 0,
		EXCHANGE = 1,
		CONFIRM_PICK = 2,
		FINISH = 3,
	}

	public DraftStatus draftStatus = DraftStatus.PICK;

	private void Awake(){
		draftStatus = DraftStatus.PICK;
		time = specifiedDraftTime[countsOfPick++];
		countsOfPick = 14;
		countsOfPack = 2;
		SetNextDraft();
	}

	public void TimerUpdate(){
		StatusText.text = draftStatus.ToString();
		time = time - Time.deltaTime > 0f ? time - Time.deltaTime : 0f;
		timerText.text = string.Format("{0:f0}", time);
		if(Mathf.Abs(time) < Mathf.Epsilon){
			SetNextDraft();
		}
	}

	private void SetNextDraft(){
		if(draftStatus == DraftStatus.PICK){
			if(countsOfPick != 15){
				draftStatus = DraftStatus.EXCHANGE;
				time = exchangeInterval;
			} else {
				draftStatus = DraftStatus.CONFIRM_PICK;
				countsOfPick = 0;
				time = specifiedPackIntervalTime[countsOfPack++];
			}
		} else if(draftStatus == DraftStatus.EXCHANGE){
			draftStatus = DraftStatus.PICK;
			time = specifiedDraftTime[countsOfPick++];
		} else if(draftStatus == DraftStatus.CONFIRM_PICK){
			if(countsOfPack != 3){
				draftStatus = DraftStatus.PICK;
				time = specifiedDraftTime[countsOfPack++];
			} else {
				draftStatus = DraftStatus.FINISH;
			}
		}
	}
}
