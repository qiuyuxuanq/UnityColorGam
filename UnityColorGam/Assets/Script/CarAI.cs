using UnityEngine;

public enum CarDirection { North, South, East, West }

public class Car : MonoBehaviour
{
    public float speed = 5f;
    public CarDirection myDirection;
    public float CarValue = 10000f;
    public float StopDistance = 2f;
    public TrafficLightController intersectionCtrl;

    private bool stoppedByLight = false;
    public bool stoppedByCar = false;
    void Update()
    {
        if (!stoppedByLight && !stoppedByCar)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("IntersectionZone") && intersectionCtrl != null)
        {
            string dir = myDirection.ToString();
            bool canGo = intersectionCtrl.CanGo(dir);

            // Debug 调试输出
            Debug.Log($"🚗 {gameObject.name} @ {dir}: CanGo = {canGo}");

            stoppedByLight = !canGo;
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

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("IntersectionZone"))
        {
            stoppedByLight = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board") && GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(CarValue);
            Destroy(gameObject);
        }
    }
}
