using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public int lifes;
    public int numHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(lifes > numHearts)
            {
                lifes = numHearts;
            }

            if (i < lifes)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
