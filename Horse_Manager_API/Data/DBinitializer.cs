using System;
using System.Linq;
using Horse_Manager_API.Models;

namespace Horse_Manager_API.Data
{
    public class DBinitializer
    {
        public static void Initialize(DBcontext context){
            //context.Database.EnsureCreated();

            if(context.Users.Any()){
                return;
            }

            User user = new User();
            user.FirstName = "Robin";
            user.LastName = "Anthonissen";
            user.Email = "robinanthonissen@hotmail.com";
            user.Address = "Molenstraat 24b";
            user.City = "Brecht";
            user.State = null;
            user.Country = "Belgium";
            user.Phone = "+32477888557";
            user.About = "Zaadvoerder C & T Horse Gear BVBA";
            user.Image = "https://scontent.cdninstagram.com/vp/e2fbd721ee90397dd5ae426594544b77/5B6A5EEF/t51.2885-19/s150x150/11909924_425726610954320_1007105184_a.jpg";;
            user.Subscription_PlanID = 1;
            context.Users.Add(user);

            Subscription_Plan prof = new Subscription_Plan();
            prof.Subscription_PlanID = 1;
            prof.Subscription_Plan_Name = "Professional";
            prof.Price = 15;
            context.Subscription_Plans.Add(prof);

            Subscription_Plan basic = new Subscription_Plan();
            basic.Subscription_PlanID = 2;
            basic.Subscription_Plan_Name = "Basic";
            basic.Price = 5;
            context.Subscription_Plans.Add(basic);

            Subscription_Plan start = new Subscription_Plan();
            start.Subscription_PlanID = 3;
            start.Subscription_Plan_Name = "Starter";
            start.Price = 0;
            context.Subscription_Plans.Add(start);

            context.SaveChanges();
        }
    }
}
