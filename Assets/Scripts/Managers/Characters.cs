using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class Characters : ScriptableObject
{
    public GameObject character;
    public Sprite image;

    public string name;

    public string power;
}
