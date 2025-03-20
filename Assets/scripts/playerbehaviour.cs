using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

public class NavMeshAvoidance : MonoBehaviour
{
    public Transform emblemPart;
    public Transform target; // The goal destination
    public List<SeekableObjectr> seeklights;
    public float avoidanceRadius = 5f; // Distance to avoid obstacles
    public LayerMask dangerMask; // LayerMask for dangerous objects
    public LayerMask seekMask; // LayerMask for objects to seek
    private NavMeshAgent agent;
    public bool canMove = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            canMove = true;        
        }


        if (canMove) 
        {
            if (target == null)
            {
                HaveTargetToGoTo();
                agent.SetDestination(target.position);
            }
            else
            {
                if  ((transform.position - target.position).magnitude >= 0.25f)
                {
                    
                }
                else
                {
                    HaveTargetToGoTo();
                    agent.SetDestination(target.position);
                }
            }
            //SeekTargets();
            AvoidDangers();
        }
    }

    bool HaveTargetToGoTo ()
    {
        if (seeklights.Count > 0)
        {
            List<SeekableObjectr> unvisited = new List<SeekableObjectr>();
            unvisited = seeklights.Where(x => x.visited == false).ToList();
            float distance = 10000000;
            SeekableObjectr closest = null;
            print(unvisited.Count);
            if (unvisited.Count == 0)
            {
                target = emblemPart;
                return true;
            }

            foreach (SeekableObjectr so in unvisited)
            {
                agent.SetDestination(so.transform.position);
                if (distance >= agent.remainingDistance)
                {
                    distance = agent.remainingDistance;                   
                    closest = so;
                }
                agent.SetDestination(transform.position);
            }

            target = closest.transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    void AvoidDangers()
    {
        Collider[] dangers = Physics.OverlapSphere(transform.position, avoidanceRadius, dangerMask);
        if (dangers.Length > 0)
        {
            Vector3 avoidDirection = Vector3.zero;

            foreach (Collider danger in dangers)
            {
                avoidDirection += (transform.position - danger.transform.position);
            }

            avoidDirection /= dangers.Length; // Get the average direction away
            avoidDirection = avoidDirection.normalized * avoidanceRadius;

            Vector3 newTarget = transform.position + avoidDirection;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(newTarget, out hit, avoidanceRadius, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position); // Move to a safe position
            }
        }
        else
        {
            if (target != null)
            {
                agent.SetDestination(target.position); // Continue to goal if no danger
            }
        }
    }

    //void SeekTargets()
    //{
    //    Collider[] seeks = Physics.OverlapSphere(transform.position, avoidanceRadius, seekMask);
    //    if (seeks.Length > 0)
    //    {
    //        Transform closestTarget = seeks[0].transform;
    //        float closestDistance = Vector3.Distance(transform.position, closestTarget.position);

    //        foreach (Collider seek in seeks)
    //        {
    //            float distance = Vector3.Distance(transform.position, seek.transform.position);
    //            if (distance < closestDistance)
    //            {
    //                closestTarget = seek.transform;
    //                closestDistance = distance;
    //            }
    //        }

    //        agent.SetDestination(closestTarget.position); // Move towards the closest seekable object
    //    }
    //}
}
