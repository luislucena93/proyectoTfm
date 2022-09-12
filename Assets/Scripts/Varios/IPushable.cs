using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    void SetAvisoPushable(bool disponible);

    void SetPushing(bool activo);

    bool IsPushing();

    Rigidbody GetRigidBody();

    float GetPaddingJugador1();

    float GetPaddingJugador2();

    TipoCajaEnum GetTipoCajaEnum();

    void SetBloquearMoviento(bool bloquear);

    bool GetBloquearMoviento();

}
