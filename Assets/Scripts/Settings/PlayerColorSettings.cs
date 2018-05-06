using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Player color settings", menuName = "Settings/Color settings")]
public class PlayerColorSettings : ScriptableObject
{
    public Material playerMaterial;
    public Color activeColor;
    public Color[] colors;
}
