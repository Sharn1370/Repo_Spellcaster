using UnityEngine;
using UnityEngine.SceneManagement;  

public class GoToNext : MonoBehaviour
{
    public void SceneTransition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);  
    }
}