using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerScreen : MonoBehaviour {

    public Text c_nameSurname;
    public Text c_dateOfBirth;
    public Text c_passportStart;
    public Text c_passportExpire;
    public Text c_visaType;
    public Text c_visaStart;
    public Text c_visaExpire;
    public Text c_today;

    public Text c_helpScreen;

    public Text c_onPassportText;

    private string xc_nameSurname;
    private string xc_dateOfBirth;
    private string xc_passportStart;
    private string xc_passportExpire;
    private string xc_visaType;
    private string xc_visaStart;
    private string xc_visaExpire;
    private string xc_today;
    private string xc_helpScreen;

    private Color xc_passportStart_color;
    private Color xc_passportExpire_color;
    private Color xc_visaStart_color;
    private Color xc_visaExpire_color;

    DateTime day = new DateTime(2019, 1, 1);

    public void ClearPassportScreen()
    {
        xc_nameSurname = "Name Surname";
        xc_dateOfBirth = "Date of Birth: ";
        xc_passportStart = "Issued On: ";
        xc_passportExpire = "Expires On: ";
        xc_visaType = "Visa Type: ";
        xc_visaStart = "Visa Start: ";
        xc_visaExpire = "Visa Expire: ";
        xc_today = day.ToShortDateString();
        SetHelpScreen("Ask the passenger where he/she has come from.");
        xc_passportStart_color = Color.white;
        xc_passportExpire_color = Color.white;
        xc_visaStart_color = Color.white;
        xc_visaExpire_color = Color.white;
        c_onPassportText.text = "Name" + "\n" + "Surname" + "\n\n" + "Country";
    }

    public void SetPassportToPaper(Passport p)
    {
        c_onPassportText.text = p.agentName + "\n" + p.surname + "\n\n" + p.cameFrom;
    }

    public void SetPassportToScreen(Passport p)
    {
        xc_nameSurname = p.agentName + " " + p.surname;
        xc_dateOfBirth = "Date of Birth: " + p.dateOfBirth.ToShortDateString();
        xc_passportStart = "Issued On: " + p.passportStart.ToShortDateString();
        xc_passportExpire = "Expires On: " + p.passportExpire.ToShortDateString();
        xc_visaType = "Visa Type: " + p.visaType;
        xc_visaStart = "Visa Start: " + p.visaStart.ToShortDateString();
        xc_visaExpire = "Visa Expire: " + p.visaExpire.ToShortDateString();
        xc_today = day.ToShortDateString();

        SetPassportToPaper(p);

        if (p.correctPassportStart)
        {
            xc_passportStart_color = Color.green;
        }
        else
        {
            xc_passportStart_color = Color.red;
        }

        if (p.correctPassportExpire)
        {
            xc_passportExpire_color = Color.green;
        }
        else
        {
            xc_passportExpire_color = Color.red;
        }

        if (p.correctVisaStart)
        {
            xc_visaStart_color = Color.green;
        }
        else
        {
            xc_visaStart_color = Color.red;
        }

        if (p.correctVisaExpire)
        {
            xc_visaExpire_color = Color.green;
        }
        else
        {
            xc_visaExpire_color = Color.red;
        }
    }

    public void SetHelpScreen(string s)
    {
        xc_helpScreen = s;
    }

    public void RefreshScreens()
    {
        c_nameSurname.text = xc_nameSurname;
        c_dateOfBirth.text = xc_dateOfBirth;
        c_passportStart.text = xc_passportStart;
        c_passportExpire.text = xc_passportExpire;
        c_visaType.text = xc_visaType;
        c_visaStart.text = xc_visaStart;
        c_visaExpire.text = xc_visaExpire;
        c_today.text = xc_today;
        c_helpScreen.text = xc_helpScreen;

        c_passportStart.color = xc_passportStart_color;
        c_passportExpire.color = xc_passportExpire_color;
        c_visaStart.color = xc_visaStart_color;
        c_visaExpire.color = xc_visaExpire_color;
    }
}
