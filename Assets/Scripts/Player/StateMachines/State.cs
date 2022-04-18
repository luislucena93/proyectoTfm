// Hacemos esta clase abstracta para que hereden de ella los distintos estados
public abstract class State
{
    // Entra en el estado
    public abstract void Enter();

    // Se ejecuta cada frame que estemos en el estado, obtenemos deltaTime de Unity y se lo pasamos al método
    public abstract void Tick(float deltaTime);

    // Sale del estado
    public abstract void Exit();
}

