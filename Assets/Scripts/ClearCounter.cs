using System;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
       if (!HasKitchenObject())
        {
            //theres no kitchen object here
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }

        }
       else
        {
            //theres kitchen object here
            if (player.HasKitchenObject())
            {
                // player carrying something
            }
            else
            {
                //player not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

    

}
