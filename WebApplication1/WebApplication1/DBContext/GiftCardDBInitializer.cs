using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WebApplication1.Models
{
    public class GiftCardDBInitializer : DropCreateDatabaseIfModelChanges<DBCon>
    {

        protected override void Seed(DBCon context)
        {
            var StoreUser = new List<StoreUser>
            {
                new StoreUser{Password="1234"},//1
                new StoreUser{Password="1234"},//2
                new StoreUser{Password="1234"},//3
                new StoreUser{Password="1234"},//4 
                new StoreUser{Password="1234"},//5
                new StoreUser{Password="1234"},//6
                new StoreUser{Password="1234"},//7
                new StoreUser{Password="1234"},//8
                new StoreUser{Password="1234"},//9                                   
            };
            foreach (var temp in StoreUser)
            {
                context.StoreUser.Add(temp);
            }
            context.SaveChanges();


            var Crypto = new SimpleCrypto.PBKDF2();
            var EncPass = Crypto.Compute("12345");
            var Useres = new List<User>
            {

                new User {Email="Aharon.vishinsky@gmail.com",
                          FirstName="Aharon",
                          LastName="vishinsky",
                          Password=EncPass,
                          PasswardSalt=Crypto.Salt,
                          Address="Bnei brak",
                          IDUser = 1},
               new User {Email="Moti.polak@gmail.com",
                          FirstName="Moti",
                          LastName="polak",
                          Password=EncPass,
                          PasswardSalt=Crypto.Salt,
                          Address="Rmat Hasharon",
                          IDUser = 2},
              new User {Email="Shlomi.Catriel@gmail.com",
                          FirstName="Shlomi",
                          LastName="Catriel",
                          Password=EncPass,
                          PasswardSalt=Crypto.Salt,
                          Address="Rmat Gan",
                          IDUser = 3}
                          ,
              new User {Email="Miri.krupnik@gmail.com",
                          FirstName="Miri",
                          LastName="krupnik",
                          Password=EncPass,
                          PasswardSalt=Crypto.Salt,
                          Address="Bnei brak",
                          IDUser = 4}
                          ,
              new User {Email="Lital.sever@gmail.com",
                          FirstName="Lital",
                          LastName="sever",
                          Password=EncPass,
                          PasswardSalt=Crypto.Salt,
                          Address="Tel Aviv",
                          IDUser = 5}
            };

            foreach (var temp in Useres)
            {
                context.Users.Add(temp);
            }
            context.SaveChanges();


            var store = new List<Store>
            {
                new Store {StoreID=1,NameOfStroe="Adidas",ContactUs="adidas Store NamalNemal Tel Aviv Street 21, Tel Aviv 073-5555377",StoreHours="Su - Th:09:30 - 22:00,Fr09:00 - 18:00,Sa10:00 - 23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\adidas.jpg")}, 
                new Store {StoreID=2,NameOfStroe="Apple",ContactUs="iDigital Store, Ramat Aviv Mall (2nd floor), Einstein 40, Tel Aviv 03-9005355",StoreHours="Su-Th:09:30 - 22:00,Fr09:00 - 18:00,Sa10:00 - 23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\apple.jpg")},
                new Store {StoreID=3,NameOfStroe="April",ContactUs="April Big Fashion Mall in Ashdod, Ariel Sharon one way, Ashdod  077-9801732 ",StoreHours="Su-Th:09:30 - 22:00,Fr09:00 - 18:00,Sa10:00 - 23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\april.jpg")}, 
                new Store {StoreID=4,NameOfStroe="H&M",ContactUs="132 Menachem Begin St, Tel aviv +74-7155777",StoreHours="Mon - Thu 09:30 - 22:00 Fri 09:00 - 15:00,Sat 21:30 - 23:00,Sun 09:30 - 22:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\h&m.jpg")}, 
                new Store {StoreID=5,NameOfStroe="Nike",ContactUs="Hamerkava 38, Holon +972-03-5625688",StoreHours="Sun-Thu:09:30-22:00 Fri:09:00-15:00 Sat:10:00-23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\nike.jpg")}, 
                new Store {StoreID=6,NameOfStroe="Pull and bear",ContactUs="50, DIZENGOFF, TEL AVIV + 972546621950",StoreHours="Sun-Thu:09:30-22:00 Fri:09:00-15:00 Sat:10:00-23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\pull and bear.jpg")}, 
                new Store {StoreID=7,NameOfStroe="Under armour",ContactUs="Under armour Store, Ramat Aviv Mall (2nd floor), Einstein 40, Tel Aviv 03-9002555",StoreHours="Sun-Thu:09:30-22:00 Fri:09:00-15:00 Sat:10:00-23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\under armour.jpg")}, 
                new Store {StoreID=8,NameOfStroe="Windows",ContactUs="Big Fashion Mall in Ashdod, Ariel Sharon one way, Ashdod  077-9801732",StoreHours="Sun-Thu:09:30-22:00 Fri:09:00-15:00 Sat:10:00-23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\windows.jpg")},
                new Store {StoreID=9,NameOfStroe="Zara",ContactUs="Hamerkava 38, Holon +972-03-5625688",StoreHours="Su - Th:09:30 - 22:00,Fr09:00 - 18:00,Sa10:00 - 23:00",
                    Image=ConvertImageToByteArray(@"C:\Program Files\Projects\WebApplication1\WebApplication1\Images\ImageLogo\zara.jpg")}           
            };

            foreach (var temp in store)
            {
                context.Stores.Add(temp);
            }
            context.SaveChanges();


           string idStore = "1";

            var GiftCard = new List<GiftCard>
            {
                //from aharon
                new GiftCard{GiftCardID=54432,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=500,FromWho="Aharon vishinsky",GiftCardValid="24/11/2016",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=5678,Email="Shlomi.Catriel@gmail.com",FirstName="Moti",LastName="Catriel",Credit=400,FromWho="Aharon vishinsky",GiftCardValid="24/10/2016",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=37506,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Aharon vishinsky",GiftCardValid="24/09/2016",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=45689,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Aharon vishinsky",GiftCardValid="24/07/2016",StoreName="Adidas",UserId=1,StoreID=idStore},

                new GiftCard{GiftCardID=45678,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=500,FromWho="Aharon vishinsky",GiftCardValid="24/11/2015",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=32440,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=400,FromWho="Aharon vishinsky",GiftCardValid="24/10/2015",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=45654,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Aharon vishinsky",GiftCardValid="24/09/2015",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=24653,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Aharon vishinsky",GiftCardValid="24/07/2015",StoreName="Adidas",UserId=1,StoreID=idStore},
               
                new GiftCard{GiftCardID=23233,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=500,FromWho="Aharon vishinsky",GiftCardValid="24/11/2014",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=11111,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=400,FromWho="Aharon vishinsky",GiftCardValid="24/10/2014",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=23461,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Aharon vishinsky",GiftCardValid="24/09/2014",StoreName="Adidas",UserId=1,StoreID=idStore},
                new GiftCard{GiftCardID=14563,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Aharon vishinsky",GiftCardValid="24/07/2014",StoreName="Adidas",UserId=1,StoreID=idStore},

                         
               //from moti 
                new GiftCard{GiftCardID=9870,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Moti polak",GiftCardValid="24/02/2016",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=45671,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=400,FromWho="Moti polak",GiftCardValid="24/03/2016",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=86734,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Moti polak",GiftCardValid="24/06/2016",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=75677,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Moti polak",GiftCardValid="24/05/2016",StoreName="Adidas",UserId=2,StoreID=idStore},

                new GiftCard{GiftCardID=23432,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Moti polak",GiftCardValid="24/08/2015",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=78800,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=400,FromWho="Moti polak",GiftCardValid="24/07/2015",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=70078,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Moti polak",GiftCardValid="24/05/2015",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=55008,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Moti polak",GiftCardValid="24/03/2015",StoreName="Adidas",UserId=2,StoreID=idStore},
              
                new GiftCard{GiftCardID=90908,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Moti polak",GiftCardValid="24/05/2014",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=90803,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=400,FromWho="Moti polak",GiftCardValid="24/07/2014",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=13099,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Moti polak",GiftCardValid="24/08/2014",StoreName="Adidas",UserId=2,StoreID=idStore},
                new GiftCard{GiftCardID=56700,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Moti polak",GiftCardValid="24/08/2014",StoreName="Adidas",UserId=2,StoreID=idStore},

             
                //from  Shlomi
                new GiftCard{GiftCardID=12778,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Shlomi Catriel",GiftCardValid="24/03/2016",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=45600,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Shlomi Catriel",GiftCardValid="24/05/2016",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=12125,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Shlomi Catriel",GiftCardValid="24/07/2016",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=50405,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Shlomi Catriel",GiftCardValid="24/05/2016",StoreName="Adidas",UserId=3,StoreID=idStore},

                new GiftCard{GiftCardID=12222,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Shlomi Catriel",GiftCardValid="24/08/2015",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=13333,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Shlomi Catriel",GiftCardValid="24/11/2015",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=12300,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Shlomi Catriel",GiftCardValid="24/02/2015",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=121409,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Shlomi Catriel",GiftCardValid="24/09/2015",StoreName="Adidas",UserId=3,StoreID=idStore},
              
                new GiftCard{GiftCardID=867867,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Shlomi Catriel",GiftCardValid="24/05/2014",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=56856856,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Shlomi Catriel",GiftCardValid="24/11/2014",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=58678,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=600,FromWho="Shlomi Catriel",GiftCardValid="24/12/2014",StoreName="Adidas",UserId=3,StoreID=idStore},
                new GiftCard{GiftCardID=34534,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Shlomi Catriel",GiftCardValid="24/12/2014",StoreName="Adidas",UserId=3,StoreID=idStore},
            
                // from miri
                new GiftCard{GiftCardID=3453,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Miri krupnik",GiftCardValid="24/03/2016",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=34509,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Miri krupnik",GiftCardValid="24/05/2016",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=45348,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=600,FromWho="Miri krupnik",GiftCardValid="24/07/2016",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=879780,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Miri krupnik",GiftCardValid="24/05/2016",StoreName="Adidas",UserId=4,StoreID=idStore},

                new GiftCard{GiftCardID=2343,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Miri krupnik",GiftCardValid="24/08/2015",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=657657,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Miri krupnik",GiftCardValid="24/11/2015",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=2347,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=600,FromWho="Miri krupnik",GiftCardValid="24/02/2015",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=65758,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Miri krupnik",GiftCardValid="24/09/2015",StoreName="Adidas",UserId=4,StoreID=idStore},
              
                new GiftCard{GiftCardID=9879,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Miri krupnik",GiftCardValid="24/05/2014",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=908,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Miri krupnik",GiftCardValid="24/11/2014",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=5567,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=600,FromWho="Miri krupnik",GiftCardValid="24/12/2014",StoreName="Adidas",UserId=4,StoreID=idStore},
                new GiftCard{GiftCardID=345,Email="Lital.sever@gmail.com",FirstName="Lital",LastName="sever",Credit=900,FromWho="Miri krupnik",GiftCardValid="24/12/2014",StoreName="Adidas",UserId=4,StoreID=idStore},
            
               // from lital
                new GiftCard{GiftCardID=567569,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Lital sever",GiftCardValid="24/06/2016",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=87978,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Lital sever",GiftCardValid="24/06/2016",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=768768,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=600,FromWho="Lital sever",GiftCardValid="24/08/2016",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=45623,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=900,FromWho="Lital sever",GiftCardValid="24/01/2016",StoreName="Adidas",UserId=5,StoreID=idStore},

                new GiftCard{GiftCardID=345780,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Lital sever",GiftCardValid="24/01/2015",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=457432,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Lital sever",GiftCardValid="24/11/2015",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=36547,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=600,FromWho="Lital sever",GiftCardValid="24/12/2015",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=746745,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=900,FromWho="Lital sever",GiftCardValid="24/12/2015",StoreName="Adidas",UserId=5,StoreID=idStore},
              
                new GiftCard{GiftCardID=474687,Email="Aharon.vishinsky@gmail.com",FirstName="Aharon",LastName="vishinsky",Credit=500,FromWho="Lital sever",GiftCardValid="24/12/2014",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=899,Email="Moti.polak@gmail.com",FirstName="Moti",LastName="polak",Credit=400,FromWho="Lital sever",GiftCardValid="24/07/2014",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=75675,Email="Shlomi.Catriel@gmail.com",FirstName="Shlomi",LastName="Catriel",Credit=600,FromWho="Lital sever",GiftCardValid="24/07/2014",StoreName="Adidas",UserId=5,StoreID=idStore},
                new GiftCard{GiftCardID=789845,Email="Miri.krupnik@gmail.com",FirstName="Miri",LastName="krupnik",Credit=900,FromWho="Lital sever",GiftCardValid="24/11/2014",StoreName="Adidas",UserId=5,StoreID=idStore}
            
            };
            foreach (var temp in GiftCard)
            {
                context.GiftCards.Add(temp);
            }
            context.SaveChanges();



        }


        private static byte[] ConvertImageToByteArray(string fileName)
        {

            Bitmap bitMap = new Bitmap(fileName);
            ImageFormat bmpFormat = bitMap.RawFormat;
            var imageToConvert = Image.FromFile(fileName);
            using (MemoryStream ms = new MemoryStream())
            {
                imageToConvert.Save(ms, bmpFormat);
                return ms.ToArray();
            }

        }
    }
}