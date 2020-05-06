using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakultetManager : MonoBehaviour
{
    public string odabraniFakultet;
    public int godinaFakulteta;
    public bool isZadnjaGodina;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
