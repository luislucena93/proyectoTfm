using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    void SetAvisoPushable(bool disponible);

    void SetPushing(bool activo);

    Rigidbody GetRigidBody();

    float GetPaddingJugador();

    TipoCajaEnum GetTipoCajaEnum();

    void SetBloquearMoviento(bool bloquear);

    bool GetBloquearMoviento();
}
