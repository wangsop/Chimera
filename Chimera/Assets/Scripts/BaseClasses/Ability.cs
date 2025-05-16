using UnityEngine;
using System.Collections;

public abstract class Ability : Entity
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void startAbility(); //triggers ability usage, updates independent of anything else
    public abstract void stopAbility(); //stops ability usage manually, if necessary

}
