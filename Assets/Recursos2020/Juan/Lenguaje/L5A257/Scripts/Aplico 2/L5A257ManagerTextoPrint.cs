using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class L5A257ManagerTextoPrint : MonoBehaviour
{
    public GameObject teclado;
    public GameObject navBar;
    public Button validar;
    public L5A257Temas temas;

    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;

    [HideInInspector] public ControlAudio controlAudio;
    ControlPuntaje controlPuntaje;
    ControlNavegacion controlNavegacion;
    // Start is called before the first frame update
    void Start()
    {
        controlAudio = FindObjectOfType<ControlAudio>();
        controlPuntaje = FindObjectOfType<ControlPuntaje>();
        controlNavegacion = FindObjectOfType<ControlNavegacion>();
        validar.onClick.AddListener(TakeScreenShoot);        
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
        navBar.SetActive(true);
        teclado.SetActive(false);
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

    public void resetAll()
    {
        validar.interactable = true;
        if (inputField3.GetComponent<L5A257Input>().inputField)
        {
            inputField3.GetComponent<L5A257Input>().inputField.GetComponent<InputField>().text = "";
            inputField3.GetComponent<L5A257Input>().inputField.gameObject.SetActive(false);
        }        
        GetComponent<L5A257BehaviourLightBox>().CloseElement();
        temas.resetAll();
        inputField1.text = "";
        inputField2.text = "";
        inputField3.text = "";
    }
}
