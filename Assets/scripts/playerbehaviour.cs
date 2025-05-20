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
    public Animator animator;
    public bool emblemPartVisible = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }

    void Update()
    {
        Vector3 velocity = agent.velocity;
        velocity.y = 0;
        float agentvelocity = agent.velocity.magnitude;
        float speed = agentvelocity;
        float scaledSpeed = speed * 3f;

        agent.updatePosition = true;
        agent.updateRotation = true;
        animator.SetFloat("Speed", scaledSpeed);
        animator.applyRootMotion = false;
        //float currentSpeed = animator.GetFloat("Speed");

        //Debug.Log($"Speed raw: {speed}, scaled: {scaledSpeed}, animator: {currentSpeed}, in locomotion: {animator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion")}");
        if (Input.GetKeyDown(KeyCode.S))
        {
            canMove = true;  
            //On Pressing S, refresh the list of seekable objects so it inlcudes all added lights.
            seeklights = FindObjectsByType<SeekableObjectr>(FindObjectsSortMode.None).ToList();      
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
                //DAMON :: Check only XZ plane and ignore
                Vector3 mePosXZ = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 targetPosXZ = new Vector3(target.position.x, 0, target.position.z);
                float distance = (mePosXZ - targetPosXZ).magnitude;
                //DAMON :: The distance where it reaches the light is actually > 0.9f so changed the check to 1f
                if  (distance < 1f)
                {
                    HaveTargetToGoTo();
                    agent.SetDestination(target.position);
                }
            }
            //SeekTargets();
            AvoidDangers();
        }
    }

    //DAMON: This return the current speed of the agent
    public float GetCurrentSpeed()
    {
        return agent.velocity.magnitude;
    }

    bool HaveTargetToGoTo ()
    {
        if (seeklights.Count > 0)
        {
            List<SeekableObjectr> unvisited = new List<SeekableObjectr>();
            unvisited = seeklights.Where(x => x.visited == false).ToList();
            float distance = 10000000;
            SeekableObjectr closest = null;
            if (unvisited.Count == 0)
            {
                if (emblemPartVisible)
                {
                    target = emblemPart;
                    return true;
                }
                else return false;
            }
            else
            {
                foreach (SeekableObjectr so in unvisited)
                {
                    //DAMON: This is the code that calculates the distance to the light using navmesh path
                    NavMeshPath path = new NavMeshPath();
                    float lengthSoFar = 0;
                    if (agent.CalculatePath(so.transform.position, path))
                    {
                        Vector3 prevPoint = transform.position;
                        foreach (Vector3 corner in path.corners)
                        {
                            lengthSoFar += Vector3.Distance(prevPoint, corner);
                            prevPoint = corner;
                        }
                    }
                    if (distance >= lengthSoFar)
                    {
                        distance = lengthSoFar;
                        closest = so;
                    }
                }
                target = closest.transform;
                return true;
            }
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
}
