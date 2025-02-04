using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class EventsFilterDto
    {
        public string EventTitle { get; set; }

        public Common.EventCategory? EventCategory { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int UserId { get; set; }

        public void SetUp()
        {
            if (EventTitle != null)
            {
                EventTitle = EventTitle.ToLower();
            }

            if(FromDate!=null)
                FromDate = FromDate.Value.ToUniversalTime();

            if(ToDate!=null)
                ToDate = ToDate.Value.ToUniversalTime();

        }
    }
}
