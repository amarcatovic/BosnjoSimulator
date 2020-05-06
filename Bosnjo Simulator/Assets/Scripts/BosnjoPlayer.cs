using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosnjoPlayer : MonoBehaviour
{
    [SerializeField] GameObject bosnjoTShirt;
    [SerializeField] GameObject bosnjoShoe1;
    [SerializeField] GameObject bosnjoShoe2;
    SpriteRenderer bosnjoTShirtSpriteRenderer;
    SpriteRenderer bosnjoShoe1SpriteRenderer;
    SpriteRenderer bosnjoShoe2SpriteRenderer;
    Color bosnjoCurrentTShirtColor;
    Color bosnjoCurrentShoesColor;

    [SerializeField] bool styleThisPlayer = true;

    SaveData sd;

    void Start()
    {
        if(styleThisPlayer)
        {
            bosnjoTShirtSpriteRenderer = bosnjoTShirt.GetComponent<SpriteRenderer>();
            bosnjoShoe1SpriteRenderer = bosnjoShoe1.GetComponent<SpriteRenderer>();
            bosnjoShoe2SpriteRenderer = bosnjoShoe2.GetComponent<SpriteRenderer>();
            sd = FindObjectOfType<Gameplay>().GetSaveData();

            ColorUtility.TryParseHtmlString(sd.playerTShirtColor, out bosnjoCurrentTShirtColor);
            ColorUtility.TryParseHtmlString(sd.playerShoesColor, out bosnjoCurrentShoesColor);

            bosnjoTShirtSpriteRenderer.color = bosnjoCurrentTShirtColor;
            bosnjoShoe1SpriteRenderer.color = bosnjoCurrentShoesColor;
            bosnjoShoe2SpriteRenderer.color = bosnjoCurrentShoesColor;
        }
    }
}
