using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class RoleTest : MonoBehaviour {
    public string mzname;

    public GameObject obj;
    public UnityArmatureComponent armatureComponent;

    public UnityDragonBonesData bonesData;
   	// Use this for initialization
	void Start () {
        //   obj= Instantiate(obj,transform.Find(mzname));
        //  obj.transform.localPosition = Vector3.zero;
        // armatureComponent.animation.


        
     //   armatureComponent.animation.Play("QAQ");
        armatureComponent.AddDBEventListener(EventObject.FADE_IN, delegate(string type,EventObject eventObject) {
            Debug.Log(eventObject.armature.animation.lastAnimationName);
        });
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
           // Debug.Log("");
            armatureComponent.animation.Play("qaq");
           
        }
        else if(Input.GetKey(KeyCode.D)){

        }
	}
}
