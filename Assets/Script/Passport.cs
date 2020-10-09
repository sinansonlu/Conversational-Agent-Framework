using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passport {

    public string agentName;
    public string surname;

    public bool isMale;

    public DateTime dateOfBirth;

    public DateTime passportStart;
    public DateTime passportExpire;
    public DateTime visaStart;
    public DateTime visaExpire;

    public string cameFrom;

    public int stayDuration;
    public int budget;

    public string visaType;
    public string purposeOfVisit;
    public string occupation;

    public bool hasPassport;
    public bool hasReturnTicket;
    public bool hasBudget;
    public bool hasOccupation;

    public bool correctVisaType;
    public bool correctPassportStart;
    public bool correctVisaStart;
    public bool correctPassportExpire;
    public bool correctVisaExpire;

    public int daysUntilStartVisa;

    public static string[] p_agentnames_m = { "David" , "James", "John", "Robert", "Michael", "William", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Donald", "Mark", "Paul", "Steven", "Andrew", "Kenneth" };
    public static string[] p_agentnames_f = { "Mary" , "Patricia", "Jennifer", "Linda", "Elizabeth", "Barbara", "Susan", "Jessica", "Sarah", "Margaret", "Karen", "Nancy", "Lisa", "Betty", "Dorothy", "Sandra", "Ashley", "Kimberly", "Donna", "Emily" };
    public static string[] p_agentsurnames = { "Smith", "Jones", "Taulor", "Williams", "Brown", "Davies", "Evans", "Wilson", "Thomas", "Roberts", "Johnson", "Lewis", "Walker", "Robinson", "Wood", "Thompson", "White", "Watson", "Jackson", "Wright", "Green", "Harris", "Cooper", "King", "Lee", "Martin", "Clarke", "James", "Morgan", "Hughes", "Edwards", "Hill", "Moore", "Clark", "Harrison", "Scott", "Young", "Morris", "Hall", "Ward", "Turner", "Carter", "Phillips", "Mitchell", "Patel", "Adams", "Campbell", "Anderson", "Allen" };

    public static string[] p_camefrom = { "Mexico", "Germany", "United Kingdom", "Italy", "China", "Spain", "France", "India", "Turkey", "Russia", "Australia", "Canada", "Egypt", "Greece" };

    public static string[] p_visaType = { "Student", "Tourism", "Athlete", "Business", "Diplomat", "Media", "Worker" };

    public static string[] p_purposeOfVisit_student = { "study", "go to a university", "get my masters degree", "get my phd" };
    public static string[] p_purposeOfVisit_tourism = { "spend my vacation", "stay at a hotel", "visit famous places", "visit a friend", "visit my relatives", };
    public static string[] p_purposeOfVisit_athlete = { "attend a sports event" };
    public static string[] p_purposeOfVisit_business = { "attend a meeting", "attend a conference" };
    public static string[] p_purposeOfVisit_diplomat = { "attend an important meeting" };
    public static string[] p_purposeOfVisit_media = { "attend a conference", "attend a cultural event", "attend an exposition" };
    public static string[] p_purposeOfVisit_worker = { "work in a restaurant", "work at a company", "find a job" };

    public static string[] p_occupation_student = { "student" };
    public static string[] p_occupation_tourism = { "doctor", "teacher", "student", "computer engineer", "carpenter", "dancer", "dentist", "physicist", "photographer", "waiter", "tailor", "sculptor", "psychiatrist", "reporter" };
    public static string[] p_occupation_athlete = { "professional athlete"  };
    public static string[] p_occupation_business = { "manager", "CEO",   };
    public static string[] p_occupation_diplomat = { "diplomat", "government officer" };
    public static string[] p_occupation_media = { "reporter", "journalist", "writer" };

    public static float hasPassportRate = 0.95f;
    public static float hasReturnTicketRate = 0.7f;
    public static float visaTypeCorrectRate = 0.8f;
    public static float passportCorrectRate = 0.9f;
    public static float visaCorrectRate = 0.8f;
    public static float dailyBudgetRequirement = 100;

    public Passport()
    {

    }

    public Passport(string agentName, string surname, DateTime dateOfBirth, DateTime passportStart, DateTime passportExpire,
        DateTime visaStart, DateTime visaExpire, string cameFrom, string visaType)
    {
        this.agentName = agentName;
        this.surname = surname;

        this.dateOfBirth = dateOfBirth;

        this.passportStart = passportStart;
        this.passportExpire = passportExpire;
        this.visaStart = visaStart;
        this.visaExpire = visaExpire;

        this.cameFrom = cameFrom;
        this.visaType = visaType;

        isMale = true;
        correctVisaType = true;
        correctPassportStart = true;
        correctVisaStart = true;
        correctPassportExpire = true;
        correctVisaExpire = true;
        hasPassport = true;
        hasReturnTicket = true;
        hasBudget = true;
        hasOccupation = true;
        budget = 1000000;

    }

    public void InitRandomPassport(bool isMale)
    {
        this.isMale = isMale;

        if(isMale)
        {
            agentName = p_agentnames_m[UnityEngine.Random.Range(0, p_agentnames_m.Length)];
        }
        else
        {
            agentName = p_agentnames_f[UnityEngine.Random.Range(0, p_agentnames_f.Length)];
        }

        surname = p_agentsurnames[UnityEngine.Random.Range(0, p_agentsurnames.Length)];
        cameFrom = p_camefrom[UnityEngine.Random.Range(0, p_camefrom.Length)];

        if (UnityEngine.Random.value > hasPassportRate)
        {
            // has no passport
            hasPassport = false;
        }
        else
        {
            hasPassport = true;
        }

        visaType = p_visaType[UnityEngine.Random.Range(0, p_visaType.Length)];

        if (visaType.Equals("Student"))
        {
            purposeOfVisit = p_purposeOfVisit_student[UnityEngine.Random.Range(0, p_purposeOfVisit_student.Length)];
            occupation = p_occupation_student[UnityEngine.Random.Range(0, p_occupation_student.Length)];
            hasOccupation = true;
            budget = UnityEngine.Random.Range(1,10) * 1000;
        }
        else if (visaType.Equals("Tourism"))
        {
            purposeOfVisit = p_purposeOfVisit_tourism[UnityEngine.Random.Range(0, p_purposeOfVisit_tourism.Length)];
            occupation = p_occupation_tourism[UnityEngine.Random.Range(0, p_occupation_tourism.Length)];
            hasOccupation = true;
            budget = UnityEngine.Random.Range(1, 40) * 1000;
        }
        else if (visaType.Equals("Athlete"))
        {
            purposeOfVisit = p_purposeOfVisit_athlete[UnityEngine.Random.Range(0, p_purposeOfVisit_athlete.Length)];
            occupation = p_occupation_athlete[UnityEngine.Random.Range(0, p_occupation_athlete.Length)];
            hasOccupation = true;
            budget = UnityEngine.Random.Range(1, 40) * 1000;
        }
        else if (visaType.Equals("Business"))
        {
            purposeOfVisit = p_purposeOfVisit_business[UnityEngine.Random.Range(0, p_purposeOfVisit_business.Length)];
            occupation = p_occupation_business[UnityEngine.Random.Range(0, p_occupation_business.Length)];
            hasOccupation = true;
            budget = UnityEngine.Random.Range(5, 80) * 1000;
        }
        else if (visaType.Equals("Diplomat"))
        {
            purposeOfVisit = p_purposeOfVisit_diplomat[UnityEngine.Random.Range(0, p_purposeOfVisit_diplomat.Length)];
            occupation = p_occupation_diplomat[UnityEngine.Random.Range(0, p_occupation_diplomat.Length)];
            hasOccupation = true;
            budget = UnityEngine.Random.Range(20, 100) * 1000;
        }
        else if (visaType.Equals("Media"))
        {
            purposeOfVisit = p_purposeOfVisit_media[UnityEngine.Random.Range(0, p_purposeOfVisit_media.Length)];
            occupation = p_occupation_media[UnityEngine.Random.Range(0, p_occupation_media.Length)];
            hasOccupation = true;
            budget = UnityEngine.Random.Range(1, 40) * 1000;
        }
        else if (visaType.Equals("Worker"))
        {
            purposeOfVisit = p_purposeOfVisit_worker[UnityEngine.Random.Range(0, p_purposeOfVisit_worker.Length)];
            occupation = "unemployed";
            hasOccupation = false;
            budget = UnityEngine.Random.Range(1, 20) * 1000;
        }

        correctVisaType = true;

        if (UnityEngine.Random.value > visaTypeCorrectRate)
        {
            string visaTypePre = visaType;
            // visa type mutation
            visaType = p_visaType[UnityEngine.Random.Range(0, p_visaType.Length)];

            if (visaType.Equals(visaTypePre))
            {
                correctVisaType = true;
            }
            else
            {
                correctVisaType = false;
            }
        }

        stayDuration = UnityEngine.Random.Range(3, 45);

        if (UnityEngine.Random.value > passportCorrectRate)
        {
            // not valid passport
            passportExpire = DateTime.Now.Subtract(TimeSpan.FromDays(UnityEngine.Random.Range(30, 400)));
            passportStart = passportExpire.Subtract(TimeSpan.FromDays(365));
            visaStart = passportStart;
            visaExpire = passportExpire;

            correctPassportStart = true;
            correctPassportExpire = false;
        }
        else
        {
            // valid passport
            passportExpire = DateTime.Now.Add(TimeSpan.FromDays(UnityEngine.Random.Range(15, 100)));
            passportStart = passportExpire.Subtract(TimeSpan.FromDays(365));


            correctPassportStart = true;
            correctPassportExpire = true;

            if (UnityEngine.Random.value > visaTypeCorrectRate)
            {
                // invalid visa
                if (UnityEngine.Random.value > 0.5f)
                {
                    // not started yet
                    daysUntilStartVisa = UnityEngine.Random.Range(1, 10);
                    visaStart = DateTime.Now.Add(TimeSpan.FromDays(daysUntilStartVisa));
                    visaExpire = passportExpire.Subtract(TimeSpan.FromDays(UnityEngine.Random.Range(0, 30)));

                    correctVisaStart = false;
                    correctVisaExpire = true;
                }
                else
                {
                    // ended previously
                    visaExpire = DateTime.Now.Subtract(TimeSpan.FromDays(UnityEngine.Random.Range(0, 30)));
                    visaStart = visaExpire.Subtract(TimeSpan.FromDays(UnityEngine.Random.Range(30, 90)));

                    correctVisaStart = true;
                    correctVisaExpire = false;
                }
            }
            else
            {
                // valid visa
                visaStart = passportStart.Add(TimeSpan.FromDays(UnityEngine.Random.Range(15, 45)));
                visaExpire = passportExpire.Subtract(TimeSpan.FromDays(UnityEngine.Random.Range(0, 30)));

                correctVisaStart = true;
                correctVisaExpire = true;
            }
        }

        dateOfBirth = DateTime.Now.Subtract(TimeSpan.FromDays(UnityEngine.Random.Range(365 * 25, 365 * 45)));

        if(budget < dailyBudgetRequirement * stayDuration)
        {
            hasBudget = false;
        }
        else
        {
            hasBudget = true;
        }

        if (UnityEngine.Random.value > hasReturnTicketRate)
        {
            hasReturnTicket = false;
        }
        else
        {
            hasReturnTicket = true;
        }
    }
}
