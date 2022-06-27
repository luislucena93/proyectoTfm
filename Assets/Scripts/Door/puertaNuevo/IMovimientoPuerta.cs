using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IMovimientoPuerta
{
    public void MovimientoAbrir();

    public void MovimientoCerrar();

    public bool IsAbierta();

    void SetIListenerAbrir(IListenerAbrir listener);

    bool isAbriendo();
    bool isCerrando();
}
