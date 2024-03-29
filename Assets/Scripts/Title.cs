﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject goSM;
    public SoundMgr scSM;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        goSM = GameObject.Find("SoundManager");
        scSM = goSM.GetComponent<SoundMgr>();
        scSM.PlayBGM(true, true, 0);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnClick();
        }
    }
    public void OnClick()
    {
        SceneManager.LoadScene(2);
    }

}
