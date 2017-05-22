using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	[SerializeField]private Text timerText;
	[SerializeField]private Text statusText;
	[SerializeField]private Text packCountText;
	private readonly float[] specifiedDraftTime = new float[15]{40f, 40f, 35f, 30f, 25f, 25f, 20f, 20f, 15f, 10f, 10f, 5f, 5f, 5f, 3f};
	private readonly float[] specifiedPackIntervalTime = new float[3]{60f, 90f, 120f};
	private const float exchangeIntervalTime = 3.0f;
	private const float openPackTime = 8.0f;
	private const float constructDeckTime = 180f;
	private float time = 0.0f;
	private int countsOfPack = 0;
	private int countsOfPick = 0;
	[SerializeField]private AudioSource audioSource;
	[SerializeField]private AudioClip audioClip;

	public enum DraftStatus : int {
		PICK = 0,
		EXCHANGE = 1,
		CONFIRM_PICK = 2,
		OPEN_PACK = 3,
		CONSTRUCT = 4,
		FINISH = 5,
	}

	public DraftStatus draftStatus = DraftStatus.PICK;

	public void Initialize() {
		draftStatus = DraftStatus.PICK;
		time = specifiedDraftTime[countsOfPick++];
		packCountText.text = "1PACK";
	}

	public void TimerUpdate(){
		statusText.text = draftStatus.ToString();
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
				time = exchangeIntervalTime;
			} else {
				draftStatus = DraftStatus.CONFIRM_PICK;
				countsOfPick = 0;
			}
		} else if(draftStatus == DraftStatus.EXCHANGE){
			draftStatus = DraftStatus.PICK;
			time = specifiedDraftTime[countsOfPick++];
		} else if(draftStatus == DraftStatus.CONFIRM_PICK){
			if(countsOfPack != 2){
				packCountText.text = string.Format("{0}PACK", countsOfPack + 1);
				draftStatus = DraftStatus.OPEN_PACK;
				time = openPackTime;
			} else {
				draftStatus = DraftStatus.CONSTRUCT;
				time = constructDeckTime;
			}
		} else if(draftStatus == DraftStatus.OPEN_PACK){
			draftStatus = DraftStatus.PICK;
			time = specifiedDraftTime[countsOfPack++];
		} else if(draftStatus ==  DraftStatus.CONSTRUCT){
			draftStatus = DraftStatus.FINISH;
			statusText.text = "FINISH";
		}
		audioSource.clip = audioClip;
		audioSource.Play();
	}
}
