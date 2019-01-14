using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ELearningSystem.Models
{
    public class User : IPrincipal
    {
        public int Id { get; set; }

      //  [Display(Name = "strUserName", ResourceType = typeof(Resources.Resource))]
       // [Required(ErrorMessageResourceName = "strUserNameError", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string UserName { get; set; }

        [StringLength(255, ErrorMessage = "Паролата трябва да е минимум 6 символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]
       // [Display(Name = "strPassword", ResourceType = typeof(Resources.Resource))]
       // [Required(ErrorMessageResourceName = "strPasswordError", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string Password { get; set; }

       // [Display(Name = "strConfirmPassword", ResourceType = typeof(Resources.Resource))]
        // [Required(ErrorMessage = "Полето потвърди парола е задължително.")]
        // [StringLength(255, ErrorMessage = "Паролата трябва да е минимум 6 символа", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        // [Required]
        [Display(Name = "Име и фамилия")]
        public string FullName { get; set; }


        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }


        [Display(Name = "Дата на раждане")]
        public DateTime Date { get; set; }

      //  [Display(Name = "strMail", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        public IIdentity Identity
        {
            get
            {
                return new GenericIdentity(UserName);
            }
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public bool IsAdmin { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}