//using RazervationServerBL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RazervationServer.DTO
//{
//    public class BusinessDayDTO
//    {

//        public BusinessDayDTO()
//        {

//        }

//        public BusinessDayDTO(BusinessDay businessDay)
//        {
//            this.BusinessId = businessDay.BusinessId;
//            this.DayNum = businessDay.DayNum;
//            this.NumberOfWorkers = businessDay.NumberOfWorkers;
//            this.DayId = businessDay.DayId;
//            this.Business = businessDay.Business;
//            this.Reservations = businessDay.Reservations;
//            this.StartTime = businessDay.StartTime.ToString();
//            this.EndTime = businessDay.EndTime.ToString();
//        }


//        public int BusinessId { get; set; }
//        public int DayNum { get; set; }
//        public string StartTime { get; set; }
//        public string EndTime { get; set; }
//        public int NumberOfWorkers { get; set; }
//        public int DayId { get; set; }

//        public virtual Business Business { get; set; }
//        public virtual ICollection<Reservation> Reservations { get; set; }


//        //public BusinessDay GetBusinessDay()
//        //{
//        //    BusinessDay bd = new BusinessDay
//        //    {
//        //        BusinessId = this.BusinessId,
//        //        DayNum = this.DayNum,
//        //        NumberOfWorkers = this.NumberOfWorkers,
//        //        DayId = this.DayId,
//        //        Business = this.Business,
//        //        Reservations = this.Reservations,
//        //        StartTime = TimeSpan.Parse(this.StartTime),
//        //        EndTime = TimeSpan.Parse(this.EndTime)

//        //    };

//        //    return bd;
//        //}








//    }
//}
