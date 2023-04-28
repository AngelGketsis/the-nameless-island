using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    // track which waypoint is currently targeted
    public int waypointIndex;

    public override void Enter()
    {

    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {
        
    }

    public void PatrolCycle()
    {
        // implement patrol logic
        if(npc.Agent.remainingDistance < 0.2f)
        {
            if(waypointIndex < npc.path.waypoints.Count - 1)
            {
                waypointIndex++;
            }
            else
            {
                waypointIndex = 0;
            }

            npc.Agent.SetDestination(npc.path.waypoints[waypointIndex].position);
        }
    }
}
