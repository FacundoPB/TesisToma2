using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{

    public Transform[] waypoints;
    private int idx;
    public float velocidad;
    public float espera;
    private Coroutine vigilarCorrutina;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if (vigilarCorrutina == null)
        {
            vigilarCorrutina = StartCoroutine(Vigilar());
        }
        

    }
    IEnumerator Vigilar()
    {
        Debug.Log("1");
        if (waypoints[idx].position != transform.position)
        {
            Quaternion targetRotation = Quaternion.LookRotation(waypoints[idx].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, velocidad * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, waypoints[idx].position, velocidad * Time.deltaTime);
            //yield return new WaitForSeconds(espera);
            
            Debug.Log("2");
        }
        else
        {
            //idx++;
            Debug.Log("3");
            if (idx < waypoints.Length - 1)
            {
                for (int i = 0; i < waypoints.Length; i++)
                {
                    Transform tmp = waypoints[i];
                    int r = Random.Range(i, waypoints.Length);
                    waypoints[i] = waypoints[r];
                    waypoints[r] = tmp;

                    yield return new WaitForSeconds(espera);
                    vigilarCorrutina = null;

                }

            }
            else
            {
                idx = 0;
                Debug.Log("4");
            }
            

        }

        //yield return new WaitForSeconds(espera);



    }
}


