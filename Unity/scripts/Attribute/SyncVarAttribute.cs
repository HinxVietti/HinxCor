using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class SyncVarAttribute : Attribute
{
    public string hock { get; set; }
}
