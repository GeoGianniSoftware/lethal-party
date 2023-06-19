using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGuard : NPCScript
{
    public List<Transform> patrolPoints;
    public int patrolIndex = -1;
    public float distanceToPoint = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(NMA.isActiveAndEnabled) {
            
            if(patrolIndex < 0) {
                IncrementPatrol();
            }
            else {
                MoveToPoint(patrolPoints[patrolIndex].position);

                if(NMA.remainingDistance <= distanceToPoint) {
                    IncrementPatrol();
                }
            }
        }

        AiTick();
    }

    void IncrementPatrol() {
        if(patrolIndex < patrolPoints.Count - 1) {
            patrolIndex++;
        }
        else {
            patrolIndex = 0;
        }
    }
}
