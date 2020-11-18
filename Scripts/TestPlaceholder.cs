using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlaceholder : MonoBehaviour
{
    public GameObject player;
    public GameObject planet;

    // Update is called once per frame
    void Update()
    {
        //smooth

        //position
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f);

        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        //rotation
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);
    }

    public void NewPlanet(GameObject planet)
    {
        this.planet = planet;
    }
}
