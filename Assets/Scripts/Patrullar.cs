using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Patrullar : MonoBehaviour
{
    public bool IsPatrolling = true;

    public Transform[] waypoints;
    private int currentWaypoint;
    public float velocidad;
    public float espera;
    private Coroutine vigilarCorrutina;
    //View
    public Light2D pointlight;
    public float viewDistance;
    public float viewAngle;
    public Transform player;
    public LayerMask viewMask;
    Color originalPointlightColor;


    void Start()
    {
        //View  player = GameObject.FindGameObjectWithTag("Player").transform;

        viewAngle = pointlight.pointLightInnerAngle;
        originalPointlightColor = pointlight.color;

        StartCoroutine(PatrollingRoutine());
    }

    private void Update()
    {
        if (CanseePlayer())
        {
            pointlight.color = Color.red;
        }
        else
        {
            pointlight.color = originalPointlightColor;
        }
    }


    bool CanseePlayer()
    {

        if (Vector2.Distance(transform.position, player.position) < viewDistance)
        {
            Vector2 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector2.Angle(transform.right, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;

    }

    IEnumerator PatrollingRoutine()
    {
        do
        {
            var toWaypoint = waypoints[currentWaypoint].position - transform.position;
            var directionToWaypoint = toWaypoint.normalized;

            if (toWaypoint.sqrMagnitude > .1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
                //TODO: Unity 2D look at target C#
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, velocidad * Time.deltaTime);

                Vector3 dir = waypoints[currentWaypoint].position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.position =
                    Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position,
                        velocidad * Time.deltaTime);
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(espera);

                if (++currentWaypoint == waypoints.Length)
                    currentWaypoint = 0;
            }
        } while (IsPatrolling);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * viewDistance);
    }


}