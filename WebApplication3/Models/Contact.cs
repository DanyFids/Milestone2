using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Contact
    {
        [Display (Name ="First Name")]
        [Required(ErrorMessage = "* First Name is Required")]
        public String FirstName { get; set; }
        [Display (Name = "Last Name")]
        [Required(ErrorMessage = "* Last Name is Required")]
        public String LastName { get; set; }
        [Required (ErrorMessage = "* Email is Required")]
        //[EmailAddress]
        /*[EmailAT]
        [EmailCOM]*/
        [RegularExpression("^[a-zA-Z0-9]+@[a-zA-Z0-9]+[.](com|ca|org|mail|edu|net|biz)$", ErrorMessage = "* Invalid Email: Email should have a '@' and end in '.com'")]
        public String Email { get; set; }
        [Required(ErrorMessage = "* Message is Required")]
        public String Message { get; set; }
    }

    public class EmailAT : RegularExpressionAttribute
    {
        public EmailAT () : base("^[a-zA-Z0-9]+[@].*$")
            {
                ErrorMessage = "*Missing @";
            }
    }

    public class EmailCOM : RegularExpressionAttribute
    {
        public EmailCOM () : base("^.*[.](com|ca|org|mail|edu|net|biz)$")
        {
            ErrorMessage = "* Missing '.com'";
        }
    }
}