using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStorage : MonoBehaviour
{
    public GameObject[] cars;//list of cars
    private bool value;//only true if one or more cars are active
    public float rate=6.0f;

    public void Awake()
    {
        value = true;
    }

    void Update()
    {

        value = CallCar(cars);

        if (value == false)
        {
            StartCoroutine(ExampleCoroutine());//this will be called once all cars are deactivated
            value = true;
        }

    }

    private bool CallCar(GameObject[] cars)
    {
        bool value = true;

        for(int i= 0;i < cars.Length - 1;i++)
        {
            if (cars[cars.Length-1].activeInHierarchy != true && cars[0].activeInHierarchy==false)//do more work on this
            {
                value = false;
            }
            else
            {
                value = true;
            }
        }

        return value;
    }

    IEnumerator ExampleCoroutine()//wait for ... seconds before car becomes active
    {
       
        foreach (GameObject car in cars)
         {
         
            yield return new WaitForSeconds(rate);
            car.SetActive(true);//works
           
        }

    }
}


