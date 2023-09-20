using Microsoft.AspNetCore.Mvc.Filters;

namespace B2B
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!Authentication.IsAuthenticated(filterContext.HttpContext.Request))
                {
                    var path = filterContext.HttpContext.Request.Path.Value.ToLower();

                    if (!path.Equals("/login") && !path.Equals("/login/changepassword"))
                    {
                        filterContext.HttpContext.Response.Redirect("/Login");
                    }
                }
                    
                //this.LogActionExecutedContext(nameof(OnActionExecuting), (FilterContext)filterContext);
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) => this.LogActionExecutedContext(nameof(OnActionExecuted), (FilterContext)filterContext);

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            try
            {
                if (!Authentication.IsAuthenticated(filterContext.HttpContext.Request))
                {
                    var path = filterContext.HttpContext.Request.Path.Value.ToLower();

                    if (!path.Equals("/login") && !path.Equals("/login/changepassword"))
                    {
                        filterContext.HttpContext.Response.Redirect("/Login");
                    }
                }
                this.LogResultExecutedContext(nameof(OnResultExecuting), (FilterContext)filterContext);
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext) => this.LogResultExecutedContext(nameof(OnResultExecuted), (FilterContext)filterContext);

        private void LogResultExecutedContext(string methodName, FilterContext filterContext)
        {
            try
            {
                //object obj1 = filterContext.RouteData.Values["controller"];
                //object obj2 = filterContext.RouteData.Values["action"];
                //string.Format("{0} controller:{1} action:{2}", (object)methodName, obj1, obj2);
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
            }
        }

        private void LogActionExecutedContext(string methodName, FilterContext filterContext)
        {
            try
            {
                /*string str1 = filterContext.RouteData.Values["controller"].ToString();
                string str2 = filterContext.RouteData.Values["action"].ToString();
                string.Format("controller:{0} action:{1}", (object)str1, (object)str2);
                if (!methodName.Equals("OnActionExecuting"))
                    return;
                IDictionary<string, object> actionArguments = ((ActionExecutingContext)filterContext).ActionArguments;
                if (actionArguments == null)
                    return;
                string str3 = JsonConvert.SerializeObject((object)new Action()
                {
                    ActionName = str2,
                    Controller = str1,
                    Arguments = (Dictionary<string, object>)actionArguments
                });
                ExecuteQuery.RunQuery("INSERT INTO [SnappDB].[dbo].[ActivityLog] VALUES('" + Cookie.Get(filterContext.HttpContext.Request, "Username") + "', '" + str3 + "', '', '" + DateTime.Now.ToString() + "', 0)", Static.ConnectionString);*/
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
            }
        }
    }

    public class Action
    {
        public string Controller { get; set; }

        public string ActionName { get; set; }

        public Dictionary<string, object> Arguments { get; set; }
    }
}
