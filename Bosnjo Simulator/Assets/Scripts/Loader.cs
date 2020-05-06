using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Loader : MonoBehaviour
{
    [SerializeField] float loadForUnits = 5f;
    [SerializeField] TextMeshProUGUI text;

    void Update()
    {
        loadForUnits -= Time.deltaTime;
        StartCoroutine(FadeTextToZeroAlpha(1f, text));
        if (loadForUnits < 0)
        {
            SceneManager.LoadScene("Home");
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t) * 0.0015f);
            yield return null;
        }
    }
}
