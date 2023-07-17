using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.ApiModels.User
{
    public class ResetPasswordModel
    {
        public string ResetPasswordToken { get; set; }
        public string NewPassword { get; set; }
    }
}
