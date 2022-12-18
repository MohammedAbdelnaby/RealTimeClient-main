using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    float durationUntilNextBalloon;
    Sprite circleTexture;

    public List<GameObject> BallsOnScreen;

    void Start()
    {
        NetworkedClientProcessing.SetGameLogic(this);
    }
    void Update()
    {
        if (NetworkedClientProcessing.GetNetworkedClient().IsHost)
        {
            durationUntilNextBalloon -= Time.deltaTime;

            if (durationUntilNextBalloon < 0)
            {
                durationUntilNextBalloon = 2f;

                float screenPositionXPercent = Random.Range(0.0f, 1.0f);
                float screenPositionYPercent = Random.Range(0.0f, 1.0f);
                Vector2 screenPosition = new Vector2(screenPositionXPercent * (float)Screen.width, screenPositionYPercent * (float)Screen.height);
                SpawnNewBalloon(screenPosition);
            } 
        }
    }

    public void SpawnNewBalloon(Vector2 screenPosition)
    {
        if(circleTexture == null)
            circleTexture = Resources.Load<Sprite>("Circle");

        GameObject balloon = new GameObject("Balloon");

        balloon.AddComponent<SpriteRenderer>();
        balloon.GetComponent<SpriteRenderer>().sprite = circleTexture;
        balloon.AddComponent<CircleClick>();
        balloon.AddComponent<CircleCollider2D>();

        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
        if (pos != Vector3.zero)
        {
            pos.z = 0;
            balloon.transform.position = pos;
            BallsOnScreen.Add(balloon);
            if (NetworkedClientProcessing.GetNetworkedClient().IsHost)
            {
                NetworkedClientProcessing.SendMessageToServer("Ball," + pos.x + "," + pos.y);
            } 
        }
        //go.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
    }

    public void SpawnNewBallonFromHost(Vector2 pos)
    {
        if (pos != Vector2.zero)
        {
            if (circleTexture == null)
                circleTexture = Resources.Load<Sprite>("Circle");

            GameObject balloon = new GameObject("Balloon");

            balloon.AddComponent<SpriteRenderer>();
            balloon.GetComponent<SpriteRenderer>().sprite = circleTexture;
            balloon.AddComponent<CircleClick>();
            balloon.AddComponent<CircleCollider2D>();

            balloon.transform.position = pos;
            BallsOnScreen.Add(balloon);
        }
        else
        {
            BallsOnScreen.Add(null);
        }
    }

    public void DestroyBallon(int ballonIndex)
    {
        Destroy(BallsOnScreen[ballonIndex]);
        Debug.Log(BallsOnScreen[ballonIndex]);
    }

}

