public static class ListDisplay{
    public static void DisplayArticleList(List<Article> ArticleList){
        // todo: support page navigation
        List<string> ListOfOutput = new List<string> {};
        foreach(Article ArticleObj in ArticleList){
            ListOfOutput.Add($"{ArticleObj.Title}: CreatedAt {ArticleObj.CreatedAt} UpdatedAt {ArticleObj.UpdatedAt}");
            string EmptyArea = "";
            string TagsToDisplay = "";

            for(int i = 0; i < ArticleObj.Title.Length; i++){
                EmptyArea += " ";
            }
            for(int i = 0; i < ArticleObj.Tags.Count; i++){
                TagsToDisplay += $"{ArticleObj.Tags[i]} ";
            }
            ListOfOutput.Add($"{EmptyArea} {ArticleObj.Description}");
            ListOfOutput.Add($"{EmptyArea} Tags: {TagsToDisplay}");
            ListOfOutput.Add($"{EmptyArea} "); // todo: other properties
        }
        foreach(string OutputLine in ListOfOutput){
            Console.WriteLine($"{OutputLine}");
        }

    }
}