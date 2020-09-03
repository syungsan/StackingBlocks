using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStageController : MonoBehaviour
{
    public int stayBlockCount;

    // public List<Collision2D> stayBlocks = new List<Collision2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(this.stayBlocks.Count);
        // Debug.Log(this.stayBlockCount);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // this.stayBlocks.Add(coll);

        if (coll.gameObject.tag == "Block")
        {
            this.stayBlockCount++;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        // this.stayBlocks.Remove(coll);

        if (coll.gameObject.tag == "Block")
        {
            this.stayBlockCount--;
        }
    }
}
