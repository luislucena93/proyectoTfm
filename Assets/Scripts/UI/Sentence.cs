using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Characters
{
    player1,
    player2
}

[System.Serializable]
public class Sentence
{
    public Characters character;
    [TextArea(3, 10)]
    public string text;

}
