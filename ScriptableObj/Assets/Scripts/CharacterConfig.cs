using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Characters/config")]
public class CharacterConfig : ScriptableObject
{
    public string characterName;
    public string description;
    public int strength;
    public int intelligence;
    public int charisma;
    public int knowledge;
    public string portraitSheetName;

    public Sprite[] Portrait {
        get {
            var sprites = Resources.LoadAll<Sprite>(portraitSheetName);
            return sprites;
        }
    }
}
