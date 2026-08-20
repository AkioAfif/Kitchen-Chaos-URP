using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;

    private float spawnPlateTimer;
    private float spawnPlateTimeMax= 4f;
    private int spawnPlateAmount;
    private int spawnPlateAmountMax = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimeMax)
        {
            spawnPlateTimer = 0;
            if (spawnPlateAmount < spawnPlateAmountMax)
            {
                spawnPlateAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player not carrying anything
            if (spawnPlateAmount > 0)
            {
                spawnPlateAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateTaken?.Invoke(this, EventArgs.Empty);

            }
            
        }
        else
        {
            //Player Carrying Object
        }
    }
}
