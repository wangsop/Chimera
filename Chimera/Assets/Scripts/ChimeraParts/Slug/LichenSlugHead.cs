using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LichenSlugHead : Head
{
    [SerializeField] private GameObject whirlpool;
    [SerializeField] private Transform whirlpoolSpawnPoint;

    private GameObject whirlpoolInstance;

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
        ability_description = "Slug >:)";
        scientist_description = "Wow a lad";
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
