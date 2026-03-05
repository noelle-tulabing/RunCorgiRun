using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    public Corgi Corgi;
    public PoopPlacer PoopPlacer;
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        // check key pressed
        if (keyboard.wKey.isPressed) // w --> up
        {
            Corgi.Move(Vector2.up);
        }
        if (keyboard.sKey.isPressed) // s --> down
        {
            Corgi.Move(Vector2.down);
        }
        if (keyboard.aKey.isPressed) // a --> left
        {
            Corgi.Move(Vector2.left);
        }
        if (keyboard.dKey.isPressed) // d --> right
        {
            Corgi.Move(Vector2.right);
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            PoopPlacer.Place(Corgi.GetPosition());
        }
    }
}
