
public class CommandLineArgs{
    public string[] ArrayOfArgs { get; }
    public List<string> ListOfArgs { get; }

    public bool HasArguments { get; }
    public string Verb { get; }
    public List<string> ArgumentsOfVerb { get; }
    public CommandLineArgs(string[] ArrayOfArgs){
        this.ArrayOfArgs = ArrayOfArgs;
        this.ListOfArgs = ArrayOfArgs.ToList();

        if(this.ListOfArgs.Count == 0){
            this.HasArguments = false;
        }else{
            this.HasArguments = true;
        }

        if (this.HasArguments){
            this.Verb = this.ListOfArgs[0];
        } else {
            this.Verb = "GetHelp";
        }

        if(this.ListOfArgs.Count > 1){
            this.ArgumentsOfVerb = this.ListOfArgs.GetRange(1, this.ListOfArgs.Count - 1);
        } else {
            this.ArgumentsOfVerb = new List<string> {};
        }

    }
}