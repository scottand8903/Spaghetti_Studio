using UnityEngine;
using UnityEngine.SceneManagement;

//THIS SCRIPT IS NOT BEIGN USED YET
///
////
////

//THIS SCRIPT IS FOR CHANGING THE SCENE BETWEEN LEVELS IF NEEDED AND FOR THE UI BUTTONS TO PLAY GAME OR CHEKPOINZ SCENEZ UPON CLICKING A BUTTON

public class changeScene : MonoBehaviour
{
    public void moveToScene(int sceneID){
        SceneManager.LoadScene(sceneID); //append this script to an object or a trigger section then specify the scene ID (find scene IDs via Edit > Build Profiles)
   }

   public void quit(){
    Application.Quit();
   }
}