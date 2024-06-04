using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsGenerator : MonoBehaviour
{
    public void GenerateResults(TextMeshProUGUI _larvaePercText, TextMeshProUGUI _algaePercText,
        TextMeshProUGUI _snailsPercText, TextMeshProUGUI _insectPercText,
        TextMeshProUGUI _resultStrength, ref int plainPlantRecommendedAmount,
        ref int snakePlantRecommendedAmount, ref int fuzzyPlantRecommendedAmount,
        ref int _larvaePercAmt, ref int _algaePercAmt, ref int _snailsPercAmt, ref int _insectPercAmt, ref bool resultsGenerated)
    {
        resultsGenerated = true;
        int leftAmount = 100;

        _larvaePercAmt = Random.Range(1, 76);
        leftAmount -= _larvaePercAmt;

        _algaePercAmt = Random.Range(1, Mathf.Min(76, leftAmount + 1));
        leftAmount -= _algaePercAmt;

        _snailsPercAmt = Random.Range(1, Mathf.Min(76, leftAmount + 1));
        leftAmount -= _snailsPercAmt;

        _insectPercAmt = leftAmount;

        _larvaePercText.text = _larvaePercAmt + "%";
        _algaePercText.text = _algaePercAmt + "%";
        _snailsPercText.text = _snailsPercAmt + "%";
        _insectPercText.text = _insectPercAmt + "%";

        //float average = (_larvaePercAmt + _algaePercAmt + _snailsPercAmt + _insectPercAmt) / 4f;

        // Update text based on the average
        if (_larvaePercAmt < _algaePercAmt || _larvaePercAmt < _snailsPercAmt  || _larvaePercAmt < _insectPercAmt)
        {
            _resultStrength.text = "Low";
            _resultStrength.color = Color.green;
            plainPlantRecommendedAmount = 1;
            snakePlantRecommendedAmount = 4;
            fuzzyPlantRecommendedAmount = 3;
        }
        else if (_larvaePercAmt > _algaePercAmt && _larvaePercAmt < _snailsPercAmt || _larvaePercAmt < _insectPercAmt)
        {
            _resultStrength.text = "Neutral";
            _resultStrength.color = Color.yellow;
            plainPlantRecommendedAmount = 2;
            snakePlantRecommendedAmount = 3;
            fuzzyPlantRecommendedAmount = 3;
        }
        else if (_larvaePercAmt > _algaePercAmt && _larvaePercAmt > _snailsPercAmt && _larvaePercAmt > _insectPercAmt)
        {
            _resultStrength.text = "High";
            _resultStrength.color = Color.red;
            plainPlantRecommendedAmount = 4;
            snakePlantRecommendedAmount = 3;
            fuzzyPlantRecommendedAmount = 2;
        }

    }
}
