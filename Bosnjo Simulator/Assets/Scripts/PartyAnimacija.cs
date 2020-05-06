using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PartyAnimacija : MonoBehaviour
{
    [SerializeField] GameObject bosnjo;
    [SerializeField] GameObject bosnjo2;
    private Animator b1;
    private Animator b2;

    void Awake()
    {
        b1 = bosnjo.GetComponent<Animator>();
        b1.SetTrigger("Dance");
        b2 = bosnjo2.GetComponent<Animator>();
        b2.SetTrigger("Dance");
        StartCoroutine(AnimPause());
    }

    private IEnumerator AnimPause()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainGame");
    }
}
