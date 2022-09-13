using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDialogosSalaNivel2 : MonoBehaviour
{
    [SerializeField]
    DialogueManager _dialogManager;
    
    [SerializeField]
    PlayerStateMachine _player1;

    [SerializeField]
    PlayerStateMachine _player2;

    private static string MENSAJE_FALLO = "¡Qué mala pata!";

    private static string MENSAJE_CHUPADO = "Estaba chupado.";

    private static string MENSAJE_MUY_BIEN = "¡Muy bien!";

    private static string MENSAJE_CONSEGUIDO = "¡¡Lo conseguimos!!";


    public void GenerarDialogo(EnumEstadoPCFinalZona3 estadoPc){
        if(_player1.isAlive() && _player2.isAlive()){
            Dialogue dialogo = new Dialogue();
            Sentence frase = new Sentence();
            frase.character = (Random.Range(0,10)%2)==1?Characters.player1El:Characters.player2Ella;
            switch(estadoPc){
                case EnumEstadoPCFinalZona3.E03:
                case EnumEstadoPCFinalZona3.E08:
                case EnumEstadoPCFinalZona3.E13:
                    frase.text = MENSAJE_FALLO;
                break;
                case EnumEstadoPCFinalZona3.E04:
                    frase.text = MENSAJE_CHUPADO;
                break;
                case EnumEstadoPCFinalZona3.E09:
                    frase.text = MENSAJE_MUY_BIEN;
                break;
                case EnumEstadoPCFinalZona3.E14:
                    frase.text = MENSAJE_CONSEGUIDO;
                break;
            }
            if(frase.text != null & frase.text.Length > 0){
                dialogo.sentences = new Sentence[] {frase};
                _dialogManager.StartDialogue(dialogo);
            }   else{
                Debug.LogError("Error generar frase Estado "+estadoPc);
            }
        }
    }
}
