interface IBlogContent {
    public string Title { get; }
    public string Filename { get; }
    public void Edit();
    public void Delete();
}