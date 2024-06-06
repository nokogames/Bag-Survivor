using System;
using UnityEngine;



public class ExecuteWithButtonAttribute : Attribute
{
    public readonly string buttonLabel; // Düğme etiketi

    public ExecuteWithButtonAttribute(string buttonLabel)
    {
        this.buttonLabel = buttonLabel;
    }
}
