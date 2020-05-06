using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeforeGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI faxNameText;
    [SerializeField] TextMeshProUGUI faxYearText;
    

    void Start()
    {
        FakultetManager fm = FindObjectOfType<FakultetManager>();
        faxNameText.text = fm.odabraniFakultet;
        faxYearText.text = "Godina studija: " + fm.godinaFakulteta.ToString();
    }

}
