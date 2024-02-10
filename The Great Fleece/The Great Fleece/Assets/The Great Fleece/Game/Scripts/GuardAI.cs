using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    public bool coinTossed;
    public Vector3 coinPos;

    private NavMeshAgent _agent;
    [SerializeField]
    private int _currentTarget;
    private bool _reverse;
    private bool _targetReached;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints.Count > 0 && wayPoints[_currentTarget] != null && coinTossed == false)
        {
            _agent.SetDestination(wayPoints[_currentTarget].position);
            
            float distance = Vector3.Distance(transform.position, wayPoints[_currentTarget].position);
            if(distance < 4f && (_currentTarget == 0 || _currentTarget == wayPoints.Count - 1))
            {
                if (_anim != null)
                {
                    _anim.SetBool("Walk", false);
                }
            }
            else
            {
                if (_anim != null)
                {
                    _anim.SetBool("Walk", true);
                }
            }

            if(distance < 4.0f && _targetReached == false && wayPoints.Count > 1)
            {
                _targetReached = true;
                StartCoroutine(WaitBeforeMoving());
            }
        }
        else
        {
            float distance = Vector3.Distance(transform.position, coinPos);
            if (distance < 4f)
            {
                _anim.SetBool("Walk", false);
            }
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        if (_currentTarget == 0 || _currentTarget == wayPoints.Count - 1)
        {
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        }

        if (_reverse == true)
        {
            _currentTarget -= 1;

            if (_currentTarget == 0)
            {
                _reverse = false;
            }
        }
        else
        {
            _currentTarget += 1;

            if (_currentTarget == wayPoints.Count)
            {
                _reverse = true;
                _currentTarget -= 1;
            }
        }

        _targetReached = false;
        
    }
}
