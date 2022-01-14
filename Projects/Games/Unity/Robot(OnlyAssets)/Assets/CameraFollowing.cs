using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject player;

    public float height;
    public float playerOffset;

    void LateUpdate()
    {
        transform.position = new Vector3(0f, height, player.transform.position.z - playerOffset);
    }
}
