using Newtonsoft.Json;
public class BlogIndexJson{
    public string BlogIndexJsonPath { get; }
    protected dynamic? BlogIndexJsonData { get; set; }
    public string BlogPath { get; }

    protected void SaveDataToFile(){
        var JsonString = JsonConvert.SerializeObject(this.BlogIndexJsonData);
        System.IO.File.WriteAllText(this.BlogIndexJsonPath, JsonString);
        this.UpdateSelfData();
    }

    public void RemoveArticleInIndexJson(int i){
        this.BlogIndexJsonData["文章列表"].RemoveAt(i);
        this.SaveDataToFile();
    }

    public void RemovePageInIndexJson(int i){
        this.BlogIndexJsonData["页面列表"].RemoveAt(i);
        this.SaveDataToFile();
    }

    public dynamic GetBlogIndexJsonData(){
        return this.BlogIndexJsonData;
    }

    protected void UpdateSelfData(){
        var BlogIndexJsonText = File.ReadAllText(this.BlogIndexJsonPath);
        this.BlogIndexJsonData = JsonConvert.DeserializeObject(BlogIndexJsonText);
    }

    public List<Article> GetListOfArticles(){
        List<Article> ListOfArticles = new List<Article> {};
        for(int i = 0; i < this.BlogIndexJsonData["文章列表"].Count; i++){
            var CurrentArticleJsonData = this.BlogIndexJsonData["文章列表"][i];
            string Title = CurrentArticleJsonData["文章标题"];
            string Filename = CurrentArticleJsonData["文件名"];
            string Description = CurrentArticleJsonData["摘要"];
            string CreatedAt = CurrentArticleJsonData["创建日期"];
            string UpdatedAt = CurrentArticleJsonData["修改日期"];
            bool IsTop = CurrentArticleJsonData["是否置顶"];
            bool IsHidden = CurrentArticleJsonData["是否隐藏"];
            bool IsCommentEnabled = CurrentArticleJsonData["启用评论"];
            var TagsData = CurrentArticleJsonData["标签"];
            List<string> Tags = new List<string> {};
            foreach (string Tag in TagsData){
                Tags.Add(Tag);
            }

            Article CurrentArticle = new Article(Title,Filename,Tags, Description, CreatedAt, UpdatedAt, IsTop, IsHidden, IsCommentEnabled, this.BlogPath, i);
            ListOfArticles.Add(CurrentArticle);
        }
        return ListOfArticles;
    }

    public void AddArticleToIndexJson(string Title, string Description, List<string> Tags){
        Article NewArticle = new Article(Title, Path.GetRandomFileName().Replace(".","") + ".md", Tags, Description, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"), false, false, true, this.BlogPath, 0);
        dynamic NewArticleObjectData = new {
            文章标题 = NewArticle.Title,
            文件名 = NewArticle.Filename,
            标签 = NewArticle.Tags,
            摘要 = NewArticle.Description,
            创建日期 = NewArticle.CreatedAt,
            修改日期 = NewArticle.UpdatedAt,
            是否置顶 = NewArticle.IsTop,
            是否隐藏 = NewArticle.IsHidden,
            启用评论 = NewArticle.IsCommentEnabled
        };
        Newtonsoft.Json.Linq.JObject NewArticleJsonData = Newtonsoft.Json.Linq.JObject.FromObject(NewArticleObjectData);
        this.BlogIndexJsonData["文章列表"].AddFirst(NewArticleJsonData);
        this.SaveDataToFile();
    }

    public List<Page> GetListOfPages(){
        List<Page> ListOfPages = new List<Page> {};
        for(int i = 0; i < this.BlogIndexJsonData["页面列表"].Count; i++){
            var CurrentPageJsonData = this.BlogIndexJsonData["页面列表"][i];
            string Title = CurrentPageJsonData["页面标题"];
            string Filename = CurrentPageJsonData["文件名"];
            bool DisplayInMenu = CurrentPageJsonData["是否显示在菜单中"];
            string DisplayNameInMenu = CurrentPageJsonData["若显示在菜单中，则在菜单中显示为"];
            bool OpenInNewTab = CurrentPageJsonData["是否在新标签页打开"];
            bool IsHTML = CurrentPageJsonData["这是一个完整的html"];
            bool IsCommentEnabled = CurrentPageJsonData["启用评论"];

            Page CurrentPage = new Page(Title, Filename, DisplayInMenu, DisplayNameInMenu, OpenInNewTab, IsHTML, IsCommentEnabled, this.BlogPath, i);
            ListOfPages.Add(CurrentPage);
        }
        return ListOfPages;
    }

    public void AddPageToIndexJson(string Title){
        // todo: finish this method
        Page NewPage = new Page(Title, Path.GetRandomFileName().Replace(".","") + ".md", true, Title, false, false, true, this.BlogPath, 0);
        dynamic NewPageObjectData = new {
            页面标题 = NewPage.Title,
            是否显示在菜单中 = NewPage.DisplayInMenu,
            是否在新标签页打开 = NewPage.OpenInNewTab,
            这是一个完整的html = NewPage.IsHTML,
            启用评论 = NewPage.IsCommentEnabled,
            文件名 = NewPage.Filename
        };
        NewPageObjectData["若显示在菜单中，则在菜单中显示为"] = NewPage.DisplayNameInMenu;
        Newtonsoft.Json.Linq.JObject NewPageJsonData = Newtonsoft.Json.Linq.JObject.FromObject(NewPageObjectData);
        this.BlogIndexJsonData["文章列表"].AddFirst(NewPageJsonData);
        this.SaveDataToFile();
    }

    public void SetAttributeInIndexJson<TValue>(string key, TValue value){
        this.BlogIndexJsonData[key] = value;
        this.SaveDataToFile();
    }

    public void SetAttributeOfArticleInIndexJson<TValue>(int IndexValue, string key, TValue value){
        // todo: 字段映射
        this.BlogIndexJsonData["文章列表"][IndexValue][key] = value;
        this.SaveDataToFile();
    }

    public void SetAttributeOfPageInIndexJson<TValue>(int IndexValue, string key, TValue value){
        this.BlogIndexJsonData["页面列表"][IndexValue][key] = value;
        this.SaveDataToFile();
    }

    public void ShowSiteAttributes(){
        
    }

    public BlogIndexJson(string BlogPath){
        this.BlogPath = BlogPath;
        this.BlogIndexJsonPath = Path.Join(BlogPath, "data", "index.json");
        this.UpdateSelfData();
    }
}