using UnityEngine;

public class Data : ScriptableObject
{
  #region Fields
  
  #endregion

  #region Properties

  public string StudiedArea { get; } = "European Union";
  
  public int NumberOfWomenAsked { get; } = 42000;
  public int MinimumWomenIntervieweeAge { get; } = 18;
  public int MaximumWomenIntervieweeAge { get; } = 74;
  
  public float WomenPhysicalAndOrSexualViolenceSince15 { get; } = 0.33f;
  public float WomenPhysicalAndOrSexualViolenceInPast12Months { get; } = 0.08f;
  public float WomenRapedSince15 { get; } = 0.05f;
  public float WomenStalkedSince15 { get; } = 0.18f;
  public float WomenStalkedInPast12Months { get; } = 0.05f;
  public float WomenSexualAbuseOrIncidentByAdultBefore15 { get; } = 0.12f;
  public float WomenAvoidPlacesInFearOfSexualAssault { get; } = 0.53f;
  public float WomenPsychologicalViolenceByPartner { get; } = 0.43f;
  public float WomenBelittledOrHumiliatedInPrivateByPartner { get; } = 0.25f;
  public float WomenPhysicallyThreatenedByPartner { get; } = 0.14f;
  public float WomenConfinedInHomeByPartner { get; } = 0.05f;

  #endregion
}
