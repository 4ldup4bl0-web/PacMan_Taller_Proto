using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Lives : MonoBehaviour
{
    public List<Image> lives;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy collisioned");
        }

    }
}
