namespace MVC_ComponentesCodeFirst.Services;

public class FormattingService
{
    public string AsReadableDate(DateTime date)
    {
        return date.ToString("D");
    }
}
