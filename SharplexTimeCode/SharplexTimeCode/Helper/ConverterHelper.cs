namespace SharplexTimeCode.Helper;

public class ConverterHelper
{
    /// <summary>
    /// This method subtracts 3 characters from the input string and then converts it to uppercase
    /// </summary>
    /// <returns></returns>
    public static string ModifyDay(string input)
    {
        var dayOfWeekString = input.ToString();
        return dayOfWeekString.Length > 3
            ? dayOfWeekString.Substring(0, 3).ToUpper()
            : dayOfWeekString.ToUpper();
    }
    
    /// <summary>
    /// This method takes an integer representing a month and returns the corresponding month name as a string
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    public static string GetMonthName(int month)
    {
        switch (month)
        {
            case 1:
                return "JAN";
            case 2:
                return "FEB";
            case 3:
                return "MAR";
            case 4:
                return "APR";
            case 5:
                return "MAY";
            case 6:
                return "JUN";
            case 7:
                return "JUL";
            case 8:
                return "AUG";
            case 9:
                return "SEP";
            case 10:
                return "OCT";
            case 11:
                return "NOV";
            case 12:
                return "DEC";
            default:
                return "Invalid month";
        }
    }
    
    public static string GetFullMonthName(int month)
    {
        switch (month)
        {
            case 1:
                return "January";
            case 2:
                return "February";
            case 3:
                return "March";
            case 4:
                return "April";
            case 5:
                return "May";
            case 6:
                return "June";
            case 7:
                return "July";
            case 8:
                return "August";
            case 9:
                return "September";
            case 10:
                return "October";
            case 11:
                return "November";
            case 12:
                return "December";
            default:
                return "Invalid month";
        }
    }
}