using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    public SecurityData secDATA;
    
    public float wanderRadius;
    public Vector3 origin;

    public Vector3 target;

    public float idleTimer;
    public AreaScript currentArea;

    [System.NonSerialized]
    public NavMeshAgent NMA;
    [System.NonSerialized]
    public MapManager MM;
    public Animator ANIM;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        Wander();
        Idle();

        AiTick();
    }

    public void AiTick() {
        ANIM.SetFloat("Speed", Mathf.Abs(NMA.velocity.magnitude));
    }

    public void MoveToPoint(Vector3 movePos) {
        NMA.destination = movePos;
    }

    public void Setup() {
        ANIM = gameObject.transform.GetChild(0).GetComponent<Animator>();
        MM = FindObjectOfType<MapManager>();
        NMA = GetComponent<NavMeshAgent>();
        origin = transform.position;
    }

    public void Idle() {
        if (idleTimer > 0) {
            NMA.isStopped = true;

            idleTimer -= Time.deltaTime;

            Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 1 << 6);
            if (colliders.Length > 0) {
                Vector3 direction = colliders[0].transform.position - transform.position;
                Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1f * Time.deltaTime);
            }
        }
        else {
            NMA.isStopped = false;
        }
    }

    public void Wander() {
        if (NMA.isActiveAndEnabled && NMA.destination != null) {
            if (NMA.remainingDistance <= 2f && idleTimer <= 0) {
                int largeMoveIndex = Random.Range(0, 10);
                if (largeMoveIndex == 0) {
                    //target = MM.getRandomPosOnMap();
                }
                else {
                    if (currentArea != null)
                        target = MM.getRandomPosWithinRect(currentArea.transform.position, currentArea.areaDimensions, false);
                    else
                        target = MM.getRandomPosWithinRect(MM.origin, MM.mapDimensions, true);
                }

                NMA.SetDestination(target);
                idleTimer += Random.Range(1f, 10f);
            }
        }
    }
    
}
