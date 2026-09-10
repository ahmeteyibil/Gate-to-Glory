using Unity.VisualScripting;
using UnityEngine;

public class DynamicObjectManager : MonoBehaviour
{
    private void OnBecameVisible()
    {
        print("Kamera "+ this.gameObject.name + " objesini görüyor.");
    }
    private void OnBecameInvisible()
    {
        //print("Dynamic object destroying..");x
        print("Kamera " + this.gameObject.name + " objesini görmüyor.");
        Destroy(this.gameObject); // KAMERA GÖRMEDÝÐÝ ZAMAN OBJEYÝ SÝLMESÝNÝ ÝSTÝYORUM
    }
}
