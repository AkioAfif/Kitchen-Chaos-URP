using Unity.Cinemachine;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{

    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;
    private State state;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        Debug.Log("Object fried");
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        state = State.Fried;
                        burningTimer = 0f;

                    }
                    break;
                case State.Fried:

                    
                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningTimeMax)
                    {
                        //burned
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        Debug.Log("Object burned");
                        state = State.Burned;
                    }
                    break;
                case State Burned:
                    break; 
            }
            Debug.Log(state);
        }
        
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //theres no kitchen object here
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                }
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
                state = State.Idle;
            }

        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return FryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (FryingRecipeSO != null)
        {
            return FryingRecipeSO.output;

        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO BurningRecipeSO in burningRecipeSOArray)
        {
            if (BurningRecipeSO.input == inputKitchenObjectSO)
            {
                return BurningRecipeSO;
            }
        }

        return null;
    }
}
