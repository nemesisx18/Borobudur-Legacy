using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FreeroamControl : MonoBehaviour
{
    //player stat
    [Header("Player Info")]
    public bool isHori;
    public bool isVerti;

    public float moveSpeed;
    private float moveH, moveV;

    public LayerMask layer;
    RaycastHit2D hit;
    
    public Animator anim;
    public AudioClip sfxWalk;
    public AudioClip openLibrary;
    public AudioSource mainAudio;

    public GameObject infoCanvas;
    public GameObject charaCanvas;
    public GameObject tutorCanvas;
    public GameObject reliefCanvas;
    public SpriteRenderer _renderer;
    private Rigidbody2D rb;

    public Sprite finalTutorial;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }

        Time.timeScale = 1;
    }

    private void Update()
    {
        if (!FrManager.fmInstance.isPaused)
        {
            Cursor.visible = false;
            
            if (isHori)
            {
                moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
            }
            else
            {
                moveH = 0;
                anim.SetBool("isIdle", true);
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                isHori = true;
                isVerti = false;
                
                if (!mainAudio.isPlaying)
                {
                    mainAudio.clip = sfxWalk;
                    mainAudio.Play();
                }

                anim.SetBool("isIdle", false);
            }
            else
            {
                mainAudio.Stop();
            }

            anim.SetFloat("xInput", moveH);
            anim.SetFloat("yInput", moveV);

            //flip player
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            //interact button
            hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layer);

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenLibrary();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenCharaSelect();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenBasement();
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
                Cursor.visible = true;
            }

            //reset position if re-select
            if(FrManager.fmInstance.justSpawn)
            {
                transform.position = new Vector3(13.02f, -2.528f, 0f);

                FrManager.fmInstance.justSpawn = false;
            }

            //tampilkan tutor final
            if(PlayerPrefs.GetString("EndGame") == "level selesai")
            {
                tutorCanvas.GetComponent<Image>().sprite = finalTutorial;
                StartCoroutine(DisableImage());
            }
        }
        else
        {
            Cursor.visible = true;
            Debug.Log("Game sedang dipause!!!!");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveH, 0);
    }

    IEnumerator DisableImage()
    {
        tutorCanvas.SetActive(true);
        yield return new WaitForSecondsRealtime(6f);
        tutorCanvas.SetActive(false);
    }

    public void OpenLibrary()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layer);

        if (hit.collider.CompareTag("Library"))
        {
            Debug.Log("arsip perpustakaan...");
            reliefCanvas.SetActive(true);
            FrManager.fmInstance.isPaused = true;
            Time.timeScale = 0;
            mainAudio.PlayOneShot(openLibrary);

            Cursor.visible = true;
        }
    }

    public void OpenCharaSelect()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layer);

        if (hit.collider.CompareTag("Chara"))
        {
            charaCanvas.SetActive(true);
            FrManager.fmInstance.isPaused = true;
            Time.timeScale = 0;

            Cursor.visible = true;
        }
    }

    public void OpenBasement()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layer);

        if (hit.collider.CompareTag("Basement"))
        {
            infoCanvas.SetActive(true);
            FrManager.fmInstance.isPaused = true;
            Time.timeScale = 0;

            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Finish")
        {
            SceneManager.LoadScene("Stage1");
            Time.timeScale = 1;
        }
    }
}
