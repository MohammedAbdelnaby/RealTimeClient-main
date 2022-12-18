using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleClick : MonoBehaviour
{
    GameLogic gameLogic;
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
    }

    void OnMouseDown()
    {
        Destroy(this.gameObject);
        int i = gameLogic.BallsOnScreen.FindIndex(0, gameLogic.BallsOnScreen.Count, (GameObject obj) => obj == this.gameObject);
        Debug.Log(gameLogic.BallsOnScreen[i].transform.position);
        Debug.Log(i);
        NetworkedClientProcessing.SendMessageToServer("Destroy," + i);
    }
}
