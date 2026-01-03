using UnityEngine;
using UnityEngine.Rendering;

public class AssetGeneration : MonoBehaviour
{

    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject barista;
    GameObject baritsa;
    Animator animator;
    [SerializeField] private Material baristaMat;
    [SerializeField] private Texture2D baristaTexture;
    static Vector3 barPos = new Vector3(39, 0, 42);
    Quaternion barAngle = Quaternion.Euler(0, -90, 0);
    Quaternion baristaAngle = Quaternion.Euler(0, 180, 0);
    float baristaWaveTime = 5f, baristaLastWave= 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        
        GameObject counter= Instantiate(bar, barPos, barAngle);
        MapGeneration.mapObjects.Add(counter);
        if(barista== null)
        {
            Debug.Log("barista is null");

            return;
        }
        baritsa= Instantiate(barista, barPos,baristaAngle);
        baritsa.transform.localScale = Vector3.one * 3;
        var smr = baritsa.GetComponentInChildren<SkinnedMeshRenderer>();

        Shader urp = Shader.Find("Universal Render Pipeline/Lit");
        if (urp == null)
        {
            Debug.LogError("URP Lit shader not found. URP not installed/active?");
            return;
        }

        // IMPORTANT: create a NEW material instance (not a variant)
        Material mat = new Material(urp);
        mat.SetTexture("_BaseMap", baristaTexture);

        smr.material = mat;
        animator= baritsa.GetComponentInChildren<Animator>();
        MapGeneration.mapObjects.Add(baritsa);
    }
    public enum State
    {
        IDLE, WAVING
    }
    State state = State.IDLE;

    // Update is called once per frame
    void Update()
    {

        if (baritsa == null) return;

        // Unity destroyed-object safe check
        if (animator == null || !animator)
        {
            animator = baritsa.GetComponentInChildren<Animator>();
            if (animator == null || !animator) return;
        }

        baristaLastWave += Time.deltaTime;

        switch (state)
        {
            case State.IDLE:
                if (baristaLastWave > baristaWaveTime)
                {
                    animator.ResetTrigger("idle");
                    animator.SetTrigger("wave");
                    state = State.WAVING;
                }
                break;
            case State.WAVING:
                if (baristaLastWave > baristaWaveTime + 2f)
                {
                    animator.ResetTrigger("wave");
                    animator.SetTrigger("idle");
                    baristaLastWave = 0;
                    state = State.IDLE;
                }
                break;

                
        }
      //  Debug.Log(baristaLastWave);
       // Debug.Log(state);
    }
    }
