using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.LuisPedroFonseca.ProCamera2D
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public GameObject currentCheckPoint;

        [SerializeField]
        float restartDelay = 1f;
        [SerializeField]
        float restartTimer;
        [SerializeField]
        Animator anim;
        PlayerHealth playerHealth;
        GameObject cameraGame;
        [SerializeField]
        new ProCamera2D camera;
        Camera mainCamera;
        


        //PlayerHealth playerHealth;
        GameObject player;

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            
            playerHealth = FindObjectOfType<PlayerHealth>();
            player = GameObject.FindGameObjectWithTag("Player");
            //cameraGame = GameObject.FindGameObjectWithTag("MainCamera");
            camera = GameObject.FindObjectOfType<ProCamera2D>();
            mainCamera = GameObject.FindObjectOfType<Camera>();
        }

        public IEnumerator RespawnPlayer()
        {
            player.gameObject.SetActive(false);
            anim.SetTrigger("Death");
            //camera.MoveCameraInstantlyToPosition(player.transform.position);
            //camera.enabled = false;
            yield return new WaitForSeconds(3f);
           
            player.gameObject.SetActive(true);
            player.transform.position = currentCheckPoint.transform.position;
            
            //SceneManager.LoadScene("level prototype");
            //Camera.main.transform.position = new Vector3(currentCheckPoint.transform.position.x, currentCheckPoint.transform.position.y, -10);

            //camera.CenterTargetOnStart = true;
            
            LifeManager.instance.TakeLife();
            playerHealth.FullHealth();
            
            anim.SetTrigger("Enter");
            //camera.enabled = true;
            //camera.Reset();
        }
    }
}


