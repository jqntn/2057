using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
class LevelManager : MonoBehaviour
{
    [Header("General")]
    public GameObject pendu;
    public int totParts;
    public int curPart;
    public int curSubPart;
    public PlayerScr playerScript;
    public Animator playerAnimator;
    public Transform player;
    public Transform playerStart;
    public Transform camTarget;
    public GameObject phone;
    public GameObject subPart1;
    public GameObject subPart2;
    public Text preDialText;
    public Text dialText1;
    public Text repText1;
    public Text dialText2;
    public Text repText2;
    public UI ui;
    bool showPhone;
    [HideInInspector] public bool canInteractWithPhone;
    bool isRepShown1;
    bool isRepShown2;
    public bool canNextSub;
    public bool canExitPhone;
    public bool flag;
    bool canTrig1;
    bool canTrig2;
    bool canTrig3;
    string curPreDial;
    string curDial1;
    string curRep1;
    string curDial2;
    string curRep2;
    [Header("Videos")]
    public GameObject vi1;
    public GameObject vi2;
    public GameObject vi3;
    public GameObject vi4;
    public GameObject vi5;
    public VideoPlayer vp1;
    public VideoPlayer vp2;
    public VideoPlayer vp3;
    public VideoPlayer vp4;
    public VideoPlayer vp5;
    public bool isV1Playing;
    public bool isV2Playing;
    public bool isV3Playing;
    public bool isV4Playing;
    public bool isV5Playing;
    [Header("Triggers")]
    public Trigger trig1;
    public Trigger trig2;
    public Trigger trig3;
    [Header("Dialogs - Part #1")]
    public string Part1_PreDial;
    public string Part1_Dial1;
    public string Part1_Rep1;
    public string Part1_Dial2;
    public string Part1_Rep2;
    [Header("Dialogs - Part #2")]
    public string Part2_PreDial;
    public string Part2_Dial1;
    public string Part2_Rep1;
    public string Part2_Dial2;
    public string Part2_Rep2;
    [Header("Dialogs - Part #3")]
    public string Part3_PreDial;
    public string Part3_Dial1;
    public string Part3_Rep1;
    public string Part3_Dial2;
    public string Part3_Rep2;
    [Header("Dialogs - Part #4")]
    public string Part4_PreDial;
    public string Part4_Dial1;
    public string Part4_Rep1;
    public string Part4_Dial2;
    public string Part4_Rep2;
    [Header("Dialogs - Part #5")]
    public string Part5_PreDial;
    public string Part5_Dial1;
    public string Part5_Rep1;
    public string Part5_Dial2;
    public string Part5_Rep2;
    //
    void Awake()
    {
        StartCoroutine(InitBools());
    }
    void Update()
    {
        if (ui.isPaused) phone.SetActive(false);
        else phone.SetActive(showPhone);
        switch (curPart)
        {
            case 1:
                curPreDial = Part1_PreDial;
                curDial1 = Part1_Dial1;
                curRep1 = Part1_Rep1;
                curDial2 = Part1_Dial2;
                curRep2 = Part1_Rep2;
                break;
            case 2:
                curPreDial = Part2_PreDial;
                curDial1 = Part2_Dial1;
                curRep1 = Part2_Rep1;
                curDial2 = Part2_Dial2;
                curRep2 = Part2_Rep2;
                break;
            case 3:
                curPreDial = Part3_PreDial;
                curDial1 = Part3_Dial1;
                curRep1 = Part3_Rep1;
                curDial2 = Part3_Dial2;
                curRep2 = Part3_Rep2;
                break;
            case 4:
                curPreDial = Part4_PreDial;
                curDial1 = Part4_Dial1;
                curRep1 = Part4_Rep1;
                curDial2 = Part4_Dial2;
                curRep2 = Part4_Rep2;
                break;
            case 5:
                curPreDial = Part5_PreDial;
                curDial1 = Part5_Dial1;
                curRep1 = Part5_Rep1;
                curDial2 = Part5_Dial2;
                curRep2 = Part5_Rep2;
                break;
        }
        if (!isRepShown1)
        {
            repText1.text = "Press Space...";
            repText1.fontStyle = FontStyle.Italic;
        }
        else
        {
            repText1.text = curRep1;
            repText1.fontStyle = FontStyle.Normal;
        }
        if (!isRepShown2)
        {
            repText2.text = "Press Space...";
            repText2.fontStyle = FontStyle.Italic;
        }
        else
        {
            repText2.text = curRep2;
            repText2.fontStyle = FontStyle.Normal;
        }
        if (trig1.isTriggered && canTrig1)
        {
            showPhone = true;
            canInteractWithPhone = true;
        }
        if (!canTrig1)
        {
            showPhone = false;
            canInteractWithPhone = false;
        }
        if (trig2.isTriggered && canTrig2) curSubPart = 2;
        if (curSubPart == 1)
        {
            subPart1.SetActive(true);
            subPart2.SetActive(false);
            preDialText.text = curPreDial;
            dialText1.text = curDial1;
            if (canInteractWithPhone && Input.GetButtonDown("Interact"))
            {
                isRepShown1 = true;
                StartCoroutine(CanNextSubDelay(true));
            }
            if (canNextSub && canInteractWithPhone && Input.GetButtonDown("Interact")) curSubPart = 2;
        }
        if (curSubPart == 2)
        {
            if (canInteractWithPhone) StartCoroutine(FlagDelay(true));
            subPart1.SetActive(false);
            subPart2.SetActive(true);
            dialText2.text = curDial2;
            if (flag && canInteractWithPhone && Input.GetButtonDown("Interact"))
            {
                isRepShown2 = true;
                StartCoroutine(CanExitPhoneDelay(true));
            }
            if (flag && canExitPhone && canInteractWithPhone && Input.GetButtonDown("Interact")) canTrig1 = false;
        }
        if (trig3.isTriggered && canTrig3) StartCoroutine(NextPart());
        // Videos
        if (isV1Playing)
        {
            playerScript.lockMov = true;
            vi1.SetActive(true);
            vp1.Play();
            StartCoroutine(EndVideo());
        }
        if (isV2Playing)
        {
            playerScript.lockMov = true;
            vi2.SetActive(true);
            vp2.Play();
            StartCoroutine(EndVideo());
        }
        if (isV3Playing)
        {
            playerScript.lockMov = true;
            vi3.SetActive(true);
            vp3.Play();
            StartCoroutine(EndVideo());
        }
        if (isV4Playing)
        {
            playerScript.lockMov = true;
            vi4.SetActive(true);
            vp4.Play();
            StartCoroutine(EndVideo());
        }
        if (isV5Playing)
        {
            playerScript.lockMov = true;
            vi5.SetActive(true);
            vp5.Play();
            StartCoroutine(EndVideo());
        }
    }
    IEnumerator FlagDelay(bool b)
    {
        yield return new WaitForSeconds(.1f);
        flag = true;
    }
    IEnumerator CanNextSubDelay(bool b)
    {
        yield return new WaitForSeconds(.1f);
        canNextSub = b;
    }
    IEnumerator CanExitPhoneDelay(bool b)
    {
        yield return new WaitForSeconds(.1f);
        canExitPhone = b;
    }
    IEnumerator NextPart()
    {
        canTrig3 = false;
        player.position = playerStart.position;
        camTarget.position = new Vector2(-1, 0);
        switch (curPart)
        {
            case 1:
                isV1Playing = true;
                break;
            case 2:
                isV2Playing = true;
                break;
            case 3:
                isV3Playing = true;
                break;
            case 4:
                isV4Playing = true;
                break;
            case 5:
                isV5Playing = true;
                break;
        }
        curPart++;
        StartCoroutine(InitBools());
        yield return new WaitForSeconds(0);
    }
    IEnumerator InitBools()
    {
        yield return new WaitForSeconds(0);
        curSubPart = 1;
        showPhone = false;
        canInteractWithPhone = false;
        isRepShown1 = false;
        isRepShown2 = false;
        canNextSub = false;
        canExitPhone = false;
        flag = false;
        trig1.isTriggered = false;
        trig2.isTriggered = false;
        trig3.isTriggered = false;
        canTrig1 = true;
        canTrig2 = true;
        canTrig3 = true;
        playerAnimator.SetInteger("charaNumber", curPart);
        if (curPart == 5) pendu.SetActive(false);
        Camera.main.orthographicSize = 5;
    }
    IEnumerator EndVideo()
    {
        yield return new WaitForSeconds(7);
        playerScript.lockMov = false;
        isV1Playing = false;
        isV2Playing = false;
        isV3Playing = false;
        isV4Playing = false;
        isV5Playing = false;
        vi1.SetActive(false);
        vi2.SetActive(false);
        vi3.SetActive(false);
        vi4.SetActive(false);
        vi5.SetActive(false);
        if (curPart > 5) SceneManager.LoadScene("Menu");
    }
}