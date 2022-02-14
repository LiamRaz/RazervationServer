//using RazervationServerBL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace RazervationServer.DTO
//{
//    public class BusinessDTO
//    {

//        public BusinessDTO()
//        {

//        }

//        public BusinessDTO(Business business)
//        {
//            this.BusinessId = business.BusinessId;
//            this.BusinessName = business.BusinessName;
//            this.BusinessAddress = business.BusinessAddress;
//            this.Bio = business.Bio;
//            this.CategoryId = business.CategoryId;
//            this.UserName = business.UserName;
//            this.InternetUrl = business.InternetUrl;
//            this.InstagramUrl = business.InstagramUrl;
//            this.FacebookUrl = business.FacebookUrl;

//            this.Category = business.Category;
//            this.UserNameNavigation = business.UserNameNavigation;
//            this.Bservices = business.Bservices;
//            this.Comments = business.Comments;
//            this.Favorites = business.Favorites;
//            this.Histories = business.Histories;
//            this.Reservations = business.Reservations;
//            this.SpecialNumberOfWorkers = business.SpecialNumberOfWorkers;

//            //this.BusinessDays = new List<BusinessDayDTO>();
//            //foreach (BusinessDay bd in business.BusinessDays)
//            //{
//            //    BusinessDayDTO businessDayDTO = new BusinessDayDTO(bd);
//            //    this.BusinessDays.Add(businessDayDTO);
//            //}

//        }


//        public int BusinessId { get; set; }
//        public string BusinessName { get; set; }
//        public string BusinessAddress { get; set; }
//        public string Bio { get; set; }
//        public int CategoryId { get; set; }
//        public string InternetUrl { get; set; }
//        public string UserName { get; set; }
//        public string InstagramUrl { get; set; }
//        public string FacebookUrl { get; set; }

//        public virtual Category Category { get; set; }
//        public virtual User UserNameNavigation { get; set; }
//        public virtual ICollection<Bservice> Bservices { get; set; }
//        //public virtual ICollection<BusinessDayDTO> BusinessDays { get; set; }
//        public virtual ICollection<Comment> Comments { get; set; }
//        public virtual ICollection<Favorite> Favorites { get; set; }
//        public virtual ICollection<History> Histories { get; set; }
//        public virtual ICollection<Reservation> Reservations { get; set; }
//        public virtual ICollection<SpecialNumberOfWorker> SpecialNumberOfWorkers { get; set; }


//        //public Business GetBusiness()
//        //{
//        //    Business b = new Business
//        //    {
//        //        BusinessId = this.BusinessId,
//        //        BusinessName = this.BusinessName,
//        //        BusinessAddress = this.BusinessAddress,
//        //        Bio = this.Bio,
//        //        CategoryId = this.CategoryId,
//        //        UserName = this.UserName,
//        //        InternetUrl = this.InternetUrl,
//        //        InstagramUrl = this.InstagramUrl,
//        //        FacebookUrl = this.FacebookUrl,


//        //        Category = this.Category,
//        //        UserNameNavigation = this.UserNameNavigation,
//        //        Bservices = this.Bservices,
//        //        Comments = this.Comments,
//        //        Favorites = this.Favorites,
//        //        Histories = this.Histories,
//        //        Reservations = this.Reservations,
//        //        SpecialNumberOfWorkers = this.SpecialNumberOfWorkers,

//        //        BusinessDays = GetBusinessDays()


//        //    };

//        //    return b;
//        //}


//        //public ICollection<BusinessDay> GetBusinessDays()
//        //{
//        //    ICollection<BusinessDay> businessDays = new List<BusinessDay>();

//        //    foreach (BusinessDayDTO businessDayDTO in this.BusinessDays)
//        //    {
//        //        businessDays.Add(businessDayDTO.GetBusinessDay());
//        //    }

//        //    return businessDays;
            
//        //}




//    }
//}
