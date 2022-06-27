using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRecuperarSalud
{
    bool IsHurt();
    void RecuperarSalud(int puntosSalud);

    bool IsDead();

    void SetAvisoCurable(bool curableAlAlcance);
}
