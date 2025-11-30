using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Lives : MonoBehaviour
{
    public List<Image> lives;
    private int currentLives;

    void Start()
    {
        currentLives = lives.Count;
        refreshUI();
    }

    public void refreshUI()
    {
        for (int i = 0; i < lives.Count; i++)
        {
            if(i < currentLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy"))
        {
            if(currentLives > 0)
            {
                currentLives--;
                refreshUI();
            }
            if (currentLives <= 0)
            {
                Debug.Log("You Loose");
                //Aqui hay que llamar a la función para cuando pierde el jugador
            }
        }

    }
}
