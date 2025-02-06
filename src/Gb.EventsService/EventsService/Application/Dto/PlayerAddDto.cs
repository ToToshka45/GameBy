using Constants;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PlayerAddDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }

        public int UserId { get; set; }

        //role
        //public 
        public EventUserRole Role { get; set; }

        public DateTime JoinDate => DateTime.Now;

        public bool IsSuccess { get; set; }

        public string ErrMessage { get; set; }

    }
}
