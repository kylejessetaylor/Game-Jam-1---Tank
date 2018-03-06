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
    BulletScript bulletScript;
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
        //if (tank1.IsDead || tank2.IsDead)
        //{
        //    Time.timeScale = 0;

        //    if (tank1.IsDead)
        //    {
        //        uiManager.Player1Score++;
        //    }

        //    if (tank2.IsDead)
        //    {
        //        uiManager.Player2Score++;
        //    }
        //}
    }

    public int CurrMapIndex
    {
        get { return currMapIndex; }
        set { currMapIndex = value; }
    }
}
