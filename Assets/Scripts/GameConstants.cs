using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    // Mediante el ID en lugar del string es m�s eficiente, readonly para que no cambie (con const no se transforma de string a hash)
    public readonly static int isMovingHash = Animator.StringToHash("isMoving");
    public readonly static int movementSpeedHash = Animator.StringToHash("movementSpeed");

    public readonly static int movementXHash = Animator.StringToHash("X");
    public readonly static int movementYHash = Animator.StringToHash("Y");

    public readonly static int isDead = Animator.StringToHash("isDead");

}
