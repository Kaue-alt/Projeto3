using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    public string tagDestino;
    private Transform portalDestino;
    void Start()
    {
        portalDestino = GameObject.FindGameObjectWithTag(tagDestino).transform;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Vector3 posPlayer = portalDestino.transform.position;
            posPlayer.z += 2.0f;
            col.transform.position = posPlayer;
        }
    }
}
