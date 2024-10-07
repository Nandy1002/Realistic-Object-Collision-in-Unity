using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ColorGenerator 
{
    public static Color SetRandomColor(int i)
    {  
        List<Color> colors = new List<Color>();
        colors.Add(Color.red);
        colors.Add(Color.blue);
        colors.Add(Color.green);
        colors.Add(Color.yellow);
        colors.Add(Color.magenta);
        colors.Add(Color.cyan);
        return colors[i];
    }
}
