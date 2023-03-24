

public class Article : IBlogContent
{
    public string Title { get; }
    public string Filename { get; }
    public List<string> Tags { get; }
    public string Description { get; }
    public string CreatedAt { get; }
    public string UpdatedAt { get; }
    public bool IsTop { get; }
    public bool IsHidden { get; }
    public bool IsCommentEnabled { get; }
    public string BlogPath { get; }
    public string ArticlePath { get; }
    public int IndexValue { get; }

    public void Edit()
    {
        new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo(this.ArticlePath)
            {
                UseShellExecute = true
            }
        }.Start();
    }

    public void Delete(){
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        BlogIndexJsonObj.RemoveArticleInIndexJson(this.IndexValue);
        System.IO.File.Delete(this.ArticlePath);
    }

    public List<string> GetListOfArticleProperties(){
        var Properties = typeof(Article).GetProperties();
        List<string> ResultList = new List<string> {};
        foreach(var Property in Properties){
            // 去除不建议被修改的值
            if ((string) Property.Name != "BlogPath" && (string) Property.Name != "ArticlePath" && (string) Property.Name != "IndexValue"){
                ResultList.Add(Property.Name);
            }
        }
        return ResultList;
    }

    public void SetArticleProperty<TValue>(string key, TValue value){
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        BlogIndexJsonObj.SetAttributeOfArticleInIndexJson<TValue>(this.IndexValue, key, value);
    }

    public Article(string Title, string Filename, List<string> Tags, string Description, string CreatedAt, string UpdatedAt, bool IsTop, bool IsHidden, bool IsCommentEnabled, string BlogPath, int IndexValue)
    {
        this.Title = Title;
        this.Filename = Filename;
        this.Tags = Tags;
        this.Description = Description;
        this.CreatedAt = CreatedAt;
        this.UpdatedAt = UpdatedAt;
        this.IsTop = IsTop;
        this.IsHidden = IsHidden;
        this.IsCommentEnabled = IsCommentEnabled;
        this.BlogPath = BlogPath;
        this.ArticlePath = Path.Join(this.BlogPath, "data", "articles", this.Filename);
        this.IndexValue = IndexValue;
    }
}