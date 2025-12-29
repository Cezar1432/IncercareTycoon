using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour


{
    [SerializeField] GameObject[] npcs= new GameObject[18];
    Vector3 spawnPose, exitPosition;
    Quaternion spawnRotation;
    Vector3[] movePoses= new Vector3[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    Animator animator;
    public enum State
    {
        GOING_IN_THE_CAFE, MOVING_AROUND, LEAVING_ENTERANCE, LEAVING_SIDEWALK
    }
    State state = State.GOING_IN_THE_CAFE;
    int pointsReached = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if(animator!= null)
        {
            animator.SetTrigger("walk");

        }
        for (int i = 0; i < movePoses.Length; i++)
        {
            movePoses[i] = new Vector3(Random.Range(4f, 46f), 0, Random.Range(7f, 48f));
        }
        if (transform.position.x > 25) 
        {
            exitPosition = new Vector3(6.5f, 0, 0f);
        }
        else 
        {
            exitPosition = new Vector3(6.5f, 0, 50f);
        }

    }
    void setState(State state)
    {
        this.state = state;
    }
    Vector3 cafeEnterance = new Vector3(6.5f, 0, 24.5f);
    public void Update()
    {
        switch (state)
        {
            case State.GOING_IN_THE_CAFE:
                {
                    
                    updateWalking(cafeEnterance);
                    if (finished(cafeEnterance))
                        setState(State.MOVING_AROUND);
                }
                break;
                case State.MOVING_AROUND:
                {
                    updateWalking(movePoses[pointsReached]);
                    if(finished(movePoses[pointsReached]))
                        pointsReached++;
                    if (pointsReached == 3)
                        setState(State.LEAVING_ENTERANCE);
                }break;

            case State.LEAVING_ENTERANCE:
                {
                    updateWalking(cafeEnterance);
                        if(finished(cafeEnterance))
                        setState(State.LEAVING_SIDEWALK);
                }
                break;
                case State.LEAVING_SIDEWALK:
                {
                    updateWalking(exitPosition);
                    if (finished(exitPosition)) 
                        Destroy(gameObject);
                }
                break;

        }
        
    }
    void updateWalking(Vector3 targetPos)
    {
        Vector3 dir = (targetPos- transform.position).normalized;
        transform.position += dir * 3 * Time.deltaTime;
        if(dir!= Vector3.zero)
        {
            transform.rotation= Quaternion.Euler(0, Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg, 0);
        }
    }

    bool finished(Vector3 targetPos)
    {
        return Vector3.Distance(transform.position, targetPos) < .5f;
    }
}
