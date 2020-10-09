using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SATApp.DATA.EF
{


    public class CoursMetadata
    {
        #region Course


        //public int CourseID { get; set; }
        [Required(ErrorMessage = "* Course Name is required")]
        [Display(Name = "Course Name")]
        [StringLength(50, ErrorMessage = "* Course Name cannot exceed 50 characters")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "*Course Description is required")]
        [Display(Name = "Course Description")]
        public string CourseDescription { get; set; }

        [Required(ErrorMessage = "*Credit Hours are required")]
        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public string Curriculum { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(50, ErrorMessage = "* Notes cannot exceed 500 characters")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "*Active is required")]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

    }

    [MetadataType(typeof(CoursMetadata))]
    public partial class Cours
    {

    }

    #endregion

    #region Enrollment
    public class EnrollmentMetadata
    {
        [Display(Name = "Enrollment ID")]
        public int EnrollmentID { get; set; }
        [Display(Name = "Student ID")]
        public int StudentID { get; set; }
        [Display(Name = "Scheduled Class ID")]
        public int ScheduledClassID { get; set; }
        [Required(ErrorMessage = "Enrollment Date is required")]
        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime EnrollmentDate { get; set; }
    }
    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment
    {

    }
    #endregion
    #region ScheduledClass
    public class ScheduledClassMetadata
    {
        //public int ScheduledClassID { get; set; }
        [Display(Name = "Course ID")]
        [Required(ErrorMessage = "*Course ID is required")]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "*Start Date is required")]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime StartDate { get; set; }

        [Required(ErrorMessage = "*End Date is required")]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime EndDate { get; set; }

        [Required(ErrorMessage = "*Instructor Name is required")]
        [StringLength(40, ErrorMessage = "*Instructor Name cannot exceed 40 characters.")]
        [Display(Name = "Instructor Name")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "*Location is required")]
        [StringLength(20, ErrorMessage = "*Location cannot exceed 20 characters.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "*Scheduled Class Status ID is required")]
        [Display(Name = "Schduled Class Status ID")]
        public int SCSID { get; set; }
    }
    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {
        public string ClassOverview
        {
            get { return CourseID + "," + StartDate + "," + InstructorName; }
        }
    }

    #endregion

    #region ScheduledClassStatus
    public class ScheduledClassStatusMetadata
    {
        [Required(ErrorMessage = "*Scheduled Class Status ID is required")]
        [Display(Name = "Scheduled Class Status ID")]
        public int SCSID { get; set; }

        [Required(ErrorMessage = "*Scheduled Class Status Name is required")]
        [Display(Name = "Scheduled Class Status Name")]
        [StringLength(50, ErrorMessage = "*Scheduled Class Status Name cannot exceed 50 characters")]
        public string SCSName { get; set; }
    }

    #endregion
    #region Student
    public class StudentMetadata
    {
        //public int StudentID { get; set; }
        [Required(ErrorMessage = "*First Name is required")]
        [Display(Name = "First Name")]
        [StringLength(20, ErrorMessage = "*First Name cannot exceed 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Last Name is required")]
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "*Last Name cannot exceed 20 characters")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(15, ErrorMessage = "*Major cannot exceed 15 characters")]
        public string Major { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(50, ErrorMessage = "*Address cannot exceed 50 characters")]
        public string Address { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(25, ErrorMessage = "*City cannot exceed 25 characters")]
        public string City { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public string State { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Zip Code")]
        [StringLength(10, ErrorMessage = "*Zip Code cannot exceed 10 characters")]
        public string ZipCode { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "*Please enter a vaild phone number")]

        [StringLength(13, ErrorMessage = "*Phone Number cannot exceed 13 characters")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*Email is required")]
        [EmailAddress(ErrorMessage = "*Please enter a valid email.")]
        [StringLength(60, ErrorMessage = "Email cannot exceed 60 characters")]
        public string Email { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(100, ErrorMessage = "*Photo URL cannot exceed 100 characters")]
        [Display(Name = "Photo URL")]
        public string PhotoURL { get; set; }

        [Required(ErrorMessage = "*Student Status ID is required")]
        [Display(Name = "Student Status ID")]
        public int SSID { get; set; }
    }
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Display(Name = "Address")]
        public string StudentAddress
        {
            get { return Address + ", " + City + "," + State + "," + ZipCode; }
        }
    }
    #endregion

    #region Student Status
    public class StudentStatusMetadata
    {
        [Display(Name = "Student Status ID")]
        public int SSID { get; set; }

        [Required(ErrorMessage = "*Student Status Name is required")]
        [Display(Name = "Student Status Name")]
        [StringLength(30, ErrorMessage = "*Student Status Name cannot exceed 30 characters")]
        public string SSName { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [Display(Name = "Student Status Description")]
        [StringLength(250, ErrorMessage = "*Student Status description cannot exceed 250 characters")]
        public string SSDescription { get; set; }
    }
    #endregion



}//end namespace
