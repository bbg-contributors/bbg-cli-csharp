using Newtonsoft.Json;

public class HelpPage
{
    public static string LocaleName = System.Globalization.CultureInfo.CurrentUICulture.Name;
    public static string HelpPageJsonText = File.ReadAllText("./HelpPage.json");
    public static dynamic? HelpPageData = JsonConvert.DeserializeObject(HelpPageJsonText);

    public static void PrintHelpText()
    {
        if (HelpPageData is not null)
        {
            var HelpText = HelpPageData[LocaleName];
            if (HelpText is null)
            {
                HelpText = HelpPageData["zh-CN"];
            }
            foreach (string Text in HelpText){
                Console.WriteLine(Text);
            }
        }else{
            throw new Exception("unable to get help text");
        }

    }

}