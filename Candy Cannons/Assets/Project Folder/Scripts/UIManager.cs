using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public enum currOption_MainMenu
{
    START = 0,
    EXIT
}

public enum currOption_Pause
{
    RESUME = 0,
    MAIN,
    EXIT
}

public enum currOption_Next
{
    NEXT = 0,
    MAIN,
    EXIT
}

public class UIManager : MonoBehaviour
{
    List<GameObject> uiObjects;

    //------ Main Menu Sprites ------// 
    //assign start sprite
    private Image startSprite_MainMenu;
    //assign quit sprite
    private Image exitSprite_MainMenu;
    //Arrays for storing Resources sprites
    private Sprite[] startSpriteList_MainMenu;
    private Sprite[] exitSpriteList_MainMenu;

    //------ Pause Sprites ------//
    //assign resume sprite
    private Image resumeSprite_Pause;
    //assign main meny sprite
    private Image mainMenuSprite_Pause;
    //assign exit sprite
    private Image exitSprite_Pause;
    //Arrays for storing Resources sprites
    private Sprite[] resumeSpriteList_Pause;
    private Sprite[] mainMenuSpriteList_Pause;
    private Sprite[] exitSpriteList_Pause;

    //------ Next Sprites ------//
    //assign next sprite
    private Image nextSprite_Next;
    private Image mainMenuSprite_Next;
    private Image exitSprite_Next;
    //Arrays for storing Resources sprites
    private Sprite[] nextSpriteList_Next;
    private Sprite[] mainMenuSpriteList_Next;
    private Sprite[] exitSpriteList_Next;

    //setup player scores
    private Text player1ScoreText;
    private Text player2ScoreText;

    private int player1Score;
    private int player2Score;

    private currOption_MainMenu currPointing_MainMenu;
    private currOption_Pause currPointing_Pause;
    private currOption_Next currPointing_Next;


    //get Xbox controller
    public XboxController controller;

    //Vector3 controllerMovement;

