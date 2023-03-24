
public class Blog{
    public string BlogPath { get; }
    public bool IsValid { get; }

    public void AssertIsBlogValid(){
        if(this.IsValid == false){
            throw new InvalidOperationException("CURRENT_CWD_IS_INVALID_BLOG_FOLDER");
        }
    }
    public List<Article> GetListOfArticles() {
        AssertIsBlogValid();
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        return BlogIndexJsonObj.GetListOfArticles();
    }
    public void AddArticle(string Title, string Description, List<string> Tags){
        AssertIsBlogValid();
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        BlogIndexJsonObj.AddArticleToIndexJson(Title, Description, Tags);
        ArticleFile ArticleFileObj = new ArticleFile(this.BlogPath, this.GetListOfArticles()[0].Filename);
        ArticleFileObj.Create();
    }

    public List<Page> GetListOfPages(){
        AssertIsBlogValid();
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        return BlogIndexJsonObj.GetListOfPages();
    }

    public void AddPage(string Title){
        AssertIsBlogValid();
        BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
        BlogIndexJsonObj.AddPageToIndexJson(Title);
        PageFile PageFileObj = new PageFile(this.BlogPath, this.GetListOfPages()[0].Filename);
        PageFileObj.Create();
    }

    public void DeleteArticle(int IndexValue){
        AssertIsBlogValid();
        this.GetListOfArticles()[IndexValue].Delete();
    }

    public void DeletePage(int IndexValue){
        AssertIsBlogValid();
        this.GetListOfPages()[IndexValue].Delete();
    }

    public bool IsCwdValidBlogFolder(){
        // 确保 index.json 文件、articles 目录和 pages 目录存在
        bool IsBlogDataFileExisting = System.IO.File.Exists(Path.Join(this.BlogPath, "data", "index.json"));
        bool IsArticleFolderExisting = System.IO.Directory.Exists(Path.Join(this.BlogPath, "data", "articles"));
        bool IsPageFolderExisting = System.IO.Directory.Exists(Path.Join(this.BlogPath, "data", "pages"));
        if(IsBlogDataFileExisting && IsArticleFolderExisting && IsPageFolderExisting){
            // 检查博客版本符合要求
            BlogIndexJson BlogIndexJsonObj = new BlogIndexJson(this.BlogPath);
            Int64 CurrentBlogVersion = BlogIndexJsonObj.GetBlogIndexJsonData()["博客程序版本（禁止修改此值，否则会导致跨版本升级异常）"];
            if (CurrentBlogVersion >= 20221223 && CurrentBlogVersion <= 20230219){
                return true;
            }else {
                return false;
            }
        }else {
            return false;
        }
    }

    public Blog(string BlogPath){
        this.BlogPath = BlogPath;
        this.IsValid = this.IsCwdValidBlogFolder();
    }
}