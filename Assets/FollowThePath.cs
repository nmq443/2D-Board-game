using UnityEngine;

public class FollowThePath : MonoBehaviour {

    public Transform[] waypoints;
    public int[] milestones = {4, 9, 14, 19, 24, 29};

    [SerializeField]
    private float moveSpeed = 1f;

    [HideInInspector]
    public int waypointIndex = 0;

    public bool moveAllowed = false;
    public int steps = 1;

	// Use this for initialization
	private void Start () {
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	private void Update () {
        if (moveAllowed)
            Move();
	}

    public void update() {
        Update();
    }

    public void move() {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position,
            moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += steps;
                Debug.Log("waypoint index: " + waypointIndex.ToString());
            }
        }
    }

    public int checkMilestones(int index)
    {
        for(int i = 0; i < milestones.Length; i++)
        {
            if(index == milestones[i]) 
                return milestones[i];
        }
        return -1;
    }

    public void changeDirection() {
        moveSpeed *= -1;
    }
}
