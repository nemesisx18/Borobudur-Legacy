using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Core Singleton mode
    public static PlayerController instance;

    //Scene manager
    [Header("Game Manager")]
    public int count;
    public bool level1;
    public bool hasKey;
    public bool isPause;
    public bool stabAnim;
    public bool waitStab;
    public string scenePassword;

    [SerializeField] Behaviour disableScript;
    public Text keyText;
    public Text reliefText;
    public List<GameObject> spawnPrefab;
    public Sprite _sprite;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject warningUI;
    public GameObject batu1;
    public GameObject batu2;
    public GameObject spawnItem;
    public GameObject batuSpesial;
    public GameObject[] tutorialCanvas;
    public Image tutorImage;

    public Sprite[] tutorialImage;

    private GameObject prefabItem;
    [SerializeField] private LayerMask mask;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask chestLayer;

    [Header("Audio Manager")]
    public AudioSource source;
    public AudioSource playerSource;
    
    public AudioClip stab;
    public AudioClip walk;
    public AudioClip peti;
    public AudioClip death;
    public AudioClip sfxWin;
    public AudioClip sfxLose;
    public AudioClip takeDMG;
    public AudioClip boxDestroy;
    public AudioClip itemCollect;

    //player stat
    [Header("Player Info")]
    public int playerHealth;
    public int keyChest;
    public int puzzleQty;
    public bool isHori;
    public bool isVerti;
    public bool isImmune;
    public bool facingRight;
    public float immunityTime;
    public float immuneDuration;
    public float groundDistance;
    private float moveH, moveV;
    [SerializeField] private float moveSpeed = 5.0f;

    public Animator anim;
    public SpriteRenderer _renderer;
    
    private Rigidbody2D rb;

    [Header("Relief status and current level info")]
    public string currentRelief;
    public string levelDone;
    [SerializeField] private string levelStatus;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                disableScript.enabled = false;
            }
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
        
        //cek apakah level sudah pernah selesai
        levelStatus = PlayerPrefs.GetString(currentRelief);
        if(levelStatus == "")
        {
            Debug.Log("level status masih kosong");
        }
        else
        {
            Debug.Log(levelStatus);
        }

        //first time tutorial
        if (level1)
        {
            if (PlayerPrefs.HasKey("key2"))
            {
                tutorialCanvas[0].SetActive(false);
                tutorialCanvas[1].SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("key2", 0);
                PlayerPrefs.Save();
            }
        }
    }

    private void Update()
    {
        if (isHori)
        {
            moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
        }
        else
        {
            moveH = 0;
            anim.SetBool("isIdle", true);
        }

        if (isVerti)
        {
            moveV = Input.GetAxisRaw("Vertical") * moveSpeed;
        }
        else
        {
            moveV = 0;
            anim.SetBool("isIdle", true);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isHori = true;
            isVerti = false;

            if (!playerSource.isPlaying)
            {
                playerSource.clip = walk;
                playerSource.Play();
            }

            anim.SetBool("isIdle", false);
        }

        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isVerti = true;
            isHori = false;

            if (!playerSource.isPlaying)
            {
                playerSource.clip = walk;
                playerSource.Play();
            }

            anim.SetBool("isIdle", false);
        }
        else
        {
            isVerti = false;
            isHori = false;

            playerSource.Stop();
        }

        anim.SetFloat("xInput", moveH);
        anim.SetFloat("yInput", moveV);

        keyText.text = keyChest.ToString();
        reliefText.text = puzzleQty.ToString();

        if (level1 && count >= 0)
        {
            tutorImage.sprite = tutorialImage[count];
        }

        RaycastHit2D ray = Physics2D.Raycast(gameObject.transform.position, Vector2.down, groundDistance, groundMask);
        if(ray.collider != null)
        {
            anim.SetBool("isUp", false);
            stabAnim = false;
        }
        else
        {
            anim.SetBool("isUp", true);
            stabAnim = true;
        }

        //flip player
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = false;
            facingRight = true;
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = true;
            facingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (!waitStab)
            {
                source.PlayOneShot(stab);
                DestroyBox();
                waitStab = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }

        if(keyChest >= 1)
        {
            hasKey = true;
        }
        else
        {
            hasKey = false;
        }

        //Lose condition
        if(playerHealth == 0)
        {
            Debug.Log("Game Over");
            Cursor.visible = true;

            anim.SetTrigger("dead");
            if (!source.isPlaying)
            {
                source.PlayOneShot(death);
            }
            StartCoroutine(PlayerDead());
        }
        else
        {
            loseUI.SetActive(false);
        }

        //Win condition
        if(puzzleQty == 4)
        {
            Debug.Log("Puzzle selesai");
            Cursor.visible = true;

            if (!isPause)
            {
                source.PlayOneShot(sfxWin);
                isPause = true;
            }
            winUI.SetActive(true);
            
            PlayerPrefs.SetString(currentRelief, levelDone);
            levelStatus = levelDone;
            Time.timeScale = 0;
        }
        else
        {
            winUI.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveH, moveV);
    }

    public void DestroyBox()
    {
        if (facingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, mask);

            if (stabAnim)
            {
                anim.SetTrigger("stab_climb");
            }
            else
            {
                anim.SetTrigger("stab");
            }

            StartCoroutine(WaitBox());

            IEnumerator WaitBox()
            {
                waitStab = true;
                yield return new WaitForSeconds(0.7f);

                if (hit.collider != null)
                {
                    Debug.Log("Hit!!!!");
                    source.PlayOneShot(boxDestroy);

                    hit.collider.GetComponent<Animator>().SetTrigger("destroy");

                    if (hit.collider.CompareTag("Box"))
                    {
                        Debug.Log("box item");

                        int nextSpawn = Random.Range(0, spawnPrefab.Count);

                        prefabItem = spawnPrefab[nextSpawn];

                        Instantiate(prefabItem, hit.transform.position, hit.transform.rotation);

                        spawnPrefab.Remove(prefabItem);

                        Destroy(hit.transform.gameObject, 0.8f);
                    }
                    else
                    {
                        Destroy(hit.transform.gameObject, 0.8f);
                    }
                }
                waitStab = false;
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, mask);

            if (stabAnim)
            {
                anim.SetTrigger("stab_climb");
            }
            else
            {
                anim.SetTrigger("stab");
            }

            StartCoroutine(WaitBox());

            IEnumerator WaitBox()
            {
                waitStab = true;
                yield return new WaitForSeconds(0.7f);

                if (hit.collider != null)
                {
                    Debug.Log("Hit!!!!");
                    source.PlayOneShot(boxDestroy);

                    hit.collider.GetComponent<Animator>().SetTrigger("destroy");

                    if (hit.collider.CompareTag("Box"))
                    {
                        Debug.Log("box item");

                        int nextSpawn = Random.Range(0, spawnPrefab.Count);

                        prefabItem = spawnPrefab[nextSpawn];

                        Instantiate(prefabItem, hit.transform.position, hit.transform.rotation);

                        spawnPrefab.Remove(prefabItem);

                        Destroy(hit.transform.gameObject, 0.8f);
                    }
                    else
                    {
                        Destroy(hit.transform.gameObject, 0.8f);
                    }
                }
                waitStab = false;
            }
        }

    }

    public void OpenChest()
    {
        RaycastHit2D hitBox = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, chestLayer);

        if(hitBox.collider != null && hasKey == true)
        {
            Debug.Log("spawn itemmm");
            hitBox.collider.GetComponent<SpriteRenderer>().sprite = _sprite;
            source.PlayOneShot(peti);
            keyChest -= 1;

            Instantiate(spawnItem, hitBox.collider.transform);
        }
    }

    IEnumerator PlayerDead()
    {
        yield return new WaitForSeconds(1f);

        source.PlayOneShot(sfxLose);
        loseUI.SetActive(true);
        isPause = true;
        Destroy(this.gameObject);
        Time.timeScale = 0;
    }

    IEnumerator PlayerHit()
    {
        isImmune = true;

        _renderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        _renderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.1f);
        _renderer.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        _renderer.color = Color.white;
        yield return new WaitForSecondsRealtime(0.1f);
        _renderer.color = new Color(1f, 1f, 1f, 0.5f);

        yield return new WaitForSeconds(immuneDuration);

        _renderer.color = Color.white;
        isImmune = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trap")
        {
            if (isImmune == false)
            {
                playerHealth -= 1;
                source.PlayOneShot(takeDMG);
                StartCoroutine(PlayerHit());
            }
        }

        if (other.tag == "Kunci")
        {
            Debug.Log("Kunci diambil");
            source.PlayOneShot(itemCollect);
            keyChest += 1;
            Destroy(other.gameObject);
        }

        if (other.tag == "Puzzle")
        {
            puzzleQty += 1;
            source.PlayOneShot(itemCollect);
            Destroy(other.gameObject, 0.5f);
        }
        if(other.tag == "Health")
        {
            if (playerHealth >= 3)
            {
                IEnumerator HealthWarn()
                {
                    warningUI.SetActive(true);

                    yield return new WaitForSecondsRealtime(1f);
                    warningUI.SetActive(false);
                }

                StartCoroutine(HealthWarn());

                Debug.Log("Darah masih penuh");
            }
            else
            {
                playerHealth += 1;
                source.PlayOneShot(itemCollect);
                Destroy(other.gameObject);
            }
        }

        if(other.tag == "Kacamata")
        {
            other.gameObject.GetComponent<KacamataUp>().enabled = true;
            source.PlayOneShot(itemCollect);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Trigger1")
        {
            if (batu1 != null)
            {
                batu1.SetActive(true);

            }
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Trigger2")
        {
            batu2.SetActive(true);
            other.gameObject.SetActive(false);
        }

        if(other.tag == "TriggerSpesial")
        {
            batuSpesial.SetActive(true);
        }

        if (other.tag == "Tutor")
        {
            other.gameObject.SetActive(false);
            tutorImage.color = new Color(1, 1, 1, 1);
            count += 1;

            if(count == tutorialImage.Length - 1)
            {
                StartCoroutine(Deactive());
            }
        } 
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSecondsRealtime(2f);
        tutorImage.color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSecondsRealtime(1f);
        tutorImage.color = new Color(0, 0, 0, 0);
    }
}
