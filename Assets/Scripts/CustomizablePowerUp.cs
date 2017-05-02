using UnityEngine;
using System.Collections;

public class CustomizablePowerUp : MonoBehaviour
{

    #region Settings
    public string powerUpName;
    [Tooltip("damage - 0, points - 1, health - 2, moveSpeed - 3, stoneHealth - 4")]
    public int powerUpNum;
    public bool isTakeable = false;
    public AudioClip pickUpSound;
    public float lifetime = 20f;
    public float duration = 10f;
    private float timeAlive;
    private bool isFlashing = false;
    private bool almostGone = false;

    public GameObject externHull;
    private GameObject _externHull;
    public float externHullRotSpeed = 0f;
    public bool externHullIsReverse = false;
    public Material externHullMaterial;

    public GameObject internHull;
    private GameObject _internHull;
    public float internHullRotSpeed = 0f;
    public bool internHullIsReverse = false;
    public Material internHullMaterial;

    public GameObject effect;
    private GameObject _effect;
    public float effectRotSpeed = 0f;
    public bool effectIsReverse = false;

    public GameObject item;
    private GameObject _item;
    public float itemRotSpeed = 0f;
    public bool itemIsReverse = false;
    public Material itemMaterial;

    private GameObject _light;
    public Color lightColor;
    public float lightIntensity = 3.0f;
    public float lightRange = 4.0f;
    #endregion

    void Start()
    {
        if (externHull != null)
        {
            _externHull = Instantiate(externHull, transform.position, transform.rotation);
            _externHull.transform.parent = transform;
            _externHull.name = "Extern Hull";
            _externHull.GetComponent<Renderer>().sharedMaterial = externHullMaterial;
            if (externHullRotSpeed > 0.0f)
            {
                _externHull.AddComponent(typeof(PowerUpRotation));
                PowerUpRotation rotScript = GetComponent<PowerUpRotation>();
                rotScript.SetRotationSpeed(externHullRotSpeed);
                rotScript.SetReverse(externHullIsReverse);
            }
            if (isTakeable)
                _externHull.AddComponent(typeof(TakeablePowerUp));
        }
        if (internHull != null)
        {
            _internHull = Instantiate(internHull, transform.position, transform.rotation);
            _internHull.transform.parent = transform;
            _internHull.name = "Intern Hull";
            _internHull.GetComponent<Renderer>().sharedMaterial = internHullMaterial;
            if (internHullRotSpeed > 0.0f)
            {
                _internHull.AddComponent(typeof(PowerUpRotation));
                PowerUpRotation rotScript = _internHull.GetComponent<PowerUpRotation>();
                rotScript.SetRotationSpeed(internHullRotSpeed);
                rotScript.SetReverse(internHullIsReverse);
            }
        }
        if (item != null)
        {
            _item = Instantiate(item, transform.position, Quaternion.identity);
            _item.transform.rotation = _item.transform.rotation * Quaternion.Euler(90, 0, 0);
            _item.transform.parent = transform;
            _item.name = "Item";
            _item.GetComponent<Renderer>().sharedMaterial = itemMaterial;
            if (itemRotSpeed > 0.0f)
            {
                _item.AddComponent(typeof(PowerUpRotation));
                PowerUpRotation rotScript = _item.GetComponent<PowerUpRotation>();
                rotScript.SetRotationSpeed(itemRotSpeed);
                rotScript.SetReverse(itemIsReverse);
            }
        }
        if (effect != null)
        {
            _effect = Instantiate(effect, transform.position, transform.rotation);
            /*if(item != null)
			{
				this._effect.transform.parent = _item.transform;
			}
			else 
			{*/
            _effect.transform.parent = transform;
            //}
            _effect.name = "Effect";
            if (effectRotSpeed > 0.0f)
            {
                _effect.AddComponent(typeof(PowerUpRotation));
                PowerUpRotation rotScript = _effect.GetComponent<PowerUpRotation>();
                rotScript.SetRotationSpeed(effectRotSpeed);
                rotScript.SetReverse(effectIsReverse);
            }
        }

        _light = new GameObject("Light");
        //this._light = (GameObject)GameObject.Instantiate(this._light);
        _light.transform.parent = transform;
        _light.transform.position = transform.position;
        //this._internHull.GetComponent<Renderer>().sharedMaterial = this.internHullMaterial;
        Light tmp = _light.AddComponent<Light>();
        tmp.color = lightColor;
        tmp.intensity = lightIntensity;
        tmp.range = lightRange;
        tmp.type = LightType.Point;
        tmp.shadows = LightShadows.Hard;
        
    }


    void Update()
    {
        if (!Manager.Instance.isPaused)
        {
            timeAlive += Time.deltaTime;
        }
        if (timeAlive > lifetime)
        {
            Destroy(gameObject);
        }
        else if (timeAlive > (3 * lifetime / 4) && !almostGone)
        {
            CancelInvoke("Flashing");
            InvokeRepeating("Flashing", 0, 0.15f);
            almostGone = true;

        }
        else if (timeAlive > lifetime / 2 && !isFlashing)
        {
            InvokeRepeating("Flashing", 0, 0.4f);
            isFlashing = true;
        }
    }

    public void Flashing()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(!child.gameObject.activeSelf);
        }
    }

}
