using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    public bool coinTossed = false;
    public Vector3 coinPosition;

    [SerializeField]
    private int currentTarget;
    private NavMeshAgent _agent;
    [SerializeField]
    private bool _reverse = false;
    [SerializeField]
    private bool _targetReached = false;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        
        if (wayPoints.Count > 0 && wayPoints[0] != null)
        {
            _agent.SetDestination(wayPoints[currentTarget].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!coinTossed && wayPoints.Count > 0 && wayPoints[currentTarget] != null)
        {
            _agent.SetDestination(wayPoints[currentTarget].position);
            float distance = Vector3.Distance(transform.position, wayPoints[currentTarget].position);

            if (distance <= 4f && (currentTarget == 0 || currentTarget == wayPoints.Count - 1))
            {
                _anim.SetBool("Walk", false);
            }
            else
            {
                _anim.SetBool("Walk", true);
            }

            if (distance <= 4f && !_targetReached && wayPoints.Count > 1)
            {
                _targetReached = true;
                StartCoroutine(WaitBeforeMoving());
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, coinPosition);
            if (distance < 4f)
            {
                _anim.SetBool("Walk", false);
                StartCoroutine("FreezeBeforeMoveAgain");
            }
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        if (currentTarget == 0)
        {
            yield return new WaitForSeconds(2.0f);
            _reverse = false;
        }
        else if (currentTarget == wayPoints.Count - 1)
        {
            yield return new WaitForSeconds(2.0f);
            _reverse = true;
        }

        if (_reverse)
        {
            currentTarget--;
        }
        else
        {
            currentTarget++;
        }
        _targetReached = false;
    }

    IEnumerator FreezeBeforeMoveAgain()
    {
        yield return new WaitForSeconds(30f);

        coinTossed = false;
    }
}
