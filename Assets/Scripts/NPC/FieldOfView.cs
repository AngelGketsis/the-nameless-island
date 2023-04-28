using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private PlayerMotor motor;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

        if(playerRef != null)
        {
            motor = playerRef.GetComponent<PlayerMotor>();
            Debug.Log("turned player to motor1!!!!!!!");
        }

        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    Debug.Log("I can see player1111111111!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    motor.SeenPlayer();
                }
                else
                {
                    canSeePlayer = false;
                    Debug.Log("Saw player1");
                    motor.SeenPlayer();
                }
            }
            else
            {
                canSeePlayer = false;
                Debug.Log("Saw player2");
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            Debug.Log("I can see player!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            motor.SeenPlayer();
        }
    }
}