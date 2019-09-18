using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LifeManager : MonoBehaviour
{

    public Image cooldown;

    // Update is called once per frame

    // 体力のgo,GameMasterのGOを取得

    private GameObject goGM;
    private GameManager scGM;

    private void Awake()
    {
        goGM = GameObject.Find("GameManager");
        scGM = goGM.GetComponent<GameManager>();
    }

    void Update()
    {
        cooldown.fillAmount = scGM.life / scGM.maxLife;
    }

    /*


        // Start is called before the first frame update
        void Start()
        {

        }
        // Update is called once per frame
        void Update()
        {
            RectTransform rt;
            rt = this.GetComponent<RectTransform>();
            Vector2 v2 = rt.localScale;
            v2.x = scGM.life;
            rt.localScale = v2;
          v2.x = v2.x / 2;
          rt.localPosition = v2;

        }
    */
}
