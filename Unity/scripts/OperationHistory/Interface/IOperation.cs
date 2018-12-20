using System;
using System.Collections.Generic;
using UnityEngine;


public interface IOperation
{
    IOperation previour { get; set; }
    IOperation next { get; set; }

    void Undo();
    void Redo();
    void Enpack();
}
