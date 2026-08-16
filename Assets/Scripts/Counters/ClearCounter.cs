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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject platekitchenObject))
                {
                    //player is carrying a plate
                    if (platekitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                   
                } else
                {
                    // Player is not carrying a plate but carrying something else
                    if (GetKitchenObject().TryGetPlate(out platekitchenObject))
                    {
                        // Counter is Holding a plate
                        if (platekitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //player not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

    

}
