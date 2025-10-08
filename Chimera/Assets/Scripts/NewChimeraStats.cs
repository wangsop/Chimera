using System;
using UnityEngine;
using System.Collections.Generic;

public class NewChimeraStats
{
    #region Attributes

    public GameObject Head;
    public GameObject Body;
    public GameObject Tail;
    public string Name;

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
        Name = $"{GetPartName(Head.name, 0)}-{GetPartName(Body.name, 1)}-{GetPartName(Tail.name, 2)}";
    }
    #endregion

    #region Naming Chimera
    private string GetPartName(string name, int part)
    {
        // part: 0=head, 1=body, 2=tail
        name = name.Replace("Prefab", "").Replace("Head", "").Replace("Body", "").Replace("Tail", "").Trim();
        var names = SplitByUppercase(name);
        if (names.Count == 2)
        {
            int index = names[0].Length > names[1].Length ? 0 : 1;
            names.Insert(index + 1, names[index].Substring(names[index].Length / 2, names[index].Length - names[index].Length / 2));
            names[index] = names[index].Substring(0, names[index].Length / 2 + 1);
        }
        else if (names.Count == 1)
        {
            int length = names[0].Length / 3;
            if (length >= 2)
            {
                names.Insert(1, names[0].Substring(length, length));
                names.Insert(2, names[0].Substring(length * 2, names[0].Length - length * 2));
                names[0] = names[0].Substring(0, length);
            }
            else if (length == 1)
            {
                names.Insert(1, names[0].Substring(1, 2));
                names.Insert(2, names[0].Substring(2, names[0].Length - 2));
                names[0] = names[0].Substring(0, 2);
            }
        }

        return part < names.Count ? names[part] : name;
    }

    private List<string> SplitByUppercase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return new List<string>();

        var parts = new List<string>();
        int wordStart = 0;

        for (int i = 1; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                parts.Add(input.Substring(wordStart, i - wordStart));
                wordStart = i;
            }
        }
        parts.Add(input.Substring(wordStart));
        return parts;
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
        return newChimera.Head.GetComponent<Head>().Equals(this.Head.GetComponent<Head>()) && newChimera.Body.GetComponent<Body>().Equals(this.Body.GetComponent<Body>()) && newChimera.Tail.GetComponent<Tail>().Equals(this.Tail.GetComponent<Tail>());
    }

    public override string ToString()
    {
        return "Chimera: {Head: " + Head.name + ", Body:" + Body.name + ", Tail:" + Tail.name + ", Level: " + level + ", XP: " + exp + "}";
    }

    #endregion
}

public class GameObjectChimera
{
    public GameObject Head;
    public GameObject Body;
    public GameObject Tail;
    public GameObject Parent;

    #region Constructor

    public GameObjectChimera(GameObject h, GameObject b, GameObject t, GameObject p)
    {
        Head = h;
        Body = b;
        Tail = t;
        Parent = p;
    }
    #endregion
}
