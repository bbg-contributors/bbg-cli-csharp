
public class ArticleFile: IBlogContentFile{
    public string FilePath { get; }
    public void Create(){
        System.IO.File.WriteAllText(this.FilePath, "");
    }

    public void Delete(){
        System.IO.File.Delete(this.FilePath);
    }
    public ArticleFile(string BlogPath, string Filename){
        this.FilePath = Path.Join(BlogPath, "data", "articles", Filename);
    }
}


public class PageFile: IBlogContentFile{
    public string FilePath { get; }
    public void Create(){
        System.IO.File.WriteAllText(this.FilePath, "");
    }
    public void Delete(){
        System.IO.File.Delete(this.FilePath);
    }
    public PageFile(string BlogPath, string Filename){
        this.FilePath = Path.Join(BlogPath, "data", "pages", Filename);
    }
}
