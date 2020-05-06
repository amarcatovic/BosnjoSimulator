﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoznjaAnimacija : MonoBehaviour
{
    [SerializeField] GameObject bosnjo;
    private Animator b1;

    void Awake()
    {
        b1 = bosnjo.GetComponent<Animator>();
        b1.SetTrigger("VoziAuto");
        StartCoroutine(AnimPause());
    }

    private IEnumerator AnimPause()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainGame");
    }
}
