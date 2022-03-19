using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectButton : MonoBehaviour
{
    public static float percentageOfInterest;
    
    // Start is called before the first frame update
    void Start()
    {
        percentageOfInterest = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WomenPhysicalAndOrSexualViolenceSince15()
    {
        percentageOfInterest = 0.33f;
    }

	public void WomenPhysicalAndOrSexualViolenceInPast12Months()
	{
		percentageOfInterest = 0.08f;
	}
	
	public void WomenRapedSince15()
	{
		percentageOfInterest = 0.05f;
	}
	
	public void WomenStalkedSince15()
	{
		percentageOfInterest = 0.18f;
	}

	public void WomenStalkedInPast12Months()
	{
		percentageOfInterest = 0.05f;
	}

	public void WomenSexualAbuseOrIncidentByAdultBefore15()
	{
		percentageOfInterest = 0.12f;
	}

	public void WomenAvoidPlacesInFearOfSexualAssault()
	{
		percentageOfInterest = 0.53f;
	}

	public void WomenPsychologicalViolenceByPartner()
	{
		percentageOfInterest = 0.43f;
	}

	public void WomenBelittledOrHumiliatedInPrivateByPartner()
	{
		percentageOfInterest = 0.25f;
	}

	public void WomenPhysicallyThreatenedByPartner()
	{
		percentageOfInterest = 0.14f;
	}

	public void WomenConfinedInHomeByPartner()
	{
		percentageOfInterest = 0.05f;
	}
	
}
