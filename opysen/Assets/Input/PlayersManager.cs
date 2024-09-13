using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager instance;
    public SpriteRenderer otherSpriteRenderer;
    public Animator otherAnimator;
    public int maxPlayers;
    public List<Controllers> activePlayers = new List<Controllers>();
    public GameObject playerSpawnEffect;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayer(Controllers player)
    {

        Debug.Log("Adding player");

        if(activePlayers.Count < maxPlayers)
        {
            try{
                activePlayers.Add(player);
                GameObject tt = GameObject.Find("s(Clone)");
                tt.GetComponent<SpriteRenderer>().sprite = otherSpriteRenderer.sprite;
                tt.GetComponent<Animator>().runtimeAnimatorController = otherAnimator.runtimeAnimatorController;
                //Instantiate(null, player.transform.position, player.transform.rotation);
                
            }
            catch
            {
                Debug.Log("It probably works, so don't touch :D");
            }
            
        }
        else
        {
            Destroy(player.gameObject);
        }
    }
}
