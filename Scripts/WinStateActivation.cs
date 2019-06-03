using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    [SerializeField] private GameObject winStateScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.hasCard)
            {
                winStateScene.SetActive(true);
            }
            else
            {
                Debug.Log("You need a key card!");
            }
        }
    }

}
