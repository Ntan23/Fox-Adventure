using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*LSP*/
public abstract class Respawners
{
    public virtual void Respawn()
    {
        
    }
}

public class EagleRespawner : Respawners
{
    public override void Respawn()
    {
        if(!GameManager.Instance.EagleIsNull()) GameManager.Instance.RespawnEagles();
    }
}


public class PowerUpRespawner : Respawners
{
    public override void Respawn()
    {
        if(!GameManager.Instance.PowerUpIsNull()) GameManager.Instance.RespawnPowerUps();
    }
}

public class Respawner : MonoBehaviour
{
    private enum Type {
        PowerUp , Eagle
    }

    [SerializeField] private Type objType;

    private PowerUpRespawner powerUpRespawner;
    private EagleRespawner eagleRespawner;

    public void RespawnObj(Respawners respawner)
    {
        respawner.Respawn();
    }

    void Start()
    {
        powerUpRespawner = new PowerUpRespawner();
        eagleRespawner = new EagleRespawner();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) 
        {
            if(objType == Type.Eagle) RespawnObj(eagleRespawner);
            if(objType == Type.PowerUp) RespawnObj(powerUpRespawner);
        }
    }
}

