using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SyntaxError
{
    public List<Error> Errors { get; set; } = new();

    public void AddError(string value, int line, int column, string message)
    {
        Errors.Add(new Error(value, line, column, message));
    }
    public bool HasErrors()
    {
        return Errors.Count > 0;
    }
}
