using UnityEngine;


public class ConwayCell : MonoBehaviour, ICell
{
    private string _ruleSet = "B2/S23";
    
    public string GetRuleSet()
    {
        return _ruleSet;
    }

    public void SetRuleSet(string rules)
    {
        _ruleSet = rules;
    }
}
