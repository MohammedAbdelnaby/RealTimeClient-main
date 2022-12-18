using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void Host()
    {
        if (NetworkedClientProcessing.GetNetworkedClient().CanBeHost)
        {
            NetworkedClientProcessing.GetNetworkedClient().IsHost = true;
            NetworkedClientProcessing.SendIsHost();
            NetworkedClientProcessing.GetNetworkedClient().StartGame();
        }
    }
}
