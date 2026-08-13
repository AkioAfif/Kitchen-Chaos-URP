using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler onPlayerGrabbedObject;


    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            //Player not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            onPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        } else
        {
            //Player Carrying Object
        }
       
    }


}
