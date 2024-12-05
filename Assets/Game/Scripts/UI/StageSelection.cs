using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    public void SelectStage(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);  // Load next scene
    }
}
