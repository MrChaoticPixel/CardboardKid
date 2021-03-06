﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemies : MonoBehaviour
{

    public NavMeshAgent NMA;
    public NavMeshPath path;
    public GameObject[] wps;
    public float timeFrNewPath, Chasespd, MaxDis, MinDis;
    public bool inCoRu, validPath, ChaseMd, RoamMd, playerVis, WaitTimer, GuardVis, chasing;
    public Vector3 target;
    public int Mode;
    public Transform player;
    public Transform[] Guards;
    public GameObject Plyr, Self;
    public MeshRenderer SelfMR;
    public Material Red, Blue;
    public Player playS;
    


    // Use this for initialization
    void Start()
    {
        
        Self = gameObject;
        SelfMR = GetComponent<MeshRenderer>();
        NMA = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        Invoke("WaitTime", 3);
        Chasespd = 60;
        MaxDis = 10;
        MinDis = 5;
        Mode = 2;
        RoamMd = false;
        ChaseMd = false;
       

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (playS.inbox == true)
        {
            ChaseMd = false;
            playerVis = false;
            Physics.IgnoreCollision(GameObject.Find("Player").GetComponent <BoxCollider>(), GetComponent<Collider>());
        }
            NavMeshHit hit;
            if (!NMA.Raycast(player.position, out hit))
            {
                if (playS.inbox == false)
                {
                    playerVis = true;
                }
            }
            else
            {
                playerVis = false;
            }
            if (Mode == 0)
        {
            RoamMd = true;
            ChaseMd = false;
        }
        if (Mode == 1)
        {
            RoamMd = false;
            ChaseMd = true;
        }
        if (RoamMd == true)
        {
            if (NMA.remainingDistance < 0.5)
            {
                int d = Random.Range(0, wps.Length);
                NMA.SetDestination(wps[d].transform.position);
            }
        }
        ChaseMode();
    }
    public void WaitTime()
    {
        Mode = 0;
        RoamMd = true;
        int d = Random.Range(0, wps.Length);
        NMA.SetDestination(wps[d].transform.position);
    }
    public void ChaseMode()
    {
        if (ChaseMd == true)
        {
            
            if (playerVis == true)
            {
                SelfMR.material = Red;
                transform.LookAt(player);
                NMA.acceleration = 300;
                NMA.speed = 80;
                NMA.SetDestination(player.transform.position);

            }
            else
            {
                SelfMR.material = Blue;
                NMA.speed = 60;
                NMA.acceleration = 20;
                int d = Random.Range(0, wps.Length);
                NMA.SetDestination(wps[d].transform.position);
            }
        }
        if (ChaseMd == false)
        {

            SelfMR.material = Blue;

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyAI")
        {
            if (RoamMd == true)
            {
                int d = Random.Range(0, wps.Length);
                NMA.SetDestination(wps[d].transform.position);
            }

        }
        if (collision.gameObject.tag == "Player")
        {
            if (RoamMd == true)
            {
                NMA.SetDestination(-player.transform.position);
            }

        }
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemyAI")
        {
            if (RoamMd == true)
            {
                int d = Random.Range(0, wps.Length);
                NMA.SetDestination(wps[d].transform.position);
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            if (RoamMd == true)
            {
                NMA.SetDestination(-player.transform.position);
            }

        }
    }
   
}
