using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarMove : MonoBehaviour
{
    public NavMeshAgent agent;


    private Vector3 destination;//original destination/target
    public Light green;//stoplight
    public Light red;//stoplight
    private bool stop = false;
    


    [SerializeField]
    private LayerMask layerMask;
    public float maxDistance = 50.0f;//max distance for ray from car
    RaycastHit raycastHit;//hit
    GameObject hit;



    private void OnEnable()//this works
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.Warp(RecycleRoad.newStart);//warp into random startposition
       
        destination = RecycleRoad.newStop;
    }

    
    void Update()
    {

        if (transform.gameObject.activeInHierarchy == true)//only when car is active
        {

            agent.SetDestination(destination);//go to destination
            //Debug.Log(destination);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Raycast
            Vector3 origin = new Vector3(transform.position.x, 0.5f, transform.position.z);//origin of raycast from center of cube
            Vector3 direction = transform.forward;//direction of raycast

            Ray ray = new Ray(origin, direction);//car raycast

            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
            {
               
                stop = true;//has encountered stopline
                hit = raycastHit.transform.gameObject;

                hit.GetComponent<Renderer>().material.color = Color.red;//change color
                Debug.DrawRay(origin, direction * maxDistance, Color.red);//draw it out
                //Debug.Log(hit.name);
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //-- If navMeshAgent is still looking for a path then use line test; setting destinations takes a little time so you check pathpending first
            if (agent.pathPending)
            {
                float dist = Vector3.Distance(transform.position, destination);
                if (dist < 2.5f)
                {
                    transform.gameObject.SetActive(false);//deactivate until its called again
                   //Debug.Log("deactivate");
                                                                           
                }
                else
                {

                    // StopProccedure( destination, hit, stop);//this works better

                    if (red.enabled == true && stop == true)//stop because it's a red light
                    {

                        if (hit.transform.tag == "Stop")
                        {

                            if (agent.destination != hit.transform.position)//if car is not yet there it goes there 
                            {
                                agent.SetDestination(hit.transform.position);//the reason this might be late is because of the path pending thing :/

                                if (agent.pathPending)
                                {
                                    float dist1 = Vector3.Distance(transform.position, hit.transform.position);//if path is still being decided and information has not loaded
                                    if (dist1 < 20.0f)
                                    {
                                        agent.Move(Vector3.zero);//stop moving once in stop position
                                        Debug.Log("detect");
                                        //Debug.Log("true");

                                    }
                                }
                                else
                                    Debug.Log("not");


                            }
                            else
                            {
                                agent.Move(Vector3.zero);//stop moving once in stop position
                            }
                        }

                        //test
                        else if (hit.transform.tag == "Car")
                        {

                            float distance1 = Vector3.Distance(hit.transform.position, transform.position);//distance between objects

                            if (distance1 < 20.0f)//stop in these units
                            {
                                agent.Move(Vector3.zero);//stop moving 
                                agent.SetDestination(this.transform.position);//stop where car is
                                Debug.Log("car");
                            }
                            else
                            {
                                agent.SetDestination(hit.transform.position);//stop
                                if (agent.pathPending)
                                {
                                    float dist1 = Vector3.Distance(transform.position, hit.transform.position);//if path is still being decided and information has not loaded
                                    if (dist1 < 20.0f)
                                    {
                                        agent.Move(Vector3.zero);//stop moving once in stop position
                                                                 //Debug.Log("detect");

                                    }
                                }
                            }
                        }
                    }
                    else if (green.enabled == true && stop == true)//light turns green so go to original target
                    {

                        agent.SetDestination(destination);

                        if (agent.pathPending)
                        {
                            float dist2 = Vector3.Distance(transform.position, destination);//if path is still being decided and information has not loaded
                            if (dist2 < 2.5f)
                            {
                                transform.gameObject.SetActive(false);//deactivate until its called again
                            }
                        }

                        stop = false;
                    }


                }
            }
            else if (!agent.pathPending)
            {
                if (agent.remainingDistance < 5)
                {
                    transform.gameObject.SetActive(false);//deactivate until its called again
                    //Debug.Log("deactivate");

                }
               else
                {

                    // StopProccedure(destination, hit, stop);//this works better

                    if (red.enabled == true && stop == true)//stop because it's a red light
                    {

                        if (hit.transform.tag == "Stop")
                        {

                            if (agent.destination != hit.transform.position)//if car is not yet there it goes there 
                            {
                                agent.SetDestination(hit.transform.position);//the reason this might be late is because of the path pending thing :/

                                if (agent.pathPending)
                                {
                                    float dist1 = Vector3.Distance(transform.position, hit.transform.position);//if path is still being decided and information has not loaded
                                    if (dist1 < 20.0f)
                                    {
                                        agent.Move(Vector3.zero);//stop moving once in stop position
                                        Debug.Log("detect");

                                    }
                                }

                            }
                            else
                            {
                                agent.Move(Vector3.zero);//stop moving once in stop position
                            }
                        }

                        //test
                        else if (hit.transform.tag == "Car")
                        {

                            float distance1 = Vector3.Distance(hit.transform.position, transform.position);//distance between objects

                            if (distance1 < 20.0f)//stop in these units
                            {
                                agent.Move(Vector3.zero);//stop moving 
                                agent.SetDestination(this.transform.position);//stop where car is
                                Debug.Log("car");
                            }
                            else
                            {
                                agent.SetDestination(hit.transform.position);//stop
                                if (agent.pathPending)
                                {
                                    float dist1 = Vector3.Distance(transform.position, hit.transform.position);//if path is still being decided and information has not loaded
                                    if (dist1 < 20.0f)
                                    {
                                        agent.Move(Vector3.zero);//stop moving once in stop position
                                                                 //Debug.Log("detect");

                                    }
                                }
                            }
                        }
                    }
                    else if (green.enabled == true && stop == true)//light turns green so go to original target
                    {

                        agent.SetDestination(destination);

                        if (agent.pathPending)
                        {
                            float dist2 = Vector3.Distance(transform.position, destination);//if path is still being decided and information has not loaded
                            if (dist2 < 2.5f)
                            {
                                transform.gameObject.SetActive(false);//deactivate until its called again
                            }
                        }

                        stop = false;
                    }
                }
            }
            
        }

    }

 
  private void StopProccedure(Vector3 destination, GameObject hit, bool stop)
    {
        agent.SetDestination(destination);

        if (red.enabled == true && stop == true)//stop because it's a red light
        {

            if (hit.transform.tag == "Stop")
            {
               
                if (agent.destination != hit.transform.position)//if car is not yet there it goes there 
                {
                    agent.SetDestination(hit.transform.position);//the reason this might be late is because of the path pending thing :/

                    if (agent.pathPending)
                    {
                        float dist1 = Vector3.Distance(transform.position, hit.transform.position);//if path is still being decided and information has not loaded
                        if (dist1 < 20.0f)
                        {
                            agent.Move(Vector3.zero);//stop moving once in stop position
                            Debug.Log("detect");

                        }
                    }

                }
                else
                {
                    agent.Move(Vector3.zero);//stop moving once in stop position
                }
            }

            //test
           else if (hit.transform.tag == "Car")
            {
               
                float distance1 = Vector3.Distance(hit.transform.position, transform.position);//distance between objects

                if (distance1 < 20.0f)//stop in these units
                {
                    agent.Move(Vector3.zero);//stop moving 
                    agent.SetDestination(this.transform.position);//stop where car is
                    Debug.Log("car");
                }
                else
                {
                    agent.SetDestination(hit.transform.position);//stop
                    if (agent.pathPending)
                    {
                        float dist1 = Vector3.Distance(transform.position, hit.transform.position);//if path is still being decided and information has not loaded
                        if (dist1 < 20.0f)
                        {
                            agent.Move(Vector3.zero);//stop moving once in stop position
                            //Debug.Log("detect");

                        }
                    }
                }
            }
        }
        else if (green.enabled == true && stop == true)//light turns green so go to original target
        {
            
            agent.SetDestination(destination);

            if (agent.pathPending)
            {
                float dist2 = Vector3.Distance(transform.position, destination);//if path is still being decided and information has not loaded
                if (dist2 < 2.5f)
                {
                    transform.gameObject.SetActive(false);//deactivate until its called again
                }
            }

            stop = false;
        }

    }

  
  }

