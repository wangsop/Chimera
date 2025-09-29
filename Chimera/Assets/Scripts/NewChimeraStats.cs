using System;
using UnityEngine;

public class NewChimeraStats
{
    #region Attributes

    public GameObject Head;
    public GameObject Body;
    public GameObject Tail;

    /*
     * Holds the base of the chimera's object for the head, body, and tail.
     * Helps for later instantiation. Anything a chimera must have is included in it save for the head, body, and tail.
     */
    public GameObject BaseObject;
    public int level;
    public int exp;

    #endregion

    #region Constructor

    public NewChimeraStats(GameObject h, GameObject b, GameObject t, GameObject baseobj)
    {
        Head = h;
        Body = b;
        Tail = t;
        BaseObject = baseobj;
        level = 1;
        exp = 0;
    }

    #endregion

    #region Override Methods

    /* Required to override for Equals(), which is implemented below.
     * No unique changes. 
     */
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /* Determines if two ChimeraStats are equal.
     * Since we know every chimera has a unique head, body, and tail, we only compare those aspects to see if they are equal.
     * This helps with avoiding duplicates when pulling a chimera from the gacha system.
     */
    public override bool Equals(object obj)
    {
        NewChimeraStats newChimera = (NewChimeraStats)obj;
        return newChimera.Head.Equals(Head) && newChimera.Body.Equals(Body) && newChimera.Tail.Equals(Tail);
    }

    public override string ToString()
    {
        return "Chimera: {Head: " + Head.name + ", Body:" + Body.name + ", Tail:" + Tail.name + ", Level: " + level + ", XP: " + exp + "}";
    }

    #endregion
}
