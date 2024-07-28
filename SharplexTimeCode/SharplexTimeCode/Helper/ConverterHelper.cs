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
                return "Jan";
            case 2:
                return "Feb";
            case 3:
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            case 12:
                return "Dec";
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