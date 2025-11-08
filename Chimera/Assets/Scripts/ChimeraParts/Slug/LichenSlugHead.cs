using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LichenSlugHead : Head
{
    [SerializeField] private GameObject whirlpool;
    [SerializeField] private Transform whirlpoolSpawnPoint;

    private GameObject whirlpoolInstance;
    //public override int rarity { get; set; } = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility()
    {
        Debug.Log("Used Lichen Slug Ability");

        // spawn whirlpool
        whirlpoolInstance = Instantiate(whirlpool, whirlpoolSpawnPoint.position, Quaternion.identity);
        Debug.Log(whirlpoolSpawnPoint.position);
    }
    protected override void Initialize()
    {
        index = 0;
        ability_name = "Whirlpool";
        ability_description = "Creates a little whirlpool of petals that will move around separately and deal damage to anything that gets in its way.";
        scientist_description = "Ah... my first creature. Its goo has quite lovely preserving properties... hopefully it keeps her in good condition until I find it.";
        base.Initialize();
    }

    // honestly not sure when the UseAbility() function is called, so I'm testing with an Update() function
    /*
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // spawn whirlpool
            whirlpoolInstance = Instantiate(whirlpool, whirlpoolSpawnPoint.position, Quaternion.identity);
        }
    }
    */
}
