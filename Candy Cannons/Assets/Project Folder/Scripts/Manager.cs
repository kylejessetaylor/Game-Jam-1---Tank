using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private GameObject tank1;
    private GameObject tank2;
    private List<GameObject> mapObjList;
    private int currMapIndex;
    private UIManager uiManager;

// Use this for initialization
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        mapObjList = GameObject.FindGameObjectsWithTag("Map").ToList();
        mapObjList[0].SetActive(true);
        for (int i = 1; i < mapObjList.Count; i++)
        {
            mapObjList[i].SetActive(false);
        }
        currMapIndex = 0;

        tank1 = GameObject.Find("PlayerTank1");
        tank2 = GameObject.Find("PlayerTank2");
    }

    // Update is called once per frame
    void Update()
    {
        if (tank1.GetComponent<IsHitScript>().IsHit == true || tank2.GetComponent<IsHitScript>().IsHit ==)
        {
            if (tank1.GetComponent<IsHitScript>().IsHit)
            {
                uiManager.Player2Score++;
            }

            if (tank2.GetComponent<IsHitScript>().IsHit)
            {
                uiManager.Player1Score++;
            }

            uiManager.switchGameState(gameState.NEXT);
        }
    }

    public void refreshStage()
    {
        mapObjList[currMapIndex].SetActive(false);
        currMapIndex = Random.Range(0, mapObjList.Count-1);
        mapObjList[currMapIndex].SetActive(true);
        tank1 = GameObject.Find("PlayerTank1");
        tank2 = GameObject.Find("PlayerTank2");
    }

    public int CurrMapIndex
    {
        get { return currMapIndex; }
        set { currMapIndex = value; }
    }
}
