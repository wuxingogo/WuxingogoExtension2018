using UnityEngine;
using System.Collections;
using System;

public class XAttribute : Attribute
{
    public string title;
 
    public XAttribute(string tooltip)
    {
        this.title = tooltip;
    }
}