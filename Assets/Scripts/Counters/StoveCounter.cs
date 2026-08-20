using System;
using Unity.Cinemachine;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgresChanged;
    public event EventHandler <OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
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
                    OnProgresChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {
                        //fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                    }
                    break;
                case State.Fried:

                    
                    burningTimer += Time.deltaTime;
                    OnProgresChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)burningTimer / burningRecipeSO.burningTimeMax
                    });
                    if (burningTimer > burningRecipeSO.burningTimeMax)
                    {
                        //burned
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Burned:
                    OnProgresChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                    break; 
            }
            
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
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    fryingTimer = 0f;

                    OnProgresChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
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

                    state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });

                    OnProgresChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });

                }
            }
            else
            {
                //player not carrying something
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgresChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }

        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return FryingRecipeSO != null;
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
