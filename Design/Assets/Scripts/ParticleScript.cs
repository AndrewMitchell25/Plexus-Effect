using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public int dist;
    ParticleSystem myParticleSystem;
    ParticleSystem.Particle[] emittedParticles;
    public GameObject line;


    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        emittedParticles = new ParticleSystem.Particle[myParticleSystem.particleCount];
        myParticleSystem.GetParticles(emittedParticles);
        
        for(int i = 0; i < emittedParticles.Length - 1; i++)
        {
            for (int j = i + 1; j < emittedParticles.Length - 1; j++)
            {
                if (Mathf.Sqrt(Mathf.Pow(emittedParticles[j].position.x - emittedParticles[i].position.x, 2) + Mathf.Pow(emittedParticles[j].position.y - emittedParticles[i].position.y, 2)) <= dist)
                {
                    DrawLine(emittedParticles[i].position, emittedParticles[j].position, Color.white);
                }
            }
        }
        
        //Debug.DrawLine(emittedParticles[0].position, emittedParticles[1].position, Color.red);

        //myParticleSystem.SetParticles(emittedParticles, emittedParticles.Length);
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.001f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
