
public class Program
{
    public static string CurrentWorkingDirectory = System.IO.Directory.GetCurrentDirectory();
    public static string GetVersion()
    {
        return "Undefined version";
    }
    public static Blog CurrentBlog = new Blog(CurrentWorkingDirectory);
    public static void Main(string[] args)
    {
        CommandLineArgs Args = new CommandLineArgs(args);
        string Verb = Args.Verb;
        List<string> ArgumentsOfVerb = Args.ArgumentsOfVerb;
        if (Verb == "GetHelp")
        {
            HelpPage.PrintHelpText();
        }
        else
        {
            try
            {
                if (Verb == "AddArticle")
                {
                    string Title = ArgumentsOfVerb[0];
                    string Description = ArgumentsOfVerb[1];
                    List<string> Tags = ArgumentsOfVerb.GetRange(2, ArgumentsOfVerb.Count - 2);
                    CurrentBlog.AddArticle(Title, Description, Tags);
                }
                else if (Verb == "AddPage")
                {
                    string Title = ArgumentsOfVerb[0];
                    CurrentBlog.AddPage(Title);
                }
                else if (Verb == "DeleteArticle")
                {
                    int IndexValue = int.Parse(ArgumentsOfVerb[0]);
                    CurrentBlog.DeleteArticle(IndexValue);
                }
                else if (Verb == "DeletePage")
                {
                    int IndexValue = int.Parse(ArgumentsOfVerb[0]);
                    CurrentBlog.DeletePage(IndexValue);
                }
            } catch (InvalidOperationException exception){
                if (exception.Message == "CURRENT_CWD_IS_INVALID_BLOG_FOLDER"){
                    // 捕获当前工作目录不是有效博客文件夹的错误。此错误一般由 Blog 类中的 AssertIsBlogValid() 方法抛出
                    Console.WriteLine("Your current working directory is not a valid BBG blog folder or the version is not compatible. The operation was cancelled.");
                }else {
                    // 对于其它错误，不进行异常处理而是直接抛出
                    throw exception;
                }
            }


        }



        if (Verb == "TestAndDebug")
        {
            // code to test
            
        }
    }
}