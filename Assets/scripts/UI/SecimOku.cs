using UnityEngine;
using UnityEngine.UI;

public class SecimOku : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; //seçim oku hareket sesi
    [SerializeField] private AudioClip interactSound; //seçim oku onaylama sesi
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {

        //seçim okunun konumunu deðiþtirir

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //seçim okunun seçim yapabilmesini saðlar

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

            //seçim okunun yukarý aþaðý hareket etmesini saðlar
            rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }


    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);


        //seçeneklerin buton komponentine eriþir 
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
