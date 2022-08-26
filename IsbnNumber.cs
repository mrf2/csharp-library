using System.Text;

class IsbnNumber
{
    private string? strIsbn;
    private int[] weightArr = { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };   // weight factor for converting to 13 digits
    private int[] weightArr2 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };       // weight factor for converting to 10 digits
    private int[] nineDigits;
    private int[] twelveDigits;
    private int checkSumDigit;
    private StringBuilder strNewIsbn;

    public IsbnNumber(string strNum)
    {
        strIsbn = strNum;
        nineDigits = new int[9];
        twelveDigits = new int[12];
        strNewIsbn = new StringBuilder();   
    }

    private void ProcessAllDigits()
    {
        if (strIsbn != null && strIsbn.Length == 10)
        {
            for (int i = 0; i < strIsbn.Length - 1; i++)
                nineDigits[i] = strIsbn[i] - '0';
        } else if (strIsbn != null && strIsbn.Length == 13)
        {
            for (int i = 3, j = 0; i < strIsbn.Length - 1; i++, j++)
            {
                nineDigits[j] = strIsbn[i] - '0';
            }
        }
    }

    private void Prepend978()
    {
        // Prepend "978" onto the front of nine digits
        twelveDigits[0] = 9;
        twelveDigits[1] = 7;
        twelveDigits[2] = 8;

        for (int i = 3, j = 0; i < twelveDigits.Length; i++, j++)
            twelveDigits[i] = nineDigits[j];
    }

    private void AddCheckSumDigit10()
    {
        int sum = 0;
        int checkDigit;
        for (int i = 0; i < twelveDigits.Length; i++)
            sum += twelveDigits[i] * weightArr[i];

        
        checkDigit = sum % 10;
        if (checkDigit == 0)
            this.checkSumDigit = 0;
        else
            this.checkSumDigit = 10 - checkDigit;
    }

    private void To13Digit()
    {
        for (int i = 0; i < twelveDigits.Length; i++)
            strNewIsbn.Append(twelveDigits[i]);
        strNewIsbn.Append(this.checkSumDigit);
    }

    public string ConvertTo13Digits()
    {
        this.ProcessAllDigits();
        this.Prepend978();
        this.AddCheckSumDigit10();
        this.To13Digit();

        if (strNewIsbn != null)
            return strNewIsbn.ToString();
        else
            return "Conversion Error";
    }

    private void AddCheckSumDigit13()
    {
        int sum = 0;
        int checkDigit;
        for (int i = 0; i < nineDigits.Length; i++)
            sum += nineDigits[i] * weightArr2[i];
        checkDigit = sum % 11;
        this.checkSumDigit = 11 - checkDigit;
    }
    
    private void To10Digit()
    {
        for (int i = 0; i < nineDigits.Length; i++)
            strNewIsbn.Append(nineDigits[i]);
        strNewIsbn.Append(this.checkSumDigit);
    }
    public string ConvertTo10Digits()
    {
        this.ProcessAllDigits();
        this.AddCheckSumDigit13();
        this.To10Digit();

        if (strNewIsbn != null)
            return strNewIsbn.ToString();
        else
            return "Conversion Error";
    }
}
