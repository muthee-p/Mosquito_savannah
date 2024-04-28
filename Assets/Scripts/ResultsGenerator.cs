using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsGenerator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _larvaePercText, _algaePercText, _snailsPercText, _insectPercText;
    [SerializeField] private TextMeshProUGUI _resultStrength;
    public int plainPlantRecommendedAmount, snakePlantRecommendedAmount, fuzzyPlantRecommendedAmount;
    private int _larvaePercAmt, _algaePercAmt, _snailsPercAmt, _insectPercAmt;

    void Start()
    {
        int leftamount= _larvaePercAmt + _algaePercAmt + _snailsPercAmt;
        _larvaePercAmt= Random.Range(1, 76); 
        _algaePercAmt = Random.Range(1, 101 - _larvaePercAmt); 
         _snailsPercAmt= Random.Range(1, 101 - _larvaePercAmt - _algaePercAmt); 
         if(leftamount<0){
            leftamount=0;
         }
        _insectPercAmt = Random.Range(1, leftamount) ;

       _larvaePercText.text = _larvaePercAmt + "%";
       _algaePercText.text= _algaePercAmt + "%";
       _snailsPercText.text=_snailsPercAmt + "%";
       _insectPercText.text =_insectPercAmt + "%";


        float average = (_larvaePercAmt + _algaePercAmt + _snailsPercAmt + _insectPercAmt) /4f;

        // Update text based on the average
        if (average >= 0 && average <= 33)
        {
            _resultStrength.text = "Low";
            _resultStrength.color = Color.green;
            plainPlantRecommendedAmount =1;
            snakePlantRecommendedAmount =4;
            fuzzyPlantRecommendedAmount =3;
        }
        else if (average >= 34 && average <= 66)
        {
            _resultStrength.text = "Neutral";
            _resultStrength.color = Color.yellow;
            plainPlantRecommendedAmount =2;
            snakePlantRecommendedAmount =3;
            fuzzyPlantRecommendedAmount =3;
        }
        else if (average >= 67 && average <= 100)
        {
            _resultStrength.text = "High";
            _resultStrength.color= Color.red;
            plainPlantRecommendedAmount =4;
            snakePlantRecommendedAmount =3;
            fuzzyPlantRecommendedAmount =2;
        }
    }
}
