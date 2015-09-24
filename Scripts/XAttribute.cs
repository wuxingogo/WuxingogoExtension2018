using UnityEngine;
using System.Collections;
using System;

public class XAttribute : Attribute
{
    public string tooltip;
 
    public XAttribute(string tooltip, string st)
    {
        this.tooltip = tooltip;
    }
}