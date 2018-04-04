using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BiranchiBOT
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
 var user=["sam","biranchi","Berline"];
function adduser(Usename,callback){
setTimeout(function(){
user.push(Usename); console.log(Usename);callback();
},200);
}

function Getuser(Usename){
setTimeout(function(){
console.log(user);
},100);
}
undefined
adduser('jacke',Getuser);
