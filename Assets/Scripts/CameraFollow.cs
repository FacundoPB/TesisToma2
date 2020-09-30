using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update

    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -1);
    }
}
