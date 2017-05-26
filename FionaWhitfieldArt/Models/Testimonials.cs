using System.Collections.Generic;

namespace FionaWhitfieldArt.Models
{
    public class Testimonials
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public List<Testimonial> TestimonialList { get; set; }
        public bool hasTestimonials { get { return TestimonialList != null && TestimonialList.Count > 0; } }
        public string ColumnClass {
            get
            {
                switch (TestimonialList.Count)
                {
                    case 1:
                        return "col-md-12";
                    case 2:
                        return "col-md-6";
                    case 3:
                        return "col-md-4";
                    case 4:
                        return "col-md-3";
                    default :
                        return "col-md-4";
                }
            }
        }
        
        public Testimonials(string title, string introduction, List<Testimonial> testimonialList)
        {
            Title = title;
            Introduction = introduction;
            TestimonialList = testimonialList;
        }
    }
}