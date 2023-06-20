using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    private static GameObject whoWinsTextShadow, player1MoveText, player2MoveText;

    private static GameObject player1, player2;

    public static int diceSideThrown = 0;
    public static int player1StartWaypoint = 0;
    public static int player2StartWaypoint = 0;

    public static bool gameOver = false;

    // Use this for initialization
    void Start () {

        whoWinsTextShadow = GameObject.Find("WhoWinsText");
        player1MoveText = GameObject.Find("Player1MoveText");
        player2MoveText = GameObject.Find("Player2MoveText");

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        player1.GetComponent<FollowThePath>().moveAllowed = false;
        player2.GetComponent<FollowThePath>().moveAllowed = false;

        whoWinsTextShadow.gameObject.SetActive(false);
        player1MoveText.gameObject.SetActive(true);
        player2MoveText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<FollowThePath>().waypointIndex > 
            player1StartWaypoint + diceSideThrown)
        {
            player1.GetComponent<FollowThePath>().moveAllowed = false;
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(true);
            player1StartWaypoint = player1.GetComponent<FollowThePath>().waypointIndex - 1;
            int milestone_index = player1.GetComponent<FollowThePath>().checkMilestones(player1StartWaypoint) + 1;
            Debug.Log("milestone index: " + milestone_index.ToString());
            if (milestone_index != 0) // next turn player1 can only move (diceSide - backSteps) steps
            {
                int backSteps = 0;
                switch(milestone_index) {
                    case 5:
                        backSteps = 1;
                        break;
                    case 9:
                        backSteps = 2;
                        break;
                    case 14:
                        backSteps = 4;
                        break;
                    case 20:
                        backSteps = 8;
                        break;
                    case 25:
                        backSteps = 16;
                        break;
                    case 30:
                        backSteps = 20;
                        break;
                }
                player1.GetComponent<FollowThePath>().waypointIndex -= backSteps;
                player1StartWaypoint = player1.GetComponent<FollowThePath>().waypointIndex - 1;
            }
        }

        if (player2.GetComponent<FollowThePath>().waypointIndex >
            player2StartWaypoint + diceSideThrown)
        {
            player2.GetComponent<FollowThePath>().moveAllowed = false;
            player2MoveText.gameObject.SetActive(false);
            player1MoveText.gameObject.SetActive(true);
            player2StartWaypoint = player2.GetComponent<FollowThePath>().waypointIndex - 1;
            int milestone_index = player2.GetComponent<FollowThePath>().checkMilestones(player2StartWaypoint) + 1;
            Debug.Log("milestone index: " + milestone_index.ToString());
            if (milestone_index != 0)
            {
                int backSteps = 0;
                switch(milestone_index) {
                    case 5:
                        backSteps = 1;
                        break;
                    case 9:
                        backSteps = 2;
                        break;
                    case 14:
                        backSteps = 4;
                        break;
                    case 20:
                        backSteps = 8;
                        break;
                    case 25:
                        backSteps = 16;
                        break;
                    case 30:
                        backSteps = 20;
                        break;
                }
                player2.GetComponent<FollowThePath>().waypointIndex -= backSteps;
                player2StartWaypoint = player2.GetComponent<FollowThePath>().waypointIndex - 1;
            }
        }

        if (player1.GetComponent<FollowThePath>().waypointIndex == 
            player1.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            whoWinsTextShadow.GetComponent<Text>().text = "Player 1 Wins";
            gameOver = true;
        }

        if (player2.GetComponent<FollowThePath>().waypointIndex ==
            player2.GetComponent<FollowThePath>().waypoints.Length)
        {
            whoWinsTextShadow.gameObject.SetActive(true);
            player1MoveText.gameObject.SetActive(false);
            player2MoveText.gameObject.SetActive(false);
            whoWinsTextShadow.GetComponent<Text>().text = "Player 2 Wins";
            gameOver = true;
        }
    }

    public static void MovePlayer(int playerToMove)
    {
        switch (playerToMove) { 
            case 1:
                player1.GetComponent<FollowThePath>().moveAllowed = true;
                break;

            case 2:
                player2.GetComponent<FollowThePath>().moveAllowed = true;
                break;
        }
    }
}
