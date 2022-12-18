using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkedClientProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromServer(string msg)
    {
        Debug.Log("msg received = " + msg + ".");

        string[] csv = msg.Split(',');

        switch (csv[0])
        {
            case "Player":
                networkedClient.CanBeHost = false;
                networkedClient.StartGame();
                break;
            case "Ball":
                gameLogic.SpawnNewBallonFromHost(StringToVector2(csv[1], csv[2]));
                break;
            case "Destroy":
                gameLogic.DestroyBallon(int.Parse(csv[1]));
                break;
            case "History":
                GameState(csv[1]);
                break;
            default:
                break;
        }

    }

    static public Vector2 StringToVector2(string X, string Y)
    {
        Vector2 temp;
        temp = new Vector2(float.Parse(X), float.Parse(Y));
        return temp;
    }

    static public void SendMessageToServer(string msg)
    {
        networkedClient.SendMessageToServer(msg);
    }

    static public void GameState(string msg)
    {
        string[] pos = msg.Split(':');
        foreach (string sPos in pos)
        {
            if (sPos != null || sPos != "")
            {
                string[] coord = sPos.Split(';');
                float X = float.Parse(coord[0]);
                float Y = float.Parse(coord[1]);
                gameLogic.SpawnNewBallonFromHost(new Vector2(X, Y)); 
            }
        }
    }

    static public void SendIsHost()
    {
        networkedClient.SendMessageToServer("Host");
    }
    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
        if (!(GetNetworkedClient().CanBeHost))
        {
            GetNetworkedClient().StartGame();
        }
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkedClient.IsConnected();
    }
    static public void ConnectToServer()
    {
        networkedClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkedClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkedClient networkedClient;
    static GameLogic gameLogic;

    static public void SetNetworkedClient(NetworkedClient NetworkedClient)
    {
        networkedClient = NetworkedClient;
    }
    static public NetworkedClient GetNetworkedClient()
    {
        return networkedClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int asd = 1;
}

static public class ServerToClientSignifiers
{
    public const int asd = 1;
}

#endregion

