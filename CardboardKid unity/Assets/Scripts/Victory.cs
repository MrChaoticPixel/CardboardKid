﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    public GoogleAnalyticsV4 G4;
    public Player P;
    public MeshRenderer VicMesRen;
    public Material closed, open;
    public bool Op, VictoryOn;
    public AudioSource Vic;
    // Use this for initialization
    void Start () {
        VictoryOn = false;
        Op = false;
        VicMesRen = GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (P.score < 30)
        {
            Op = false;
            VicMesRen.material = closed;
        }
        if (P.score >= 30)
        {
            Op = true;
            VicMesRen.material = open;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           if (Op == true)
            {
                Vic.Play();
                VictoryOn = true;
                P.canbox = false;
                Time.timeScale = 0;
                Debug.Log("YouWin " + "Time: " + Timer.TimeScoreHours + ":" + Timer.TimeScoreMin + ":" + Timer.TimeScoreSeconds.ToString("f2"));
                G4.LogEvent("finalscore", "score", "gamevictory", 1);

                // Builder Hit with all Event parameters.
                G4.LogEvent(new EventHitBuilder()
                    .SetEventCategory("finalscore")
                    .SetEventAction("score")
                    .SetEventLabel("gamevictory")
                    .SetEventValue(1));

                Debug.Log("Sent");
            }
        
        }
    }
}
