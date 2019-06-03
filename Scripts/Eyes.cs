using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameoverCutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Caught!");
            _gameoverCutscene.SetActive(true);
        }
    }

}
