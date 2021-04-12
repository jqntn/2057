using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaPick : MonoBehaviour
{
    public int charaNumber;
    //1) hoodie boi, 2) echarpe fella, 3) bracelet man, 4) red comrad, 5) stripped sweat pal, 6) freckles friend

    //tête : capuche ou cheveux
    public bool capucheOn;
    public GameObject capuche1;
    public GameObject capuche2;
    public GameObject capuche3;
    public GameObject hair;

    //veste
    private bool vesteOn;
    public GameObject chestNormal;
    public GameObject chestRaye;

    //pieds + manche
    private bool carminOn;
    public GameObject mancheNormal;
    public GameObject mancheCarmin;
    public GameObject shoeDnormal;
    public GameObject shoeGnormal;
    public GameObject shoeDcarmin;
    public GameObject shoeGcarmin;

    //taches de rousseur;
    private bool frecklesOn;
    public GameObject freckles;

    //écharpe;
    private bool echarpeOn;
    public GameObject scarf;

    //bracelets;
    private bool bracesOn;
    public GameObject bracelet1;
    public GameObject bracelet2;

    public GameObject levelmana;


    // Start is called before the first frame update
    void Start()
    {
        checkNumber();
    }

    private void Update()
    {
        checkNumber();
    }

    public void checkNumber() //active ou désactives les booléens
    {
        //obtient quel perso est sélectionné
        charaNumber = levelmana.GetComponent<LevelManager>().curPart;
        
        switch (charaNumber)
        {
            case 1: //hoodie boi
                capucheOn = true;
                vesteOn = false;
                carminOn = false;
                frecklesOn = false;
                echarpeOn = false;
                bracesOn = false;
                break;
            case 2: //écharpe fella
                capucheOn = false;
                vesteOn = false;
                carminOn = false;
                frecklesOn = false;
                echarpeOn = true;
                bracesOn = false;
                break;
            case 3: //bracelet man
                capucheOn = false;
                vesteOn = false;
                carminOn = false;
                frecklesOn = false;
                echarpeOn = false;
                bracesOn = true;
                break;
            case 4: //carmin comrad
                capucheOn = false;
                vesteOn = false;
                carminOn = true;
                frecklesOn = false;
                echarpeOn = false;
                bracesOn = false;
                break;
            case 5: //stripped sweat pal
                capucheOn = false;
                vesteOn = true;
                carminOn = false;
                frecklesOn = false;
                echarpeOn = false;
                bracesOn = false;
                break;
            case 6: //freckles friend
                capucheOn = false;
                vesteOn = false;
                carminOn = false;
                frecklesOn = true;
                echarpeOn = false;
                bracesOn = false;
                break;

        }

        //applique la fonction
        changeCharacter();

    }

    private void changeCharacter() //cache ou active en conséquence des booléens
    {
        switch (capucheOn)
        {
            case (true):
                capuche1.SetActive(true);
                capuche2.SetActive(true);
                capuche3.SetActive(true);
                hair.SetActive(false);
                break;
            case (false):
                capuche1.SetActive(false);
                capuche2.SetActive(false);
                capuche3.SetActive(false);
                hair.SetActive(true);
                break;
        }
        switch (vesteOn)
        {
            case (true):
                chestNormal.SetActive(false);
                chestRaye.SetActive(true);
                break;
            case (false):
                chestRaye.SetActive(false);
                chestNormal.SetActive(true);
                break;
        }
        switch (carminOn)
        {
            case (true):
                mancheCarmin.SetActive(true);
                shoeDcarmin.SetActive(true);
                shoeGcarmin.SetActive(true);

                mancheNormal.SetActive(false);
                shoeDnormal.SetActive(false);
                shoeGnormal.SetActive(false);
                break;
            case (false):
                mancheCarmin.SetActive(false);
                shoeDcarmin.SetActive(false);
                shoeGcarmin.SetActive(false);

                mancheNormal.SetActive(true);
                shoeDnormal.SetActive(true);
                shoeGnormal.SetActive(true);
                break;
        }
        switch (frecklesOn)
        {
            case (true):
                freckles.SetActive(true);
                break;
            case (false):
                freckles.SetActive(false);
                break;
        }
        switch (echarpeOn)
        {
            case (true):
                scarf.SetActive(true);
                break;
            case (false):
                scarf.SetActive(false);
                break;
        }
        switch(bracesOn)
        {
            case (true):
                bracelet1.SetActive(true);
                bracelet2.SetActive(true);
                break;
            case (false):
                bracelet1.SetActive(false);
                bracelet2.SetActive(false);
                break;
                
        }
    }
}
