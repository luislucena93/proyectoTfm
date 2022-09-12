using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHUDAnim : MonoBehaviour
{
    Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();
    }
    
    public void Mostrar(){
        _animator.SetBool("ocultar",false);
    }

    public void Ocultar(){
        _animator.SetBool("ocultar",true);
    }
}
