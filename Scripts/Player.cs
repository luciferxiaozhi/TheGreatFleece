using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private Vector3 _target;
    private bool _coinTossed = false;

    public GameObject coin;
    public AudioClip coinSoundAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // move to point
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log(hitInfo.point);
                _agent.SetDestination(hitInfo.point);
//                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//                cube.transform.position = hitInfo.point;
                _anim.SetBool("Walk", true);
                _target = hitInfo.point;
            }
        }

        float distance = Vector3.Distance(transform.position, _target);
        if (distance < 1.0f)
        {
            _anim.SetBool("Walk", false);
        }

        if (Input.GetMouseButtonDown(1) && !_coinTossed) // throw coin
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                _coinTossed = true;
                _anim.SetTrigger("Throw");
                Instantiate(coin, hitInfo.point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(coinSoundAudioClip, hitInfo.point);
                SendAIToCoinSpot(hitInfo.point);
            }
        }
    }

    void SendAIToCoinSpot(Vector3 coinPosition)
    {
        GameObject[] guards = guards = GameObject.FindGameObjectsWithTag("Guard1");
        foreach (var guard in guards)
        {
            NavMeshAgent currentGuardAgent = guard.GetComponent<NavMeshAgent>();
            GuardAI currentGuard = guard.GetComponent<GuardAI>();
            Animator currentGuardAnim = guard.GetComponent<Animator>();

            currentGuard.coinPosition = coinPosition;
            currentGuard.coinTossed = true;
            currentGuardAgent.SetDestination(coinPosition);
            currentGuardAnim.SetBool("Walk", true);

        }
    }

}
