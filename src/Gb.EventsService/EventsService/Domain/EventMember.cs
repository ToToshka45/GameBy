using Common;
using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EventMember:BaseEntity
    {
        public int EventId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public  ParticipationState  ParticipationState {get;set;} 

        //role
        //public 
        public EventUserRole Role { get; set; }

        public DateTime JoinDate => DateTime.Now;

        public DateTime? LeaveDate { get; set; }

        public bool IsAbsent { get; set; }


    }
}
