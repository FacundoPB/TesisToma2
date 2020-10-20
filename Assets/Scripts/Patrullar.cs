using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    public bool IsPatrolling = true;

    public Transform[] waypoints;
    private int currentWaypoint;
    public float velocidad;
    public float espera;
    private Coroutine vigilarCorrutina;

    void Start()
    {
        StartCoroutine(PatrollingRoutine());
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
}