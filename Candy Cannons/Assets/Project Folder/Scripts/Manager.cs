using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private GameObject tank1;
    private GameObject tank2;
    public List<GameObject> mapObjList;
    private int currMapIndex;
    private UIManager uiManager;
    private GameObject currMap;

// Use this for initialization
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        currMap =  Instantiate(mapObjList[0]);
        currMap.SetActive(true);

        currMapIndex = 0;

        tank1 = GameObject.Find("PlayerTank1");
        tank2 = GameObject.Find("PlayerTank2");
    }

    // Update is called once per frame
    void Update()
    {
        if (tank1.GetComponent<IsHitScript>().IsHit == true || tank2.GetComponent<IsHitScript>().IsHit == true)
        {
            if (tank1.GetComponent<IsHitScript>().IsHit == true)
            {
                uiManager.Player2Score++;
                tank1.GetComponent<IsHitScript>().IsHit = false;

            }

            if (tank2.GetComponent<IsHitScript>().IsHit == true)
            {
                uiManager.Player1Score++;
                tank2.GetComponent<IsHitScript>().IsHit = false;
            }

            uiManager.switchGameState(gameState.NEXT);
        }
    }

    public void refreshStage()
    {
        currMap.SetActive(false);
        currMapIndex = Random.Range(0, mapObjList.Count-1);
        currMap = Instantiate(mapObjList[currMapIndex]);
        currMap.SetActive(true);

        tank1 = GameObject.Find("PlayerTank1");
        tank2 = GameObject.Find("PlayerTank2");
        //Turns off Mine projectiles
        List<GameObject> mine = GameObject.FindGameObjectsWithTag("Projectile").ToList();

        for (int i = 0; i < mine.Count; i++)
        {
            mine[i].SetActive(false);
        }

        tank1.transform.position = new Vector3(tank1.transform.position.x, 0, tank1.transform.position.z);
        tank2.transform.position = new Vector3(tank2.transform.position.x, 0, tank2.transform.position.z);

    }

    public int CurrMapIndex
    {
        get { return currMapIndex; }
        set { currMapIndex = value; }
    }
}
