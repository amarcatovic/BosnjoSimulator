using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] VariableJoystick variableJoystick;
    [SerializeField] GameObject bosnjo1;
    private Animator b1;
    private Rigidbody2D rb;
    bool leftClicked = false;
    bool rightClicked = false;
    float timePassedSinceLastArrowPressed = 0f;
    float maxTimePassed = 1f;

    private void Awake()
    {
        b1 = bosnjo1.GetComponent<Animator>();
        b1.SetBool("Idle_01", false);
        rb = bosnjo1.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (variableJoystick.Horizontal < 0)
        {
            Vector3 temp = bosnjo1.transform.localScale;
            temp.x = 0.15f;
            bosnjo1.transform.localScale = temp;
            if (bosnjo1.transform.position.x <= -2.3f)
            {
                bosnjo1.transform.position = new Vector3(-2.3f, bosnjo1.transform.position.y, -2f);
            }
            b1 = bosnjo1.GetComponent<Animator>();
            b1.SetBool("Idle_01", false);
            b1.SetBool("Run_01", true);
            Vector3 bosnjoTrci = new Vector3(bosnjo1.transform.position.x, bosnjo1.transform.position.y, -2);
            bosnjoTrci.x -= Time.deltaTime * 0.5f;
            bosnjo1.transform.position = bosnjoTrci;
        }
        else if (variableJoystick.Horizontal > 0)
        {
            Vector3 temp = bosnjo1.transform.localScale;
            temp.x = -0.15f;
            bosnjo1.transform.localScale = temp;
            if (bosnjo1.transform.position.x >= 2.3f)
            {
                bosnjo1.transform.position = new Vector3(2.3f, bosnjo1.transform.position.y, -2f);
            }
            b1 = bosnjo1.GetComponent<Animator>();
            b1.SetBool("Idle_01", false);
            b1.SetBool("Run_01", true);
            Vector3 bosnjoTrci = new Vector3(bosnjo1.transform.position.x, bosnjo1.transform.position.y, -2);
            bosnjoTrci.x += Time.deltaTime * 0.5f;
            bosnjo1.transform.position = bosnjoTrci;
        }
        else
        {
            b1.SetBool("Idle_01", true);
            b1.SetBool("Run_01", false);
        }
        Debug.Log(bosnjo1.transform.position);
    }

}
