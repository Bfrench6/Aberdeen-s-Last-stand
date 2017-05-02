using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public float duration;
    private bool active = false;
    public float activeTime;
    public int PUType = -1;
    public Color color;
    public string Name;

    
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(active)
        {
            activeTime += Time.deltaTime;
        }
        if(activeTime >= duration && active)
        {
            active = false;
            activeTime = 0;
            switch (PUType)
            {
                case ((int)Manager.powerType.damage):
                    Manager.Instance.doubleDamage = false;
                    break;
                case ((int)Manager.powerType.points):
                    Manager.Instance.doublePoints = false;
                    break;
                case ((int)Manager.powerType.moveSpeed):
                    Manager.Instance.movePU = false;
                    break;
            }
            Destroy(this);
        }	
	}

    public void Activate()
    {
        active = true;
    }

    public void SetPUType(int num)
    {
        PUType = num;
    }

    public int GetPUType()
    {
        return PUType;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }

    public void SetDuration (float dur)
    {
        duration = dur;
    }

    public void resetDuration ()
    {
        activeTime = 0;
    }

    public bool isActive ()
    {
        return active;
    }

}
