using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SoundMonsterScript : MonoBehaviour
{
    AIDestinationSetter destinationSetter;
    Patrol patrol;
    public Transform lastKnownPosition;

    public L2MScript l2ms;

    // Start is called before the first frame update
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        patrol = GetComponent<Patrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == destinationSetter.target.position)
        {
            patrol.enabled = true;
            destinationSetter.enabled = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(collision.gameObject);
            l2ms.Death();
        }
    }

    private void OnParticleCollision(GameObject collision)
    {
        patrol.enabled = false;
        destinationSetter.enabled = true;
        float x = collision.transform.position.x;
        float y = collision.transform.position.y;
        lastKnownPosition.position = new Vector3(x, y, 0);
        destinationSetter.target = lastKnownPosition;
    }
}