    // Use this for initialization
    void Awake()
    {
        //Load all UI Objects in Scene
        loadUIObjects();

        //------ Load Main Menu Sprites from Resource ------// 
        startSpriteList_MainMenu = Resources.LoadAll<Sprite>("StartSprites_MainMenu");
        exitSpriteList_MainMenu = Resources.LoadAll<Sprite>("ExitSprites_MainMenu");
        //------ Load Pause Sprites from Resource ------//
        resumeSpriteList_Pause = Resources.LoadAll<Sprite>("ResumeSprites_Pause");
        mainMenuSpriteList_Pause = Resources.LoadAll<Sprite>("MainMenuSprites_Pause");
        exitSpriteList_Pause = Resources.LoadAll<Sprite>("ExitSprites_Pause");
        //------ Load Next Sprites from Resource ------//
        nextSpriteList_Next = Resources.LoadAll<Sprite>("NextSprites_Next");
        mainMenuSpriteList_Next = Resources.LoadAll<Sprite>("MainMenuSprites_Next");
        exitSpriteList_Next = Resources.LoadAll<Sprite>("ExitSprites_Next");

        //------ Setup Main Menu's buttons' references ------//
        startSprite_MainMenu = GameObject.Find("StartButton_Main").GetComponent<Image>();
        exitSprite_MainMenu = GameObject.Find("ExitButton_Main").GetComponent<Image>();
        //initialize which sprite to assign to each option(1 = highlighted sprite, 0 = not highlighted sprite)
        startSprite_MainMenu.sprite = startSpriteList_MainMenu[1];
        exitSprite_MainMenu.sprite = exitSpriteList_MainMenu[0];

        //------ Setup Pause's buttons' reference ------//
        resumeSprite_Pause = GameObject.Find("ResumeButton_Pause").GetComponent<Image>();
        mainMenuSprite_Pause = GameObject.Find("MainMenuButton_Pause").GetComponent<Image>();
        exitSprite_Pause = GameObject.Find("ExitButton_Pause").GetComponent<Image>();
        //initialize which sprite to assign to each option (1 = highlighted sprite, 0 = not highlighted sprite)
        resumeSprite_Pause.sprite = resumeSpriteList_Pause[1];
        mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
        exitSprite_Pause.sprite = exitSpriteList_Pause[0];

        //------ Setup Players' scores' reference ------//
        player1ScoreText = GameObject.Find("Player1Score").GetComponent<Text>();
        player2ScoreText = GameObject.Find("Player2Score").GetComponent<Text>();
        player1Score = 0;
        player2Score = 0;

        //------ Setup Next's buttons' reference ------//
        nextSprite_Next = GameObject.Find("NextButton_Next").GetComponent<Image>();
        mainMenuSprite_Next = GameObject.Find("MainMenuButton_Next").GetComponent<Image>();
        exitSprite_Next = GameObject.Find("ExitButton_Next").GetComponent<Image>();
        //initialize which sprite to assign to each option (1 = highlighted sprite, 0 = not highlighted sprite)
        nextSprite_Next.sprite = nextSpriteList_Next[1];
        mainMenuSprite_Next.sprite = mainMenuSpriteList_Next[0];
        exitSprite_Next.sprite = exitSpriteList_Next[0];

        //set current pointing in Main Menu to START
        currPointing_MainMenu = currOption_MainMenu.START;
        //set current pointing in Pause to RESUME
        currPointing_Pause = currOption_Pause.RESUME;
        ////set current pointing in Pause to NEXT
        currPointing_Next = currOption_Next.NEXT;

        //------ All UI Elements Loaded, turn off IngameUI and PauseUI ------//
        uiObjects[0].SetActive(true);
        uiObjects[1].SetActive(false);
        uiObjects[2].SetActive(false);
        uiObjects[3].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (uiObjects[0].activeInHierarchy) //if currently at MAIN MENU State
        {
            Time.timeScale = 0;

            if (currPointing_MainMenu == currOption_MainMenu.START)
            {
                if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    startSprite_MainMenu.sprite = startSpriteList_MainMenu[0];
                    exitSprite_MainMenu.sprite = exitSpriteList_MainMenu[1];
                    currPointing_MainMenu = currOption_MainMenu.EXIT;
                }

                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    uiObjects[0].SetActive(false);
                    uiObjects[1].SetActive(true);
                    uiObjects[2].SetActive(false);
                    uiObjects[3].SetActive(false);
                }
            }
            else if (currPointing_MainMenu == currOption_MainMenu.EXIT)
            {
                if (XCI.GetButtonDown(XboxButton.DPadUp, controller) || XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    startSprite_MainMenu.sprite = startSpriteList_MainMenu[1];
                    exitSprite_MainMenu.sprite = exitSpriteList_MainMenu[0];
                    currPointing_MainMenu = currOption_MainMenu.START;
                }

                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Application.Quit();
                }
            }
        }

        if (uiObjects[1].activeInHierarchy) //if currently at INGAME State
        {
            Time.timeScale = 1;

            if (XCI.GetButtonDown(XboxButton.Start, controller))
            {
                Time.timeScale = 0;
                uiObjects[0].SetActive(false);
                uiObjects[1].SetActive(false);
                uiObjects[2].SetActive(true);
                uiObjects[3].SetActive(false);
            }
        }

        if (uiObjects[2].activeInHierarchy) //if currently at PAUSE State
        {
            if (currPointing_Pause == currOption_Pause.RESUME)
            {
                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Time.timeScale = 1;
                    uiObjects[0].SetActive(false);
                    uiObjects[1].SetActive(true);
                    uiObjects[2].SetActive(false);
                    uiObjects[3].SetActive(false);
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Pause = currOption_Pause.EXIT;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[0];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSprite_Pause.sprite = exitSpriteList_Pause[1];

                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Pause = currOption_Pause.MAIN;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[0];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[1];
                    exitSprite_Pause.sprite = exitSpriteList_Pause[0];
                }

            }
            else if (currPointing_Pause == currOption_Pause.MAIN)
            {
                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Time.timeScale = 0;
                    uiObjects[0].SetActive(true);
                    uiObjects[1].SetActive(false);
                    uiObjects[2].SetActive(false);
                    uiObjects[3].SetActive(false);
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Pause = currOption_Pause.RESUME;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[1];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSprite_Pause.sprite = exitSpriteList_Pause[0];
                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Pause = currOption_Pause.EXIT;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[0];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSprite_Pause.sprite = exitSpriteList_Pause[1];
                }
            }
            else if (currPointing_Pause == currOption_Pause.EXIT)
            {
                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Application.Quit();
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Pause = currOption_Pause.MAIN;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[0];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[1];
                    exitSprite_Pause.sprite = exitSpriteList_Pause[0];
                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Pause = currOption_Pause.RESUME;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[1];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSprite_Pause.sprite = exitSpriteList_Pause[0];
                }
            }
        }

        if (uiObjects[3].activeInHierarchy) //if currently at In NEXT State
        {
            if (currPointing_Next == currOption_Next.NEXT)
            {
                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Time.timeScale = 1;
                    uiObjects[0].SetActive(false);
                    uiObjects[1].SetActive(true);
                    uiObjects[2].SetActive(false);
                    uiObjects[3].SetActive(false);
                    //turn off current map
                    //launch next map
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Next = currOption_Next.EXIT;
                    nextSprite_Next.sprite = nextSpriteList_Next[0];
                    mainMenuSprite_Next.sprite = mainMenuSpriteList_Next[0];
                    exitSprite_Next.sprite = exitSpriteList_Next[1];

                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Next = currOption_Next.MAIN;
                    nextSprite_Next.sprite = nextSpriteList_Next[0];
                    mainMenuSprite_Next.sprite = mainMenuSpriteList_Next[1];
                    exitSprite_Next.sprite = exitSpriteList_Next[0];
                }

            }
            else if (currPointing_Next == currOption_Next.MAIN)
            {
                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Time.timeScale = 0;
                    uiObjects[0].SetActive(true);
                    uiObjects[1].SetActive(false);
                    uiObjects[2].SetActive(false);
                    uiObjects[3].SetActive(false);
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Next = currOption_Next.NEXT;
                    nextSprite_Next.sprite = nextSpriteList_Next[1];
                    mainMenuSprite_Next.sprite = mainMenuSpriteList_Next[0];
                    exitSprite_Next.sprite = exitSpriteList_Next[0];
                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Next = currOption_Next.EXIT;
                    nextSprite_Next.sprite = nextSpriteList_Next[0];
                    mainMenuSprite_Next.sprite = mainMenuSpriteList_Next[0];
                    exitSprite_Next.sprite = exitSpriteList_Next[1];
                }
            }
            else if (currPointing_Next == currOption_Next.EXIT)
            {
                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Application.Quit();
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Next = currOption_Next.MAIN;
                    nextSprite_Next.sprite = nextSpriteList_Next[0];
                    mainMenuSprite_Next.sprite = mainMenuSpriteList_Next[1];
                    exitSprite_Next.sprite = exitSpriteList_Next[0];
                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Next = currOption_Next.NEXT;
                    nextSprite_Next.sprite = nextSpriteList_Next[1];
                    mainMenuSprite_Next.sprite = mainMenuSpriteList_Next[0];
                    exitSprite_Next.sprite = exitSpriteList_Next[0];
                }
            }
        }
    }

    private void loadUIObjects()
    {
        uiObjects = new List<GameObject>();
        GameObject mainMenuUI = GameObject.Find("MainMenuUI");
        GameObject ingameUI = GameObject.Find("IngameUI");
        GameObject pauseUI = GameObject.Find("PauseUI");
        GameObject nextUI = GameObject.Find("NextUI");
        uiObjects.Add(mainMenuUI);
        uiObjects.Add(ingameUI);
        uiObjects.Add(pauseUI);
        uiObjects.Add(nextUI);
    }

    //Getter & Setter for both player's scores
    public int Player1Score
    {
        get { return player1Score; }
        set { player1Score = value; }
    }

    public int Player2Score
    {
        get { return player2Score; }
        set { player2Score = value; }
    }

    public List<GameObject> UIObjects
    {
        get { return uiObjects; }
        set { uiObjects = value; }
    }
}