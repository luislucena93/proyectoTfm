using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Characters
{
    player1El,
    player2Ella,
    npc1,
    npc1OjosCerrados
}

[System.Serializable]
public class Sentence
{
    public Characters character;
    [TextArea(3, 10)]
    public string text;

}
