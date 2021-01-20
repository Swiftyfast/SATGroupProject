using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SATGroupProject.DATA.EF//.Metadata
{
    #region Enrollment
    public class EnrollmentMetadata
    {
        public int EnrollmentId { get; set; }

        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "Scheduled Class ID")]
        public int ScheduledClassId { get; set; }

        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime EnrollmentDate { get; set; }

        public virtual ScheduledClass ScheduledClass { get; set; }
        public virtual Student Student { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }
    #endregion

    #region Course
    public class CourseMetadata
    {
        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }
        public string Curriculum { get; set; }
        public string Notes { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }
    #endregion

    #region Student
    public class StudentMetadata
    {
        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Major { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public int SSID { get; set; }

        //[Display(Name = "Full Name")]
        //public string Fullname { get { return LastName + ", " + FirstName; } }
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student {
        //[Display(Name = "Full Name")]
        //public string FullName
        //{
        //    get
        //    {
        //        return (FirstName + " " + LastName);
        //    }
        //}
        [Display(Name = "Full Name")]
        public string Fullname { get { return LastName + ", " + FirstName; } }
    }
    #endregion

    #region StudentStatus
    public class StudentStatusMetadata
    {
        [Display(Name = "Student Status ID")]
        public int SSID { get; set; }

        [Display(Name = "Student Status Name")]
        public string SSName { get; set; }

        [Display(Name = "Student Status Description")]
        public string SSDescription { get; set; }
    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }
    #endregion

    #region ScheduledClass
    public class ScheduledClassMetadata
    {
        [Display(Name = "Scheduled Class ID")]
        public int ScheduledClassId { get; set; }

        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime EndDate { get; set; }

        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }
        public string Location { get; set; }
        public int SCSID { get; set; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass {

        
    }
    #endregion

    #region ScheduledClassStatus
    public class ScheduledClassStatusMetadata
    {
        [Display(Name = "Scheduled Class Status ID")]
        public int SCSID { get; set; }

        [Display(Name = "Scheduled Class Status")]
        public string SCSName { get; set; }
    }

    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus { }
    #endregion
}
