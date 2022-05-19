using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuerta{

    void Abrir(TipoLlaveEnum tipoLlave);
    void Abrir();

    void Cerrar();

    bool isAbierta();

    bool isBloqueada();
}
