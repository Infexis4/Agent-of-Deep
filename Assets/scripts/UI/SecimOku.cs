using UnityEngine;
using UnityEngine.UI;

public class SecimOku : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; //se�im oku hareket sesi
    [SerializeField] private AudioClip interactSound; //se�im oku onaylama sesi
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {

        //se�im okunun konumunu de�i�tirir

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //se�im okunun se�im yapabilmesini sa�lar

        if (Input.GetKeyDown(KeyCode.Return))
            Interact();
    }


    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = options.Length - 1;

        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

            //se�im okunun yukar� a�a�� hareket etmesini sa�lar
            rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }


    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);


        //se�eneklerin buton komponentine eri�ir 
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
