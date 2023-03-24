
public class Page : IBlogContent{
    public string Title { get; }
    public bool DisplayInMenu { get; }
    public string DisplayNameInMenu { get; }
    public bool OpenInNewTab { get; }
    public string Filename { get; }
    public bool IsHTML { get; }
    public bool IsCommentEnabled { get; }
    public string BlogPath { get; }
    public int IndexValue { get; }
    public string PagePath { get; }

    public void Edit(){
        new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo(this.PagePath)
            {
                UseShellExecute = true
            }
        }.Start();
    }

    public void Delete(){
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        BlogIndexJsonObj.RemovePageInIndexJson(this.IndexValue);
        System.IO.File.Delete(this.PagePath);
    }

    public List<string> GetListOfPageProperties(){
        var Properties = typeof(Page).GetProperties();
        List<string> ResultList = new List<string> {};
        foreach(var Property in Properties){
            // 去除不建议被修改的值
            if ((string) Property.Name != "BlogPath" && (string) Property.Name != "PagePath" && (string) Property.Name != "IndexValue"){
                ResultList.Add(Property.Name);
            }
        }
        return ResultList;
    }

    public void SetPageProperty<TValue>(string key, TValue value){
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        BlogIndexJsonObj.SetAttributeOfPageInIndexJson<TValue>(this.IndexValue, key, value);
    }

    public Page(string Title, string Filename, bool DisplayInMenu, string DisplayNameInMenu, bool OpenInNewTab, bool IsHTML, bool IsCommentEnabled, string BlogPath, int IndexValue){
        this.Title = Title;
        this.Filename = Filename;
        this.DisplayInMenu = DisplayInMenu;
        this.DisplayNameInMenu = DisplayNameInMenu;
        this.OpenInNewTab = OpenInNewTab;
        this.IsHTML = IsHTML;
        this.IsCommentEnabled = IsCommentEnabled;
        this.BlogPath = BlogPath;
        this.IndexValue = IndexValue;
        this.PagePath = Path.Join(this.BlogPath, "data", "pages", this.Filename);
    }
}