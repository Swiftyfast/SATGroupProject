using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SATGroupProject.DATA.EF//.Metadata
{
    #region Enrollment
    class EnrollmentMetadata
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int ScheduledClassId { get; set; }
        public System.DateTime EnrollmentDate { get; set; }

        public virtual ScheduledClass ScheduledClass { get; set; }
        public virtual Student Student { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }
    #endregion

    #region Course


    class CourseMetadata
    {

    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }
    #endregion

}
