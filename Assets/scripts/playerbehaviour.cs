using UnityEngine;
using UnityEngine.AI;
public class playerbehaviour : MonoBehaviour
{
    public Transform target; 
    public float avoidanceRadius = 5f;
    public LayerMask dangerMask; 
    public LayerMask seekMask; 
    private NavMeshAgent agent;
    private bool canMove = false;

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
            SeekTargets();
            AvoidDangers();
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
            agent.SetDestination(target.position); // Continue to goal if no danger
        }
    }

    void SeekTargets()
    {
        Collider[] seeks = Physics.OverlapSphere(transform.position, avoidanceRadius, seekMask);
        if (seeks.Length > 0)
        {
            Transform closestTarget = seeks[0].transform;
            float closestDistance = Vector3.Distance(transform.position, closestTarget.position);

            foreach (Collider seek in seeks)
            {
                float distance = Vector3.Distance(transform.position, seek.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = seek.transform;
                    closestDistance = distance;
                }
            }

            agent.SetDestination(closestTarget.position); // Move towards the closest seekable object
        }
    }
}


