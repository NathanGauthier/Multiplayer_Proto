using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using System;

public class Unit : NetworkBehaviour
{
    [SerializeField] private UnitsMovement unitMovement = null;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;

    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }


    public override void OnStartClient()
    {
        if(isServer || !hasAuthority) return ;
        AuthorityOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        if(isServer || !hasAuthority) return ;

        AuthorityOnUnitDespawned?.Invoke(this);
    }

    //[Client]
    public void Select()
    {
        if(!hasAuthority) return ;
        onSelected?.Invoke();
    }

   // [Client]
    public void Deselect()
    {
        if(!hasAuthority) return ;
        onDeselected?.Invoke();
    }

    public UnitsMovement GetUnitMovement()
    {
        return unitMovement;
    }
}
