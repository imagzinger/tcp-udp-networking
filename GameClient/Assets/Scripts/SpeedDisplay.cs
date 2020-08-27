using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class SpeedDisplay : MonoBehaviour
{
    public Text speed;
    private int index = 0;
    public float velocity;
    //public float[] velocity = { 0f, 0f, 0f};
    //public float avgVelocity;
    private float carX1, carX2, carY1, carY2;
    [SerializeField] GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        speed.text = "0";
        carX1 = car.transform.position.x;
        carY1 = car.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        car = GameObject.FindGameObjectWithTag("car");
        carX2 = car.transform.position.x;
        carY2 = car.transform.position.z;
        
        if (index == 3)
            index = 0;
        //velocity
        velocity = Distance() / Time.deltaTime;
        //avgVelocity = (velocity[0] + velocity[1] + velocity[2])/3;
        carX1 = carX2;
        carY1 = carY2;
        speed.text = ((int) (7 * velocity)).ToString();
        index++;
    }

    private float Distance()
    {
        return Mathf.Sqrt(Mathf.Pow((carX2 - carX1), 2) + Mathf.Pow((carY2 - carY1), 2));
    }
}
