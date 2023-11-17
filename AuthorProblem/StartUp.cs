namespace AuthorProblem
{
    [Author("Viktor")]
    [Author("Students")]
    public class StartUp
    {
        static void Main()
        {
            Tracker tracker = new Tracker();
            tracker.PrintMethodsByAuthor();


        }
        [Author("blabla")]
        public void CustomMethod() 
        {

        }
    }
}