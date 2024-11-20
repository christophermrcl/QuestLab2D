using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class RangedAmmo : MonoBehaviour
{
    public Image ammoFill;
    public TextMeshProUGUI ammoText;

    public int maxAmmoCount;
    public int currentAmmoCount;

    private bool isReloading = false;
    public float reloadTime = 1f;
    private float reloadBuffer;

    private GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmoCount = maxAmmoCount;
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.isPaused)
        {
            return;
        }

        if (isReloading)
        {
            if(reloadBuffer >= (maxAmmoCount - currentAmmoCount) * reloadTime)
            {
                isReloading = false;
                reloadBuffer = 0;
                currentAmmoCount = maxAmmoCount;
            }
            else
            {
                reloadBuffer += Time.deltaTime;
            }

            ammoFill.fillAmount = reloadBuffer / ((maxAmmoCount - currentAmmoCount) * reloadTime);
        }
        if (!isReloading)
        {
            ammoFill.fillAmount = (float) currentAmmoCount / (float) maxAmmoCount;
        }

        if(currentAmmoCount <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
        }

        ammoText.text = currentAmmoCount.ToString();
    }

    public void Shot()
    {
        currentAmmoCount--;
    }
}
