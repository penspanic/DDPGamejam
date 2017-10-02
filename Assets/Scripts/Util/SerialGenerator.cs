using UnityEngine;
using System.Collections;

public class SerialGenerator
{
    private int number = 0;

    public int Get()
    {
        return ++number;
    }
}