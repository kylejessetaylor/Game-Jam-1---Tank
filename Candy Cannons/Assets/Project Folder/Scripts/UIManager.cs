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
    private Image exitSpite_Pause;
    //Arrays for storing Resources sprites
    private Sprite[] resumeSpriteList_Pause;
    private Sprite[] mainMenuSpriteList_Pause;
    private Sprite[] exitSpiteList_Pause;

    private currOption_MainMenu currPointing_MainMenu;
    private currOption_Pause currPointing_Pause;

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
        exitSpiteList_Pause = Resources.LoadAll<Sprite>("ExitSprites_Pause");

        //------ Setup Main Menu's buttons' references ------//
        startSprite_MainMenu = GameObject.Find("StartButton_Main").GetComponent<Image>();
        exitSprite_MainMenu = GameObject.Find("ExitButton_Main").GetComponent<Image>();
        //initialize which sprite to assign to each option(1 = highlighted sprite, 0 = not highlighted sprite)
        startSprite_MainMenu.sprite = startSpriteList_MainMenu[1];
        exitSprite_MainMenu.sprite = exitSpriteList_MainMenu[0];

        //------ Setup Pause's buttons' reference ------//
        resumeSprite_Pause = GameObject.Find("ResumeButton_Pause").GetComponent<Image>();
        mainMenuSprite_Pause = GameObject.Find("MainMenuButton_Pause").GetComponent<Image>();
        exitSpite_Pause = GameObject.Find("ExitButton_Pause").GetComponent<Image>();
        //initialize which sprite to assign to each option (1 = highlighted sprite, 0 = not highlighted sprite)
        resumeSprite_Pause.sprite = resumeSpriteList_Pause[1];
        mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
        exitSpite_Pause.sprite = exitSpiteList_Pause[0];



        //set current pointing in Main Menu to START
        currPointing_MainMenu = currOption_MainMenu.START;
        //set current pointing in Pause to Resume
        currPointing_Pause = currOption_Pause.RESUME;

        //------ All UI Elements Loaded, turn off IngameUI and PauseUI ------//
        uiObjects[0].SetActive(true);
        uiObjects[1].SetActive(false);
        uiObjects[2].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (uiObjects[0].activeInHierarchy) //if currently at Main Menu State
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

        if (uiObjects[1].activeInHierarchy) //if currently at In Game State
        {
            Time.timeScale = 1;

            if (XCI.GetButtonDown(XboxButton.Start, controller))
            {
                Time.timeScale = 0;
                uiObjects[0].SetActive(false);
                uiObjects[1].SetActive(false);
                uiObjects[2].SetActive(true);
            }
        }

        if (uiObjects[2].activeInHierarchy) //if currently at In Pause State
        {
            if (currPointing_Pause == currOption_Pause.RESUME)
            {
                if (XCI.GetButtonDown(XboxButton.B, controller))
                {
                    Time.timeScale = 1;
                    uiObjects[0].SetActive(false);
                    uiObjects[1].SetActive(true);
                    uiObjects[2].SetActive(false);
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Pause = currOption_Pause.EXIT;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[0];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSpite_Pause.sprite = exitSpiteList_Pause[1];

                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Pause = currOption_Pause.MAIN;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[0];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[1];
                    exitSpite_Pause.sprite = exitSpiteList_Pause[0];
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
                }

                if (XCI.GetButtonDown(XboxButton.DPadUp, controller))
                {
                    currPointing_Pause = currOption_Pause.RESUME;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[1];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSpite_Pause.sprite = exitSpiteList_Pause[0];
                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Pause = currOption_Pause.EXIT;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[0];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSpite_Pause.sprite = exitSpiteList_Pause[1];
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
                    exitSpite_Pause.sprite = exitSpiteList_Pause[0];
                }

                if (XCI.GetButtonDown(XboxButton.DPadDown, controller))
                {
                    currPointing_Pause = currOption_Pause.RESUME;
                    resumeSprite_Pause.sprite = resumeSpriteList_Pause[1];
                    mainMenuSprite_Pause.sprite = mainMenuSpriteList_Pause[0];
                    exitSpite_Pause.sprite = exitSpiteList_Pause[0];
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
        uiObjects.Add(mainMenuUI);
        uiObjects.Add(ingameUI);
        uiObjects.Add(pauseUI);
    }
}