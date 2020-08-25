using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class SpeedDisplay : MonoBehaviour
{
    public Text speed;
    public float velocity;
    private float carX1, carX2, carY1, carY2;
    [SerializeField] GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        speed.text = "0";
        car = GameObject.FindGameObjectWithTag("car");
        carX1 = car.transform.position.x;
        carY1 = car.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        car = GameObject.FindGameObjectWithTag("car");
        carX2 = car.transform.position.x;
        carY2 = car.transform.position.z;
        velocity = Distance() / Time.deltaTime;
        carX1 = carX2;
        carY1 = carY2;
        speed.text = velocity.ToString();
    }

    private float Distance()
    {
        return Mathf.Sqrt(Mathf.Pow((carX2 - carX1), 2) + Mathf.Pow((carY2 - carY1), 2));
    }
}
