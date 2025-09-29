using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

/*
 * This script is to manage the animation boolean parameters since there are many objects that will have similar parameters.
 * Instead of diving into each object everything is set in this one script and are accessed and set here.
 * Note that everything is set to be static so there is only one instance of each bool being used and no one will overlap so encapsulation is happyish.
*/
internal class AnimationStrings
{
    internal static string IsChimera = "isChimera";
    internal static string IsAlive = "isAlive";
    internal static string lockVelocity = "lockVelocity";
}