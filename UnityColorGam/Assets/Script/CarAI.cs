using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private float Score = 0f;
    public float CarValue = 10000f;

    public float StopDistance = 2f;
    public bool stoppedByLight = false;
    public bool stoppedByCar = false;

    // Update is called once per frame
    void Update()
    {
        if (!stoppedByLight && !stoppedByCar)
        {
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("TrafficLight"))
        {
            var lightCtrl = other.GetComponent<TrafficLightController>();
            if (lightCtrl != null)
            {
                // stoppedByLight = !lightCtrl.CanGoStright();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TrafficLight"))
        {
            stoppedByLight = false;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, StopDistance))
        {
            if (hit.collider.CompareTag("Car"))
            {
                stoppedByCar = true;
                return;
            }
        }
        stoppedByCar = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(CarValue);
            }

            Destroy(gameObject);
        }
    }
}
