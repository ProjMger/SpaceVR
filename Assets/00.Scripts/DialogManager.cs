using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DialogManager : MonoBehaviour
{

    public enum Dialogs { 
        DiaStart, 
        DiaWarp,
        DiaSightseeing,
        DiaEarthMoonSize,
        DiaEarthRot,
        DiaEarthMarker
    }

    Dialogs logs;

    public SpawnEffect spawnEffect;
    public Text shownText;
    public GameObject dialogPanel;
    public GameObject planetButtons;
    public PlayableDirector director;
    public GameObject earth;
    public GameObject sun;
    public GameObject moon;

    bool isDialogDone;
    string[] dialog;


    void Start()
    {
        director.played += Director_Played;
        logs = Dialogs.DiaStart;
        StartCoroutine(DisplayDialog());
    }

    void Update()
    {
        if(isDialogDone)
        {
            switch (logs)
            {
                case Dialogs.DiaStart:
                    isDialogDone = false;
                    logs = Dialogs.DiaWarp;
                    StartCoroutine(DisplayDialog());
                    break;
                case Dialogs.DiaWarp:
                    isDialogDone = false;
                    logs = Dialogs.DiaSightseeing;
                    StartCoroutine(sightseeingMode());
                    break;
                case Dialogs.DiaSightseeing:
                    isDialogDone = false;
                    planetButtons.SetActive(true);
                    dialogPanel.SetActive(false);
                    break;
                case Dialogs.DiaEarthMoonSize:
                    isDialogDone = false;
                    dialogPanel.SetActive(false);
                    director.Play();
                    break;
                case Dialogs.DiaEarthRot:
                    isDialogDone = false;
                    dialogPanel.SetActive(false);
                    director.Play();
                    break;
                case Dialogs.DiaEarthMarker:
                    isDialogDone = false;
                    dialogPanel.SetActive(false);
                    director.Play();
                    break;
            }
        }
    }

    public IEnumerator DisplayDialog()
    {
        shownText.text = "";
        string currentText = "";
        yield return new WaitForSeconds(1f);
        switch (logs)
        {

            case Dialogs.DiaStart:
                dialog = new string[] {
                    "안녕, 나는 너의 우주여행을 도와줄 도롱이라고해!",
                    "곧 우주로 출발 할거야, 준비 됐어?",
                    "10... 9... 8... 7..."
                };
                break;
            case Dialogs.DiaWarp:
                dialog = new string[] {
                    "우주에 도착했어!",
                    "우주선의 내부때문에 우주가 잘 안보이네...",
                    "잠깐만 기다려줘, 우주선의 '관찰모드'를 금방 켜줄게"
                };
                break;
            case Dialogs.DiaSightseeing:
                dialog = new string[]
                {
                    "관찰모드 후 대사(DiaSightseeing)"
                };
                break;
            case Dialogs.DiaEarthMoonSize:
                dialog = new string[]
                {
                    "지구선택 후 대사(DiaEarthMoonSize)"
                };
                break;
            case Dialogs.DiaEarthRot:
                dialog = new string[]
                {
                    "지구자전 대사 (DiaEarthRot)"
                };
                break;
            case Dialogs.DiaEarthMarker:
                dialog = new string[]
                {
                    "지구마커 대사 (DiaEarthRot)"
                };
                break;
        }                        

        int j = 0;
        for (int i = 0; i <= dialog[j].Length; i++)
        {
            currentText = dialog[j].Substring(0, i);
            shownText.text = currentText;
            if (i == dialog[j].Length)
            {

                yield return new WaitForSeconds(0.5f);
                if (dialog.Length > j+1)
                {
                    j += 1;
                    i = 0;
                }
                else
                {
                    isDialogDone = true;
                    StopAllCoroutines();
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator sightseeingMode()
    {
        spawnEffect.enabled = true;
        dialogPanel.SetActive(false);
        yield return new WaitForSeconds(2f);
        dialogPanel.SetActive(true);
        spawnEffect.enabled = false;
        StartCoroutine(DisplayDialog());
    }
    void Director_Played(PlayableDirector obj)
    {
        planetButtons.SetActive(false);
    }
    public void EarthMoonSignal()
    {
        dialogPanel.SetActive(true);
        director.Pause();
        logs = Dialogs.DiaEarthMoonSize;
        StartCoroutine(DisplayDialog());
    }
    public void EarthRotSignal()
    {
        dialogPanel.transform.LookAt(earth.transform);
        dialogPanel.SetActive(true);
        director.Pause();
        logs = Dialogs.DiaEarthRot;
        StartCoroutine(DisplayDialog());
    }
    public void EarthMarkerSignal()
    {
        dialogPanel.transform.LookAt(earth.transform);
        dialogPanel.SetActive(true);
        director.Pause();
        logs = Dialogs.DiaEarthMarker;
        StartCoroutine(DisplayDialog());
    }
}