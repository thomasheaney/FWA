namespace FionaWhitfieldArt.Models
{
    public class Testimonials
    {
        public string Title { get; set; }
        public string Introduction { get; set; }

        public Testimonials(string title, string introduction)
        {
            Title = title;
            Introduction = introduction;
        }
    }
}