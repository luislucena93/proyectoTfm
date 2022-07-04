using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaquina
{

    void Encender(bool encender);

    void AlternarEstado();

    bool IsEncendida();
}