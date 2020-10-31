using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SoundMonsterScript : MonoBehaviour
{
    AIDestinationSetter destinationSetter;
    public Transform lastKnownPosition;

    // Start is called before the first frame update
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnParticleCollision(GameObject collision)
    {
        float x = collision.transform.position.x;
        float y = collision.transform.position.y;
        lastKnownPosition.position = new Vector3(x, y, 0);
        destinationSetter.target = lastKnownPosition;
    }
}
