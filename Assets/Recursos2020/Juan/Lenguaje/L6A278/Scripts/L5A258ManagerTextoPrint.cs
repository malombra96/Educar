using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class L5A258ManagerTextoPrint : MonoBehaviour
{
    [HideInInspector] public ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;
    public GameObject personaje;

    public GameObject teclado;    
    public Button validar;    
    public Button cerrar;    

    public InputField[] inputFields;        
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        validar.onClick.AddListener(TakeScreenShoot);
        cerrar.onClick.AddListener(delegate { inputSecundario(null); });
    }
    public void TakeScreenShoot()
    {
        controlAudio.PlayAudio(0);
        validar.interactable = false;
        StartCoroutine(UploadPNG());
        controlPuntaje.IncreaseScore();
        controlNavegacion.Forward();
    }
    private void OnDisable()
    {       
        teclado.SetActive(false);
        for (int i = 0; i < inputFields.Length; i++)
        {
            var input = inputFields[i];
            if (input.GetComponent<L6A278Input>().inputField)                            
                input.GetComponent<L6A278Input>().inputField.transform.parent.gameObject.SetActive(false);            
        }
    }
    IEnumerator UploadPNG()
    {
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        //string ToBase64String byte[]
        string encodedText = System.Convert.ToBase64String(bytes);

        var image_url = "data:image/png;base64," + encodedText;

        print(image_url);

     #if !UNITY_EDITOR
      openWindow(image_url);
     #endif

    }
    [DllImport("__Internal")]
    private static extern void openWindow(string url);
    public void inputSecundario(GameObject g)
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            var input = inputFields[i];
            if (g)
            {
                if (input.gameObject == g)
                {
                    input.GetComponent<L6A278Input>().inputField.transform.parent.gameObject.SetActive(true);
                    input.GetComponent<L6A278Input>().inputField.SetActive(true);
                }
                else if (input.GetComponent<L6A278Input>().inputField)
                    input.GetComponent<L6A278Input>().inputField.SetActive(false);
            }
            else if (input.GetComponent<L6A278Input>().inputField)
            {
                print("df");
                input.GetComponent<L6A278Input>().inputField.transform.parent.gameObject.SetActive(false);
            }
        }
    }
    public void resetAll()
    {
        validar.interactable = true;
        for(int i = 0; i < inputFields.Length; i++)
        {
            inputFields[i].text = "";
        }
    }
}
