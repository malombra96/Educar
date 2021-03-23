using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
class BehaviourEditorWebGL
{
    static BehaviourEditorWebGL()
    {
        PlayerSettings.WebGL.memorySize = 256;
        PlayerSettings.WebGL.dataCaching = false;
        //PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
        PlayerSettings.runInBackground = false;
        PlayerSettings.defaultScreenHeight = 1366;
        PlayerSettings.defaultScreenWidth = 768;
        PlayerSettings.WebGL.linkerTarget = WebGLLinkerTarget.Asm;
    }
}
