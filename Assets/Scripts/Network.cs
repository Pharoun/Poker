using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Network : NetworkBehaviour
{
    [HideInInspector] public int clientsCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        clientsCount++;
    }
}
