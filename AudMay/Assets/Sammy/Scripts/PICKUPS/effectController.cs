using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class effectController : MonoBehaviour
{

    public string effect;
    public bool trigger = false;
    public int collectables = 0;

    public GameObject eyepatchSquare;
    public GameObject blindfoldSquare;
    CharacterController controller;
    public Camera camera;
    public FirstPersonController fpc;

    //heights
    public float height = 2.0f;
    public float growHeight = 5.0f;
    public float shrinkHeight = 0.1f;

    private bool jump = false;

    public CharacterController cc;




    // Start is called before the first frame update
    void Start()
    {
        fpc = GetComponent<FirstPersonController>();
        eyepatchSquare.SetActive(false);
        blindfoldSquare.SetActive(false);
        controller = GetComponent<CharacterController>();
        height = controller.height;

        print("This is running!");
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            RunEffect();
            trigger = false;
        }
        if (jump)
        {
            fpc.m_Jump = true;
        }
    }

    void RunEffect()
    {
        if (!effect.Equals(null)){
            if (effect.Equals("jump"))
            {
                //Pogostick
                jump = true;
            }
            else if (effect.Equals("lowerVision"))
            {
                //Blindfold
                blindfoldSquare.SetActive(true);
            }
            else if (effect.Equals("healWhenKill"))
            {
                //book
            }
            else if (effect.Equals("grow"))
            {
                //cake
                controller.height = growHeight;
            }
            else if (effect.Equals("strength"))
            {
                //can of spinach
            }
            else if (effect.Equals("9lives"))
            {
                //cat paw
            }
            else if (effect.Equals("shrink"))
            {
                //cupcake
                controller.height = shrinkHeight;
                //camera.transform.position += new Vector3(0, 5, 0);
            }
            else if (effect.Equals("1/3bigPrize"))
            {
                //Sugar, Spice, and Everything Nice
                collectables++;
            }
            else if (effect.Equals("blockHalfVision"))
            {
                //eyepatch
                eyepatchSquare.SetActive(true);
            }
            else if (effect.Equals("reverseControls"))
            {
                //swirl
            }
        }
        
    }
    public void StopEffect()
    {
        if (!effect.Equals(null))
        {
            if (effect.Equals("jump"))
            {
                //Pogostick
                jump = false;
            }
            else if (effect.Equals("lowerVision"))
            {
                //Blindfold
                blindfoldSquare.SetActive(false);
            }
            else if (effect.Equals("healWhenKill"))
            {
                //book
            }
            else if (effect.Equals("grow"))
            {
                //cake
                controller.height = height;

            }
            else if (effect.Equals("strength"))
            {
                //can of spinach
            }
            else if (effect.Equals("9lives"))
            {
                //cat paw
            }
            else if (effect.Equals("shrink"))
            {
                //cupcake
                controller.height = height;
                cc.enabled = false;
                gameObject.transform.position = new Vector3(transform.position.x, 10f, transform.position.z);
                cc.enabled = true;

            }
            else if (effect.Equals("1/3bigPrize"))
            {
                //Sugar, Spice, and Everything Nice
                collectables++;
            }
            else if (effect.Equals("blockHalfVision"))
            {
                //eyepatch
                eyepatchSquare.SetActive(false);
            }
            else if (effect.Equals("reverseControls"))
            {
                //swirl
            }
        }

    }
}
